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

        public TB_Product GetProduct(string productID)
        {
            if (string.IsNullOrWhiteSpace(productID))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Product ID cannot be empty.", Logger);
            }

            Guid convertedProductID = Guid.Empty;
            Guid.TryParse(productID, out convertedProductID);
            if (convertedProductID == Guid.Empty)
            {
                throw new YumikiException(ExceptionCode.E_WRONG_TYPE, "Product ID must be GUID type.", Logger);
            }

            return Repository.GetProduct(convertedProductID);
        }

        public void SaveProduct(TB_Product product)
        {
            if (string.IsNullOrWhiteSpace(product.ProductCode))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Product Code is required.", Logger);
            }

            if (string.IsNullOrWhiteSpace(product.ProductName))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Product Name is required.", Logger);
            }

            if (string.IsNullOrWhiteSpace(product.FeaturedImage))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Featured Image is required.", Logger);
            }

            if (product.Price <= decimal.Zero)
            {
                throw new YumikiException(ExceptionCode.E_WRONG_VALUE, "Price must be greater than 0.", Logger);
            }

            Repository.SaveProduct(product);
        }
    }
}
