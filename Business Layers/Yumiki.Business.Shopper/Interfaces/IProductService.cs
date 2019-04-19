using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Business.Base;
using Yumiki.Business.Shopper.Interfaces;
using Yumiki.Commons.Exceptions;
using Yumiki.Data.Shopper.Interfaces;
using Yumiki.Entity.Shopper;

namespace Yumiki.Business.Shopper.Interfaces
{
    public interface IProductService
    {
        List<TB_Product> GetProducts(bool showInactive);

        List<TB_Product> GetProducts(string term);

        TB_Product GetProduct(string productID);

        void SaveProduct(TB_Product product);
    }
}
