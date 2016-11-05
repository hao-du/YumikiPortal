using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Entity.Administration;

namespace Yumiki.Business.Administration.Interfaces
{
    public interface IGroupService
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
        TB_Group GetGroup(string id);

        /// <summary>
        /// Create/Update a group
        /// </summary>
        /// <param name="group">If group id is empty, then this is new group. Otherwise, this needs to be updated</param>
        void SaveGroup(TB_Group group);

        /// <summary>
        /// Get all users assigned to group or user not in group based on showAssginedUser parameter.
        /// </summary>
        /// <param name="groupID">Group ID of group need to be get all users.</param>
        /// <param name="showAssginedUser">Switch assgined/free user list.</param>
        /// <returns></returns>
        List<TB_User> GetUsersFromGroup(string groupID, bool showAssginedUser);

        /// <summary>
        /// Assign user list to group.
        /// </summary>
        /// <param name="groupID">Specify group to asssign users.</param>
        /// <param name="userIDs">List of Users will be assigned to group.</param>
        void AddUsersToGroup(string groupID, List<string> userIDs);

        /// <summary>
        /// Unassign user list to group.
        /// </summary>
        /// <param name="groupID">Specify group to unasssign users.</param>
        /// <param name="userIDs">List of Users will be unassigned to group.</param>
        void RemoveUsersFromGroup(string groupID, List<string> userIDs);

        /// <summary>
        /// Get all privilegdes assigned to group or privilegde not in group based on showAssginedPrivilege parameter.
        /// </summary>
        /// <param name="groupID">Group ID of group need to be get all privileges.</param>
        /// <param name="showAssginedPrivilege">Switch assgined/free privilege list.</param>
        /// <returns></returns>
        List<TB_Privilege> GetPrivilegesFromGroup(string groupID, bool showAssginedPrivilege);

        /// <summary>
        /// Assign privilege list to group.
        /// </summary>
        /// <param name="groupID">Specify group to asssign privileges.</param>
        /// <param name="privilegeIDs">List of Privileges will be assigned to group.</param>
        void AddPrivilegesToGroup(string groupID, List<string> privilegeIDs);

        /// <summary>
        /// Unassign privilege list to group.
        /// </summary>
        /// <param name="groupID">Specify group to unasssign privileges.</param>
        /// <param name="privilegeIDs">List of Privileges will be unassigned to group.</param>
        void RemovePrivilegesFromGroup(string groupID, List<string> privilegeIDs);
    }
}
