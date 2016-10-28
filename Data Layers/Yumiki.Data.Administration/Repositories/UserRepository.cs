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
                user.TB_PasswordHistory.Add(new TB_PasswordHistory { Password = user.CurrentPassword, Descriptions = string.Format("Initial Password: \"{0}\"", user.CurrentPassword) });
                Context.TB_User.Add(user);
            }
            else
            {
                TB_User dbUser = Context.TB_User.Where(c => c.ID == user.ID).Single();
                dbUser.UserLoginName = user.UserLoginName;
                dbUser.FirstName = user.FirstName;
                dbUser.LastName = user.LastName;
                dbUser.Descriptions = user.Descriptions;
                dbUser.IsActive = user.IsActive;
            }

            Save();
        }

        /// <summary>
        /// Update new password of specific user.
        /// </summary>
        /// <param name="userID">GUID for user needs to be updated new value for password</param>
        /// <param name="newPassword">New password for user</param>
        public void ResetPassword(Guid userID, string newPassword)
        {
            TB_User dbUser = Context.TB_User.Where(c => c.ID == userID).Single();
            dbUser.TB_PasswordHistory.Add(new TB_PasswordHistory { Password = newPassword, Descriptions = string.Format("Password has changed from \"{0}\" to \"{1}\"", dbUser.CurrentPassword, newPassword) });
            dbUser.CurrentPassword = newPassword;

            Save();
        }

        /// <summary>
        /// Get the last updated records of password changing history.
        /// </summary>
        /// <param name="noOfRecords">Max of records will be retrieved</param>
        /// <param name="userID">GUID for user needs to get the passwords</param>
        /// <returns></returns>
        public List<string> GetPasswords(int noOfRecords, Guid userID)
        {
            return Context.TB_PasswordHistory.Where(c => c.UserID == userID).OrderByDescending(c => c.CreateDate).Take(noOfRecords).Select(c => c.Password).ToList();
        }

        /// <summary>
        /// Get history list of specific user.
        /// </summary>
        /// <param name="userID">User Id to retrieve history</param>
        /// <returns></returns>
        public List<TB_PasswordHistory> GetPasswordHistoryList(Guid userID)
        {
            return Context.TB_PasswordHistory.Where(c => c.UserID == userID).ToList();
        }

        /// <summary>
        /// Get All contact types which contain the User Addresses.
        /// </summary>
        /// <param name="userID">User need to get address details.</param>
        /// <param name="showInactive">Get active or inactive records.</param>
        /// <returns></returns>
        public List<TB_ContactType> GetAllContacts(Guid userID, bool showInactive)
        {
            return context.TB_ContactType.Include(TB_UserAddress.FieldName.TB_UserAddress)
                            .Where(c => c.TB_UserAddress.Any(e => e.UserID == userID && e.IsActive != showInactive))
                            .ToList();
        }

        /// <summary>
        /// Remote method to Contact Type Repo to get all contact types for binding data purpose.
        /// </summary>
        /// <param name="showInactive">Show list of inactive contact types or active contactTypes</param>
        /// <returns>List of all active contactType</returns>
        public List<TB_ContactType> GetAllContactTypes(bool showInactive)
        {
            ContactTypeRepository contactTypeRepository = new ContactTypeRepository();
            contactTypeRepository.AssignContext(Context);

            return contactTypeRepository.GetAllContactTypes(showInactive);
        }

        /// <summary>
        /// Get specific user address from Database
        /// </summary>
        /// <param name="id">User Address ID - Must be Guid value</param>
        /// <returns>User Address object</returns>
        public TB_UserAddress GetUserAddress(Guid id)
        {
            return Context.TB_UserAddress.Where(c => c.ID == id).SingleOrDefault();
        }

        /// <summary>
        /// Create/Update a user Address
        /// </summary>
        /// <param name="userAddress">If user address id is empty, then this is new user address. Otherwise, this needs to be updated</param>
        public void SaveUserAddress(TB_UserAddress userAddress)
        {
            if (userAddress.ID == Guid.Empty)
            {
                Context.TB_UserAddress.Add(userAddress);
            }
            else
            {
                TB_UserAddress dbUserAddress = Context.TB_UserAddress.Where(c => c.ID == userAddress.ID).Single();
                dbUserAddress.UserAddress = userAddress.UserAddress;
                dbUserAddress.UserContactTypeID = userAddress.UserContactTypeID;
                dbUserAddress.IsPrimary = userAddress.IsPrimary;
                dbUserAddress.Descriptions = userAddress.Descriptions;
                dbUserAddress.IsActive = userAddress.IsActive;
            }

            if(userAddress.IsPrimary)
            {
                //Update Primary flag of all contacts of user which has the same ContactType to false.
                //Only one contact is primary for each contact type.
                Context.TB_UserAddress.Where(c => c.ID != userAddress.ID 
                                                && c.IsPrimary
                                                && c.UserID == userAddress.UserID
                                                && c.UserContactTypeID == userAddress.UserContactTypeID)
                                        .ToList()
                                        .ForEach(c => c.IsPrimary = false);
            }

            Save();
        }
    }
}
