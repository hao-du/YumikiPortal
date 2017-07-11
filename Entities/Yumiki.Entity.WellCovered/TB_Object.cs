namespace Yumiki.Entity.WellCovered
{
    using System;
    using System.Collections.Generic;
    using Yumiki.Entity.Base;

    public partial class TB_Object : IEntity
    {
        public TB_Object()
        {
            Fields = new HashSet<TB_Field>();
        }

        public Guid ID { get; set; }
        public string ObjectName { get; set; }
        public string DisplayName { get; set; }
        public string ApiName { get; set; }
        public Guid? AppID { get; set; }
        public string Descriptions { get; set; }
        public Guid UserID { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public virtual TB_App App { get; set; }
        public virtual ICollection<TB_Field> Fields { get; set; }
    }
}
