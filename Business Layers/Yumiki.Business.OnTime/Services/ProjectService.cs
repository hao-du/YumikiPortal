using System.Collections.Generic;
using Yumiki.Business.Base;
using Yumiki.Business.OnTime.Interfaces;
using Yumiki.Data.OnTime.Interfaces;
using Yumiki.Entity.OnTime;

namespace Yumiki.Business.OnTime.Services
{
    public class ProjectService : BaseService<IProjectRepository>, IProjectService
    {
        /// <summary>
        /// Get all active/Inactive project
        /// </summary>
        public List<TB_Project> GetAllProjects(bool isActive)
        {
            return Repository.GetAllProjects(isActive);
        }
    }
}
