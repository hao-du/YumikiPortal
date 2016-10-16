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
        /// <summary>
        /// Get all active groups from Database
        /// </summary>
        /// <returns>List of all active group</returns>
        public List<TB_Group> GetAllGroups()
        {
            IGroupRepository groupRepository = Service.GetInstance<IGroupRepository>();
            return groupRepository.GetAllGroups();
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

            IGroupRepository groupRepository = Service.GetInstance<IGroupRepository>();
            return groupRepository.GetGroup(groupID);
        }
    }
}
