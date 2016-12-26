namespace Yumiki.Entity.MoneyTrace
{
    using Base;
    using System;
    using System.Collections.Generic;

    public partial class TB_Currency : IEntity
    {
        public TB_Currency()
        {
            TB_Trace = new HashSet<TB_Trace>();
        }

        public Guid ID { get; set; }

        public string CurrencyName { get; set; }

        public string CurrencyShortName { get; set; }

        public Guid UserID { get; set; }

        public string Descriptions { get; set; }

        public bool IsShareable { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public virtual TB_User TB_User { get; set; }

        public virtual ICollection<TB_Trace> TB_Trace { get; set; }
    }
}
