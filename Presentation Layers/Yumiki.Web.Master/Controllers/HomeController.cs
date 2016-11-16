using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yumiki.Business.Master.Interfaces;
using Yumiki.Common.Dictionary;
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
            string userID = Session[HttpConstants.Session.UserID]?.ToString();
            string menu = string.Empty;

            if (!string.IsNullOrEmpty(userID))
            {
                menu = BusinessService.GetPrivilege(userID);
            }

            return PartialView("GetMenu", menu);
        }

    }
}