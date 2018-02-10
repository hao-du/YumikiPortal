
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
    public class MD_LiveRecord
    {
        public MD_LiveRecord()
        {
            Fields = new List<MD_Field>();
            Attachments = new List<MD_Attachment>();
        }

        public string LiveRecordID { get; set; }
        public string ObjectID { get; set; }
        public IEnumerable<MD_Field> Fields { get; set; }
        public IEnumerable<MD_Attachment> Attachments { get; set; }
    }
}