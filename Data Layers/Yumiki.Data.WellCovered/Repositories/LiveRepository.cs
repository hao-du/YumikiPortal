﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Commons.Dictionaries;
using Yumiki.Commons.Helpers;
using Yumiki.Data.WellCovered.Interfaces;
using Yumiki.Entity.WellCovered;

namespace Yumiki.Data.WellCovered.Repositories
{
    public class LiveRepository : WellCoveredRepository, ILiveRepository
    {
        #region Publish Object
        /// <summary>
        /// Check if Apps is ready for publish (Has objects and fields)
        /// </summary>
        /// <param name="objectID">Apps need to be checked the validity</param>
        /// <returns>True if Valid</returns>
        public bool IsAppValidToPublish(Guid appID)
        {
            return Context.TB_App.Where(c => c.ID == appID && c.Objects.Where(d => d.Fields.Any()).Any()).SingleOrDefault() != null;
        }

        /// <summary>
        /// Publish App and its related objects and fields to Live.
        /// </summary>
        /// <param name="objectID">Id of App need to be gone live.</param>
        public void PublishApp(Guid appID)
        {
            IEnumerable<TB_Object> objects = Context.TB_Object.Include(TB_Object.FieldName.App).Include(TB_Object.FieldName.Fields).Where(c => c.AppID == appID).ToList();

            Logger.Debug(string.Format("Publishing app with ID '{0}' with {1} objects.", appID.ToString(), objects.Count()));

            foreach (TB_Object obj in objects)
            {
                PublishObject(obj);
            }
        }

        /// <summary>
        /// Check if Object is ready for publish (Has object and fields)
        /// </summary>
        /// <param name="objectID">Object need to be checked the validity</param>
        /// <returns>True if Valid</returns>
        public bool IsObjectValidToPublish(Guid objectID)
        {
            return Context.TB_Object.Where(c => c.ID == objectID && c.Fields.Any()).SingleOrDefault() != null;
        }

        /// <summary>
        /// Publish object and its related fields to Live.
        /// </summary>
        /// <param name="objectID">Id of Object need to be gone live.</param>
        public void PublishObject(Guid objectID)
        {
            TB_Object obj = Context.TB_Object.Include(TB_Object.FieldName.App).Include(TB_Object.FieldName.Fields).Where(c => c.ID == objectID).SingleOrDefault();
            PublishObject(obj);
        }

        /// <summary>
        /// Publish object and its related fields to Live.
        /// </summary>
        /// <param name="obj">Object need to be gone live.</param>
        private void PublishObject(TB_Object obj)
        {
            AddSystemFields(obj);

            //Publish Object
            Logger.Debug(string.Format("Publishing '{0}' object.", obj.ObjectName));
            if (obj != null)
            {
                if (CheckTableExist(obj.ApiName))
                {
                    Logger.Debug(string.Format("'{0}' existed in DB.", obj.ObjectName));
                    AlterTable(obj);
                }
                else
                {
                    Logger.Debug(string.Format("'{0}' is new.", obj.ObjectName));
                    CreateTable(obj);
                }
            }

            RebuildIndex(obj);
        }

        /// <summary>
        /// Add system fields which are able to edit. E.g IsActive, Descriptions
        /// </summary>
        /// <param name="obj">Object need to add the system fields</param>
        private void AddSystemFields(TB_Object obj)
        {
            bool areSystemFieldsAdded = false;
            //Publish System Editable Fields: IsActive, Description
            if (!obj.Fields.Any(c => c.ApiName.Equals(CommonProperties.IsActive, StringComparison.InvariantCultureIgnoreCase)))
            {
                TB_Field field = new TB_Field()
                {
                    FieldName = CommonProperties.IsActive,
                    ApiName = CommonProperties.IsActive,
                    DisplayName = "Is Active",
                    ObjectID = obj.ID,
                    IsRequired = true,
                    FieldType = EN_DataType.E_BOOL,
                    UserID = obj.UserID,
                    Descriptions = "Field is managed by system",
                    IsSystemField = true,
                    IsDisplayable = false,
                    FieldOrder = 3000
                };
                Context.TB_Field.Add(field);
                areSystemFieldsAdded = true;
            }

            if (!obj.Fields.Any(c => c.ApiName.Equals(CommonProperties.Descriptions, StringComparison.InvariantCultureIgnoreCase)))
            {
                TB_Field field = new TB_Field()
                {
                    FieldName = CommonProperties.Descriptions,
                    ApiName = CommonProperties.Descriptions,
                    DisplayName = CommonProperties.Descriptions,
                    ObjectID = obj.ID,
                    IsRequired = false,
                    FieldType = EN_DataType.E_STRING,
                    FieldLength = 255,
                    UserID = obj.UserID,
                    Descriptions = "Field is managed by system",
                    IsSystemField = true,
                    IsDisplayable = false,
                    FieldOrder = 4000
                };
                Context.TB_Field.Add(field);
                areSystemFieldsAdded = true;
            }

            if (areSystemFieldsAdded)
            {
                Save();
            }
        }

        /// <summary>
        /// Check if Table Name is existed.
        /// </summary>
        /// <param name="tableName">Table need to be checked</param>
        /// <returns>True if Existed</returns>
        private bool CheckTableExist(string tableName)
        {
            return Context.Database.SqlQuery<int?>(@"SELECT 1 FROM sys.tables WHERE name = @tableName", new SqlParameter("@tableName", tableName)).Any();
        }

        /// <summary>
        /// Rebuild Search Index after Publishing App or Object.
        /// </summary>
        /// <param name="obj">Table (object) need to be rebuilt.</param>
        private void RebuildIndex(TB_Object obj)
        {
            //Remove all records belong to table need to be rebuilt.
            Context.TB_LiveIndex.RemoveRange(Context.TB_LiveIndex.Where(c => c.ObjectID == obj.ID).ToList());

            StringBuilder displayContents = new StringBuilder();
            StringBuilder fullTextIndex = new StringBuilder();
            fullTextIndex.AppendFormat("{0} {1} {2} ", obj.ObjectName, obj.DisplayName, obj.ApiName);
            fullTextIndex.AppendFormat("{0} ", obj.AppName);

            MD_Live live = FetchViewObjectData(obj.ID, true);

            bool isActive = false;
            IEnumerable<TB_Field> fields = obj.Fields.OrderBy(c => c.FieldOrder);

            foreach (DataRow row in live.Datasource)
            {
                foreach (TB_Field field in fields)
                {
                    object value = row[field.DisplayName];

                    if (field.IsDisplayable)
                    {
                        displayContents.AppendFormat("{0}:{1} - ", field.DisplayName, value);
                    }
                    if (field.CanIndex)
                    {
                        fullTextIndex.AppendFormat("{0} ", value);
                    }

                    if (field.ApiName == CommonProperties.IsActive)
                    {
                        isActive = (bool)value;

                        //If isActive == false, skip build index for row.
                        if (!isActive)
                        {
                            break;
                        }
                    }
                }

                if (isActive)
                {
                    SaveIndex(obj, (Guid)row[CommonProperties.ID], fullTextIndex.ToString(), displayContents.ToString());
                }
            }

            Save();
        }

        /// <summary>
        /// Create physical table in DB
        /// </summary>
        /// <param name="obj">Object need to be created</param>
        private void CreateTable(TB_Object obj)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.AppendFormat("CREATE TABLE {0} (", obj.ApiName);
            foreach (TB_Field field in obj.Fields)
            {
                string fieldName = field.ApiName;
                string fieldType = EnumHelper.GetMappingValue(field.FieldType);
                string fieldLength = GetFieldLength(field);

                sqlBuilder.AppendFormat("{0} {1}{2} NULL, ", fieldName, fieldType, fieldLength);
            }

            sqlBuilder.AppendFormat("{0} UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, ", CommonProperties.ID);
            sqlBuilder.AppendFormat("{0} DATETIME NOT NULL, ", CommonProperties.CreateDate);
            sqlBuilder.AppendFormat("{0} DATETIME NULL", CommonProperties.LastUpdateDate);

            sqlBuilder.Append(")");

            Logger.Debug(string.Format("Create table script: {0}", sqlBuilder.ToString()));

            ExecuteCommand(sqlBuilder.ToString(), new object[] { });

            Logger.Debug(string.Format("Created {0} table.", obj.ApiName));
        }

        private const string ColumnName = "COLUMN_NAME";
        private const string DataType = "DATA_TYPE";
        private const string Length = "CHARACTER_MAXIMUM_LENGTH";
        /// <summary>
        /// Alter the existing table
        /// </summary>
        /// <param name="obj">Object need to be altered</param>
        private void AlterTable(TB_Object obj)
        {
            EnumerableRowCollection<DataRow> fieldInfos = GetDynamicRecords(string.Format("SELECT {0}, {1}, {2} FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{3}'", ColumnName, DataType, Length, obj.ApiName)).AsEnumerable();

            StringBuilder sqlBuilder = new StringBuilder();

            foreach (TB_Field field in obj.Fields)
            {
                string fieldName = field.ApiName;
                string fieldType = EnumHelper.GetMappingValue(field.FieldType);
                string fieldLength = GetFieldLength(field);

                DataRow row = fieldInfos.Where(c => c.Field<string>(ColumnName) == fieldName).FirstOrDefault();

                //New field for existing table
                if (row == null)
                {
                    sqlBuilder.AppendFormat("ALTER TABLE {0} ADD {1} {2}{3} NULL;", obj.ApiName, fieldName, fieldType, fieldLength);
                }
                else
                {
                    //If field type is changed
                    if (fieldType != row.Field<string>(DataType))
                    {
                        sqlBuilder.AppendFormat("ALTER TABLE {0} DROP COLUMN {1};", obj.ApiName, fieldName);
                        sqlBuilder.AppendFormat("ALTER TABLE {0} ADD {1} {2}{3} NULL;", obj.ApiName, fieldName, fieldType, fieldLength);
                    }
                    //If length's nvarchar column is changed
                    else if (field.FieldType == EN_DataType.E_STRING && field.FieldLength != row.Field<int>(Length))
                    {
                        sqlBuilder.AppendFormat("ALTER TABLE {0} ALTER COLUMN {1} {2}{3};", obj.ApiName, fieldName, fieldType, fieldLength);
                    }
                }
            }

            if (sqlBuilder.Length > 0)
            {
                Logger.Debug(string.Format("Alter table script: {0}.", sqlBuilder.ToString()));

                ExecuteCommand(sqlBuilder.ToString(), new object[] { });

                Logger.Debug(string.Format("Altered {0} table.", obj.ApiName));
            }
            else
            {
                Logger.Debug(string.Format("There is no column updated for {0} table", obj.ApiName));
            }
        }

        /// <summary>
        /// Get field length of field
        /// </summary>
        /// <param name="field">Field need to get the length</param>
        /// <returns>Length in SQL Column format</returns>
        private string GetFieldLength(TB_Field field)
        {
            string fieldLength;
            switch (field.FieldType)
            {
                case EN_DataType.E_DECIMAL:
                    fieldLength = "(18, 2)";
                    break;
                case EN_DataType.E_DATASOURCE:
                    fieldLength = "(255)";
                    break;
                case EN_DataType.E_STRING:
                    fieldLength = field.FieldLength.HasValue ? string.Format("({0})", field.FieldLength.Value.ToString()) : string.Empty;
                    break;
                default:
                    fieldLength = string.Empty;
                    break;
            }
            return fieldLength;
        }
        #endregion

        /// <summary>
        /// Get fields from ObjectID
        /// </summary>
        public IEnumerable<TB_Field> GetFields(Guid objectID)
        {
            IFieldRepository fieldRepository = this.GetAlternativeRepository<IFieldRepository>();
            return fieldRepository.GetAllFields(false, objectID);
        }

        /// <summary>
        /// Fetch all data from Object
        /// </summary>
        /// <param name="objectID">Object ID need to fetch data</param>
        public MD_Live FetchViewObjectData(Guid objectID, bool isActive)
        {
            MD_Live live = new MD_Live();

            TB_Object obj = Context.TB_Object.Include(TB_Object.FieldName.Fields).Where(c => c.ID == objectID).SingleOrDefault();

            live.ObjectID = obj.ID;
            live.LiveName = obj.DisplayName;

            if (obj == null)
            {
                return null;
            }

            foreach (TB_Field field in obj.Fields.Where(c => c.IsDisplayable && c.IsActive).OrderBy(c => c.FieldOrder))
            {
                live.ColumnNames.Add(field.DisplayName, field.FieldType);
            }

            live.Datasource = GetDynamicRecords(PrepareQueryStament(obj, isActive)).AsEnumerable();

            return live;
        }

        /// <summary>
        /// Fetch record from Object by ID
        /// </summary>
        /// <param name="objectID">Object ID need to fetch data</param>
        public MD_Live FetchViewObjectDataByID(Guid objectID, Guid recordID)
        {
            MD_Live live = new MD_Live();

            TB_Object obj = Context.TB_Object.Include(TB_Object.FieldName.Fields).Where(c => c.ID == objectID).SingleOrDefault();

            live.ObjectID = obj.ID;
            live.LiveName = obj.DisplayName;

            if (obj == null)
            {
                return null;
            }

            foreach (TB_Field field in obj.Fields)
            {
                live.ColumnNames.Add(field.DisplayName, field.FieldType);
            }

            string sqlStament = PrepareQueryStament(obj, null, false);

            string whereStament = string.Format(" WHERE [0].ID = '{0}' ", recordID.ToString());

            live.Datasource = GetDynamicRecords(string.Join(" ", sqlStament, whereStament)).AsEnumerable();

            return live;
        }

        /// <summary>
        /// Prepare basic SELECT SQL Stament for Live
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        private string PrepareQueryStament(TB_Object obj, bool? isActive, bool getLinkDisplayName = true)
        {
            StringBuilder sqlSelectBuilder = new StringBuilder(" SELECT [0].ID ");

            StringBuilder sqlFromBuilder = new StringBuilder();
            sqlFromBuilder.AppendFormat(" FROM {0} AS [0] ", obj.ApiName);

            int aliasUniqueID = 1;

            foreach (TB_Field field in obj.Fields)
            {
                bool hasDatasource = false;
                if (field.FieldType == EN_DataType.E_DATASOURCE)
                {
                    //Format: Link DisplayField=API_Field_Name>>API_Object_Name
                    string[] datasourceFormat = field.Datasource.Split(new string[] { ">>" }, StringSplitOptions.RemoveEmptyEntries);

                    if (getLinkDisplayName)
                    {
                        if (datasourceFormat[0].StartsWith("Link", StringComparison.InvariantCultureIgnoreCase))
                        {
                            hasDatasource = true;

                            string[] parameters = datasourceFormat[0].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                            string displayField = parameters[1].Split(new string[] { "DisplayField=" }, StringSplitOptions.RemoveEmptyEntries)[0];

                            sqlSelectBuilder.AppendFormat(" , [{0}].{1} as [{2}]", aliasUniqueID, displayField, field.DisplayName);

                            sqlFromBuilder.AppendFormat(" LEFT JOIN {0} AS [{1}] ON [0].{2} = [{1}].ID ", datasourceFormat[1], aliasUniqueID, field.ApiName);
                        }
                    }
                }

                if (!hasDatasource)
                {
                    sqlSelectBuilder.AppendFormat(" ,[0].{0} as [{1}] ", field.ApiName, field.DisplayName);
                }

                aliasUniqueID++;
            }

            StringBuilder sqlWhereBuilder = new StringBuilder();
            if (isActive.HasValue)
            {
                sqlWhereBuilder.AppendFormat(" WHERE [0].IsActive = {0} ", isActive.Value ? 1 : 0);
            }

            return string.Format("{0}{1}{2}", sqlSelectBuilder.ToString(), sqlFromBuilder.ToString(), sqlWhereBuilder.ToString());
        }

        /// <summary>
        /// Get Datasource from Datasource Field
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="displayFieldName"></param>
        /// <returns></returns>
        public IEnumerable<MD_Datasource> GetDataSource(string tableName, string displayFieldName)
        {
            string sql = string.Format(" SELECT ID AS {0}, {1} AS {2} FROM {3} WHERE {4} = 1 ", MD_Datasource.FieldNames.ID, displayFieldName, MD_Datasource.FieldNames.DisplayText, tableName, CommonProperties.IsActive);

            EnumerableRowCollection<DataRow> rows = GetDynamicRecords(sql).AsEnumerable();

            List<MD_Datasource> datasource = new List<MD_Datasource>();

            foreach (DataRow row in rows)
            {
                datasource.Add(new MD_Datasource
                {
                    ID = row[MD_Datasource.FieldNames.ID].ToString(),
                    DisplayText = row[MD_Datasource.FieldNames.DisplayText].ToString()
                });
            }

            return datasource;
        }

        /// <summary>
        /// Save an record of object to DB
        /// </summary>
        /// <param name="record"></param>
        public void Add(Guid objectID, IEnumerable<TB_Field> record)
        {
            TB_Object obj = Context.TB_Object.Include(TB_Object.FieldName.App).Include(TB_Object.FieldName.Fields).Where(c => c.ID == objectID).SingleOrDefault();

            Guid liveID = Guid.NewGuid();

            StringBuilder sqlInsertBuilder = new StringBuilder();
            StringBuilder sqlValueBuilder = new StringBuilder();
            List<SqlParameter> pamameters = new List<SqlParameter>();

            sqlInsertBuilder.AppendFormat(" INSERT INTO {0} ", obj.ApiName);
            sqlInsertBuilder.AppendFormat(" ([{0}], [{1}], [{2}] ", CommonProperties.ID, CommonProperties.CreateDate, CommonProperties.LastUpdateDate);

            sqlValueBuilder.AppendFormat(" VALUES (@{0}, @{1}, @{2} ", CommonProperties.ID, CommonProperties.CreateDate, CommonProperties.LastUpdateDate);
            pamameters.Add(new SqlParameter() { ParameterName = string.Format("@{0}", CommonProperties.ID), Value = liveID, SqlDbType = SqlDbType.UniqueIdentifier });
            pamameters.Add(new SqlParameter() { ParameterName = string.Format("@{0}", CommonProperties.CreateDate), Value = DateTimeExtension.GetSystemDatetime(), SqlDbType = SqlDbType.DateTime });
            pamameters.Add(new SqlParameter() { ParameterName = string.Format("@{0}", CommonProperties.LastUpdateDate), Value = DateTimeExtension.GetSystemDatetime(), SqlDbType = SqlDbType.DateTime });

            StringBuilder displayContents = new StringBuilder();

            StringBuilder fullTextIndex = new StringBuilder();
            fullTextIndex.AppendFormat("{0} {1} {2} ", obj.ObjectName, obj.DisplayName, obj.ApiName);
            fullTextIndex.AppendFormat("{0} ", obj.AppName);

            bool isActive = false;
            foreach (TB_Field field in obj.Fields.OrderBy(c => c.FieldOrder))
            {
                sqlInsertBuilder.AppendFormat(" ,[{0}] ", field.ApiName);
                sqlValueBuilder.AppendFormat(" ,@{0} ", field.ApiName);

                object value = record.Where(c => c.ApiName == field.ApiName).First().Value;

                pamameters.Add(PrepareParameter(field, value));

                if (field.IsDisplayable)
                {
                    displayContents.AppendFormat("{0}:{1} - ", field.DisplayName, value);
                }
                if (field.CanIndex)
                {
                    fullTextIndex.AppendFormat("{0} ", value);
                }

                if (field.ApiName == CommonProperties.IsActive)
                {
                    isActive = (bool)value;
                }
            }

            sqlInsertBuilder.Append(" ) ");
            sqlValueBuilder.Append(" ) ");

            ExecuteCommand(sqlInsertBuilder.Append(sqlValueBuilder.ToString()).ToString(), pamameters.ToArray());

            SaveIndex(obj, liveID, isActive ? fullTextIndex.ToString() : string.Empty, displayContents.ToString(), true);
        }

        /// <summary>
        /// Save an record of object to DB
        /// </summary>
        /// <param name="record"></param>
        public void Update(Guid objectID, Guid recordID, IEnumerable<TB_Field> record)
        {
            TB_Object obj = Context.TB_Object.Include(TB_Object.FieldName.App).Include(TB_Object.FieldName.Fields).Where(c => c.ID == objectID).SingleOrDefault();

            //For Full Text Search
            StringBuilder displayContents = new StringBuilder();
            StringBuilder fullTextIndex = new StringBuilder();
            fullTextIndex.AppendFormat("{0} {1} {2} ", obj.ObjectName, obj.DisplayName, obj.ApiName);
            fullTextIndex.AppendFormat("{0} ", obj.AppName);

            StringBuilder sqlUpdateBuilder = new StringBuilder();
            StringBuilder sqlSetBuilder = new StringBuilder();
            StringBuilder sqlWhereBuilder = new StringBuilder();
            List<SqlParameter> pamameters = new List<SqlParameter>();

            sqlUpdateBuilder.AppendFormat(" UPDATE {0} ", obj.ApiName);
            sqlSetBuilder.AppendFormat(" SET [{0}] = @{1} ", CommonProperties.LastUpdateDate, CommonProperties.LastUpdateDate);

            pamameters.Add(new SqlParameter() { ParameterName = string.Format("@{0}", CommonProperties.LastUpdateDate), Value = DateTimeExtension.GetSystemDatetime(), SqlDbType = SqlDbType.DateTime });

            foreach (TB_Field field in obj.Fields)
            {
                sqlSetBuilder.AppendFormat(" , [{0}] = @{1} ", field.ApiName, field.ApiName);

                object value = record.Where(c => c.ApiName == field.ApiName).First().Value;

                pamameters.Add(PrepareParameter(field, value));

                if (field.IsDisplayable)
                {
                    displayContents.AppendFormat("{0}:{1} - ", field.DisplayName, value);
                }
                if (field.CanIndex)
                {
                    fullTextIndex.AppendFormat("{0} ", value);
                }
            }

            sqlWhereBuilder.AppendFormat(" WHERE [{0}] = @{1} ", CommonProperties.ID, CommonProperties.ID);
            pamameters.Add(new SqlParameter() { ParameterName = string.Format("@{0}", CommonProperties.ID), Value = recordID, SqlDbType = SqlDbType.UniqueIdentifier });

            ExecuteCommand(string.Format(" {0} {1} {2} ", sqlUpdateBuilder.ToString(), sqlSetBuilder.ToString(), sqlWhereBuilder.ToString()), pamameters.ToArray());

            SaveIndex(obj, recordID, fullTextIndex.ToString(), displayContents.ToString(), true);
        }

        /// <summary>
        /// Save index for full text search
        /// </summary>
        private void SaveIndex(TB_Object obj, Guid liveID, string fullTextIndex, string displayContents, bool saveImmediately = false)
        {
            //Modify field in Index
            TB_LiveIndex liveIndex = Context.TB_LiveIndex.SingleOrDefault(c => c.LiveID == liveID);

            if (liveIndex == null)
            {
                if (!string.IsNullOrWhiteSpace(fullTextIndex))
                {   
                    //Add field to Index
                    liveIndex = new TB_LiveIndex()
                    {
                        LiveID = liveID,
                        ObjectID = obj.ID,
                        AppID = obj.AppID,
                        FullTextIndex = fullTextIndex.ToString(),
                        DisplayContents = displayContents.ToString(),
                        UserID = obj.UserID
                    };

                    Context.TB_LiveIndex.Add(liveIndex);
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(fullTextIndex))
                {
                    Context.TB_LiveIndex.Remove(liveIndex);
                }
                else
                {
                    liveIndex.ObjectID = obj.ID;
                    liveIndex.AppID = obj.AppID;
                    liveIndex.FullTextIndex = fullTextIndex;
                    liveIndex.DisplayContents = displayContents;
                    liveIndex.UserID = obj.UserID;
                }
            }

            if (saveImmediately)
            {
                Save();
            }
        }

        private SqlParameter PrepareParameter(TB_Field field, object value)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = string.Format("@{0}", field.ApiName);
            parameter.Value = value;

            switch (field.FieldType)
            {
                case EN_DataType.E_INT:
                    parameter.SqlDbType = SqlDbType.Int;
                    break;
                case EN_DataType.E_DECIMAL:
                    parameter.SqlDbType = SqlDbType.Decimal;
                    break;
                case EN_DataType.E_STRING:
                    parameter.SqlDbType = SqlDbType.NVarChar;
                    break;
                case EN_DataType.E_BOOL:
                    parameter.SqlDbType = SqlDbType.Bit;
                    break;
                case EN_DataType.E_DATE:
                    parameter.SqlDbType = SqlDbType.Date;
                    break;
                case EN_DataType.E_DATETIME:
                    parameter.SqlDbType = SqlDbType.DateTime;
                    break;
                case EN_DataType.E_TIME:
                    parameter.SqlDbType = SqlDbType.Time;
                    break;
                case EN_DataType.E_DATASOURCE:
                    parameter.SqlDbType = SqlDbType.NText;
                    break;
            }

            return parameter;
        }
    }
}
