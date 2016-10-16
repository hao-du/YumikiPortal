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
            if(countGroupName > 0)
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
            if(group.ID == Guid.Empty)
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
    }
}
