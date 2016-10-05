using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Data.Administration.Interfaces;
using Yumiki.Entity.Administration;

namespace Yumiki.Data.Administration.Repositories
{
    public class UserRepository : AdministrationRepository, IUserRepository
    {
        /// <summary>
        /// Get all active users from Database
        /// </summary>
        /// <returns>List of all active users</returns>
        public List<TB_User> GetAllUsers()
        {
            return Context.TB_User.Where(c => c.IsActive).ToList();
        }
    }
}
