using System.IO;
using System.Linq;
using System.Web.Mvc;
using Yumiki.Business.OnTime.Interfaces;
using Yumiki.Commons.Helpers;
using Yumiki.Entity.OnTime;
using Yumiki.Web.Base;
using Yumiki.Web.Ontime.Models;

namespace Yumiki.Web.OnTime.Controllers
{
    public class TaskController : BaseController<ITaskService>
    {
        //Landing Page
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            return View();
        }

        public ActionResult Submit()
        {
            var tube = BusinessService.GetMetadata();

            ViewBag.Users = tube.Item1.Select(c=> new MD_User(c));
            ViewBag.Phases = tube.Item2.Select(c => new MD_Phase(c));
            ViewBag.Projects = tube.Item3.Select(c => new MD_Project(c));

            ViewBag.Statuses = EnumHelper.GetDatasource<EN_TaskStatus>();

            return View();
        }
    }
}
