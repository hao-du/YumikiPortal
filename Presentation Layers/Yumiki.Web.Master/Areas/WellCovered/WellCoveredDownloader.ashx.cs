using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Yumiki.Business.WellCovered.Interfaces;
using Yumiki.Commons.Exceptions;
using Yumiki.Commons.Settings;
using Yumiki.Entity.WellCovered;
using Yumiki.Web.Base;

namespace Yumiki.Web.WellCovered
{
    /// <summary>
    /// Summary description for WellCoverDownloader
    /// </summary>
    [Obsolete("This class is no longer used. Just for reference.")]
    public class WellCoveredDownloader : HttpHandler<IAttachmentService>
    {
        public override void StartProcess(HttpContext context)
        {
            base.StartProcess(context);

            string attachmentID = "";
            // get the id from the querystring
            if (context.Request.QueryString["id"] != null)
            {
                attachmentID = context.Request.QueryString["id"].ToString();
            }
            TB_Attachment attachment = BusinessService.GetAttachmentByID(attachmentID);

            string filePath = System.IO.Path.Combine(SystemSettings.DefaultUploadFolderPath, attachment.AttachmentPath);
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(filePath);

            try
            {
                if (fileInfo.Exists)
                {
                    context.Response.Clear();
                    context.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + attachment.AttachmentName + "\"");
                    context.Response.AddHeader("Content-Length", fileInfo.Length.ToString());
                    context.Response.ContentType = "application/octet-stream";
                    context.Response.TransmitFile(fileInfo.FullName);
                    context.Response.Flush();
                }
                else
                {
                    throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Attachment does not exist.");
                }
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write(ex.Message);
            }
            finally
            {
                context.Response.End();
            }
        }
    }
}