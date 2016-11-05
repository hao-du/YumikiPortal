using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Entity.Master;

namespace Yumiki.Business.Master.Interfaces
{
    public interface ISecurityService
    {
        /// <summary>
        /// Log in method.
        /// </summary>
        /// <param name="userName">User Login Name.</param>
        /// <param name="password">Password of user.</param>
        /// <returns></returns>
        TB_User Login(string userName, string password);
    }
}
