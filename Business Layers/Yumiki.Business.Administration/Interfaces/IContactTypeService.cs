using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Entity.Administration;

namespace Yumiki.Business.Administration.Interfaces
{
    public interface IContactTypeService
    {
        /// <summary>
        /// Get all active contact types from Database
        /// </summary>
        /// <param name="showInactive">Show list of inactive contact types or active contactTypes</param>
        /// <returns>List of all active contactType</returns>
        List<TB_ContactType> GetAllContactTypes(bool showInactive);

        /// <summary>
        /// Get specific contact type from Database
        /// </summary>
        /// <param name="id">ContactType ID - Must be Guid value</param>
        /// <returns>ContactType object</returns>
        TB_ContactType GetContactType(string id);

        /// <summary>
        /// Create/Update a contact type
        /// </summary>
        /// <param name="contactType">If contactType id is empty, then this is new contactType. Otherwise, this needs to be updated</param>
        void SaveContactType(TB_ContactType contactType);
    }
}
