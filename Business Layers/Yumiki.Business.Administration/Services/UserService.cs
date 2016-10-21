using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Yumiki.Business.Administration.Interfaces;
using Yumiki.Business.Base;
using Yumiki.Common.Helper;
using Yumiki.Data.Administration.Interfaces;
using Yumiki.Data.Administration.Repositories;
using Yumiki.Entity.Administration;

namespace Yumiki.Business.Administration.Services
{
    public class UserService : BaseService, IUserService
    {
        private IUserRepository repository;
        private IUserRepository Repository
        {
            get
            {
                if (repository == null)
                {
                    repository = Service.GetInstance<IUserRepository>();
                }
                return repository;
            }
        }

        public bool CheckValidUserName(string userName, Guid excludedUserID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get all active users from Database
        /// </summary>
        /// <param name="showInactive">Show list of inactive users or active users</param>
        /// <returns>List of all active user</returns>
        public List<TB_User> GetAllUsers(bool showInactive)
        {
            return Repository.GetAllUsers(showInactive);
        }

        /// <summary>
        /// Get specific user from Database
        /// </summary>
        /// <param name="id">User ID - Must be Guid value</param>
        /// <returns>User object</returns>
        public TB_User GetUser(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new AdvanceException(ExceptionCode.E_EMPTY_VALUE, "User ID cannot be empty.", null);
            }

            Guid userID = Guid.Empty;
            Guid.TryParse(id, out userID);
            if (userID == Guid.Empty)
            {
                throw new AdvanceException(ExceptionCode.E_WRONG_TYPE, "User ID must be GUID type.", null);
            }

            return Repository.GetUser(userID);
        }

        /// <summary>
        /// Create/Update a user
        /// </summary>
        /// <param name="user">If user id is empty, then this is new user. Otherwise, this needs to be updated</param>
        public void SaveUser(TB_User user)
        {
            if (!user.UserLoginName.All(char.IsLetterOrDigit))
            {
                throw new AdvanceException(ExceptionCode.E_EMPTY_VALUE, "User Login Name only allows alphanumeric.", null);
            }

            if (string.IsNullOrEmpty(user.UserLoginName))
            {
                throw new AdvanceException(ExceptionCode.E_EMPTY_VALUE, "User Name is required.", null);
            }

            if (string.IsNullOrEmpty(user.CurrentPassword) && user.ID.Equals(Guid.Empty))
            {
                throw new AdvanceException(ExceptionCode.E_EMPTY_VALUE, "Password is required.", null);
            }

            if (!Repository.CheckValidUserName(user.UserLoginName, user.ID))
            {
                throw new AdvanceException(ExceptionCode.E_DUPLICATED, "User Login Name was used.", null);
            }

            user.CurrentPassword = SecurityHelper.Encrypt(user.CurrentPassword, user.UserLoginName);

            Repository.SaveUser(user);
        }

        /// <summary>
        /// Update new password of specific user.
        /// </summary>
        /// <param name="userID">GUID for user needs to be updated new value for password</param>
        /// <param name="newPassword">New password for user</param>
        public void ResetPassword(string userID, string userLogInName, string newPassword)
        {
            if (string.IsNullOrEmpty(newPassword))
            {
                throw new AdvanceException(ExceptionCode.E_EMPTY_VALUE, "New Password cannot be empty.", null);
            }

            if (string.IsNullOrEmpty(userID))
            {
                throw new AdvanceException(ExceptionCode.E_EMPTY_VALUE, "User ID cannot be empty.", null);
            }

            Guid convertedID = Guid.Empty;
            Guid.TryParse(userID, out convertedID);
            if (convertedID == Guid.Empty)
            {
                throw new AdvanceException(ExceptionCode.E_WRONG_TYPE, "User ID must be GUID type.", null);
            }

            newPassword = SecurityHelper.Encrypt(newPassword, userLogInName);

            int maxRecord = 5;
            List<string> passwords = Repository.GetPasswords(maxRecord, convertedID);
            if (passwords.Contains(newPassword))
            {
                throw new AdvanceException(ExceptionCode.E_DUPLICATED, string.Format("New password cannot be the same value with {0} previous password.", maxRecord), null);
            }

            Repository.ResetPassword(convertedID, newPassword);
        }

        /// <summary>
        /// Get history list of specific user.
        /// </summary>
        /// <param name="userID">User Id to retrieve history</param>
        /// <returns>List of user password changed history</returns>
        public List<TB_PasswordHistory> GetPasswordHistoryList(string userID)
        {
            if (string.IsNullOrEmpty(userID))
            {
                throw new AdvanceException(ExceptionCode.E_EMPTY_VALUE, "User ID cannot be empty.", null);
            }

            Guid convertedID = Guid.Empty;
            Guid.TryParse(userID, out convertedID);
            if (convertedID == Guid.Empty)
            {
                throw new AdvanceException(ExceptionCode.E_WRONG_TYPE, "User ID must be GUID type.", null);
            }

            return Repository.GetPasswordHistoryList(convertedID);
        }
    }
}
