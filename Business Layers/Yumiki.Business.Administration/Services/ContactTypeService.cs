using System;
using System.Collections.Generic;
using Yumiki.Business.Administration.Interfaces;
using Yumiki.Business.Base;
using Yumiki.Commons.Exceptions;
using Yumiki.Data.Administration.Interfaces;
using Yumiki.Entity.Administration;

namespace Yumiki.Business.Administration.Services
{
    public class ContactTypeService : BaseService<IContactTypeRepository>, IContactTypeService
    {
        /// <summary>
        /// Get all active contact types from Database
        /// </summary>
        /// <param name="showInactive">Show list of inactive contact types or active contactTypes</param>
        /// <returns>List of all active contactType</returns>
        public List<TB_ContactType> GetAllContactTypes(bool showInactive)
        {
            return Repository.GetAllContactTypes(showInactive);
        }

        public TB_ContactType GetContactType(Guid id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get specific contact type from Database
        /// </summary>
        /// <param name="id">ContactType ID - Must be Guid value</param>
        /// <returns>ContactType object</returns>
        public TB_ContactType GetContactType(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Contact Type ID cannot be empty.", Logger);
            }

            Guid contactTypeID = Guid.Empty;
            Guid.TryParse(id, out contactTypeID);
            if (contactTypeID == Guid.Empty)
            {
                throw new YumikiException(ExceptionCode.E_WRONG_TYPE, "Contact Type ID must be GUID type.", Logger);
            }

            return Repository.GetContactType(contactTypeID);
        }

        /// <summary>
        /// Create/Update a contact type
        /// </summary>
        /// <param name="contactType">If contactType id is empty, then this is new contactType. Otherwise, this needs to be updated</param>
        public void SaveContactType(TB_ContactType contactType)
        {
            if (string.IsNullOrWhiteSpace(contactType.ContactTypeName))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Contact Type Name is required.", Logger);
            }

            if (!Repository.CheckValidContactTypeName(contactType.ContactTypeName, contactType.ID))
            {
                throw new YumikiException(ExceptionCode.E_DUPLICATED, "Contact Type Name was used.", Logger);
            }

            Repository.SaveContactType(contactType);
        }
    }
}
