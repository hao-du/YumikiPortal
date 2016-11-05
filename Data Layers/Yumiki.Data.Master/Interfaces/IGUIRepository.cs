using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Entity.Master;

namespace Yumiki.Data.Master.Interfaces
{
    public interface IGUIRepository
    {
        /// <summary>
        /// Get all privileges (menus) from database and show to UI.
        /// </summary>
        /// <param name="userId">Menu list from this specific user.</param>
        /// <returns></returns>
        List<VW_Privilege> GetPrivilege(Guid userID);
    }
}
