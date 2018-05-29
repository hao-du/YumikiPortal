using System;
using System.Collections.Generic;
using System.Linq;
using Yumiki.Data.OnTime.Interfaces;
using Yumiki.Entity.OnTime;
using Yumiki.Entity.OnTime.ServiceObjects;

namespace Yumiki.Data.OnTime.Repositories
{
    public class TaskRepository : OnTimeRepository, ITaskRepository
    {
        /// <summary>
        /// Get only tasks assign to logged user
        /// </summary>
        public GetTaskResponse GetMyTasks(GetTaskRequest request)
        {
            IQueryable<TB_Task> taskQuery = Context.TB_Task
                                                .Include("Project").Include("Phase").Include("AssignedUser")
                                                .Where(c => c.AssignedUserID == request.UserID
                                                        && c.Phase.PhaseAssignments.Any(d => d.UserID == request.UserID)
                                                        && c.Project.ProjectAssignments.Any(d => d.UserID == request.UserID)
                                                        && c.PhaseID == request.PhaseID
                                                        && (request.ProjectID == Guid.Empty || c.ProjectID == request.ProjectID));

            return GetTasks(taskQuery, request);
        }

        /// <summary>
        /// Get only tasks which created by logged user
        /// </summary>
        public GetTaskResponse GetMyCreatedTasks(GetTaskRequest request)
        {
            IQueryable<TB_Task> taskQuery = Context.TB_Task
                                            .Include("Project").Include("Phase").Include("AssignedUser")
                                            .Where(c => c.UserID == request.UserID
                                                    && c.Phase.PhaseAssignments.Any(d => d.UserID == request.UserID)
                                                    && c.Project.ProjectAssignments.Any(d => d.UserID == request.UserID)
                                                    && c.PhaseID == request.PhaseID
                                                    && (request.ProjectID == Guid.Empty || c.ProjectID == request.ProjectID));

            return GetTasks(taskQuery, request);
        }

        /// <summary>
        /// Get all tasks with order from newest to oldest
        /// </summary>
        public GetTaskResponse GetTasks(GetTaskRequest request)
        {
            IQueryable<TB_Task> taskQuery = Context.TB_Task
                                            .Include("Project").Include("Phase").Include("AssignedUser")
                                            .Where(c => c.Phase.PhaseAssignments.Any(d => d.UserID == request.UserID)
                                                    && c.Project.ProjectAssignments.Any(d => d.UserID == request.UserID)
                                                    && c.PhaseID == request.PhaseID
                                                    && (request.ProjectID == Guid.Empty || c.ProjectID == request.ProjectID));

            return GetTasks(taskQuery, request);
        }

        /// <summary>
        /// Get all unassigned tasks
        /// </summary>
        public GetTaskResponse GetUnassignedTasks(GetTaskRequest request)
        {
            IQueryable<TB_Task> taskQuery = Context.TB_Task
                                            .Include("Project").Include("Phase").Include("AssignedUser")
                                            .Where(c => c.Phase.PhaseAssignments.Any(d => d.UserID == request.UserID)
                                                    && c.Project.ProjectAssignments.Any(d => d.UserID == request.UserID)
                                                    && c.AssignedUserID == null
                                                    && c.PhaseID == request.PhaseID
                                                    && (request.ProjectID == Guid.Empty || c.ProjectID == request.ProjectID));

            return GetTasks(taskQuery, request);
        }

        private GetTaskResponse GetTasks(IQueryable<TB_Task> taskQuery, GetTaskRequest request)
        {
            int count = taskQuery.Count();
            if (request.EnablePaging)
            {
                taskQuery = taskQuery.OrderByDescending(c => c.LastUpdateDate).Skip((request.CurrentPage - 1) * request.ItemsPerPage).Take(request.ItemsPerPage);
            }

            return new GetTaskResponse
            {
                Records = taskQuery.ToList(),
                CurrentPage = request.CurrentPage,
                ItemsPerPage = request.ItemsPerPage,
                TotalItems = count
            };
        }

        /// <summary>
        /// Get a task by id
        /// </summary>
        /// <param name="id">Task ID</param>
        public TB_Task GetTask(Guid id)
        {
            return Context.TB_Task.SingleOrDefault(c => c.ID == id);
        }

        /// <summary>
        /// Create/Update a Task
        /// </summary>
        /// <param name="task">If Task id is empty, then this is new Task. Otherwise, this needs to be updated</param>
        public void SaveTask(TB_Task task)
        {
            if (task.ID == Guid.Empty)
            {
                Context.TB_Task.Add(task);
            }
            else
            {
                TB_Task dbTask = Context.TB_Task.Single(c => c.ID == task.ID);
                dbTask.TaskName = task.TaskName;
                dbTask.ProjectID = task.ProjectID;
                dbTask.PhaseID = task.PhaseID;
                dbTask.StartDate = task.StartDate;
                dbTask.EndDate = task.EndDate;
                dbTask.Priority = task.Priority;
                dbTask.Status = task.Status;
                dbTask.TaskDescriptions = task.TaskDescriptions;
                dbTask.AssignedUserID = task.AssignedUserID;
                dbTask.Descriptions = task.Descriptions;
                dbTask.IsActive = task.IsActive;
            }

            Save();
        }

        /// <summary>
        /// Get Phases, Projects and Users to be metadata (e.g Binding dropdown controls)
        /// </summary>
        /// <returns>Tube Type</returns>
        public (List<TB_Phase>, List<TB_Project>, List<TB_User>) GetMetadata(bool excludeUsers = false)
        {
            List<TB_Phase> phases = this.GetAlternativeRepository<IPhaseRepository>().GetAllPhases(true);

            List<TB_Project> projects = this.GetAlternativeRepository<IProjectRepository>().GetAllProjects(true);

            if (excludeUsers)
            {
                return (phases, projects, null);
            }

            List<TB_User> users = this.GetAlternativeRepository<IUserAssignmentRepository>().GetUsers();

            return (phases, projects, users);
        }
    }
}
