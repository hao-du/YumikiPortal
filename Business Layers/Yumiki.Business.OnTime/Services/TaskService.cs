using System;
using System.Collections.Generic;
using Yumiki.Business.Base;
using Yumiki.Business.OnTime.Interfaces;
using Yumiki.Commons.Exceptions;
using Yumiki.Data.OnTime.Interfaces;
using Yumiki.Entity.OnTime;
using Yumiki.Entity.OnTime.ServiceObjects;

namespace Yumiki.Business.OnTime.Services
{
    public class TaskService : BaseService<ITaskRepository>, ITaskService
    {
        /// <summary>
        /// Get a task by id
        /// </summary>
        /// <param name="id">Task ID</param>
        public TB_Task GetTask(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Task ID cannot be empty.", Logger);
            }

            Guid convertedID = Guid.Empty;
            Guid.TryParse(id, out convertedID);
            if (convertedID == Guid.Empty)
            {
                throw new YumikiException(ExceptionCode.E_WRONG_TYPE, "Task ID must be GUID type.", Logger);
            }

            return Repository.GetTask(convertedID);
        }

        /// <summary>
        /// Create/Update a Task
        /// </summary>
        /// <param name="task">If Task id is empty, then this is new Task. Otherwise, this needs to be updated</param>
        public void SaveTask(TB_Task task)
        {
            if (string.IsNullOrWhiteSpace(task.TaskName))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Task Name is required.", Logger);
            }

            if (task.StartDate > task.EndDate)
            {
                throw new YumikiException(ExceptionCode.E_INVALID_VALUE, "Start Date must be smaller than End Date.", Logger);
            }

            Repository.SaveTask(task);
        }

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
        public (IEnumerable<TB_Task>, IEnumerable<TB_Task>, IEnumerable<TB_Task>, IEnumerable<TB_Task>) GetAllTasksWithTypes(GetTaskRequest request)
        {
            return (
                    GetTasks(request, EN_TaskType.E_MY_TASK).Records,
                    GetTasks(request, EN_TaskType.E_MY_CREATED_TASK).Records,
                    GetTasks(request, EN_TaskType.E_LASTEST_TASK).Records,
                    GetTasks(request, EN_TaskType.E_UNASSIGNED_TASK).Records
                    );
        }

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
        public GetTaskResponse GetTasks(GetTaskRequest request, EN_TaskType taskType)
        {
            switch (taskType)
            {
                case EN_TaskType.E_MY_TASK:
                    return Repository.GetMyTasks(request);
                case EN_TaskType.E_MY_CREATED_TASK:
                    return Repository.GetMyCreatedTasks(request);
                case EN_TaskType.E_LASTEST_TASK:
                    return Repository.GetTasks(request);
                case EN_TaskType.E_UNASSIGNED_TASK:
                    return Repository.GetUnassignedTasks(request);
            }

            return new GetTaskResponse();
        }

        /// <summary>
        /// Get Phases, Projects and Users to be metadata (e.g Binding dropdown controls)
        /// </summary>
        /// <returns>Tube Type</returns>
        (List<TB_User>, List<TB_Phase>, List<TB_Project>) ITaskService.GetMetadata(bool getUser = true)
        {
            return Repository.GetMetadata(getUser);
        }
    }
}
