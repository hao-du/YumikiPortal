using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Business.Base;
using Yumiki.Business.WellCovered.Interfaces;
using Yumiki.Commons.Entities;
using Yumiki.Commons.Exceptions;
using Yumiki.Commons.Helpers;
using Yumiki.Data.WellCovered.Interfaces;
using Yumiki.Entity.WellCovered;

namespace Yumiki.Business.WellCovered.Services
{
    public class FieldService : BaseService<IFieldRepository>, IFieldService
    {
        /// <summary>
        /// Get all active Fields from specific Object in Database.
        /// </summary>
        /// <param name="showInactive">Show list of inactive Fields or active Fields.</param>
        /// <returns>List of all active Fields.</returns>
        public IEnumerable<TB_Field> GetAllFields(bool showInactive, string objectID)
        {
            if (string.IsNullOrWhiteSpace(objectID))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Object ID cannot be empty.", Logger);
            }

            Guid convertedID = Guid.Empty;
            Guid.TryParse(objectID, out convertedID);
            if (convertedID == Guid.Empty)
            {
                throw new YumikiException(ExceptionCode.E_WRONG_TYPE, "Object ID must be GUID type.", Logger);
            }

            return Repository.GetAllFields(showInactive, convertedID);
        }

        /// <summary>
        /// Return specific Field by id.
        /// </summary>
        /// <param name="id">TB_Field Guid ID.</param>
        /// <returns>Result with TB_Field type.</returns>
        public TB_Field GetFieldByID(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Field ID cannot be empty.", Logger);
            }

            Guid convertedID = Guid.Empty;
            Guid.TryParse(id, out convertedID);
            if (convertedID == Guid.Empty)
            {
                throw new YumikiException(ExceptionCode.E_WRONG_TYPE, "Field ID must be GUID type.", Logger);
            }

            return  Repository.GetFieldByID(convertedID);
        }

        /// <summary>
        /// Save Field to DB
        /// </summary>
        /// <param name="field">If Field id is empty, then this is new Field. Otherwise, this needs to be updated</param>
        public void Save(TB_Field field)
        {
            if (string.IsNullOrWhiteSpace(field.FieldName))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Object Name cannot be empty.", Logger);
            }

            if (string.IsNullOrWhiteSpace(field.DisplayName))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Display Name cannot be empty.", Logger);
            }

            if (string.IsNullOrWhiteSpace(field.ApiName))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Api Name cannot be empty.", Logger);
            }

            if (Repository.Any(field))
            {
                throw new YumikiException(ExceptionCode.E_DUPLICATED, "Api Name have already been used.", Logger);
            }

            switch (field.FieldType)
            {
                case EN_DataType.E_STRING:
                    if (!field.FieldLength.HasValue || field.FieldLength <= 0)
                    {
                        throw new YumikiException(ExceptionCode.E_WRONG_VALUE, "Field Length must greater than 0 if Data Type is String.", Logger);
                    }
                    break;
                case EN_DataType.E_DATASOURCE:
                    if (string.IsNullOrWhiteSpace(field.Datasource))
                    {
                        throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Datasource cannot be empty if Data Type is Datasource.", Logger);
                    }
                    break;
            }

            Repository.Save(field);
        }

        /// <summary>
        /// Get datasource with EN_DataType type.
        /// </summary>
        /// <returns></returns>
        public List<ExtendEnum> GetDataTypes()
        {
            return EnumHelper.GetDatasource<EN_DataType>();
        }
    }
}
