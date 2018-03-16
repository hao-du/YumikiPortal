using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yumiki.Business.WellCovered.Interfaces;
using Yumiki.Commons.Exceptions;
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
                attachment.FileName = file.FileName;
                attachment.LiveRecordID = Guid.Parse(recordID);

                BusinessService.Save(attachment.ToObject(), SystemSettings.DefaultUploadFolderPath, file.InputStream);
            }
            catch (Exception ex)
            {
                SendError(ex);
            }

            return RedirectToLiveEditPage(objectID, recordID);
        }

        public FileResult Download(string id)
        {
            MD_Attachment attachment = new MD_Attachment(BusinessService.GetAttachmentByID(id));

            string filePath = System.IO.Path.Combine(SystemSettings.DefaultUploadFolderPath, attachment.FilePath);
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(filePath);

            if (fileInfo.Exists)
            {
                byte[] fileBytes = System.IO.File.ReadAllBytes(fileInfo.FullName);
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, attachment.FileName);
            }
            else
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Attachment does not exist.", Logger);
            }
        }

        [HttpDelete]
        public ActionResult Delete(string recordID, string objectID, string attachmentID)
        {
            try
            {
                MD_Attachment attachment = new MD_Attachment();
                attachment.ID = Guid.Parse(attachmentID);

                BusinessService.Delete(attachment.ToObject(), SystemSettings.DefaultUploadFolderPath);
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
