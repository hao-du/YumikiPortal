using System;
using System.Collections.Generic;
using Yumiki.Data.Base;
using Yumiki.Entity.OnTime;
using Yumiki.Entity.OnTime.ServiceObjects;

namespace Yumiki.Data.OnTime.Interfaces
{
    public interface IUserAssignmentRepository : IShareableRepository<OnTimeModel>
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

        /// <summary>
        /// Save User Assignment for Project
        /// </summary>
        void SaveProjectAssignment(Guid userID, Guid projectID, bool isAssigned);

        /// <summary>
        /// Save Phase and User to Assignment
        /// </summary>
        /// <returns></returns>
        void SavePhaseAssignment(Guid userID, Guid phaseID, bool isAssigned);
    }
}
