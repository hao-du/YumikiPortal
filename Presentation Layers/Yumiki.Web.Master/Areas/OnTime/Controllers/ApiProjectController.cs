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
    [RoutePrefix("api/ontime/project")]
    public class ApiProjectController : ApiBaseController<IProjectService>
    {
        [Route("getall", Name = "GetAllProjects")]
        [HttpGet()]
        public IHttpActionResult Get(bool isActive)
        {
            try
            {
                IEnumerable<MD_Project> projects = BusinessService.GetAllProjects(isActive).Select(c => new MD_Project(c));

                return Ok(projects);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("get", Name = "GetProject")]
        [HttpGet()]
        public IHttpActionResult Get(string id)
        {
            try
            {
                MD_Project project = new MD_Project(BusinessService.GetProject(id));

                return Ok(project);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("save", Name = "SaveProject")]
        [HttpPost()]
        public IHttpActionResult Save(MD_Project project)
        {
            try
            {
                BusinessService.SaveProject(project.ToObject());

                return Ok(project);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
