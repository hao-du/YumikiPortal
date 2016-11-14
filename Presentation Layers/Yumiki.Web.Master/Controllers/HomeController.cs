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
    public class HomeController : BaseController
    {
        ISecurityService securityService;
        ISecurityService SecurityService
        {
            get
            {
                if (securityService == null)
                {
                    securityService = Service.GetInstance<ISecurityService>();
                }
                return securityService;
            }
        }

        // GET: Home
        public ActionResult Index()
        {
            return View();//Redirect(HttpConstants.Pages.Login);
        }

        [ChildActionOnly]
        public PartialViewResult GetAddress()
        {
            return PartialView("_address");
        }

    }
}