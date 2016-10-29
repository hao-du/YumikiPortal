using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Entity.Administration;

namespace Yumiki.Business.Administration.Interfaces
{
    public interface IPrivilegeService
    {
        /// <summary>
        /// Get all active privileges from Database
        /// </summary>
        /// <param name="showInactive">Show list of inactive privileges or active privileges</param>
        /// <param name="parentID">Get privileges from Parent Privilege - Guid.Empty is Root, NULL to ignore this filter</param>
        /// <returns>List of all active privilege</returns>
        List<TB_Privilege> GetAllPrivileges(bool showInactive, string parentID);

        /// <summary>
        /// Get specific privilege from Database
        /// </summary>
        /// <param name="id">Privilege ID - Must be Guid value</param>
        /// <returns>Privilege object</returns>
        TB_Privilege GetPrivilege(string id);

        /// <summary>
        /// Create/Update a privilege
        /// </summary>
        /// <param name="privilege">If privilege id is empty, then this is new privilege. Otherwise, this needs to be updated</param>
        void SavePrivilege(TB_Privilege privilege);
    }
}
