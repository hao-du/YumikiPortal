using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yumiki.Business.Shopper.Interfaces;
using Yumiki.Web.Base;

namespace Yumiki.Web.Shopper.Controllers
{
    public class ReceiptController : BaseController<IReceiptService>
    {
        // GET: Currency
        public ActionResult Index()
        {
            return View();
        }
    }
}