using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Business.Base;
using Yumiki.Business.WellCovered.Interfaces;
using Yumiki.Commons.Exceptions;
using Yumiki.Data.WellCovered.Interfaces;
using Yumiki.Entity.WellCovered;

namespace Yumiki.Business.WellCovered.Interfaces
{
    public interface IAttachmentService
    {
        /// <summary>
        /// Return specific attachment by id
        /// </summary>
        /// <param name="id">TB_Attachment Guid ID</param>
        /// <returns>Result with TB_Attachment type</returns>
        TB_Attachment GetAttachmentByID(string id);

        /// <summary>
        /// Save attachment to DB
        /// </summary>
        /// If attachment id is empty, then this is new attachment. Otherwise, this needs to be updated
        void Save(TB_Attachment attachment, string filePathPrefix, Stream fileStream);

        /// <summary>
        /// delete physical file
        /// </summary>
        void Delete(TB_Attachment attachment, string filePathPrefix);
    }
}
