using System.Web.Mvc;
using Yumiki.Business.OnTime.Interfaces;
using Yumiki.Web.Base;

namespace Yumiki.Web.OnTime.Controllers
{
    public class TestOnController : BaseController<IProjectService>
    {
        // GET: App
        public ActionResult Index()
        {
            BusinessService.GetAllProjects(true);

            return View();
        }
    }
}
