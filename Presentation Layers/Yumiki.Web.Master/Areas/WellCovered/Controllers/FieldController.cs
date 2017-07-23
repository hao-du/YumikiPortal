using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yumiki.Business.WellCovered.Interfaces;
using Yumiki.Commons.Dictionaries;
using Yumiki.Commons.Entities;
using Yumiki.Commons.Settings;
using Yumiki.Entity.WellCovered;
using Yumiki.Web.Base;
using Yumiki.Web.WellCovered.Models;

namespace Yumiki.Web.WellCovered.Controllers
{
    public class FieldController : BaseController<IFieldService>
    {
        public const string PostDropdownList = "PostDropdownList";
        public const string PostSave = "PostSave";

        // GET: App
        public ActionResult List(bool active, string objectID)
        {
            IEnumerable<MD_Field> apps = BusinessService.GetAllFields(!active, objectID).Select(c => new MD_Field(c));

            return View(apps);
        }

        // GET: App/Create
        public ActionResult Create(string objectID)
        {
            MD_Field field = new MD_Field();

            Guid convertedObjectID;
            if(Guid.TryParse(objectID, out convertedObjectID))
            {
                field.ObjectID = convertedObjectID;
            }

            InitDatasource();

            return View(field);
        }

        // POST: App/Create
        [HttpPost]
        public ActionResult Create(string action, MD_Field field)
        {
            switch (action)
            {
                case PostDropdownList:
                    ModelState.Clear();
                    break;
                case PostSave:
                    if (ModelState.IsValid)
                    {
                        BusinessService.Save(field.ToObject());

                        return RedirectToAction("List");
                    }
                    break;
            }

            InitDatasource();

            return View(field);
        }

        // GET: App/Edit/5
        public ActionResult Edit(string id)
        {
            MD_Field field = new MD_Field(BusinessService.GetFieldByID(id));

            InitDatasource();

            return View(field);
        }

        // POST: App/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, string action, MD_Field field)
        {
            if (ModelState.IsValid)
            {
                BusinessService.Save(field.ToObject());

                return RedirectToAction("List");
            }

            InitDatasource();

            return View(field);
        }

        private void InitDatasource()
        {
            List<ExtendEnum> enums = BusinessService.GetDataTypes();
            ViewBag.DataTypeDatasource = new SelectList(enums, ExtendEnum.FieldName.Value, ExtendEnum.FieldName.DisplayText);
        }
    }
}
