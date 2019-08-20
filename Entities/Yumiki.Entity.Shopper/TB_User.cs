namespace Yumiki.Entity.Shopper
{
    using System;
    using System.Collections.Generic;
    using Yumiki.Entity.Base;

    public partial class TB_User : IEntity
    {
        public TB_User()
        {
            AdditionalFees = new HashSet<TB_AdditionalFee>();
            FeeTypes = new HashSet<TB_FeeType>();
            Invoices = new HashSet<TB_Invoice>();
            InvoiceDetails = new HashSet<TB_InvoiceDetail>();
            InvoiceExtraFees = new HashSet<TB_InvoiceExtraFee>();
            Products = new HashSet<TB_Product>();
            Receipts = new HashSet<TB_Receipt>();
            ReceiptDetails = new HashSet<TB_ReceiptDetail>();
            ReceiptExtraFees = new HashSet<TB_ReceiptExtraFee>();
            Stocks = new HashSet<TB_Stock>();
            ProductQuantityOffsets = new HashSet<TB_ProductQuantityOffset>();
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

        public virtual ICollection<TB_Stock> Stocks { get; set; }

        public virtual ICollection<TB_AdditionalFee> AdditionalFees { get; set; }

        public virtual ICollection<TB_FeeType> FeeTypes { get; set; }

        public virtual ICollection<TB_Invoice> Invoices { get; set; }

        public virtual ICollection<TB_InvoiceDetail> InvoiceDetails { get; set; }

        public virtual ICollection<TB_InvoiceExtraFee> InvoiceExtraFees { get; set; }

        public virtual ICollection<TB_Product> Products { get; set; }

        public virtual ICollection<TB_Receipt> Receipts { get; set; }

        public virtual ICollection<TB_ReceiptDetail> ReceiptDetails { get; set; }

        public virtual ICollection<TB_ProductQuantityOffset> ProductQuantityOffsets { get; set; }

        public virtual ICollection<TB_ReceiptExtraFee> ReceiptExtraFees { get; set; }
    }
}
