using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Yumiki.Business.WellCovered.Interfaces;
using Yumiki.Commons.Settings;
using Yumiki.Entity.WellCovered;
using Yumiki.Web.Base;

namespace Yumiki.Web.WellCovered.Controllers
{
    public class AppController : BaseController<IAppService>
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}