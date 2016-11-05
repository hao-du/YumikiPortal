using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Data.Administration.Interfaces;
using Yumiki.Entity.Administration;

namespace Yumiki.Data.Administration.Repositories
{
    public class GroupRepository : AdministrationRepository, IGroupRepository
    {
        /// <summary>
        /// Get all active groups from Database
        /// </summary>
        /// <param name="showInactive">Show list of inactive groups or active groups</param>
        /// <returns>List of all active group</returns>
        public List<TB_Group> GetAllGroups(bool showInactive)
        {
            return Context.TB_Group.Where(c => c.IsActive == !showInactive).ToList();
        }

        /// <summary>
        /// Get specific group from Database
        /// </summary>
        /// <param name="id">Group ID - Must be Guid value</param>
        /// <returns>Group object</returns>
        public TB_Group GetGroup(Guid id)
        {
            return Context.TB_Group.Where(c => c.ID == id).SingleOrDefault();
        }

        /// <summary>
        /// Check duplicate group name between the values in UI and database
        /// </summary>
        /// <param name="groupName">Group Name will be saved into database</param>
        /// <param name="excludedGroupID">ID which need to be excluded to check</param>
        /// <returns></returns>
        public bool CheckValidGroupName(string groupName, Guid excludedGroupID)
        {
            int countGroupName = Context.TB_Group.Where(c => c.ID != excludedGroupID
                                                        && c.GroupName.ToLower() == groupName.ToLower()
                                                        && c.IsActive).Count();
            if (countGroupName > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Create/Update a group
        /// </summary>
        /// <param name="group">If group id is empty, then this is new group. Otherwise, this needs to be updated</param>
        public void SaveGroup(TB_Group group)
        {
            if (group.ID == Guid.Empty)
            {
                Context.TB_Group.Add(group);
            }
            else
            {
                TB_Group dbGroup = Context.TB_Group.Where(c => c.ID == group.ID).Single();
                dbGroup.GroupName = group.GroupName;
                dbGroup.Descriptions = group.Descriptions;
                dbGroup.IsActive = group.IsActive;
            }

            Save();
        }

        /// <summary>
        /// Get all users assigned to group or user not in group based on showAssginedUser parameter.
        /// </summary>
        /// <param name="groupID">Group ID of group need to be get all users.</param>
        /// <param name="showAssginedUser">Switch assgined/free user list.</param>
        /// <returns></returns>
        public List<TB_User> GetUsersFromGroup(Guid groupID, bool showAssginedUser)
        {
            IEnumerable<TB_User> assginedUsers = Context.TB_Group.Include(TB_User.FieldName.TB_User)
                                                        .Where(c => c.ID == groupID)
                                                        .SelectMany(c => c.TB_User).Where(c => c.IsActive).AsEnumerable();
            if (showAssginedUser)
            {
                return assginedUsers.ToList();
            }
            else
            {
                return Context.TB_User.Where(c => c.IsActive).Except(assginedUsers).ToList();
            }
        }

        /// <summary>
        /// Assign user list to group.
        /// </summary>
        /// <param name="groupID">Specify group to asssign users.</param>
        /// <param name="userIDs">List of Users will be assigned to group.</param>
        public void AddUsersToGroup(Guid groupID, List<Guid> userIDs)
        {
            TB_Group group = Context.TB_Group.Include(TB_User.FieldName.TB_User).Where(c => c.ID == groupID).Single();
            List<TB_User> unassignedUsers = Context.TB_User.Where(c => userIDs.Contains(c.ID)).ToList();

            foreach (TB_User user in unassignedUsers)
            {
                group.TB_User.Add(user);
            }

            Save();
        }

        /// <summary>
        /// Unassign user list to group.
        /// </summary>
        /// <param name="groupID">Specify group to unasssign users.</param>
        /// <param name="userIDs">List of Users will be unassigned to group.</param>
        public void RemoveUsersFromGroup(Guid groupID, List<Guid> userIDs)
        {
            TB_Group group = Context.TB_Group.Include(TB_User.FieldName.TB_User).Where(c => c.ID == groupID).Single();
            List<TB_User> assginedUsers = group.TB_User.Where(c => userIDs.Contains(c.ID)).ToList();

            foreach (TB_User user in assginedUsers)
            {
                group.TB_User.Remove(user);
            }

            Save();
        }

        /// <summary>
        /// Get all privileges assigned to group or privilege not in group based on showAssginedPrivilege parameter.
        /// </summary>
        /// <param name="groupID">Group ID of group need to be get all privileges.</param>
        /// <param name="showAssginedPrivilege">Switch assgined/free privilege list.</param>
        /// <returns></returns>
        public List<TB_Privilege> GetPrivilegesFromGroup(Guid groupID, bool showAssginedPrivilege)
        {
            IEnumerable<TB_Privilege> assginedPrivileges = Context.TB_Group.Include(TB_Privilege.FieldName.TB_Privilege)
                                                        .Where(c => c.ID == groupID)
                                                        .SelectMany(c => c.TB_Privilege).Where(c => c.IsActive).AsEnumerable();
            if (showAssginedPrivilege)
            {
                return assginedPrivileges.ToList();
            }
            else
            {
                return Context.TB_Privilege.Where(c => c.IsActive).Except(assginedPrivileges).ToList();
            }
        }

        /// <summary>
        /// Assign privilege list to group.
        /// </summary>
        /// <param name="groupID">Specify group to asssign privileges.</param>
        /// <param name="privilegeIDs">List of Privileges will be assigned to group.</param>
        public void AddPrivilegesToGroup(Guid groupID, List<Guid> privilegeIDs)
        {
            TB_Group group = Context.TB_Group.Include(TB_Privilege.FieldName.TB_Privilege).Where(c => c.ID == groupID).Single();
            List<TB_Privilege> unassignedPrivileges = Context.TB_Privilege.Where(c => privilegeIDs.Contains(c.ID)).ToList();

            foreach (TB_Privilege privilege in unassignedPrivileges)
            {
                group.TB_Privilege.Add(privilege);
            }

            Save();
        }

        /// <summary>
        /// Unassign privilege list to group.
        /// </summary>
        /// <param name="groupID">Specify group to unasssign privileges.</param>
        /// <param name="privilegeIDs">List of Privileges will be unassigned to group.</param>
        public void RemovePrivilegesFromGroup(Guid groupID, List<Guid> privilegeIDs)
        {
            TB_Group group = Context.TB_Group.Include(TB_Privilege.FieldName.TB_Privilege).Where(c => c.ID == groupID).Single();
            List<TB_Privilege> assginedPrivileges = group.TB_Privilege.Where(c => privilegeIDs.Contains(c.ID)).ToList();

            foreach (TB_Privilege privilege in assginedPrivileges)
            {
                group.TB_Privilege.Remove(privilege);
            }

            Save();
        }
    }
}
