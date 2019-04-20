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
    public class ReceiptController : BaseController<IReceiptService>
    {
        // GET: Currency
        public ActionResult Index()
        {
            List<ExtendEnum> enums = BusinessService.GetStatuses();
            ViewBag.Statuses = new SelectList(enums, ExtendEnum.FieldName.Value, ExtendEnum.FieldName.DisplayText);
            ViewBag.CurrentDate = DateTimeExtension.GetSystemDatetime().ToString(Formats.DateTime.LongDate);

            return View();
        }
    }
}