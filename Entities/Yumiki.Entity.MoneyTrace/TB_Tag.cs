namespace Yumiki.Entity.MoneyTrace
{
    using Base;
    using System;
    using System.Collections.Generic;

    public partial class TB_Tag : IEntity
    {
        public Guid ID { get; set; }

        public string TagName { get; set; }

        public string Descriptions { get; set; }

        public Guid? UserID { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public virtual TB_User TB_User { get; set; }
    }
}
