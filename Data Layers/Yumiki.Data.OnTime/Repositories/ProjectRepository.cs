using System.Collections.Generic;
using System.Linq;
using Yumiki.Data.OnTime.Interfaces;
using Yumiki.Entity.OnTime;

namespace Yumiki.Data.OnTime.Repositories
{
    public class ProjectRepository : OnTimeRepository, IProjectRepository
    {
        /// <summary>
        /// Get all active/Inactive project
        /// </summary>
        public List<TB_Project> GetAllProjects(bool isActive)
        {
            return Context.TB_Project.Where(c => c.IsActive == isActive).ToList();
        }
    }
}
