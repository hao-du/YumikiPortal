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
        [Route("getall", Name = "GetAllTasks")]
        [HttpGet()]
        public IHttpActionResult Get()
        {
            try
            {
                IEnumerable<MD_Task> tasks = BusinessService.GetAllTasks().Select(c => new MD_Task(c));

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
