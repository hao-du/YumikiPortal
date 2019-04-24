namespace Yumiki.Entity.Shopper
{
    using System;
    using Yumiki.Entity.Base;

    public partial class TB_Stock : IEntity
    {
        public Guid ID { get; set; }

        public Guid ProductID { get; set; }

        public Guid? InvoiceDetailID { get; set; }

        public Guid? ReceiptDetailID { get; set; }

        public int Quantity { get; set; }

        public Guid UserID { get; set; }

        public string Descriptions { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public virtual TB_Product Product { get; set; }

        public virtual TB_InvoiceDetail InvoiceDetail { get; set; }

        public virtual TB_ReceiptDetail ReceiptDetail { get; set; }
    }
}
