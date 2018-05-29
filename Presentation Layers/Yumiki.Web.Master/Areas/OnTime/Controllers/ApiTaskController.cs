using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Yumiki.Business.OnTime.Interfaces;
using Yumiki.Commons.Settings;
using Yumiki.Entity.OnTime;
using Yumiki.Entity.OnTime.ServiceObjects;
using Yumiki.Web.Base;
using Yumiki.Web.OnTime.Models;

namespace Yumiki.Web.OnTime.Controllers
{
    [RoutePrefix("api/ontime/task")]
    public class ApiTaskController : ApiBaseController<ITaskService>
    {
        [Route("gettasks", Name = "GetTasks")]
        [HttpGet()]
        public IHttpActionResult Get(bool getDefaultMetadata, int taskType, string phaseID, string projectID)
        {
            try
            {
                MD_Phase phase = new MD_Phase();
                if (getDefaultMetadata && string.IsNullOrWhiteSpace(phaseID))
                {
                    phase = new MD_Phase(BusinessService.GetMetadata(true).Item1.FirstOrDefault(c=>c.Status == EN_PhaseStatus.E_IN_PROGRESS));
                }

                GetTaskRequest request = new GetTaskRequest();
                request.UserID = CurrentUser.UserID;
                request.PhaseID = phase.ID;
                if (!string.IsNullOrWhiteSpace(phaseID))
                {
                    request.PhaseID = Guid.Parse(phaseID);
                }
                request.ProjectID = Guid.Empty;
                if (!string.IsNullOrWhiteSpace(projectID))
                {
                    request.ProjectID = Guid.Parse(projectID);
                }

                MD_PagingTask response = new MD_PagingTask(BusinessService.GetTasks(request, (EN_TaskType)taskType));
                response.DefaultPhaseID = request.PhaseID == Guid.Empty? string.Empty : request.PhaseID.ToString();
                response.DefaultProjectID = request.ProjectID == Guid.Empty ? string.Empty : request.ProjectID.ToString();

                return Ok(response);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("get", Name = "GetTask")]
        [HttpGet()]
        public IHttpActionResult Get(string id)
        {
            try
            {
                MD_Task task = new MD_Task(BusinessService.GetTask(id));

                return Ok(task);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("save", Name = "SaveTask")]
        [HttpPost()]
        public IHttpActionResult Save(MD_Task task)
        {
            try
            {
                BusinessService.SaveTask(task.ToObject());

                return Ok(task);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
