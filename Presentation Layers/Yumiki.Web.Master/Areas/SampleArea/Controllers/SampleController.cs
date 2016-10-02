using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Yumiki.Web.Master.Areas.SampleArea.Controllers
{
    public class SampleController : Controller
    {
        // GET: SampleArea/Sample
        public ActionResult Index()
        {
            return View();
        }
    }
}