using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Data.WellCovered.Interfaces;
using Yumiki.Entity.WellCovered;

namespace Yumiki.Data.WellCovered.Repositories
{
    public class AttachmentRepository : WellCoveredRepository, IAttachmentRepository
    {
        /// <summary>
        /// Get all attachment from live record
        /// </summary>
        /// <param name="recordID">Id of relevent live record</param>
        /// <returns>List of attachments</returns>
        public IEnumerable<TB_Attachment> GetAllAttachments(Guid recordID)
        {
            List<TB_Attachment> attachments = Context.TB_Attachment.Where(c => c.LiveRecordID == recordID).ToList();

            return attachments;
        }

        /// <summary>
        /// Save attachment to DB
        /// </summary>
        /// <param name="attachment">
        /// If attachment id is empty, then this is new attachment. Otherwise, this needs to be updated
        /// NOTE: If inactive field is false, delete physical file
        /// </param>
        public void Save(TB_Attachment attachment)
        {
            if (attachment.ID == Guid.Empty)
            {
                Context.TB_Attachment.Add(attachment);
            }
            else
            {
                TB_Attachment dbAttachment = Context.TB_Attachment.Single(c => c.ID == attachment.ID);

                if (!attachment.IsActive)
                {
                    Context.TB_Attachment.Remove(dbAttachment);
                }
                else
                {
                    dbAttachment.AttachmentName = attachment.AttachmentName;
                    dbAttachment.AttachmentPath = attachment.AttachmentPath;
                    dbAttachment.LiveRecordID = attachment.LiveRecordID;
                    dbAttachment.Descriptions = attachment.Descriptions;
                }
            }

            Save();
        }
    }
}
