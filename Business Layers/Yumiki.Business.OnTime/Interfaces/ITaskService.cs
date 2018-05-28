using System.Collections.Generic;
using Yumiki.Entity.OnTime;
using Yumiki.Entity.OnTime.ServiceObjects;

namespace Yumiki.Business.OnTime.Interfaces
{
    public interface ITaskService
    {
        /// <summary>
        /// Get a task by id
        /// </summary>
        /// <param name="id">Task ID</param>
        TB_Task GetTask(string id);

        /// <summary>
        /// Create/Update a Task
        /// </summary>
        /// <param name="task">If Task id is empty, then this is new Task. Otherwise, this needs to be updated</param>
        void SaveTask(TB_Task task);

        /// <summary>
        /// Get Phases, Projects and Users to be metadata (e.g Binding dropdown controls)
        /// </summary>
        /// <returns>Tube Type</returns>
        (List<TB_User>, List<TB_Phase>, List<TB_Project>) GetMetadata(bool getUser = true);

        /// <summary>
        /// Get all tasks with all task types which loggedin User has permission to access
        /// </summary>
        /// <returns>
        /// Tube type with order:
        /// 1. My Tasks
        /// 2. My Created Tasks
        /// 3. Latest updated Tasks
        /// 4. Unassigned Tasks
        /// </returns>
        (IEnumerable<TB_Task>, IEnumerable<TB_Task>, IEnumerable<TB_Task>, IEnumerable<TB_Task>) GetAllTasksWithTypes(GetTaskRequest request);

        /// <summary>
        /// Get tasks will specific task type
        /// </summary>
        /// <param name="taskType">
        /// My Tasks
        /// My Created Tasks
        /// Latest updated Tasks
        /// Unassigned Tasks
        /// </param>
        /// <returns></returns>
        GetTaskResponse GetTasks(GetTaskRequest request, EN_TaskType taskType);
    }
}
