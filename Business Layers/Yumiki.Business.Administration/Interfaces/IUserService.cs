﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Entity.Administration;
using Yumiki.Entity.Administration.CustomObjects;

namespace Yumiki.Business.Administration.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Get all active users from Database.
        /// </summary>
        /// <param name="showInactive">Show list of inactive users or active users.</param>
        /// <returns>List of all active user.</returns>
        List<TB_User> GetAllUsers(bool showInactive);

        /// <summary>
        /// Get specific user from Database.
        /// </summary>
        /// <param name="id">User ID - Must be Guid value.</param>
        /// <returns>User object</returns>
        TB_User GetUser(string id);

        /// <summary>
        /// Create/Update a user.
        /// </summary>
        /// <param name="user">If user id is empty, then this is new user. Otherwise, this needs to be updated.</param>
        void SaveUser(TB_User user);

        /// <summary>
        /// Check duplicate user name between the values in UI and database.
        /// </summary>
        /// <param name="userName">User Name will be saved into database.</param>
        /// <param name="excludedUserID">ID which need to be excluded to check.</param>
        /// <returns></returns>
        bool CheckValidUserName(string userName, Guid excludedUserID);

        /// <summary>
        /// Update new password of specific user.
        /// </summary>
        /// <param name="userID">GUID for user needs to be updated new value for password.</param>
        /// <param name="newPassword">New password for user.</param>
        /// <param name="userLogInName">For security purpose.</param>
        void ResetPassword(string userID, string userLogInName, string newPassword);

        /// <summary>
        /// Get history list of specific user.
        /// </summary>
        /// <param name="userID">User Id to retrieve history.</param>
        /// <returns>List of user password changed history.</returns>
        List<TB_PasswordHistory> GetPasswordHistoryList(string userID);

        /// <summary>
        /// Get All contact types which contain the User Addresses.
        /// </summary>
        /// <param name="userID">User need to get address details.</param>
        /// <param name="showInactive">Get active or inactive records.</param>
        /// <returns></returns>
        List<ContactTypeWithUserAddress> GetAllContacts(string userID, bool showInactive);

        /// <summary>
        /// Remote method to Contact Type Repo to get all contact types for binding data purpose.
        /// </summary>
        /// <param name="showInactive">Show list of inactive contact types or active contactTypes</param>
        /// <returns>List of all active contactType</returns>
        List<TB_ContactType> GetAllContactTypes(bool showInactive);

        /// <summary>
        /// Create/Update a user Address
        /// </summary>
        /// <param name="userAddress">If user address id is empty, then this is new user address. Otherwise, this needs to be updated</param>
        void SaveUserAddress(TB_UserAddress userAddress);

        /// <summary>
        /// Get specific user address from Database
        /// </summary>
        /// <param name="id">User Address ID - Must be Guid value</param>
        /// <returns>User Address object</returns>
        TB_UserAddress GetUserAddress(string id);
    }
}
