using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Yumiki.Business.OnTime.Interfaces;
using Yumiki.Commons.Settings;
using Yumiki.Entity.OnTime;
using Yumiki.Web.Base;
using Yumiki.Web.Ontime.Models;

namespace Yumiki.Web.OnTime.Controllers
{
    [RoutePrefix("api/ontime/task")]
    public class ApiTaskController : ApiBaseController<ITaskService>
    {
        [Route("getdashboard", Name = "GetDashboard")]
        [HttpGet()]
        public IHttpActionResult Get()
        {
            try
            {
                var tube = BusinessService.GetAllTasksWithTypes(CurrentUser.UserID.ToString(), 10);

                MD_TaskDashBoard dashBoard = new MD_TaskDashBoard();
                dashBoard.MyTasks = tube.Item1.Select(c=>new MD_Task(c));
                dashBoard.MyCreatedTasks = tube.Item2.Select(c => new MD_Task(c));
                dashBoard.LatestTasks = tube.Item3.Select(c => new MD_Task(c));
                dashBoard.UnassignedTasks = tube.Item4.Select(c => new MD_Task(c));

                return Ok(dashBoard);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("gettasks", Name = "GetTasks")]
        [HttpGet()]
        public IHttpActionResult Get(int taskType)
        {
            try
            {
                IEnumerable<MD_Task> tasks = BusinessService.GetTasks(CurrentUser.UserID.ToString(), null, (EN_TaskType)taskType).Select(c=>new MD_Task(c));

                return Ok(tasks);
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
