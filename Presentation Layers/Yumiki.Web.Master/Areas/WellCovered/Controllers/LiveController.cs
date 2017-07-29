using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yumiki.Business.WellCovered.Interfaces;
using Yumiki.Commons.Exceptions;
using Yumiki.Entity.WellCovered;
using Yumiki.Web.Base;
using Yumiki.Web.WellCovered.Models;

namespace Yumiki.Web.WellCovered.Controllers
{
    public class LiveController : BaseController<ILiveService>
    {
        public const string objectID = "objectID";
        public const string __RequestVerificationToken = "__RequestVerificationToken";

        public ActionResult List(string objectID, bool active)
        {
            MD_Live live = new MD_Live();

            try
            {
                live = BusinessService.FetchViewObjectData(objectID, active);
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

                InitDataSource(fields);
            }
            catch (Exception ex)
            {
                SendError(ex);
            }

            ViewBag.ObjectID = LiveController.objectID;

            return View(fields);
        }

        // POST: App/Create
        [HttpPost]
        public ActionResult Create(FormCollection formCollection)
        {
            string objectID = formCollection[LiveController.objectID];
            try
            {
                Dictionary<string, string> dicFields = new Dictionary<string, string>();

                foreach(string key in formCollection.AllKeys)
                {
                    if(!key.Equals(LiveController.__RequestVerificationToken, StringComparison.InvariantCultureIgnoreCase))
                    {
                        dicFields.Add(key, formCollection[key]);
                    }
                }

                BusinessService.Add(objectID, dicFields);

                return RedirectToAction("List", new { active = true, objectID = objectID });
            }
            catch (Exception ex)
            {
                SendError(ex);
            }

            ViewBag.ObjectID = objectID;

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

        private void InitDataSource(IEnumerable<MD_Field> fields)
        {
            IEnumerable<MD_Field> dataSourceFields = fields.Where(c => c.FieldType == EN_DataType.E_DATASOURCE);
            if (dataSourceFields.Any())
            {
                ViewBag.Datasources = new Dictionary<string, SelectList>();

                foreach (MD_Field dataSourceField in dataSourceFields)
                {
                    IEnumerable<MD_Datasource> dataSource = BusinessService.GetDataSource(dataSourceField.Datasource);

                    SelectList select = new SelectList(dataSource, MD_Datasource.FieldNames.ID, MD_Datasource.FieldNames.DisplayText);

                    ((Dictionary<string, SelectList>)ViewBag.Datasources).Add(dataSourceField.ApiName, select);
                }
            }
        }
    }
}