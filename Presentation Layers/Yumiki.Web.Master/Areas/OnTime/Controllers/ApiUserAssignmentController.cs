using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Yumiki.Business.OnTime.Interfaces;
using Yumiki.Commons.Settings;
using Yumiki.Entity.OnTime;
using Yumiki.Entity.OnTime.ServiceObjects;
using Yumiki.Web.Base;
using Yumiki.Web.Ontime.Models;

namespace Yumiki.Web.OnTime.Controllers
{
    [RoutePrefix("api/ontime/assignment")]
    public class ApiUserAssignmentController : ApiBaseController<IUserAssignmentService>
    {
        [Route("getusers", Name = "GetUsers")]
        [HttpGet()]
        public IHttpActionResult GetUsers()
        {
            try
            {
                IEnumerable<MD_User> users = BusinessService.GetUsers().Select(c => new MD_User(c));

                return Ok(users);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("getassignments", Name = "GetAssignments")]
        [HttpGet()]
        public IHttpActionResult GetAssignments(string userID)
        {
            try
            {
                GetUserWithAssignmentResponse response = BusinessService.GetUserWithAssignments(userID);

                MD_User user = new MD_User(response.UserWithAssignments);
                user.ProjectAssignments = response.ProjectAssignments.Select(c => new MD_Project(c));
                user.PhaseAssignments = response.PhaseAssignments.Select(c => new MD_Phase(c));

                return Ok(user);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("saveprojectassignment", Name = "SaveProjectAssignment")]
        [HttpPost()]
        public IHttpActionResult SaveProjectAssignment(MD_ProjectAssignment assignment)
        {
            try
            {
                BusinessService.SaveProjectAssignment(assignment.UserID, assignment.ProjectID, assignment.IsAssigned);

                return Ok(true);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("savephaseassignment", Name = "SavePhaseAssignment")]
        [HttpPost()]
        public IHttpActionResult SavePhaseAssignment(MD_PhaseAssignment assignment)
        {
            try
            {
                BusinessService.SavePhaseAssignment(assignment.UserID, assignment.PhaseID, assignment.IsAssigned);

                return Ok(true);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
