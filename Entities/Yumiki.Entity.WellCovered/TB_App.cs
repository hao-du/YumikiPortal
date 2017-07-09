namespace Yumiki.Entity.WellCovered
{
    using System;
    using System.Collections.Generic;

    public partial class TB_App
    {
        public TB_App()
        {
            Objects = new HashSet<TB_Object>();
        }
        public Guid ID { get; set; }
        public string AppName { get; set; }
        public string Descriptions { get; set; }
        public Guid UserID { get; set; }
        public bool IsShareable { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public virtual ICollection<TB_Object> Objects { get; set; }
        public virtual TB_User User { get; set; }
    }
}
