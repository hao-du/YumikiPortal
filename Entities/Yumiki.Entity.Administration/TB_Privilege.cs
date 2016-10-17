namespace Yumiki.Entity.Administration
{
    using Base;
    using System;
    using System.Collections.Generic;

    public partial class TB_Privilege : IEntity
    {
        public TB_Privilege()
        {
            TB_Privilege1 = new HashSet<TB_Privilege>();
            TB_Group = new HashSet<TB_Group>();
        }

        public Guid ID { get; set; }
        public string PrivilegeName { get; set; }
        public string PagePath { get; set; }
        public bool IsDisplayable { get; set; }
        public Guid? ParentPrivilegeID { get; set; }
        public string Descriptions { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public virtual ICollection<TB_Privilege> TB_Privilege1 { get; set; }
        public virtual TB_Privilege TB_Privilege2 { get; set; }
        public virtual ICollection<TB_Group> TB_Group { get; set; }
    }
}
