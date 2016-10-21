﻿using System;
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
        /// <param name="showInactive">Show list of inactive users or active users</param>
        /// <returns>List of all active user</returns>
        List<TB_User> GetAllUsers(bool showInactive);

        /// <summary>
        /// Get specific user from Database
        /// </summary>
        /// <param name="id">User ID - Must be Guid value</param>
        /// <returns>User object</returns>
        TB_User GetUser(Guid id);

        /// <summary>
        /// Create/Update a user
        /// </summary>
        /// <param name="user">If user id is empty, then this is new user. Otherwise, this needs to be updated</param>
        void SaveUser(TB_User user);

        /// <summary>
        /// Check duplicate user name between the values in UI and database
        /// </summary>
        /// <param name="userName">User Name will be saved into database</param>
        /// <param name="excludedUserID">ID which need to be excluded to check</param>
        /// <returns></returns>
        bool CheckValidUserName(string userName, Guid excludedUserID);

        /// <summary>
        /// Update new password of specific user.
        /// </summary>
        /// <param name="userID">GUID for user needs to be updated new value for password</param>
        /// <param name="newPassword">New password for user</param>
        void ResetPassword(Guid userID, string newPassword);

        /// <summary>
        /// Get history list of specific user.
        /// </summary>
        /// <param name="userID">User Id to retrieve history</param>
        /// <returns></returns>
        List<TB_PasswordHistory> GetPasswordHistoryList(Guid userID);
    }
}
