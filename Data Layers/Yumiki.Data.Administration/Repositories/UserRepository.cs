using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Data.Administration.Interfaces;
using Yumiki.Entity.Administration;

namespace Yumiki.Data.Administration.Repositories
{
    public class UserRepository : AdministrationRepository, IUserRepository
    {
        /// <summary>
        /// Get all active users from Database
        /// </summary>
        /// <param name="showInactive">Show list of inactive users or active users</param>
        /// <returns>List of all active user</returns>
        public List<TB_User> GetAllUsers(bool showInactive)
        {
            return Context.TB_User.Where(c => c.IsActive == !showInactive).ToList();
        }

        /// <summary>
        /// Get specific user from Database
        /// </summary>
        /// <param name="id">User ID - Must be Guid value</param>
        /// <returns>User object</returns>
        public TB_User GetUser(Guid id)
        {
            return Context.TB_User.Where(c => c.ID == id).SingleOrDefault();
        }

        /// <summary>
        /// Check duplicate user name between the values in UI and database
        /// </summary>
        /// <param name="userName">User Name will be saved into database</param>
        /// <param name="excludedUserID">ID which need to be excluded to check</param>
        /// <returns></returns>
        public bool CheckValidUserName(string userLoginName, Guid excludedUserID)
        {
            int countUserName = Context.TB_User.Where(c => c.ID != excludedUserID
                                                        && c.UserLoginName.ToLower() == userLoginName.ToLower()
                                                        && c.IsActive).Count();
            if (countUserName > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Create/Update a user
        /// </summary>
        /// <param name="user">If user id is empty, then this is new user. Otherwise, this needs to be updated</param>
        public void SaveUser(TB_User user)
        {
            if (user.ID == Guid.Empty)
            {
                Context.TB_User.Add(user);
            }
            else
            {
                TB_User dbUser = Context.TB_User.Where(c => c.ID == user.ID).Single();
                dbUser.UserLoginName = user.UserLoginName;
                dbUser.FirstName = user.FirstName;
                dbUser.LastName = user.LastName;
                dbUser.CurrentPassword = user.CurrentPassword;
                dbUser.Descriptions = user.Descriptions;
                dbUser.IsActive = user.IsActive;
            }

            Save();
        }
    }
}
