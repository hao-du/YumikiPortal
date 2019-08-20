namespace Yumiki.Entity.Shopper
{
    using System;
    using System.Collections.Generic;
    using Yumiki.Entity.Base;

    public partial class TB_ProductQuantityOffset : IEntity
    {
        public TB_ProductQuantityOffset()
        {
            Stocks = new HashSet<TB_Stock>();
        }

        public Guid ID { get; set; }

        public Guid ProductID { get; set; }

        public int Quantity { get; set; }

        public Guid UserID { get; set; }

        public string Descriptions { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public virtual TB_Product Product { get; set; }

        public virtual TB_User User { get; set; }

        public virtual ICollection<TB_Stock> Stocks { get; set; }
    }
}
