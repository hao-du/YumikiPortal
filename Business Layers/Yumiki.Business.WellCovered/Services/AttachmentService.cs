using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Business.Base;
using Yumiki.Business.WellCovered.Interfaces;
using Yumiki.Commons.Exceptions;
using Yumiki.Commons.Helpers;
using Yumiki.Data.WellCovered.Interfaces;
using Yumiki.Entity.WellCovered;
using Yumiki.Commons.Storage;

namespace Yumiki.Business.WellCovered.Services
{
    public class AttachmentService : BaseService<IAttachmentRepository>, IAttachmentService
    {
        /// <summary>
        /// Return specific attachment by id
        /// </summary>
        /// <param name="id">TB_Attachment Guid ID</param>
        /// <returns>Result with TB_Attachment type</returns>
        public TB_Attachment GetAttachmentByID(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Attachment ID cannot be empty.", Logger);
            }

            Guid convertedID = Guid.Empty;
            Guid.TryParse(id, out convertedID);
            if (convertedID == Guid.Empty)
            {
                throw new YumikiException(ExceptionCode.E_WRONG_TYPE, "Attachment ID must be GUID type.", Logger);
            }

            return Repository.GetAttachmentByID(convertedID);
        }

        /// <summary>
        /// Save attachment to DB
        /// </summary>
        /// <param name="app">
        /// If attachment id is empty, then this is new attachment. Otherwise, this needs to be updated
        /// NOTE: If inactive field is false, delete physical file
        public void Save(TB_Attachment attachment, string filePathPrefix, Stream fileStream)
        {
            if (attachment.ID == Guid.Empty)
            {
                attachment.ID = Guid.NewGuid();
            }

            attachment.AttachmentPath = FileSystem.GenerateFilePath(attachment.ID, attachment.AttachmentName);

            Repository.Save(attachment);

            FileSystem.Save(attachment.ID, filePathPrefix, attachment.AttachmentName, fileStream);
        }

        /// <summary>
        /// delete physical file
        /// </summary>
        public void Delete(TB_Attachment attachment, string filePathPrefix)
        {
            attachment.IsActive = false;

            Repository.Save(attachment);

            FileSystem.Delete(attachment.ID, filePathPrefix, attachment.AttachmentName);
        }
    }
}
