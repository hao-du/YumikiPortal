using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Entity.Administration;

namespace Yumiki.Data.Administration
{
    public class UserRepository : AdministrationRepository
    {
        public List<TB_User> GetAllUsers()
        {
            return Context.TB_User.Where(c => c.IsActive).ToList();
        }
    }
}
