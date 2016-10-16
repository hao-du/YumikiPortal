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
        /// <returns>List of all active group</returns>
        List<TB_Group> GetAllGroups();

        /// <summary>
        /// Get specific group from Database
        /// </summary>
        /// <param name="id">Group ID - Must be Guid value</param>
        /// <returns>Group object</returns>
        TB_Group GetGroup(Guid id);
    }
}
