﻿namespace Yumiki.Entity.Administration
{
    using Base;
    using System;
    using System.Collections.Generic;

    public partial class TB_Group : IEntity
    {
        public TB_Group()
        {
            TB_Privilege = new HashSet<TB_Privilege>();
            TB_User = new HashSet<TB_User>();
        }

        public Guid ID { get; set; }
        public string GroupName { get; set; }
        public string Descriptions { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public virtual ICollection<TB_Privilege> TB_Privilege { get; set; }
        public virtual ICollection<TB_User> TB_User { get; set; }
    }
}
