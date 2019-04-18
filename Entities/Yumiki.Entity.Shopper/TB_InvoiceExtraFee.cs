namespace Yumiki.Entity.Shopper
{
    using System;
    using Yumiki.Entity.Base;

    public partial class TB_InvoiceExtraFee : IEntity
    {
        public Guid ID { get; set; }

        public Guid InvoiceID { get; set; }

        public Guid FeeTypeID { get; set; }

        public decimal Amount { get; set; }

        public Guid UserID { get; set; }

        public string Descriptions { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public virtual TB_FeeType FeeType { get; set; }

        public virtual TB_Invoice Invoice { get; set; }

        public virtual TB_User User { get; set; }
    }
}
