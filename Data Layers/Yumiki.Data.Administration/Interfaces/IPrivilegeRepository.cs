using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Entity.Administration;

namespace Yumiki.Data.Administration.Interfaces
{
    public interface IPrivilegeRepository
    {
        /// <summary>
        /// Get all active privileges from Database
        /// </summary>
        /// <param name="showInactive">Show list of inactive privileges or active privileges</param>
        /// <param name="parentID">Get privileges from Parent Privilege - Guid.Empty is Root</param>
        /// <returns>List of all active privilege</returns>
        List<TB_Privilege> GetAllPrivileges(bool showInactive, Guid? parentID);

        /// <summary>
        /// Get specific privilege from Database
        /// </summary>
        /// <param name="id">Privilege ID - Must be Guid value</param>
        /// <returns>Privilege object</returns>
        TB_Privilege GetPrivilege(Guid id);

        /// <summary>
        /// Check duplicate privilege name and path between the values in UI and database
        /// </summary>
        /// <param name="privilegeName">Privilege Name will be saved into database</param>
        /// <param name="pagePath">Privilege Path will be saved into database</param>
        /// <param name="excludedPrivilegeID">ID which need to be excluded to check</param>
        /// <returns></returns>
        bool CheckValidPrivilegeName(string privilegeName, string pagePath, Guid excludedPrivilegeID);

        /// <summary>
        /// Create/Update a privilege
        /// </summary>
        /// <param name="privilege">If privilege id is empty, then this is new privilege. Otherwise, this needs to be updated</param>
        void SavePrivilege(TB_Privilege privilege);
    }
}
