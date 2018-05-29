using System;
using System.Collections.Generic;
using Yumiki.Entity.OnTime;
using Yumiki.Entity.OnTime.ServiceObjects;

namespace Yumiki.Data.OnTime.Interfaces
{
    public interface ITaskRepository
    {
        /// <summary>
        /// Get a task by id
        /// </summary>
        /// <param name="id">Task ID</param>
        TB_Task GetTask(Guid id);

        /// <summary>
        /// Create/Update a Task
        /// </summary>
        /// <param name="task">If Task id is empty, then this is new Task. Otherwise, this needs to be updated</param>
        void SaveTask(TB_Task task);

        /// <summary>
        /// Get Phases, Projects and Users to be metadata (e.g Binding dropdown controls)
        /// </summary>
        /// <returns>Tube Type</returns>
        (List<TB_Phase>, List<TB_Project>, List<TB_User>) GetMetadata(bool excludeUsers = false);

        /// <summary>
        /// Get only tasks assign to logged user
        /// </summary>
        GetTaskResponse GetMyTasks(GetTaskRequest request);

        /// <summary>
        /// Get only tasks which created by logged user
        /// </summary>
        GetTaskResponse GetMyCreatedTasks(GetTaskRequest request);

        /// <summary>
        /// Get all tasks with order from newest to oldest
        /// </summary>
        GetTaskResponse GetTasks(GetTaskRequest request);

        /// <summary>
        /// Get all unassigned tasks
        /// </summary>
        GetTaskResponse GetUnassignedTasks(GetTaskRequest request);
    }
}
