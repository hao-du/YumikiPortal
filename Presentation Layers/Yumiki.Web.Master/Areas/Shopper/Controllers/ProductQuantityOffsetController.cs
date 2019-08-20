using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yumiki.Business.Shopper.Interfaces;
using Yumiki.Commons.Dictionaries;
using Yumiki.Commons.Entities;
using Yumiki.Commons.Helpers;
using Yumiki.Web.Base;

namespace Yumiki.Web.Shopper.Controllers
{
    public class ProductQuantityOffsetController : BaseController<IProductQuantityOffsetService>
    {
        // GET: Currency
        public ActionResult Index()
        {
            return View();
        }
    }
}