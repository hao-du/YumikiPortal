using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Data.Administration;
using Yumiki.Entity.Administration;

namespace Yumiki.Business.Administration
{
    public class UserService
    {
        public List<TB_User> GetAllUsers()
        {
            UserRepository userRepository = new UserRepository();
            return userRepository.GetAllUsers();
        }
    }
}
