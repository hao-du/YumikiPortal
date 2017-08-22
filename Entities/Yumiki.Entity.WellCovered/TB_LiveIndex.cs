using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Entity.Base;

namespace Yumiki.Entity.WellCovered
{
    public partial class TB_LiveIndex: IEntity
    {
        public Guid ID { get; set; }

        public Guid? AppID { get; set; }
        public Guid ObjectID { get; set; }
        public Guid LiveID { get; set; }

        public string FullTextIndex { get; set; }
        public string DisplayContents { get; set; }

        public Guid UserID { get; set; }
        public bool IsActive { get; set; }
        public string Descriptions { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
    }
}
