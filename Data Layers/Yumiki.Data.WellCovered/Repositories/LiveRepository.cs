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
            IEnumerable<TB_Object> objects = Context.TB_Object.Include(TB_Object.FieldName.Fields).Where(c => c.AppID == appID).ToList();

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
            TB_Object obj = Context.TB_Object.Include(TB_Object.FieldName.Fields).Where(c => c.ID == objectID).SingleOrDefault();
            PublishObject(obj);
        }

        /// <summary>
        /// Publish object and its related fields to Live.
        /// </summary>
        /// <param name="obj">Object need to be gone live.</param>
        private void PublishObject(TB_Object obj)
        {
            Logger.Debug(string.Format("Publishing '{0}' object.", obj.ObjectName));
            if (obj != null)
            {
                if (CheckExist(obj.ApiName))
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
        }

        /// <summary>
        /// Check if Table Name is existed.
        /// </summary>
        /// <param name="tableName">Table need to be checked</param>
        /// <returns>True if Existed</returns>
        private bool CheckExist(string tableName)
        {
            return Context.Database.SqlQuery<int?>(@"SELECT 1 FROM sys.tables WHERE name = @tableName", new SqlParameter("@tableName", tableName)).Any();
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
            sqlBuilder.AppendFormat("{0} VARCHAR(255) NULL, ", CommonProperties.Descriptions);
            sqlBuilder.AppendFormat("{0} BIT NOT NULL, ", CommonProperties.IsActive);
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
        /// Fetch all data from Object
        /// </summary>
        /// <param name="objectID">Object ID need to fetch data</param>
        public MD_Live FetchObjectData(Guid objectID, bool isActive)
        {
            MD_Live live = new MD_Live();

            TB_Object obj = Context.TB_Object.Include(TB_Object.FieldName.Fields).Where(c => c.ID == objectID).SingleOrDefault();

            live.ObjectID = obj.ID;
            live.LiveName = obj.DisplayName;

            if (obj == null)
            {
                return null;
            }

            StringBuilder sqlSelectBuilder = new StringBuilder(" SELECT [0].ID ");

            StringBuilder sqlFromBuilder = new StringBuilder();
            sqlFromBuilder.AppendFormat(" FROM {0} AS [0] ", obj.ApiName);

            int aliasUniqueID = 1;

            foreach(TB_Field field in obj.Fields)
            {
                live.ColumnNames.Add(field.DisplayName);

                bool hasDatasource = false;
                if (field.FieldType == EN_DataType.E_DATASOURCE)
                {
                    //Format: Link DisplayField=API_Field_Name>>API_Object_Name
                    string[] datasourceFormat = field.Datasource.Split(new string[] { ">>" }, StringSplitOptions.RemoveEmptyEntries);

                    if (datasourceFormat[0].StartsWith("Link", StringComparison.InvariantCultureIgnoreCase))
                    {
                        hasDatasource = true;
                        
                        string[] parameters = datasourceFormat[0].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                        string displayField = parameters[1].Split(new string[] { "DisplayField=" }, StringSplitOptions.RemoveEmptyEntries)[0];

                        sqlSelectBuilder.AppendFormat(" , [{0}].{1} as [{2}]", aliasUniqueID, displayField, field.DisplayName);

                        sqlFromBuilder.AppendFormat(" LEFT JOIN {0} AS {1} ON [0].{2} = [{1}].ID ", datasourceFormat[1], aliasUniqueID, field.ApiName);
                    }
                }

                if (!hasDatasource)
                {
                    sqlSelectBuilder.AppendFormat(" ,[0].{0} as [{1}] ", field.ApiName, field.DisplayName);
                }

                aliasUniqueID++;
            }

            StringBuilder sqlWhereBuilder = new StringBuilder();
            sqlWhereBuilder.AppendFormat(" WHERE [0].IsActive = {0} ", isActive ? 1 : 0);

            live.Datasource = GetDynamicRecords(string.Format("{0}{1}{2}", sqlSelectBuilder.ToString(), sqlFromBuilder.ToString(), sqlWhereBuilder.ToString())).AsEnumerable();

            return live;
        }
    }
}