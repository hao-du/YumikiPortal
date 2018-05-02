using System.IO;
using System.Web.Mvc;
using Yumiki.Business.OnTime.Interfaces;
using Yumiki.Web.Base;

namespace Yumiki.Web.OnTime.Controllers
{
    public class ProjectController : BaseController<IProjectService>
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
            return View();
        }
    }
}
