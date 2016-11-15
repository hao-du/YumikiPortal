using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Business.Base;
using Yumiki.Business.Master.Interfaces;
using Yumiki.Common.Dictionary;
using Yumiki.Common.Helper;
using Yumiki.Data.Master.Interfaces;
using Yumiki.Entity.Master;

namespace Yumiki.Business.Master.Services
{
    public class SecurityService : BaseService<ISecurityRepository>, ISecurityService
    {
        public TB_User Login(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new AdvanceException(ExceptionCode.E_EMPTY_VALUE, "User Name cannot be empty.", null);
            }
            if (string.IsNullOrEmpty(password))
            {
                throw new AdvanceException(ExceptionCode.E_EMPTY_VALUE, "Password cannot be empty.", null);
            }

            TB_User user = Repository.Login(userName, SecurityHelper.Encrypt(password, userName));
            if(user == null)
            {
                throw new AdvanceException(ExceptionCode.E_SECURITY_ERROR, "Invalid User Name or Password.", null);
            }
            if (!user.IsActive)
            {
                throw new AdvanceException(ExceptionCode.E_SECURITY_ERROR, "This user is no longer active. Please contact Administrator to re-activate it.", null);
            }

            return user;
        }
    }
}
