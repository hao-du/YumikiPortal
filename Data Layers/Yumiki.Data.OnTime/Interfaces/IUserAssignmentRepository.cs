using System;
using System.Collections.Generic;
using Yumiki.Entity.OnTime;
using Yumiki.Entity.OnTime.ServiceObjects;

namespace Yumiki.Data.OnTime.Interfaces
{
    public interface IUserAssignmentRepository
    {
        /// <summary>
        /// Get All Active Users
        /// </summary>
        /// <returns></returns>
        List<TB_User> GetUsers();

        /// <summary>
        /// Get a specific user with Projects and Phases Assignment
        /// </summary>
        /// <returns></returns>
        GetUserWithAssignmentResponse GetUserWithAssignments(Guid userID);
    }
}
