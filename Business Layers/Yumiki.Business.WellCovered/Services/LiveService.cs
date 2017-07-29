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
using Yumiki.Entity.WellCovered;

namespace Yumiki.Business.WellCovered.Services
{
    public class LiveService : BaseService<ILiveRepository>, ILiveService
    {
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
                foreach(string item in datasourceFormat[1].Split(','))
                {
                    datasource.Add(new MD_Datasource() { ID = item, DisplayText = item });
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

            IEnumerable<TB_Field> dbFields = Repository.GetFields(convertedObjectID);

            foreach (TB_Field field in dbFields)
            {
                string value = inputFields[field.ApiName];

                if (field.IsRequired && string.IsNullOrWhiteSpace(value))
                {
                    throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, string.Format("{0} field is required.", field.DisplayName), Logger);
                }

                switch (field.FieldType)
                {
                    case EN_DataType.E_INT:
                        field.Value = int.Parse(value);
                        break;
                    case EN_DataType.E_DECIMAL:
                        field.Value = decimal.Parse(value);
                        break;
                    case EN_DataType.E_BOOL:
                        field.Value = bool.Parse(value);
                        break;
                    case EN_DataType.E_DATE:
                    case EN_DataType.E_DATETIME:
                    case EN_DataType.E_TIME:
                        field.Value = DateTime.Parse(value);
                        break;
                    default:
                        //Datasource Type and String type use String as it is.
                        field.Value = value;
                        break;
                }
            }

            Repository.Add(convertedObjectID, dbFields);
        }
    }
}
