using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Business.Base;
using Yumiki.Business.WellCovered.Interfaces;
using Yumiki.Commons.Exceptions;
using Yumiki.Commons.Helpers;
using Yumiki.Data.WellCovered.Interfaces;
using Yumiki.Entity.WellCovered;

namespace Yumiki.Business.WellCovered.Services
{
    public class AttachmentService : BaseService<IAttachmentRepository>, IAttachmentService
    {
        /// <summary>
        /// Save attachment to DB
        /// </summary>
        /// <param name="app">
        /// If attachment id is empty, then this is new attachment. Otherwise, this needs to be updated
        /// NOTE: If inactive field is false, delete physical file
        public void Save(TB_Attachment attachment)
        {
            Repository.Save(attachment);
        }
    }
}
