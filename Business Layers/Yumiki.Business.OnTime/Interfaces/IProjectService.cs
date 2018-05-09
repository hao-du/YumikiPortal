using System.Collections.Generic;
using Yumiki.Entity.OnTime;

namespace Yumiki.Business.OnTime.Interfaces
{
    public interface IProjectService
    {
        /// <summary>
        /// Get all active/Inactive project
        /// </summary>
        List<TB_Project> GetAllProjects(bool isActive);
    }
}
