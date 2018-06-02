using System;
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

        /// <summary>
        /// Get a project by id
        /// </summary>
        /// <param name="id">Project ID</param>
        public TB_Project GetProject(Guid id)
        {
            return Context.TB_Project.SingleOrDefault(c => c.ID == id);
        }

        /// <summary>
        /// Create/Update a Project
        /// </summary>
        /// <param name="project">If Project id is empty, then this is new Project. Otherwise, this needs to be updated</param>
        public void SaveProject(TB_Project project)
        {
            if (project.ID == Guid.Empty)
            {
                Context.TB_Project.Add(project);
            }
            else
            {
                TB_Project dbProject = Context.TB_Project.Single(c => c.ID == project.ID);
                dbProject.ProjectName = project.ProjectName;
                dbProject.Prefix = project.Prefix;
                dbProject.UserID = project.UserID;
                dbProject.Descriptions = project.Descriptions;
                dbProject.IsActive = project.IsActive;
            }

            Save();
        }
    }
}
