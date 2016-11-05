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

        /// <summary>
        /// Get all users assigned to group or user not in group based on showAssginedUser parameter.
        /// </summary>
        /// <param name="groupID">Group ID of group need to be get all users.</param>
        /// <param name="showAssginedUser">Switch assgined/free user list.</param>
        /// <returns></returns>
        List<TB_User> GetUsersFromGroup(Guid groupID, bool showAssginedUser);

        /// <summary>
        /// Assign user list to group.
        /// </summary>
        /// <param name="groupID">Specify group to asssign users.</param>
        /// <param name="userIDs">List of Users will be assigned to group.</param>
        void AddUsersToGroup(Guid groupID, List<Guid> userIDs);

        /// <summary>
        /// Unassign user list to group.
        /// </summary>
        /// <param name="groupID">Specify group to unasssign users.</param>
        /// <param name="userIDs">List of Users will be unassigned to group.</param>
        void RemoveUsersFromGroup(Guid groupID, List<Guid> userIDs);
    }
}
