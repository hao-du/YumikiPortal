namespace Yumiki.Entity.Shopper
{
    using System;
    using System.Collections.Generic;
    using Yumiki.Entity.Base;

    public partial class TB_Receipt : IEntity
    {
        public TB_Receipt()
        {
            ReceiptDetails = new HashSet<TB_ReceiptDetail>();
            ReceiptExtraFees = new HashSet<TB_ReceiptExtraFee>();
        }

        public Guid ID { get; set; }

        public string ExternalReceiptID { get; set; }

        public string ExternalReceiptUrl { get; set; }

        public decimal TotalAmount { get; set; }

        public DateTime ReceiptDate { get; set; }

        public int Status { get; set; }

        public Guid UserID { get; set; }

        public string Descriptions { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public virtual TB_User User { get; set; }

        public virtual ICollection<TB_ReceiptDetail> ReceiptDetails { get; set; }

        public virtual ICollection<TB_ReceiptExtraFee> ReceiptExtraFees { get; set; }
    }
}
