using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Business.Administration.Interfaces;
using Yumiki.Business.Base;
using Yumiki.Common.Helper;
using Yumiki.Data.Administration.Interfaces;
using Yumiki.Entity.Administration;

namespace Yumiki.Business.Administration.Services
{
    public class GroupService : BaseService, IGroupService
    {
        private IGroupRepository repository;
        private IGroupRepository Repository
        {
            get
            {
                if(repository == null)
                {
                    repository = Service.GetInstance<IGroupRepository>();
                }
                return repository;
            }
        }

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
            if (string.IsNullOrEmpty(id))
            {
                throw new AdvanceException(ExceptionCode.E_EMPTY_VALUE, "Group ID cannot be empty.", null);
            }

            Guid groupID = Guid.Empty;
            Guid.TryParse(id, out groupID);
            if(groupID == Guid.Empty)
            {
                throw new AdvanceException(ExceptionCode.E_WRONG_TYPE, "Group ID must be GUID type.", null);
            }

            return Repository.GetGroup(groupID);
        }

        /// <summary>
        /// Create/Update a group
        /// </summary>
        /// <param name="group">If group id is empty, then this is new group. Otherwise, this needs to be updated</param>
        public void SaveGroup(TB_Group group)
        {
            if (string.IsNullOrEmpty(group.GroupName))
            {
                throw new AdvanceException(ExceptionCode.E_EMPTY_VALUE, "Group Name is required.", null);
            }

            if (!Repository.CheckValidGroupName(group.GroupName, group.ID))
            {
                throw new AdvanceException(ExceptionCode.E_DUPLICATED, "Group Name was used.", null);
            }

            Repository.SaveGroup(group);
        }
    }
}
