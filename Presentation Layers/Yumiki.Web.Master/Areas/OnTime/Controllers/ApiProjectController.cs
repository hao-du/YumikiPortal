using System;
using System.Collections.Generic;
using System.Web.Http;
using Yumiki.Business.OnTime.Interfaces;
using Yumiki.Commons.Settings;
using Yumiki.Entity.OnTime;
using Yumiki.Web.Base;

namespace Yumiki.Web.OnTime.Controllers
{
    [RoutePrefix("api/ontime/project")]
    public class ApiProjectController : ApiBaseController<IProjectService>
    {
        [Route("getall", Name = "GetAllProjects")]
        [HttpGet()]
        public IHttpActionResult Get()
        {
            try
            {
                List<TB_Project> projects = BusinessService.GetAllProjects(true);

                //List<Project> projects = new List<Project>();
                //projects.Add(new Project() { ProjectName = "Project 1", ProjectDescription = "Project 1 Description", IsActive = true });
                //projects.Add(new Project() { ProjectName = "Project 2", ProjectDescription = "Project 2 Description", IsActive = true });
                //projects.Add(new Project() { ProjectName = "Project 3", ProjectDescription = "Project 3 Description", IsActive = true });
                //projects.Add(new Project() { ProjectName = "Project 4", ProjectDescription = "Project 4 Description", IsActive = true });
                //projects.Add(new Project() { ProjectName = "Project 5", ProjectDescription = "Project 5 Description", IsActive = true });
                //projects.Add(new Project() { ProjectName = "Project 6", ProjectDescription = "Project 6 Description", IsActive = true });
                //projects.Add(new Project() { ProjectName = "Project 7", ProjectDescription = "Project 7 Description", IsActive = true });
                //projects.Add(new Project() { ProjectName = "Project 8", ProjectDescription = "Project 8 Description", IsActive = true });
                //projects.Add(new Project() { ProjectName = "Project 9", ProjectDescription = "Project 9 Description", IsActive = true });
                //projects.Add(new Project() { ProjectName = "Project 10", ProjectDescription = "Project 10 Description", IsActive = true });

                return Ok(projects);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }

    public class Project
    {
        public string ProjectName { get; set; }
        public  string ProjectDescription { get; set; }
        public  bool IsActive { get; set; }
    }
}
