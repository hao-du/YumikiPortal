using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Entity.Administration;

namespace Yumiki.Data.Administration.Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// Get all active users from Database
        /// </summary>
        /// <returns>List of all active users</returns>
        List<TB_User> GetAllUsers();
    }
}
