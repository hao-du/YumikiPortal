using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Data.Master.Interfaces;
using Yumiki.Entity.Master;

namespace Yumiki.Data.Master.Repositories
{
    public class SecurityRepository : MasterRepository, ISecurityRepository
    {
        /// <summary>
        /// Log in method.
        /// </summary>
        /// <param name="userName">User Login Name.</param>
        /// <param name="password">Password of user.</param>
        /// <returns></returns>
        public TB_User Login(string userName, string password)
        {
            return Context.TB_User.Where(c => c.UserLoginName.Equals(userName) && c.CurrentPassword.Equals(password)).SingleOrDefault();
        }
    }
}
