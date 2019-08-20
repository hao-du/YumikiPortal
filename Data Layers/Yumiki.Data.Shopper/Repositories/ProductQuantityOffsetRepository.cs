using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Data.Shopper.Interfaces;
using Yumiki.Entity.Shopper;

namespace Yumiki.Data.Shopper.Repositories
{
    public class ProductQuantityOffsetRepository : ShopperRepository, IProductQuantityOffsetRepository
    {
        public List<TB_ProductQuantityOffset> GetProductQuantityOffsets(bool showInactive, Guid? productID)
        {
            IQueryable<TB_ProductQuantityOffset> query = Context.TB_ProductQuantityOffset
                .Include(TB_ProductQuantityOffset.FieldName.Product)
                .Where(c => c.IsActive == !showInactive);

            if (productID.HasValue)
            {
                query = query.Where(c => c.ProductID == productID.Value);
            }

            return query.OrderByDescending(c => c.LastUpdateDate)
                .ToList();
        }

        public TB_ProductQuantityOffset GetProductQuantityOffset(Guid productQuantityOffsetID)
        {
            return Context.TB_ProductQuantityOffset
                .Include(TB_ProductQuantityOffset.FieldName.Product)
                .SingleOrDefault(c => c.ID == productQuantityOffsetID);
        }

        public void SaveProductQuantityOffset(TB_ProductQuantityOffset productQuantityOffset)
        {
            if (productQuantityOffset.ID == Guid.Empty)
            {
                Context.TB_ProductQuantityOffset.Add(productQuantityOffset);

                TB_Stock stock = new TB_Stock();
                stock.UserID = productQuantityOffset.UserID;
                stock.ProductID = productQuantityOffset.ProductID;
                stock.Quantity = productQuantityOffset.Quantity;
                productQuantityOffset.Stocks.Add(stock);
            }
            else
            {
                TB_ProductQuantityOffset dbProductQuantityOffset = Context.TB_ProductQuantityOffset
                    .Include(TB_ProductQuantityOffset.FieldName.Stocks)
                    .Single(c => c.ID == productQuantityOffset.ID);

                dbProductQuantityOffset.Quantity = productQuantityOffset.Quantity;
                dbProductQuantityOffset.ProductID = productQuantityOffset.ProductID;
                dbProductQuantityOffset.Descriptions = productQuantityOffset.Descriptions;
                dbProductQuantityOffset.IsActive = productQuantityOffset.IsActive;

                if (!dbProductQuantityOffset.IsActive)
                {
                    Context.TB_Stock.RemoveRange(dbProductQuantityOffset.Stocks);
                }
                else
                {
                    TB_Stock dbStock = dbProductQuantityOffset.Stocks.SingleOrDefault();
                    if (dbStock == null)
                    {
                        TB_Stock stock = new TB_Stock();
                        stock.UserID = productQuantityOffset.UserID;
                        stock.ProductID = productQuantityOffset.ProductID;
                        stock.Quantity = productQuantityOffset.Quantity;
                        productQuantityOffset.Stocks.Add(stock);
                    }
                    else
                    {
                        dbStock.ProductID = productQuantityOffset.ProductID;
                        dbStock.Quantity = productQuantityOffset.Quantity;
                    }
                }
            }

            Save();
        }
    }
}
