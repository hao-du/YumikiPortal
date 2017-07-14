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
        public ActionResult List(bool active = true)
        {
            IEnumerable<MD_Object> apps = BusinessService.GetAllObjects(!active).Select(c => new MD_Object(c));

            return View(apps);
        }

        // GET: App/Create
        public ActionResult Create()
        {
            InitDatasource();

            return View();
        }

        // POST: App/Create
        [HttpPost]
        public ActionResult Create(MD_Object obj)
        {
            if (ModelState.IsValid)
            {
                BusinessService.Save(obj.ToObject());

                return RedirectToAction("List");
            }

            InitDatasource();

            return View(obj);
        }

        // GET: App/Edit/5
        public ActionResult Edit(string id)
        {
            MD_Object app = new MD_Object(BusinessService.GetObjectByID(id));

            InitDatasource();

            return View(app);
        }

        // POST: App/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, MD_Object obj)
        {
            if (ModelState.IsValid)
            {
                BusinessService.Save(obj.ToObject());

                return RedirectToAction("List");
            }

            InitDatasource();

            return View(obj);
        }

        private void InitDatasource()
        {
            IEnumerable<MD_App> apps = BusinessService.GetApps(CurrentUser.UserID.ToString()).Select(c => new MD_App(c));
            ViewBag.AppDatasource = new SelectList(apps, CommonProperties.ID, TB_App.FieldName.AppName);
        }
    }
}
