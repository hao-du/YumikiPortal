using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Business.Administration.Interfaces;
using Yumiki.Business.Base;
using Yumiki.Data.Administration.Interfaces;
using Yumiki.Data.Administration.Repositories;
using Yumiki.Entity.Administration;

namespace Yumiki.Business.Administration.Services
{
    public class UserService : BaseService, IUserService
    {
        /// <summary>
        /// Get all active users from Database
        /// </summary>
        /// <returns>List of all active users</returns>
        public List<TB_User> GetAllUsers()
        {
            IUserRepository groupRepository = Service.GetInstance<IUserRepository>();
            return groupRepository.GetAllUsers();
        }
    }
}
