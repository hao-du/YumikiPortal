namespace Yumiki.Entity.Shopper
{
    using System;
    using System.Collections.Generic;
    using Yumiki.Entity.Base;

    public partial class TB_InvoiceDetail : IEntity
    {
        public TB_InvoiceDetail()
        {
            Stock = new HashSet<TB_Stock>();
        }

        public Guid ID { get; set; }

        public Guid InvoiceID { get; set; }

        public Guid ProductID { get; set; }

        public decimal OriginalPrice { get; set; }

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        public decimal Amount { get; set; }

        public Guid UserID { get; set; }

        public string Descriptions { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public virtual TB_Invoice Invoice { get; set; }

        public virtual TB_Product Product { get; set; }

        public virtual TB_User User { get; set; }

        public virtual ICollection<TB_Stock> Stock { get; set; }
    }
}
