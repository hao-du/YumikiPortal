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
    public class UserAssignmentService : BaseService<IUserAssignmentRepository>, IUserAssignmentService
    {
        /// <summary>
        /// Get All Active Users
        /// </summary>
        /// <returns></returns>
        public List<TB_User> GetUsers()
        {
            return Repository.GetUsers();
        }

        /// <summary>
        /// Get a specific user with Projects and Phases Assignment
        /// </summary>
        /// <returns></returns>
        public GetUserWithAssignmentResponse GetUserWithAssignments(string userID)
        {
            if (string.IsNullOrWhiteSpace(userID))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Project ID cannot be empty.", Logger);
            }

            Guid convertedID = Guid.Empty;
            Guid.TryParse(userID, out convertedID);
            if (convertedID == Guid.Empty)
            {
                throw new YumikiException(ExceptionCode.E_WRONG_TYPE, "Project ID must be GUID type.", Logger);
            }

            return Repository.GetUserWithAssignments(convertedID);
        }
    }
}
