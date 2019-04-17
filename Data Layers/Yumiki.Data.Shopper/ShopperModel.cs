namespace Yumiki.Entity.Shopper
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ShopperModel : DbContext
    {
        public ShopperModel()
            : base("name=ShopperModel")
        {
        }
    }
}
