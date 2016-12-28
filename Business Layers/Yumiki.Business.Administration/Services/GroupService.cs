using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Business.Administration.Interfaces;
using Yumiki.Business.Base;
using Yumiki.Commons.Exceptions;
using Yumiki.Data.Administration.Interfaces;
using Yumiki.Entity.Administration;

namespace Yumiki.Business.Administration.Services
{
    public class GroupService : BaseService<IGroupRepository>, IGroupService
    {
        /// <summary>
        /// Get all active groups from Database
        /// </summary>
        /// <param name="showInactive">Show list of inactive groups or active groups</param>
        /// <returns>List of all active group</returns>
        public List<TB_Group> GetAllGroups(bool showInactive)
        {
            return Repository.GetAllGroups(showInactive);
        }

        /// <summary>
        /// Get specific group from Database
        /// </summary>
        /// <param name="id">Group ID - Must be Guid value</param>
        /// <returns>Group object</returns>
        public TB_Group GetGroup(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Group ID cannot be empty.", Logger);
            }

            Guid groupID = Guid.Empty;
            Guid.TryParse(id, out groupID);
            if (groupID == Guid.Empty)
            {
                throw new YumikiException(ExceptionCode.E_WRONG_TYPE, "Group ID must be GUID type.", Logger);
            }

            return Repository.GetGroup(groupID);
        }

        /// <summary>
        /// Create/Update a group
        /// </summary>
        /// <param name="group">If group id is empty, then this is new group. Otherwise, this needs to be updated</param>
        public void SaveGroup(TB_Group group)
        {
            if (string.IsNullOrWhiteSpace(group.GroupName))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Group Name is required.", Logger);
            }

            if (!Repository.CheckValidGroupName(group.GroupName, group.ID))
            {
                throw new YumikiException(ExceptionCode.E_DUPLICATED, "Group Name was used.", Logger);
            }

            Repository.SaveGroup(group);
        }

        /// <summary>
        /// Get all users assigned to group or user not in group based on showAssginedUser parameter.
        /// </summary>
        /// <param name="groupID">Group ID of group need to be get all users.</param>
        /// <param name="showAssginedUser">Switch assgined/free user list.</param>
        /// <returns></returns>
        public List<TB_User> GetUsersFromGroup(string groupID, bool showAssginedUser)
        {
            if (string.IsNullOrWhiteSpace(groupID))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Group ID cannot be empty.", Logger);
            }

            Guid convertedGroupID = Guid.Empty;
            Guid.TryParse(groupID, out convertedGroupID);
            if (convertedGroupID == Guid.Empty)
            {
                throw new YumikiException(ExceptionCode.E_WRONG_TYPE, "Group ID must be GUID type.", Logger);
            }

            return Repository.GetUsersFromGroup(convertedGroupID, showAssginedUser);
        }

        /// <summary>
        /// Assign user list to group.
        /// </summary>
        /// <param name="groupID">Specify group to asssign users.</param>
        /// <param name="userIDs">List of Users will be assigned to group.</param>
        public void AddUsersToGroup(string groupID, List<string> userIDs)
        {
            if (string.IsNullOrWhiteSpace(groupID))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Group ID cannot be empty.", Logger);
            }

            Guid convertedGroupID = Guid.Empty;
            Guid.TryParse(groupID, out convertedGroupID);
            if (convertedGroupID == Guid.Empty)
            {
                throw new YumikiException(ExceptionCode.E_WRONG_TYPE, "Group ID must be GUID type.", Logger);
            }

            List<Guid> convertedUserIds = new List<Guid>();
            foreach (string userID in userIDs)
            {
                if (string.IsNullOrWhiteSpace(userID))
                {
                    throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "User ID cannot be empty.", Logger);
                }

                Guid convertedUserID = Guid.Empty;
                Guid.TryParse(userID, out convertedUserID);
                if (convertedUserID == Guid.Empty)
                {
                    throw new YumikiException(ExceptionCode.E_WRONG_TYPE, "User ID must be GUID type.", Logger);
                }

                convertedUserIds.Add(convertedUserID);
            }

            Repository.AddUsersToGroup(convertedGroupID, convertedUserIds);
        }

        /// <summary>
        /// Unassign user list to group.
        /// </summary>
        /// <param name="groupID">Specify group to unasssign users.</param>
        /// <param name="userIDs">List of Users will be unassigned to group.</param>
        public void RemoveUsersFromGroup(string groupID, List<string> userIDs)
        {
            if (string.IsNullOrWhiteSpace(groupID))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Group ID cannot be empty.", Logger);
            }

            Guid convertedGroupID = Guid.Empty;
            Guid.TryParse(groupID, out convertedGroupID);
            if (convertedGroupID == Guid.Empty)
            {
                throw new YumikiException(ExceptionCode.E_WRONG_TYPE, "Group ID must be GUID type.", Logger);
            }

            List<Guid> convertedUserIds = new List<Guid>();
            foreach (string userID in userIDs)
            {
                if (string.IsNullOrWhiteSpace(userID))
                {
                    throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "User ID cannot be empty.", Logger);
                }

                Guid convertedUserID = Guid.Empty;
                Guid.TryParse(userID, out convertedUserID);
                if (convertedUserID == Guid.Empty)
                {
                    throw new YumikiException(ExceptionCode.E_WRONG_TYPE, "User ID must be GUID type.", Logger);
                }

                convertedUserIds.Add(convertedUserID);
            }

            Repository.RemoveUsersFromGroup(convertedGroupID, convertedUserIds);
        }

        /// <summary>
        /// Get all privilegdes assigned to group or privilegde not in group based on showAssginedPrivilege parameter.
        /// </summary>
        /// <param name="groupID">Group ID of group need to be get all privileges.</param>
        /// <param name="showAssginedPrivilege">Switch assgined/free privilege list.</param>
        /// <returns></returns>
        public List<TB_Privilege> GetPrivilegesFromGroup(string groupID, bool showAssginedPrivilege)
        {
            if (string.IsNullOrWhiteSpace(groupID))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Group ID cannot be empty.", Logger);
            }

            Guid convertedGroupID = Guid.Empty;
            Guid.TryParse(groupID, out convertedGroupID);
            if (convertedGroupID == Guid.Empty)
            {
                throw new YumikiException(ExceptionCode.E_WRONG_TYPE, "Group ID must be GUID type.", Logger);
            }

            return Repository.GetPrivilegesFromGroup(convertedGroupID, showAssginedPrivilege);
        }

        /// <summary>
        /// Assign privilege list to group.
        /// </summary>
        /// <param name="groupID">Specify group to asssign privileges.</param>
        /// <param name="privilegeIDs">List of Privileges will be assigned to group.</param>
        public void AddPrivilegesToGroup(string groupID, List<string> privilegeIDs)
        {
            if (string.IsNullOrWhiteSpace(groupID))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Group ID cannot be empty.", Logger);
            }

            Guid convertedGroupID = Guid.Empty;
            Guid.TryParse(groupID, out convertedGroupID);
            if (convertedGroupID == Guid.Empty)
            {
                throw new YumikiException(ExceptionCode.E_WRONG_TYPE, "Group ID must be GUID type.", Logger);
            }

            List<Guid> convertedPrivilegeIds = new List<Guid>();
            foreach (string privilegeID in privilegeIDs)
            {
                if (string.IsNullOrWhiteSpace(privilegeID))
                {
                    throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Privilege ID cannot be empty.", Logger);
                }

                Guid convertedPrivilegeID = Guid.Empty;
                Guid.TryParse(privilegeID, out convertedPrivilegeID);
                if (convertedPrivilegeID == Guid.Empty)
                {
                    throw new YumikiException(ExceptionCode.E_WRONG_TYPE, "Privilege ID must be GUID type.", Logger);
                }

                convertedPrivilegeIds.Add(convertedPrivilegeID);
            }

            Repository.AddPrivilegesToGroup(convertedGroupID, convertedPrivilegeIds);
        }

        /// <summary>
        /// Unassign privilege list to group.
        /// </summary>
        /// <param name="groupID">Specify group to unasssign privileges.</param>
        /// <param name="privilegeIDs">List of Privileges will be unassigned to group.</param>
        public void RemovePrivilegesFromGroup(string groupID, List<string> privilegeIDs)
        {
            if (string.IsNullOrWhiteSpace(groupID))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Group ID cannot be empty.", Logger);
            }

            Guid convertedGroupID = Guid.Empty;
            Guid.TryParse(groupID, out convertedGroupID);
            if (convertedGroupID == Guid.Empty)
            {
                throw new YumikiException(ExceptionCode.E_WRONG_TYPE, "Group ID must be GUID type.", Logger);
            }

            List<Guid> convertedPrivilegeIds = new List<Guid>();
            foreach (string privilegeID in privilegeIDs)
            {
                if (string.IsNullOrWhiteSpace(privilegeID))
                {
                    throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Privilege ID cannot be empty.", Logger);
                }

                Guid convertedPrivilegeID = Guid.Empty;
                Guid.TryParse(privilegeID, out convertedPrivilegeID);
                if (convertedPrivilegeID == Guid.Empty)
                {
                    throw new YumikiException(ExceptionCode.E_WRONG_TYPE, "Privilege ID must be GUID type.", Logger);
                }

                convertedPrivilegeIds.Add(convertedPrivilegeID);
            }

            Repository.RemovePrivilegesFromGroup(convertedGroupID, convertedPrivilegeIds);
        }
    }
}
