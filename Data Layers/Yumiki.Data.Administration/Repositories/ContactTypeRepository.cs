using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Data.Administration.Interfaces;
using Yumiki.Entity.Administration;

namespace Yumiki.Data.Administration.Repositories
{
    public class ContactTypeRepository : AdministrationRepository, IContactTypeRepository
    {
        /// <summary>
        /// Get all active contact types from Database
        /// </summary>
        /// <param name="showInactive">Show list of inactive contact types or active contactTypes</param>
        /// <returns>List of all active contactType</returns>
        public List<TB_ContactType> GetAllContactTypes(bool showInactive)
        {
            return Context.TB_ContactType.Where(c => c.IsActive == !showInactive).ToList();
        }

        /// <summary>
        /// Get specific contact type from Database
        /// </summary>
        /// <param name="id">ContactType ID - Must be Guid value</param>
        /// <returns>ContactType object</returns>
        public TB_ContactType GetContactType(Guid id)
        {
            return Context.TB_ContactType.Where(c => c.ID == id).SingleOrDefault();
        }

        /// <summary>
        /// Check duplicate contactType name between the values in UI and database
        /// </summary>
        /// <param name="contactTypeName">Contact type Name will be saved into database</param>
        /// <param name="excludedContactTypeID">ID which need to be excluded to check</param>
        /// <returns></returns>
        public bool CheckValidContactTypeName(string contactTypeName, Guid excludedContactTypeID)
        {
            int countContactTypeName = Context.TB_Group.Where(c => c.ID != excludedContactTypeID
                                                        && c.GroupName.ToLower() == contactTypeName.ToLower()
                                                        && c.IsActive).Count();
            if (countContactTypeName > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Create/Update a contact type
        /// </summary>
        /// <param name="contactType">If contactType id is empty, then this is new contactType. Otherwise, this needs to be updated</param>
        public void SaveContactType(TB_ContactType contactType)
        {
            if (contactType.ID == Guid.Empty)
            {
                Context.TB_ContactType.Add(contactType);
            }
            else
            {
                TB_ContactType dbContactType = Context.TB_ContactType.Where(c => c.ID == contactType.ID).Single();
                dbContactType.ContactTypeName = contactType.ContactTypeName;
                dbContactType.Descriptions = contactType.Descriptions;
                dbContactType.IsActive = contactType.IsActive;
            }

            Save();
        }
    }
}
