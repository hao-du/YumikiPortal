using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Data.Base;
using Yumiki.Entity.WellCovered;

namespace Yumiki.Data.WellCovered.Interfaces
{
    public interface IAttachmentRepository : IShareableRepository<WellCoveredModel>
    {
        /// <summary>
        /// Get all attachment from live record
        /// </summary>
        /// <param name="recordID">Id of relevent live record</param>
        /// <returns>List of attachments</returns>
        IEnumerable<TB_Attachment> GetAllAttachments(Guid recordID);

        /// <summary>
        /// Save attachment to DB
        /// </summary>
        /// <param name="attachment">
        /// If attachment id is empty, then this is new attachment. Otherwise, this needs to be updated
        /// NOTE: If inactive field is false, delete physical file
        /// </param>
        void Save(TB_Attachment attachment);
    }
}
