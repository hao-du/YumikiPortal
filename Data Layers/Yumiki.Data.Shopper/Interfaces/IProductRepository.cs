using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Commons.Entities.Parameters;
using Yumiki.Data.Base;
using Yumiki.Entity.Shopper;
using Yumiki.Entity.Shopper.ServiceObjects;

namespace Yumiki.Data.Shopper.Interfaces
{
    public interface IProductRepository
    {
        GetResponse<TB_Product> GetProducts(GetShopperRequest<TB_Product> request);

        TB_Product GetProduct(Guid productID);

        void SaveProduct(TB_Product product);
    }
}
