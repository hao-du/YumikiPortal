using System;
using System.Collections.Generic;
using System.Linq;
using Yumiki.Commons.Dictionaries;
using Yumiki.Commons.Helpers;
using Yumiki.Data.MoneyTrace.Interfaces;
using Yumiki.Entity.MoneyTrace;

namespace Yumiki.Data.MoneyTrace.Repositories
{
    public class UserRepository : MoneyTraceRepository, IUserRepository
    {
        /// <summary>
        /// Get all users from Trace DB.
        /// </summary>
        /// <param name="showInactive">Get whether inactive users or active users.</param>
        /// <param name="loggedOnUserID">User Id need to be excluded from result.</param>
        /// <returns></returns>
        public List<TB_User> GetAllUsers(bool showInactive, Guid loggedOnUserID)
        {
            return Context.TB_User.Where(c => c.IsActive == !showInactive && c.ID != loggedOnUserID).OrderBy(c=>c.LastName).ThenBy(c=>c.FirstName).ToList();
        }
    }
}
