using System.IO;
using System.Linq;
using System.Web.Mvc;
using Yumiki.Business.OnTime.Interfaces;
using Yumiki.Commons.Helpers;
using Yumiki.Entity.OnTime;
using Yumiki.Web.Base;
using Yumiki.Web.OnTime.Models;

namespace Yumiki.Web.OnTime.Controllers
{
    public class TaskController : BaseController<ITaskService>
    {
        //Landing Page
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IndexContent()
        {
            var tube = BusinessService.GetMetadata(false);

            ViewBag.Phases = tube.Item1.Select(c => new MD_Phase(c));
            ViewBag.Projects = tube.Item2.Select(c => new MD_Project(c));

            return View();
        }

        public ActionResult Detail()
        {
            return View();
        }

        public ActionResult DetailContent()
        {
            var tube = BusinessService.GetMetadata();

            ViewBag.Phases = tube.Item1.Select(c => new MD_Phase(c));
            ViewBag.Projects = tube.Item2.Select(c => new MD_Project(c));
            ViewBag.Users = tube.Item3.Select(c=> new MD_User(c));
            
            ViewBag.Statuses = EnumHelper.GetDatasource<EN_TaskStatus>();
            ViewBag.Priorities = EnumHelper.GetDatasource<EN_Priority>();

            return View();
        }
    }
}
