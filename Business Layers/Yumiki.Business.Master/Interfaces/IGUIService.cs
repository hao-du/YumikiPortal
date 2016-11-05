using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Entity.Master;

namespace Yumiki.Business.Master.Interfaces
{
    public interface IGUIService
    {
        /// <summary>
        /// Get all privileges (menus) from database and show to UI.
        /// </summary>
        /// <param name="userId">Menu list from this specific user.</param>
        /// <returns>List of privilege in HTML Format</returns>
        string GetPrivilege(string userID);
    }
}
