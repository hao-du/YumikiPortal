using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yumiki.Business.WellCovered.Interfaces;
using Yumiki.Commons.Dictionaries;
using Yumiki.Commons.Exceptions;
using Yumiki.Commons.Settings;
using Yumiki.Entity.MoneyTrace.ServiceObjects;
using Yumiki.Entity.WellCovered;
using Yumiki.Web.Base;
using Yumiki.Web.WellCovered.Models;

namespace Yumiki.Web.WellCovered.Controllers
{
    public class LiveController : BaseController<ILiveService>
    {
        public const string ObjectID = "objectID";
        public const string RecordID = "recordID";
        public const string RequestVerificationToken = "__RequestVerificationToken";

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

        public ActionResult Search(string keywords, int? pageIndex)
        {
            if (string.IsNullOrWhiteSpace(keywords))
            {
                return View();
            }

            if (!pageIndex.HasValue)
            {
                pageIndex = 1;
            }

            GetSearchIndexResponse response = new GetSearchIndexResponse();
            try
            {
                GetSearchIndexRequest request = new GetSearchIndexRequest()
                {
                    Keywords = keywords,
                    UserID = CurrentUser.UserID,
                    CurrentPage = pageIndex.Value
                };

                response = BusinessService.Search(request);
            }
            catch (Exception ex)
            {
                SendError(ex);
            }

            return View(response);
        }

        // GET: App/Create
        public ActionResult Create(string objectID)
        {
            MD_LiveRecord liveRecord = new MD_LiveRecord();
            try
            {
                liveRecord.Fields = BusinessService.GetFields(objectID).Select(c => new MD_Field(c));

                MD_Field isActiveField = liveRecord.Fields.Where(c => c.ApiName.Equals(CommonProperties.IsActive, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
                if(isActiveField != null)
                {
                    isActiveField.Value = true;
                }

                InitDataSource(liveRecord.Fields);
            }
            catch (Exception ex)
            {
                SendError(ex);
            }

            liveRecord.ObjectID = objectID;

            return View(liveRecord);
        }

        // POST: App/Create
        [HttpPost]
        public ActionResult Create(FormCollection formCollection)
        {
            string objectID = formCollection[LiveController.ObjectID];
            try
            {
                Dictionary<string, string> dicFields = new Dictionary<string, string>();

                foreach (string key in formCollection.AllKeys)
                {
                    if (!key.Equals(LiveController.RequestVerificationToken, StringComparison.InvariantCultureIgnoreCase))
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

            MD_LiveRecord liveRecord = new MD_LiveRecord();

            liveRecord.ObjectID = objectID;

            liveRecord.Fields = SetFieldValues(formCollection);
            InitDataSource(liveRecord.Fields);

            return View(liveRecord);
        }

        // GET: App/Edit
        public ActionResult Edit(string objectID, string recordID)
        {
            MD_LiveRecord liveRecord = new MD_LiveRecord();

            try
            {
                liveRecord.ObjectID = objectID;
                liveRecord.LiveRecordID = recordID;

                liveRecord.Fields = BusinessService.GetFieldsByID(objectID, recordID).Select(c => new MD_Field(c));
                InitDataSource(liveRecord.Fields);

                //Get all attachments from object
                liveRecord.Attachments = BusinessService.GetAttachments(recordID).Select(c => new MD_Attachment(c));
            }
            catch (Exception ex)
            {
                SendError(ex);
            }

            return View(liveRecord);
        }

        // POST: App/Edit
        [HttpPost]
        public ActionResult Edit(FormCollection formCollection)
        {
            string objectID = formCollection[LiveController.ObjectID];
            string recordID = formCollection[LiveController.RecordID];

            try
            {
                Dictionary<string, string> dicFields = new Dictionary<string, string>();

                foreach (string key in formCollection.AllKeys)
                {
                    if (!key.Equals(LiveController.RequestVerificationToken, StringComparison.InvariantCultureIgnoreCase))
                    {
                        dicFields.Add(key, formCollection[key]);
                    }
                }

                BusinessService.Update(objectID, recordID, dicFields);

                return RedirectToAction("List", new { active = true, objectID = objectID });
            }
            catch (Exception ex)
            {
                SendError(ex);
            }

            MD_LiveRecord liveRecord = new MD_LiveRecord();

            liveRecord.ObjectID = objectID;
            liveRecord.LiveRecordID = recordID;

            liveRecord.Fields = SetFieldValues(formCollection);
            InitDataSource(liveRecord.Fields);

            //Get all attachments from object
            liveRecord.Attachments = BusinessService.GetAttachments(recordID).Select(c => new MD_Attachment(c));

            return View(liveRecord);
        }

        /// <summary>
        /// When validation fails, add values which entered by user back to UI by assign value in submitted form to return Field list.
        /// </summary>
        /// <param name="formCollection">Form Collection which contains entered values.</param>
        /// <returns></returns>
        private IEnumerable<MD_Field> SetFieldValues(FormCollection formCollection)
        {
            IEnumerable<MD_Field> fields = new List<MD_Field>();
            try
            {
                fields = BusinessService.GetFields(formCollection[LiveController.ObjectID]).Select(c => new MD_Field(c));

                foreach (string key in formCollection.AllKeys)
                {
                    MD_Field selectedField = fields.Where(c => c.ApiName == key).FirstOrDefault();

                    if (selectedField != null)
                    {
                        string value = formCollection[key];

                        if (!string.IsNullOrWhiteSpace(value))
                        {
                            switch (selectedField.FieldType)
                            {
                                case EN_DataType.E_INT:
                                    selectedField.Value = int.Parse(value);
                                    break;
                                case EN_DataType.E_DECIMAL:
                                    selectedField.Value = decimal.Parse(value);
                                    break;
                                case EN_DataType.E_BOOL:
                                    selectedField.Value = bool.Parse(value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)[0]);
                                    break;
                                case EN_DataType.E_DATE:
                                    selectedField.Value = DateTime.Parse(value).Date;
                                    break;
                                case EN_DataType.E_DATETIME:
                                    selectedField.Value = DateTime.Parse(value);
                                    break;
                                case EN_DataType.E_TIME:
                                    selectedField.Value = DateTime.Parse(value).TimeOfDay;
                                    break;
                                default:
                                    //Datasource Type and String type use String as it is.
                                    selectedField.Value = value;
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SendError(ex);
            }

            return fields;
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
        public ActionResult PublishObject(string appID, string objectID)
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

            return RedirectToAction("List", "Object", new { active = true, appID = appID });
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

                    SelectList select = new SelectList(dataSource, MD_Datasource.FieldNames.ID, MD_Datasource.FieldNames.DisplayText, dataSourceField.Value);

                    ((Dictionary<string, SelectList>)ViewBag.Datasources).Add(dataSourceField.ApiName, select);
                }
            }
        }
    }
}