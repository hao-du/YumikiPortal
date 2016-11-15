namespace Yumiki.Entity.MoneyTrace
{
    using Base;
    using System;
    using System.Collections.Generic;

    public partial class TB_Trace : IEntity
    {
        public Guid ID { get; set; }

        public decimal Amount { get; set; }

        public DateTime TraceDate { get; set; }

        public Guid UserID { get; set; }

        public Guid CategoryID { get; set; }

        public Guid CurrencyID { get; set; }

        public string Descriptions { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public virtual TB_Category TB_Category { get; set; }

        public virtual TB_Currency TB_Currency { get; set; }

        public virtual TB_User TB_User { get; set; }
    }
}
