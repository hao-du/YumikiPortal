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

        /// <summary>
        /// Get a project by id
        /// </summary>
        /// <param name="id">Project ID</param>
        TB_Project GetProject(string id);

        /// <summary>
        /// Create/Update a Project
        /// </summary>
        /// <param name="project">If Project id is empty, then this is new Project. Otherwise, this needs to be updated</param>
        void SaveProject(TB_Project project);
    }
}
