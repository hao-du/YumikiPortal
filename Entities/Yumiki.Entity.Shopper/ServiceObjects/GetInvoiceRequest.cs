using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Entity.Base;

namespace Yumiki.Entity.Shopper.ServiceObjects
{
    public class GetShopperRequest<T> : PagingEntity<T> where T : IEntity
    {
        public bool ShowInactive { get; set; } = false;
        public Guid UserID { get; set; }
        public Guid ProductID { get; set; }
        public string ProductTerm { get; set; }
    }
}
