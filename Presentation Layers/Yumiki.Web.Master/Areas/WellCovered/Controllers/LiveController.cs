using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yumiki.Business.WellCovered.Interfaces;
using Yumiki.Web.Base;
using Yumiki.Web.WellCovered.Models;

namespace Yumiki.Web.WellCovered.Controllers
{
    public class LiveController : BaseController<ILiveService>
    {
        // POST:
        [HttpPost]
        public ActionResult PublishApp(string appID)
        {
            try
            {
                BusinessService.PublishApp(appID);

                SendInformation("Publish App Successfully!");
            }
            catch (Exception ex)
            {
                SendError(ex);
            }

            return RedirectToAction("List", "App", new { active = true });
        }

        // POST:
        [HttpPost]
        public ActionResult PublishObject(string objectID)
        {
            try
            {
                BusinessService.PublishObject(objectID);

                SendInformation("Publish Object Successfully!");
            }
            catch (Exception ex)
            {
                SendError(ex);
            }

            return RedirectToAction("List", "Object", new { active = true });
        }
    }
}