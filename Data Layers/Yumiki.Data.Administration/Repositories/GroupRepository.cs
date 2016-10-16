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
        /// <returns>List of all active group</returns>
        public List<TB_Group> GetAllGroups()
        {
            return Context.TB_Group.Where(c => c.IsActive).ToList();
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
    }
}
