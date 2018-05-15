using System;
using System.Collections.Generic;
using System.Linq;
using Yumiki.Data.OnTime.Interfaces;
using Yumiki.Entity.OnTime;
using Yumiki.Entity.OnTime.ServiceObjects;

namespace Yumiki.Data.OnTime.Repositories
{
    public class UserAssignmentRepository : OnTimeRepository, IUserAssignmentRepository
    {
        /// <summary>
        /// Get All Active Users
        /// </summary>
        /// <returns></returns>
        public List<TB_User> GetUsers()
        {
            return Context.TB_User.Where(c => c.IsActive).ToList();
        }

        /// <summary>
        /// Get a specific user with Projects and Phases Assignment
        /// </summary>
        /// <returns></returns>
        public GetUserWithAssignmentResponse GetUserWithAssignments(Guid userID)
        {
            GetUserWithAssignmentResponse response = new GetUserWithAssignmentResponse();

            response.UserWithAssignments = Context.TB_User.Include("ProjectAssignments").Include("PhaseAssignments").Single(c => c.ID == userID);

            response.PhaseAssignments = Context.TB_Phase.Where(c => c.IsActive).ToList();
            foreach (TB_Phase phase in response.PhaseAssignments.Where(c => response.UserWithAssignments.PhaseAssignments.Any(d => d.PhaseID == c.ID)))
            {
                phase.IsAssigned = true;
            }

            response.ProjectAssignments = Context.TB_Project.Where(c => c.IsActive).ToList();
            foreach (TB_Project project in response.ProjectAssignments.Where(c => response.UserWithAssignments.ProjectAssignments.Any(d => d.ProjectID == c.ID)))
            {
                project.IsAssigned = true;
            }

            return response;
        }

        /// <summary>
        /// Get a specific user with Projects and Phases Assignment
        /// </summary>
        /// <returns></returns>
        public void SaveProjectAssignment(Guid userID, Guid projectID, bool isAssigned)
        {
            TB_ProjectAssignment assignment = Context.TB_ProjectAssignment.SingleOrDefault(c => c.UserID == userID && c.ProjectID == projectID);

            if(isAssigned && assignment == null)
            {
                assignment = new TB_ProjectAssignment();
                assignment.UserID = userID;
                assignment.ProjectID = projectID;

                Context.TB_ProjectAssignment.Add(assignment);

                Save();
            }
            else if(!isAssigned && assignment != null)
            {
                Context.TB_ProjectAssignment.Remove(assignment);

                Save();
            }
        }
    }
}
