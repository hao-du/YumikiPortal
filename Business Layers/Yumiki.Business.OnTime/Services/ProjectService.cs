﻿using System;
using System.Collections.Generic;
using Yumiki.Business.Base;
using Yumiki.Business.OnTime.Interfaces;
using Yumiki.Commons.Exceptions;
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

        /// <summary>
        /// Get a project by id
        /// </summary>
        /// <param name="id">Project ID</param>
        public TB_Project GetProject(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Project ID cannot be empty.", Logger);
            }

            Guid convertedID = Guid.Empty;
            Guid.TryParse(id, out convertedID);
            if (convertedID == Guid.Empty)
            {
                throw new YumikiException(ExceptionCode.E_WRONG_TYPE, "Project ID must be GUID type.", Logger);
            }

            return Repository.GetProject(convertedID);
        }

        /// <summary>
        /// Create/Update a Project
        /// </summary>
        /// <param name="project">If Project id is empty, then this is new Project. Otherwise, this needs to be updated</param>
        public void SaveProject(TB_Project project)
        {
            if (string.IsNullOrWhiteSpace(project.ProjectName))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Project Name is required.", Logger);
            }

            if (string.IsNullOrWhiteSpace(project.Prefix))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Prefix is required.", Logger);
            }

            Repository.SaveProject(project);
        }
    }
}
