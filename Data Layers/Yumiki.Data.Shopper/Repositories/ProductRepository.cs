using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Data.Shopper.Interfaces;
using Yumiki.Entity.Shopper;
using LinqKit;
using System.Linq.Expressions;

namespace Yumiki.Data.Shopper.Repositories
{
    public class ProductRepository : ShopperRepository, IProductRepository
    {
        public List<TB_Product> GetProducts(bool showInactive, string term)
        {
            Expression<Func<TB_Product, bool>> expression = PredicateBuilder.New<TB_Product>(true);
            expression = expression.And(c => c.IsActive != showInactive);

            if (!string.IsNullOrWhiteSpace(term))
            {
                expression = expression.And(c => c.ProductCode.Contains(term) || c.ProductName.Contains(term));
            }

            return Context.TB_Product.Include(TB_Product.FieldName.Stocks).Where(expression).ToList();
        }

        public TB_Product GetProduct(Guid productID)
        {
            return Context.TB_Product.Include(TB_Product.FieldName.Stocks).SingleOrDefault(c => c.ID == productID);
        }

        public void SaveProduct(TB_Product product)
        {
            if (product.ID == Guid.Empty)
            {
                Context.TB_Product.Add(product);
            }
            else
            {
                TB_Product dbProduct = Context.TB_Product.Single(c => c.ID == product.ID);
                dbProduct.ProductName = product.ProductName;
                dbProduct.ProductCode = product.ProductCode;
                dbProduct.OriginalPrice = product.OriginalPrice;
                dbProduct.Price = product.Price;
                dbProduct.FeaturedImage = product.FeaturedImage;
                dbProduct.SourceUrl = product.SourceUrl;
                dbProduct.Keywords = product.Keywords;

                dbProduct.Descriptions = product.Descriptions;
                dbProduct.IsActive = product.IsActive;
            }

            Save();
        }
    }
}
