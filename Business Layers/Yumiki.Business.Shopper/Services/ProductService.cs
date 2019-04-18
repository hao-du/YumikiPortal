using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Business.Base;
using Yumiki.Business.Shopper.Interfaces;
using Yumiki.Commons.Exceptions;
using Yumiki.Commons.Helpers;
using Yumiki.Data.Shopper.Interfaces;
using Yumiki.Entity.Shopper;

namespace Yumiki.Business.Shopper.Services
{
    public class ProductService : BaseService<IProductRepository>, IProductService
    {
        public List<TB_Product> GetProducts(bool showInactive)
        {
            return Repository.GetProducts(showInactive);
        }

        public TB_Product GetProduct(Guid productID)
        {
            return Repository.GetProduct(productID);
        }

        public void SaveProduct(TB_Product product)
        {
            Repository.SaveProduct(product);
        }
    }
}
