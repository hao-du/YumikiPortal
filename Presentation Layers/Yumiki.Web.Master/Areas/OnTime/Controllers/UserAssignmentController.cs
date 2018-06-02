using System.IO;
using System.Web.Mvc;
using Yumiki.Business.OnTime.Interfaces;
using Yumiki.Web.Base;

namespace Yumiki.Web.OnTime.Controllers
{
    public class UserAssignmentController : BaseController<IProjectService>
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

        public ActionResult UserList()
        {
            return View();
        }

        public ActionResult AssignmentList()
        {
            return View();
        }
    }
}
