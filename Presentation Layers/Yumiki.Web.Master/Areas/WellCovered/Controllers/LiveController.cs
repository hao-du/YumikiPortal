using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yumiki.Business.WellCovered.Interfaces;
using Yumiki.Entity.WellCovered;
using Yumiki.Web.Base;
using Yumiki.Web.WellCovered.Models;

namespace Yumiki.Web.WellCovered.Controllers
{
    public class LiveController : BaseController<ILiveService>
    {
        public ActionResult List(string objectID, bool active)
        {
            MD_Live live = new MD_Live();

            try
            {
                live = BusinessService.FetchObjectData(objectID, active);
            }
            catch (Exception ex)
            {
                SendError(ex);
            }

            return View(live);
        }

        // GET: App/Create
        public ActionResult Create(string objectID)
        {
            IEnumerable<MD_Field> fields = new List<MD_Field>();
            try
            {
                fields = BusinessService.GetFields(objectID).Select(c=> new MD_Field(c));
            }
            catch (Exception ex)
            {
                SendError(ex);
            }

            ViewBag.ObjectID = objectID;

            return View(fields);
        }

        // POST: App/Create
        [HttpPost]
        public ActionResult Create(FormCollection formCollection)
        {
            try
            {
            }
            catch (Exception ex)
            {
                SendError(ex);
            }

            return View();
        }

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