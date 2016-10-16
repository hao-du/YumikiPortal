using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Entity.Administration;

namespace Yumiki.Data.Administration.Interfaces
{
    public interface IGroupRepository
    {
        /// <summary>
        /// Get all active groups from Database
        /// </summary>
        /// <param name="showInactive">Show list of inactive groups or active groups</param>
        /// <returns>List of all active group</returns>
        List<TB_Group> GetAllGroups(bool showInactive);

        /// <summary>
        /// Get specific group from Database
        /// </summary>
        /// <param name="id">Group ID - Must be Guid value</param>
        /// <returns>Group object</returns>
        TB_Group GetGroup(Guid id);

        /// <summary>
        /// Create/Update a group
        /// </summary>
        /// <param name="group">If group id is empty, then this is new group. Otherwise, this needs to be updated</param>
        void SaveGroup(TB_Group group);

        /// <summary>
        /// Check duplicate group name between the values in UI and database
        /// </summary>
        /// <param name="groupName">Group Name will be saved into database</param>
        /// <param name="excludedGroupID">ID which need to be excluded to check</param>
        /// <returns></returns>
        bool CheckValidGroupName(string groupName, Guid excludedGroupID);
    }
}
