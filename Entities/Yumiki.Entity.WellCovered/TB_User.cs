namespace Yumiki.Entity.WellCovered
{
    using System;
    using System.Collections.Generic;

    public partial class TB_User
    {
        public TB_User()
        {
            Apps = new HashSet<TB_App>();
            Fields = new HashSet<TB_Field>();
        }
        public Guid ID { get; set; }
        public string UserLoginName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TimeZone { get; set; }
        public string Descriptions { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public virtual ICollection<TB_App> Apps { get; set; }
        public virtual ICollection<TB_Field> Fields { get; set; }
    }
}
