using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yumiki.Business.WellCovered.Interfaces;
using Yumiki.Commons.Settings;
using Yumiki.Web.Base;
using Yumiki.Web.WellCovered.Models;

namespace Yumiki.Web.WellCovered.Controllers
{
    public class AppController : BaseController<IAppService>
    {
        // GET: App
        public ActionResult List(bool active = true)
        {
            IEnumerable<MD_App> apps = new List<MD_App>();
            try
            {
                apps = BusinessService.GetAllApps(!active, CurrentUser.UserID, true).Select(c => new MD_App(c));
            }
            catch (Exception ex)
            {
                SendError(ex);
            }

            return View(apps);
        }

        // GET: App/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: App/Create
        [HttpPost]
        public ActionResult Create(MD_App app)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    BusinessService.Save(app.ToObject());

                    return RedirectToAction("List");
                }
            }
            catch (Exception ex)
            {
                SendError(ex);
            }

            return View(app);
        }

        // GET: App/Edit/5
        public ActionResult Edit(string id)
        {
            MD_App app = null;
            try
            {
                app = new MD_App(BusinessService.GetAppByID(id));
            }
            catch (Exception ex)
            {
                SendError(ex);
            }

            return View(app);
        }

        // POST: App/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, MD_App app)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    BusinessService.Save(app.ToObject());

                    return RedirectToAction("List");
                }
            }
            catch (Exception ex)
            {
                SendError(ex);
            }

            return View(app);
        }
    }
}
