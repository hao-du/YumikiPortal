using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yumiki.Business.Shopper.Interfaces;
using Yumiki.Commons.Dictionaries;
using Yumiki.Commons.Helpers;
using Yumiki.Web.Base;

namespace Yumiki.Web.Shopper.Controllers
{
    public class ReportController : BaseController<IReportService>
    {
        // GET: Report
        public ActionResult Index()
        {
            ViewBag.CurrentDate = DateTimeExtension.GetSystemDatetime().ToString(Formats.DateTime.LongDate);

            return View();
        }
    }
}