using System;
using System.Collections.Generic;
using Yumiki.Entity.OnTime;

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
        (List<TB_User>, List<TB_Phase>, List<TB_Project>) GetMetadata();

        /// <summary>
        /// Get only tasks assign to logged user
        /// </summary>
        List<TB_Task> GetMyTasks(Guid userID, int? totalRecords);

        /// <summary>
        /// Get only tasks which created by logged user
        /// </summary>
        List<TB_Task> GetMyCreatedTasks(Guid userID, int? totalRecords);

        /// <summary>
        /// Get all tasks with order from newest to oldest
        /// </summary>
        List<TB_Task> GetTasks(Guid userID, int? totalRecords);

        /// <summary>
        /// Get all unassigned tasks
        /// </summary>
        List<TB_Task> GetUnassignedTasks(Guid userID, int? totalRecords);
    }
}
