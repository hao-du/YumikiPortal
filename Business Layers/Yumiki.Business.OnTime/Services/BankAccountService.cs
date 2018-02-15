using Yumiki.Business.Base;
using Yumiki.Business.OnTime.Interfaces;
using Yumiki.Data.OnTime.Interfaces;

namespace Yumiki.Business.OnTime.Services
{
    public class ProjectService : BaseService<IProjectRepository>, IProjectService
    {
        /// <summary>
        /// Get all active/Inactive project
        /// </summary>
        public void GetAllProjects(bool isActive)
        {
            Repository.GetAllProjects(isActive);
        }
    }
}
