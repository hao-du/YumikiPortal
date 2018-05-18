using System;
using System.Collections.Generic;
using System.Linq;
using Yumiki.Data.OnTime.Interfaces;
using Yumiki.Entity.OnTime;

namespace Yumiki.Data.OnTime.Repositories
{
    public class TaskRepository : OnTimeRepository, ITaskRepository
    {
        /// <summary>
        /// Get only tasks assign to logged user
        /// </summary>
        public List<TB_Task> GetMyTasks(Guid userID, int? totalRecords)
        {
            IQueryable<TB_Task> taskQuery = Context.TB_Task
                                                .Include("Project").Include("Phase").Include("AssignedUser")
                                                .Where(c => c.AssignedUserID == userID
                                                        && c.Phase.PhaseAssignments.Any(d => d.UserID == userID)
                                                        && c.Project.ProjectAssignments.Any(d => d.UserID == userID))
                                                .OrderByDescending(c => c.LastUpdateDate);

            return GetTasks(taskQuery, totalRecords);
        }

        /// <summary>
        /// Get only tasks which created by logged user
        /// </summary>
        public List<TB_Task> GetMyCreatedTasks(Guid userID, int? totalRecords)
        {
            IQueryable<TB_Task> taskQuery = Context.TB_Task
                                            .Include("Project").Include("Phase").Include("AssignedUser")
                                            .Where(c => c.UserID == userID
                                                    && c.Phase.PhaseAssignments.Any(d => d.UserID == userID)
                                                    && c.Project.ProjectAssignments.Any(d => d.UserID == userID))
                                            .OrderByDescending(c => c.LastUpdateDate);

            return GetTasks(taskQuery, totalRecords);
        }

        /// <summary>
        /// Get all tasks with order from newest to oldest
        /// </summary>
        public List<TB_Task> GetTasks(Guid userID, int? totalRecords)
        {
            IQueryable<TB_Task> taskQuery = Context.TB_Task
                                            .Include("Project").Include("Phase").Include("AssignedUser")
                                            .Where(c => c.Phase.PhaseAssignments.Any(d => d.UserID == userID)
                                                    && c.Project.ProjectAssignments.Any(d => d.UserID == userID))
                                            .OrderByDescending(c => c.LastUpdateDate);

            return GetTasks(taskQuery, totalRecords);
        }

        /// <summary>
        /// Get all unassigned tasks
        /// </summary>
        public List<TB_Task> GetUnassignedTasks(Guid userID, int? totalRecords)
        {
            IQueryable<TB_Task> taskQuery = Context.TB_Task
                                            .Include("Project").Include("Phase").Include("AssignedUser")
                                            .Where(c => c.Phase.PhaseAssignments.Any(d => d.UserID == userID)
                                                    && c.Project.ProjectAssignments.Any(d => d.UserID == userID)
                                                    && c.AssignedUserID == null)
                                            .OrderByDescending(c => c.LastUpdateDate);

            return GetTasks(taskQuery, totalRecords);
        }

        private List<TB_Task> GetTasks(IQueryable<TB_Task> taskQuery, int? totalRecords)
        {
            if (totalRecords.HasValue)
            {
                taskQuery = taskQuery.Take(totalRecords.Value);
            }

            return taskQuery.ToList();
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
        public (List<TB_User>, List<TB_Phase>, List<TB_Project>) GetMetadata()
        {
            List<TB_User> users = this.GetAlternativeRepository<IUserAssignmentRepository>().GetUsers();

            List<TB_Phase> phases = this.GetAlternativeRepository<IPhaseRepository>().GetAllPhases(true);

            List<TB_Project> projects = this.GetAlternativeRepository<IProjectRepository>().GetAllProjects(true);

            return (users, phases, projects);
        }
    }
}
