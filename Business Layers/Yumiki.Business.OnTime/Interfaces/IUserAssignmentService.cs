using System.Collections.Generic;
using Yumiki.Entity.OnTime;
using Yumiki.Entity.OnTime.ServiceObjects;

namespace Yumiki.Business.OnTime.Interfaces
{
    public interface IUserAssignmentService
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
        GetUserWithAssignmentResponse GetUserWithAssignments(string userID);

        /// <summary>
        /// Save User Assignment for Project
        /// </summary>
        void SaveProjectAssignment(string userID, string projectID, bool isAssigned);
    }
}
