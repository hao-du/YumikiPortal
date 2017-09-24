using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yumiki.Business.WellCovered.Interfaces;
using Yumiki.Commons.Dictionaries;
using Yumiki.Commons.Settings;
using Yumiki.Entity.WellCovered;
using Yumiki.Web.Base;
using Yumiki.Web.WellCovered.Models;

namespace Yumiki.Web.WellCovered.Controllers
{
    public class ObjectController : BaseController<IObjectService>
    {
        // GET: App
        public ActionResult List(bool? active, string appID)
        {
            IEnumerable<MD_Object> objects = new List<MD_Object>();
            try
            {
                if(!active == null) { active = true; }

                objects = BusinessService.GetAllObjects(!active.Value, appID).Select(c => new MD_Object(c));

                InitDatasource(appID, true);
            }
            catch (Exception ex)
            {
                SendError(ex);
            }

            return View(objects);
        }

        // GET: App/Create
        public ActionResult Create(string appID)
        {
            InitDatasource(null);

            return View();
        }

        // POST: App/Create
        [HttpPost]
        public ActionResult Create(MD_Object obj, string appID)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    BusinessService.Save(obj.ToObject());

                    return RedirectToAction("List", new { active = true, appID = appID });
                }
            }
            catch (Exception ex)
            {
                SendError(ex);
            }

            InitDatasource(null);

            return View(obj);
        }

        // GET: App/Edit/5
        public ActionResult Edit(string id, string appID)
        {
            MD_Object app = null;
            try
            {
                app = new MD_Object(BusinessService.GetObjectByID(id));
            }
            catch (Exception ex)
            {
                SendError(ex);
            }

            InitDatasource(null);

            return View(app);
        }

        // POST: App/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, MD_Object obj, string appID)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    BusinessService.Save(obj.ToObject());

                    return RedirectToAction("List", new { active = true, appID = appID });
                }
            }
            catch (Exception ex)
            {
                SendError(ex);
            }

            InitDatasource(null);

            return View(obj);
        }

        private void InitDatasource(string appID, bool includeFreeObject = false)
        {
            try
            {
                List<MD_App> apps = BusinessService.GetApps(CurrentUser.UserID.ToString()).Select(c => new MD_App(c)).ToList();

                if (includeFreeObject)
                {
                    MD_App app = new MD_App();
                    app.ID = Guid.Empty;
                    app.AppName = "Free Objects";

                    apps.Insert(0, app);
                }

                Guid convertedAppID;
                if (Guid.TryParse(appID, out convertedAppID))
                {
                    ViewBag.AppDatasource = new SelectList(apps, CommonProperties.ID, TB_App.FieldName.AppName, convertedAppID);
                }
                else
                {
                    ViewBag.AppDatasource = new SelectList(apps, CommonProperties.ID, TB_App.FieldName.AppName);
                }
            }
            catch (Exception ex)
            {
                SendError(ex);
            }
        }
    }
}
