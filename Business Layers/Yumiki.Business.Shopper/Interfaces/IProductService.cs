using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Business.Base;
using Yumiki.Business.Shopper.Interfaces;
using Yumiki.Commons.Entities.Parameters;
using Yumiki.Commons.Exceptions;
using Yumiki.Data.Shopper.Interfaces;
using Yumiki.Entity.Shopper;
using Yumiki.Entity.Shopper.ServiceObjects;

namespace Yumiki.Business.Shopper.Interfaces
{
    public interface IProductService
    {
        GetResponse<TB_Product> GetProducts(GetShopperRequest<TB_Product> request);

        GetResponse<TB_Product> GetProductsByTerm(GetShopperRequest<TB_Product> request);

        TB_Product GetProduct(string productID);

        void SaveProduct(TB_Product product);
    }
}
