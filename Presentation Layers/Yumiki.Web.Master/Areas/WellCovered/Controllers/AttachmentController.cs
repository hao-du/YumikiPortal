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
    public class AttachmentController : BaseController<IAttachmentService>
    {
        [HttpPost]
        public ActionResult Upload(string recordID, string objectID, HttpPostedFileBase file)
        {
            try
            {
                MD_Attachment attachment = new MD_Attachment();
                attachment.AttachmentName = file.FileName;
                attachment.FilePath = file.FileName;
                attachment.LiveRecordID = Guid.Parse(recordID);

                BusinessService.Save(attachment.ToObject());
            }
            catch (Exception ex)
            {
                SendError(ex);
            }

            return RedirectToLiveEditPage(objectID, recordID);
        }

        [HttpDelete]
        public ActionResult Delete(string recordID, string objectID, string attachmentID)
        {
            try
            {
                MD_Attachment attachment = new MD_Attachment();
                attachment.ID = Guid.Parse(attachmentID);
                attachment.IsActive = false;

                BusinessService.Save(attachment.ToObject());
            }
            catch (Exception ex)
            {
                SendError(ex);
            }

            return RedirectToLiveEditPage(objectID, recordID);
        }

        private RedirectToRouteResult RedirectToLiveEditPage(string objectID, string recordID)
        {
            return RedirectToAction("Edit", "Live", new { objectID, recordID });
        }
    }
}
