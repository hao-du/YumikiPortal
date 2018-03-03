
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Yumiki.Commons.Settings;
using Yumiki.Entity.Base;
using Yumiki.Entity.WellCovered;

namespace Yumiki.Web.WellCovered.Models
{
    public class MD_Attachment : Model<TB_Attachment>
    {
        public MD_Attachment()
        {
            this._internalItem = new TB_Attachment();
        }

        public MD_Attachment(TB_Attachment atttachment)
        {
            _internalItem = atttachment;
        }

        [Required]
        public string FileName
        {
            get
            {
                return _internalItem.AttachmentName;
            }
            set
            {
                _internalItem.AttachmentName = value;
            }
        }

        [Required]
        public string FilePath
        {
            get
            {
                return _internalItem.AttachmentPath;
            }
            set
            {
                _internalItem.AttachmentPath = value;
            }
        }

        [Required]
        public Guid  LiveRecordID
        {
            get
            {
                return _internalItem.LiveRecordID;
            }
            set
            {
                _internalItem.LiveRecordID = value;
            }
        }
    }
}