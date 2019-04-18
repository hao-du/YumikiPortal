namespace Yumiki.Entity.Shopper
{
    using System;
    using System.Collections.Generic;
    using Yumiki.Entity.Base;

    public partial class TB_Product : IEntity
    {
        public TB_Product()
        {
            InvoiceDetails = new HashSet<TB_InvoiceDetail>();
            ReceiptDetails = new HashSet<TB_ReceiptDetail>();
        }

        public Guid ID { get; set; }

        public string ProductName { get; set; }

        public string ProductCode { get; set; }

        public decimal Price { get; set; }

        public string FeaturedImage { get; set; }

        public string SourceUrl { get; set; }

        public string Keywords { get; set; }

        public Guid UserID { get; set; }

        public string Descriptions { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public virtual ICollection<TB_InvoiceDetail> InvoiceDetails { get; set; }

        public virtual TB_User User { get; set; }

        public virtual ICollection<TB_ReceiptDetail> ReceiptDetails { get; set; }
    }
}
