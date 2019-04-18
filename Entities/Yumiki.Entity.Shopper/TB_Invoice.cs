namespace Yumiki.Entity.Shopper
{
    using System;
    using System.Collections.Generic;
    using Yumiki.Entity.Base;

    public partial class TB_Invoice : IEntity
    {
        public TB_Invoice()
        {
            InvoiceDetails = new HashSet<TB_InvoiceDetail>();
            InvoiceExtraFees = new HashSet<TB_InvoiceExtraFee>();
        }

        public Guid ID { get; set; }

        public string InvoiceNumber { get; set; }

        public string CustomerName { get; set; }

        public string CustomerAddress { get; set; }

        public string CustomerPhone { get; set; }

        public string CustomerEmail { get; set; }

        public string CustomerNote { get; set; }

        public decimal TotalAmount { get; set; }

        public DateTime InvoiceDate { get; set; }

        public int Status { get; set; }

        public Guid UserID { get; set; }

        public string Descriptions { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public virtual TB_User User { get; set; }

        public virtual ICollection<TB_InvoiceDetail> InvoiceDetails { get; set; }

        public virtual ICollection<TB_InvoiceExtraFee> InvoiceExtraFees { get; set; }
    }
}
