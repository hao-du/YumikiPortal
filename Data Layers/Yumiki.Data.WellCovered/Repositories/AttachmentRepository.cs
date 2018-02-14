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
        /// Return specific attachment by id
        /// </summary>
        /// <param name="id">TB_Attachment Guid ID</param>
        /// <returns>Result with TB_Attachment type</returns>
        public TB_Attachment GetAttachmentByID(Guid id)
        {
            return Context.TB_Attachment.SingleOrDefault(c => c.ID == id);
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
            TB_Attachment dbAttachment = Context.TB_Attachment.SingleOrDefault(c => c.ID == attachment.ID);

            if (dbAttachment == null)
            {
                Context.TB_Attachment.Add(attachment);
            }
            else if (!attachment.IsActive)
            {
                //Assing missing value to let caller handle.
                attachment.AttachmentName = dbAttachment.AttachmentName;
                attachment.AttachmentPath = dbAttachment.AttachmentPath;

                Context.TB_Attachment.Remove(dbAttachment);
            }

            Save();
        }
    }
}
