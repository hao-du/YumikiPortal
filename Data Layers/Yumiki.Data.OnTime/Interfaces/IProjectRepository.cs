using System.Collections.Generic;
using Yumiki.Entity.OnTime;

namespace Yumiki.Data.OnTime.Interfaces
{
    public interface IProjectRepository
    {
        /// <summary>
        /// Get all active/Inactive project
        /// </summary>
        List<TB_Project> GetAllProjects(bool isActive);
    }
}
