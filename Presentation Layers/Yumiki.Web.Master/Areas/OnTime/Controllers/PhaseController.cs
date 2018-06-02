using System.IO;
using System.Web.Mvc;
using Yumiki.Business.OnTime.Interfaces;
using Yumiki.Commons.Helpers;
using Yumiki.Entity.OnTime;
using Yumiki.Web.Base;

namespace Yumiki.Web.OnTime.Controllers
{
    public class PhaseController : BaseController<IPhaseService>
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
            ViewBag.StatusList = EnumHelper.GetDatasource<EN_PhaseStatus>();

            return View();
        }
    }
}
