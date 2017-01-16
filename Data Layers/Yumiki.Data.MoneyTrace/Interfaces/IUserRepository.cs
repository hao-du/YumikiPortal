using System;
using System.Collections.Generic;
using Yumiki.Entity.MoneyTrace;

namespace Yumiki.Data.MoneyTrace.Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// Get all users from Trace DB.
        /// </summary>
        /// <param name="showInactive">Get whether inactive users or active users.</param>
        /// <param name="loggedOnUserID">User Id need to be excluded from result.</param>
        /// <returns></returns>
        List<TB_User> GetAllUsers(bool showInactive, Guid loggedOnUserID);
    }
}
