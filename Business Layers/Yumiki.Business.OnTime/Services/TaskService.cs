﻿using System;
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
        public (List<TB_Task>, List<TB_Task>, List<TB_Task>, List<TB_Task>) GetAllTasksWithTypes(string userID, int? totalRecords)
        {
            if (string.IsNullOrWhiteSpace(userID))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "User ID cannot be empty.", Logger);
            }

            Guid convertedUserID = Guid.Empty;
            Guid.TryParse(userID, out convertedUserID);
            if (convertedUserID == Guid.Empty)
            {
                throw new YumikiException(ExceptionCode.E_WRONG_TYPE, "User ID must be GUID type.", Logger);
            }

            return (
                    GetTasks(userID, totalRecords, EN_TaskType.E_MY_TASK),
                    GetTasks(userID, totalRecords, EN_TaskType.E_MY_CREATED_TASK),
                    GetTasks(userID, totalRecords, EN_TaskType.E_LASTEST_TASK),
                    GetTasks(userID, totalRecords, EN_TaskType.E_UNASSIGNED_TASK)
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
        public List<TB_Task> GetTasks(string userID, int? totalRecords, EN_TaskType taskType)
        {
            if (string.IsNullOrWhiteSpace(userID))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "User ID cannot be empty.", Logger);
            }

            Guid convertedUserID = Guid.Empty;
            Guid.TryParse(userID, out convertedUserID);
            if (convertedUserID == Guid.Empty)
            {
                throw new YumikiException(ExceptionCode.E_WRONG_TYPE, "User ID must be GUID type.", Logger);
            }

            switch (taskType)
            {
                case EN_TaskType.E_MY_TASK:
                    return Repository.GetMyTasks(convertedUserID, totalRecords);
                case EN_TaskType.E_MY_CREATED_TASK:
                    return Repository.GetMyCreatedTasks(convertedUserID, totalRecords);
                case EN_TaskType.E_LASTEST_TASK:
                    return Repository.GetTasks(convertedUserID, totalRecords);
                case EN_TaskType.E_UNASSIGNED_TASK:
                    return Repository.GetUnassignedTasks(convertedUserID, totalRecords);
            }

            return new List<TB_Task>();
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