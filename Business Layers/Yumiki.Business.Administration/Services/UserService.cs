using System;
using System.Collections.Generic;
using System.Linq;
using Yumiki.Business.Administration.Interfaces;
using Yumiki.Business.Base;
using Yumiki.Commons.Exceptions;
using Yumiki.Commons.Security;
using Yumiki.Data.Administration.Interfaces;
using Yumiki.Entity.Administration;

namespace Yumiki.Business.Administration.Services
{
    public class UserService : BaseService<IUserRepository>, IUserService
    {
        public bool CheckValidUserName(string userName, Guid excludedUserID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get all active users from Database.
        /// </summary>
        /// <param name="showInactive">Show list of inactive users or active users.</param>
        /// <returns>List of all active user.</returns>
        public List<TB_User> GetAllUsers(bool showInactive)
        {
            return Repository.GetAllUsers(showInactive);
        }

        /// <summary>
        /// Get specific user from Database.
        /// </summary>
        /// <param name="id">User ID - Must be Guid value.</param>
        /// <returns>User object.</returns>
        public TB_User GetUser(string id)
        {
            return Repository.GetUser(CheckandConvertUserID(id));
        }

        /// <summary>
        /// Create/Update a user.
        /// </summary>
        /// <param name="user">If user id is empty, then this is new user. Otherwise, this needs to be updated.</param>
        public void SaveUser(TB_User user)
        {
            if (!user.UserLoginName.All(char.IsLetterOrDigit))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "User Login Name only allows alphanumeric.", Logger);
            }

            if (string.IsNullOrWhiteSpace(user.UserLoginName))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "User Name is required.", Logger);
            }

            if (string.IsNullOrWhiteSpace(user.CurrentPassword) && user.ID.Equals(Guid.Empty))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Password is required.", Logger);
            }

            if (!Repository.CheckValidUserName(user.UserLoginName, user.ID))
            {
                throw new YumikiException(ExceptionCode.E_DUPLICATED, "User Login Name was used.", Logger);
            }

            user.CurrentPassword = Crypto.Encrypt(user.CurrentPassword, user.UserLoginName);

            Repository.SaveUser(user);
        }

        /// <summary>
        /// Update new password of specific user.
        /// </summary>
        /// <param name="userID">GUID for user needs to be updated new value for password.</param>
        /// <param name="newPassword">New password for user.</param>
        public void ResetPassword(string userID, string userLogInName, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(newPassword))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "New Password cannot be empty.", Logger);
            }

            Guid convertedID = CheckandConvertUserID(userID);

            newPassword = Crypto.Encrypt(newPassword, userLogInName);

            int maxRecord = 5;
            List<string> passwords = Repository.GetPasswords(maxRecord, convertedID);
            if (passwords.Contains(newPassword))
            {
                throw new YumikiException(ExceptionCode.E_DUPLICATED, string.Format("New password cannot be the same value with {0} previous password.", maxRecord), null);
            }

            Repository.ResetPassword(convertedID, newPassword);
        }

        /// <summary>
        /// Get history list of specific user.
        /// </summary>
        /// <param name="userID">User Id to retrieve history.</param>
        /// <returns>List of user password changed history.</returns>
        public List<TB_PasswordHistory> GetPasswordHistoryList(string userID)
        {
            return Repository.GetPasswordHistoryList(CheckandConvertUserID(userID));
        }

        /// <summary>
        /// Get All contact types which contain the User Addresses.
        /// </summary>
        /// <param name="userID">User need to get address details.</param>
        /// <param name="showInactive">Get active or inactive records.</param>
        /// <returns></returns>
        public List<TB_ContactType> GetAllContacts(string userID, bool showInactive)
        {
            Guid convertedID = CheckandConvertUserID(userID);

            return Repository.GetAllContacts(convertedID, showInactive);
        }

        /// <summary>
        /// Remote method to Contact Type Repo to get all contact types for binding data purpose.
        /// </summary>
        /// <param name="showInactive">Show list of inactive contact types or active contactTypes</param>
        /// <returns>List of all active contactType</returns>
        public List<TB_ContactType> GetAllContactTypes(bool showInactive)
        {
            return Repository.GetAllContactTypes(showInactive);
        }

        /// <summary>
        /// Get specific user address from Database
        /// </summary>
        /// <param name="id">User Address ID - Must be Guid value</param>
        /// <returns>User Address object</returns>
        public TB_UserAddress GetUserAddress(string id)
        {
            Guid convertedID = CheckandConvertUserID(id);

            return Repository.GetUserAddress(convertedID);
        }

        /// <summary>
        /// Create/Update a user Address
        /// </summary>
        /// <param name="userAddress">If user address id is empty, then this is new user address. Otherwise, this needs to be updated</param>
        public void SaveUserAddress(TB_UserAddress userAddress)
        {
            if (string.IsNullOrWhiteSpace(userAddress.UserAddress))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Contact Details is required.", Logger);
            }

            if (userAddress.UserContactTypeID.Equals(Guid.Empty))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Contact Type is required.", Logger);
            }

            Repository.SaveUserAddress(userAddress);
        }

        /// <summary>
        /// Check validation for the userID before convert it to GUID and then convert it.
        /// </summary>
        /// <param name="userID">User ID need to check and convert.</param>
        /// <returns>A converted GUID User ID.</returns>
        private Guid CheckandConvertUserID(string userID)
        {
            if (string.IsNullOrWhiteSpace(userID))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "User ID cannot be empty.", Logger);
            }

            Guid convertedID = Guid.Empty;
            Guid.TryParse(userID, out convertedID);
            if (convertedID == Guid.Empty)
            {
                throw new YumikiException(ExceptionCode.E_WRONG_TYPE, "User ID must be GUID type.", Logger);
            }

            return convertedID;
        }
    }
}
