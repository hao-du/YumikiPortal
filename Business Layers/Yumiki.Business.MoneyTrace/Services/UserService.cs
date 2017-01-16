using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yumiki.Business.Base;
using Yumiki.Business.MoneyTrace.Interfaces;
using Yumiki.Commons.Dictionaries;
using Yumiki.Commons.Exceptions;
using Yumiki.Data.MoneyTrace.Interfaces;
using Yumiki.Entity.MoneyTrace;

namespace Yumiki.Business.MoneyTrace.Services
{
    public class UserService : BaseService<IUserRepository>, IUserService
    {
        /// <summary>
        /// Get all users from Trace DB.
        /// </summary>
        /// <param name="showInactive">Get whether inactive users or active users.</param>
        /// <param name="loggedOnUserID">User Id need to be excluded from result.</param>
        /// <returns></returns>
        public List<TB_User> GetAllUsers(bool showInactive, Guid loggedOnUserID)
        {
            return Repository.GetAllUsers(showInactive, loggedOnUserID);
        }
    }
}
