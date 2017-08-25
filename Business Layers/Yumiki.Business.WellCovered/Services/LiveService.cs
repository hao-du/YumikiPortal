using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Business.Base;
using Yumiki.Business.WellCovered.Interfaces;
using Yumiki.Commons.Exceptions;
using Yumiki.Commons.Helpers;
using Yumiki.Data.WellCovered.Interfaces;
using Yumiki.Data.WellCovered.Repositories;
using Yumiki.Entity.MoneyTrace.ServiceObjects;
using Yumiki.Entity.WellCovered;

namespace Yumiki.Business.WellCovered.Services
{
    public class LiveService : BaseService<ILiveRepository>, ILiveService
    {
        /// <summary>
        /// Perform Full text search for all content.
        /// </summary>
        /// <param name="keywords">Search keywords like google.</param>
        /// <returns>List of search result in TB_Index format.</returns>
        public GetSearchIndexResponse Search(GetSearchIndexRequest request)
        {

            if (string.IsNullOrWhiteSpace(request.Keywords))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Keywords must not be empty.", Logger);
            }

            return Repository.Search(request);
        }

        /// <summary>
        /// Get fields from ObjectID
        /// </summary>
        public IEnumerable<TB_Field> GetFields(string objectID)
        {
            Guid convertedID = Guid.Empty;
            Guid.TryParse(objectID, out convertedID);
            if (convertedID == Guid.Empty)
            {
                throw new YumikiException(ExceptionCode.E_WRONG_TYPE, "Object ID must be GUID type.", Logger);
            }

            return Repository.GetFields(convertedID);
        }

        /// <summary>
        /// Publish App and its related objects and fields to Live.
        /// </summary>
        /// <param name="objectID">Id of App need to be gone live.</param>
        public void PublishApp(string appID)
        {
            Guid convertedID = Guid.Empty;
            Guid.TryParse(appID, out convertedID);
            if (convertedID == Guid.Empty)
            {
                throw new YumikiException(ExceptionCode.E_WRONG_TYPE, "App ID must be GUID type.", Logger);
            }

            if (!Repository.IsAppValidToPublish(convertedID))
            {
                throw new YumikiException(ExceptionCode.E_INVALID_VALUE, "App can only be published if it has objects and fields.", Logger);
            }

            Repository.PublishApp(convertedID);
        }

        /// <summary>
        /// Publish object and its related fields to Live.
        /// </summary>
        /// <param name="objectID">Id of Object need to be gone live.</param>
        public void PublishObject(string objectID)
        {
            Guid convertedID = Guid.Empty;
            Guid.TryParse(objectID, out convertedID);
            if (convertedID == Guid.Empty)
            {
                throw new YumikiException(ExceptionCode.E_WRONG_TYPE, "Object ID must be GUID type.", Logger);
            }

            if (!Repository.IsObjectValidToPublish(convertedID))
            {
                throw new YumikiException(ExceptionCode.E_INVALID_VALUE, "Object can only be published if it has fields.", Logger);
            }

            Repository.PublishObject(convertedID);
        }

        /// <summary>
        /// Get Datasource from Datasource Field
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="displayFieldName"></param>
        /// <returns></returns>
        public IEnumerable<MD_Datasource> GetDataSource(string dataSource)
        {
            string[] datasourceFormat = dataSource.Split(new string[] { ">>" }, StringSplitOptions.RemoveEmptyEntries);

            //Link DisplayField=API_Field_Name>>API_Object_Name
            if (datasourceFormat[0].StartsWith("Link", StringComparison.InvariantCultureIgnoreCase))
            {
                string tableName = datasourceFormat[1];

                string[] parameters = datasourceFormat[0].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                string displayFieldName = parameters[1].Split(new string[] { "DisplayField=" }, StringSplitOptions.RemoveEmptyEntries)[0];

                return Repository.GetDataSource(tableName, displayFieldName);
            }
            //List>>A,B,C,D,E,F
            else
            {
                List<MD_Datasource> datasource = new List<MD_Datasource>();
                if (datasourceFormat.Count() > 1)
                {
                    foreach (string item in datasourceFormat[1].Split(','))
                    {
                        datasource.Add(new MD_Datasource() { ID = item, DisplayText = item });
                    }
                }
                return datasource;
            }
        }

        /// <summary>
        /// Fetch all data from Object
        /// </summary>
        /// <param name="objectID">Object ID need to fetch data</param>
        public MD_Live FetchViewObjectData(string objectID, bool isActive)
        {
            Guid convertedID = Guid.Empty;
            Guid.TryParse(objectID, out convertedID);
            if (convertedID == Guid.Empty)
            {
                throw new YumikiException(ExceptionCode.E_WRONG_TYPE, "Object ID must be GUID type.", Logger);
            }

            return Repository.FetchViewObjectData(convertedID, isActive);
        }

        /// <summary>
        /// Fetch record from Object by ID
        /// </summary>
        /// <param name="objectID">Object ID need to fetch data</param>
        public IEnumerable<TB_Field> GetFieldsByID(string objectID, string recordID)
        {
            Guid convertedObjectID = Guid.Empty;
            Guid.TryParse(objectID, out convertedObjectID);
            if (convertedObjectID == Guid.Empty)
            {
                throw new YumikiException(ExceptionCode.E_WRONG_TYPE, "Object ID must be GUID type.", Logger);
            }

            Guid convertedRecordD = Guid.Empty;
            Guid.TryParse(recordID, out convertedRecordD);
            if (convertedRecordD == Guid.Empty)
            {
                throw new YumikiException(ExceptionCode.E_WRONG_TYPE, "Record ID must be GUID type.", Logger);
            }

            MD_Live live = Repository.FetchViewObjectDataByID(convertedObjectID, convertedRecordD);
            DataRow row = live.Datasource.FirstOrDefault();

            IEnumerable<TB_Field> fields = this.GetFields(objectID);

            if (row != null)
            {
                foreach (TB_Field field in fields)
                {
                    field.Value = row[field.DisplayName];
                }
            }

            return fields;
        }

        /// <summary>
        /// Save an record of object to DB
        /// </summary>
        /// <param name="record"></param>
        public void Add(string objectID, Dictionary<string, string> inputFields)
        {
            Guid convertedObjectID = Guid.Empty;
            Guid.TryParse(objectID, out convertedObjectID);
            if (convertedObjectID == Guid.Empty)
            {
                throw new YumikiException(ExceptionCode.E_WRONG_TYPE, "Object ID must be GUID type.", Logger);
            }

            IEnumerable<TB_Field> dbFields = AssignValueToFields(convertedObjectID, inputFields);

            Repository.Add(convertedObjectID, dbFields);
        }

        /// <summary>
        /// Update an record of object to DB
        /// </summary>
        /// <param name="record"></param>
        public void Update(string objectID, string recordID, Dictionary<string, string> inputFields)
        {
            Guid convertedObjectID = Guid.Empty;
            Guid.TryParse(objectID, out convertedObjectID);
            if (convertedObjectID == Guid.Empty)
            {
                throw new YumikiException(ExceptionCode.E_WRONG_TYPE, "Object ID must be GUID type.", Logger);
            }

            Guid convertedRecordD = Guid.Empty;
            Guid.TryParse(recordID, out convertedRecordD);
            if (convertedRecordD == Guid.Empty)
            {
                throw new YumikiException(ExceptionCode.E_WRONG_TYPE, "Record ID must be GUID type.", Logger);
            }

            IEnumerable<TB_Field> dbFields = AssignValueToFields(convertedObjectID, inputFields);

            Repository.Update(convertedObjectID, convertedRecordD, dbFields);
        }

        /// <summary>
        /// Assign value to fields.
        /// </summary>
        /// <param name="objectID"></param>
        /// <param name="inputFields"></param>
        /// <returns></returns>
        private IEnumerable<TB_Field> AssignValueToFields(Guid objectID, Dictionary<string, string> inputFields)
        {
            IEnumerable<TB_Field> dbFields = Repository.GetFields(objectID);

            foreach (TB_Field field in dbFields)
            {
                string value = inputFields[field.ApiName];

                if (field.IsRequired && string.IsNullOrWhiteSpace(value))
                {
                    throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, string.Format("{0} field is required.", field.DisplayName), Logger);
                }

                if (!string.IsNullOrWhiteSpace(value))
                {
                    switch (field.FieldType)
                    {
                        case EN_DataType.E_INT:
                            field.Value = int.Parse(value);
                            break;
                        case EN_DataType.E_DECIMAL:
                            field.Value = decimal.Parse(value);
                            break;
                        case EN_DataType.E_BOOL:
                            field.Value = bool.Parse(value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)[0]);
                            break;
                        case EN_DataType.E_DATE:
                            field.Value = DateTime.Parse(value).Date;
                            break;
                        case EN_DataType.E_DATETIME:
                            field.Value = DateTime.Parse(value);
                            break;
                        case EN_DataType.E_TIME:
                            field.Value = DateTime.Parse(value).TimeOfDay;
                            break;
                        default:
                            //Datasource Type and String type use String as it is.
                            field.Value = value;
                            break;
                    }
                }
            }

            return dbFields;
        }
    }
}
