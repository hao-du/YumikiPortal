namespace Yumiki.Entity.MoneyTrace
{
    using Base;
    using System;
    using System.Collections.Generic;

    public partial class TB_Bank : IEntity
    {
        public TB_Bank()
        {
            Traces = new HashSet<TB_Trace>();
        }

        public Guid ID { get; set; }

        public string BankName { get; set; }

        public Guid UserID { get; set; }

        public string Descriptions { get; set; }

        public bool IsShareable { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public virtual ICollection<TB_Trace> Traces { get; set; }
    }
}
