using System;
using System.Collections.Generic;
using Yumiki.Business.Base;
using Yumiki.Business.OnTime.Interfaces;
using Yumiki.Commons.Exceptions;
using Yumiki.Data.OnTime.Interfaces;
using Yumiki.Entity.OnTime;

namespace Yumiki.Business.OnTime.Services
{
    public class TaskService : BaseService<ITaskRepository>, ITaskService
    {
        /// <summary>
        /// Get all active/Inactive task
        /// </summary>
        public List<TB_Task> GetAllTasks()
        {
            return Repository.GetAllTasks();
        }

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
        /// Get Phases, Projects and Users to be metadata (e.g Binding dropdown controls)
        /// </summary>
        /// <returns>Tube Type</returns>
        (List<TB_User>, List<TB_Phase>, List<TB_Project>) ITaskService.GetMetadata()
        {
            return Repository.GetMetadata();
        }
    }
}
