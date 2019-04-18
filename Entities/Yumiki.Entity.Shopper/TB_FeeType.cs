namespace Yumiki.Entity.Shopper
{
    using System;
    using System.Collections.Generic;
    using Yumiki.Entity.Base;

    public partial class TB_FeeType : IEntity
    {
        public TB_FeeType()
        {
            AdditionalFees = new HashSet<TB_AdditionalFee>();
            InvoiceExtraFees = new HashSet<TB_InvoiceExtraFee>();
            ReceiptExtraFees = new HashSet<TB_ReceiptExtraFee>();
        }

        public Guid ID { get; set; }

        public string FeeTypeName { get; set; }

        public bool ShowInReceipt { get; set; }

        public bool ShowInInvoice { get; set; }

        public bool ShowInAdditionalFee { get; set; }

        public Guid UserID { get; set; }

        public string Descriptions { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public virtual ICollection<TB_AdditionalFee> AdditionalFees { get; set; }

        public virtual TB_User User { get; set; }

        public virtual ICollection<TB_InvoiceExtraFee> InvoiceExtraFees { get; set; }

        public virtual ICollection<TB_ReceiptExtraFee> ReceiptExtraFees { get; set; }
    }
}
