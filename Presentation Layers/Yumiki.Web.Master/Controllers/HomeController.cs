using System;
using System.Web.Mvc;
using Yumiki.Business.Master.Interfaces;
using Yumiki.Commons.Dictionaries;
using Yumiki.Commons.Settings;
using Yumiki.Web.Base;

namespace Yumiki.Web.Master.Controllers
{
    public class HomeController : BaseController<IGUIService>
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToLoginPage(false);
        }

        [ChildActionOnly]
        public PartialViewResult GetMenu()
        {
            string menu = string.Empty;

            if (CurrentUser.IsAuthenticated)
            {
                menu = BusinessService.GetPrivilege(CurrentUser.UserID.ToString());
            }

            return PartialView("GetMenu", menu);
        }

    }
}