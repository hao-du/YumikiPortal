using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Data.Master.Interfaces;
using Yumiki.Entity.Master;

namespace Yumiki.Data.Master.Repositories
{
    public class GUIRepository : MasterRepository, IGUIRepository
    {
        /// <summary>
        /// Get all privileges (menus) from database and show to UI.
        /// </summary>
        /// <param name="userId">Menu list from this specific user.</param>
        /// <returns></returns>
        public List<VW_Privilege> GetPrivilege(Guid userID)
        {
            return Context.VW_Privilege.Where(c => c.UserID == userID).ToList();
        }
    }
}
