using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yumiki.Business.MoneyTrace.Interfaces;
using Yumiki.Commons.Dictionaries;
using Yumiki.Commons.Helpers;
using Yumiki.Web.Base;

namespace Yumiki.Web.MoneyTrace.Controllers
{
    public class ReportController : BaseController<IReportService>
    {
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }
    }
}