using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Business.Administration.Interfaces;
using Yumiki.Business.Base;
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
    }
}
