using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yumiki.Business.MoneyTrace.Interfaces;
using Yumiki.Web.Base;

namespace Yumiki.Web.MoneyTrace.Controllers
{
    public class BankAccountController : BaseController<IBankAccountService>
    {
        // GET: Currency
        public ActionResult Index()
        {
            return View();
        }
    }
}