using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            if (string.IsNullOrEmpty(user.UserLoginName))
            {
                throw new AdvanceException(ExceptionCode.E_EMPTY_VALUE, "User Name is required.", null);
            }

            if (string.IsNullOrEmpty(user.CurrentPassword))
            {
                throw new AdvanceException(ExceptionCode.E_EMPTY_VALUE, "Password is required.", null);
            }

            if (!Repository.CheckValidUserName(user.UserLoginName, user.ID))
            {
                throw new AdvanceException(ExceptionCode.E_DUPLICATED, "User Login Name was used.", null);
            }

            Repository.SaveUser(user);
        }
    }
}
