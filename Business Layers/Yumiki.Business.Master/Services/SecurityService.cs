using Yumiki.Business.Base;
using Yumiki.Business.Master.Interfaces;
using Yumiki.Commons.Exceptions;
using Yumiki.Commons.Security;
using Yumiki.Data.Master.Interfaces;
using Yumiki.Entity.Master;

namespace Yumiki.Business.Master.Services
{
    public class SecurityService : BaseService<ISecurityRepository>, ISecurityService
    {
        /// <summary>
        /// Log in method.
        /// </summary>
        /// <param name="userName">User Login Name.</param>
        /// <param name="password">Password of user.</param>
        /// <returns></returns>
        public TB_User Login(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "User Name cannot be empty.", Logger);
            }
            if (string.IsNullOrEmpty(password))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Password cannot be empty.", Logger);
            }

            TB_User user = Repository.Login(userName, Crypto.Encrypt(password, userName));
            if(user == null)
            {
                throw new YumikiException(ExceptionCode.E_SECURITY_ERROR, "Invalid User Name or Password.", Logger);
            }
            if (!user.IsActive)
            {
                throw new YumikiException(ExceptionCode.E_SECURITY_ERROR, "This user is no longer active. Please contact Administrator to re-activate it.", Logger);
            }

            return user;
        }

        /// <summary>
        /// This is only used for mock up
        /// </summary>
        /// <param name="userName">User Login Name.</param>
        public TB_User Login(string userName)
        {
            return Repository.Login(userName);
        }
    }
}
