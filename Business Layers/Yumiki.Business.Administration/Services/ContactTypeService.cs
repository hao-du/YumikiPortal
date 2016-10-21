using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Business.Administration.Interfaces;
using Yumiki.Business.Base;
using Yumiki.Common.Helper;
using Yumiki.Data.Administration.Interfaces;
using Yumiki.Entity.Administration;

namespace Yumiki.Business.Administration.Services
{
    public class ContactTypeService : BaseService, IContactTypeService
    {
        private IContactTypeRepository repository;
        private IContactTypeRepository Repository
        {
            get
            {
                if (repository == null)
                {
                    repository = Service.GetInstance<IContactTypeRepository>();
                }
                return repository;
            }
        }

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
            if (string.IsNullOrEmpty(id))
            {
                throw new AdvanceException(ExceptionCode.E_EMPTY_VALUE, "Contact Type ID cannot be empty.", null);
            }

            Guid contactTypeID = Guid.Empty;
            Guid.TryParse(id, out contactTypeID);
            if (contactTypeID == Guid.Empty)
            {
                throw new AdvanceException(ExceptionCode.E_WRONG_TYPE, "Contact Type ID must be GUID type.", null);
            }

            return Repository.GetContactType(contactTypeID);
        }

        /// <summary>
        /// Create/Update a contact type
        /// </summary>
        /// <param name="contactType">If contactType id is empty, then this is new contactType. Otherwise, this needs to be updated</param>
        public void SaveContactType(TB_ContactType contactType)
        {
            if (string.IsNullOrEmpty(contactType.ContactTypeName))
            {
                throw new AdvanceException(ExceptionCode.E_EMPTY_VALUE, "Contact Type Name is required.", null);
            }

            if (!Repository.CheckValidContactTypeName(contactType.ContactTypeName, contactType.ID))
            {
                throw new AdvanceException(ExceptionCode.E_DUPLICATED, "Contact Type Name was used.", null);
            }

            Repository.SaveContactType(contactType);
        }
    }
}
