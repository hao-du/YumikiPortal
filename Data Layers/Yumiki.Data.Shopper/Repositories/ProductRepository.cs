using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Data.Shopper.Interfaces;
using Yumiki.Entity.Shopper;
using LinqKit;
using System.Linq.Expressions;
using Yumiki.Entity.Shopper.ServiceObjects;
using Yumiki.Commons.Entities.Parameters;

namespace Yumiki.Data.Shopper.Repositories
{
    public class ProductRepository : ShopperRepository, IProductRepository
    {
        public GetResponse<TB_Product> GetProducts(GetShopperRequest<TB_Product> request)
        {
            Expression<Func<TB_Product, bool>> expression = PredicateBuilder.New<TB_Product>(true);
            expression = expression.And(c => c.IsActive != request.ShowInactive);

            if (!string.IsNullOrWhiteSpace(request.ProductTerm))
            {
                expression = expression.And(c => c.ProductCode.Contains(request.ProductTerm) || c.ProductName.Contains(request.ProductTerm));
            }

            IQueryable<TB_Product> query = Context.TB_Product.Include(TB_Product.FieldName.Stocks).Where(expression).OrderByDescending(c => c.LastUpdateDate);

            int count = 0;
            if (request.EnablePaging)
            {
                count = query.Count();
                query = query.Skip((request.CurrentPage - 1) * request.ItemsPerPage).Take(request.ItemsPerPage);
            }

            return new GetResponse<TB_Product>
            {
                Records = query.ToList(),
                CurrentPage = request.CurrentPage,
                ItemsPerPage = request.ItemsPerPage,
                TotalItems = count
            };
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
