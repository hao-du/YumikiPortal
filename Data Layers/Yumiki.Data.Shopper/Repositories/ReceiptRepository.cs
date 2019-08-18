using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Data.Shopper.Interfaces;
using Yumiki.Entity.Shopper;

namespace Yumiki.Data.Shopper.Repositories
{
    public class ReceiptRepository : ShopperRepository, IReceiptRepository
    {
        public List<TB_Receipt> GetReceipts(bool showInactive)
        {
            return Context.TB_Receipt
                .Where(c => c.IsActive == !showInactive)
                .OrderByDescending(c => c.ReceiptDate)
                .ToList();
        }

        public TB_Receipt GetReceipt(Guid receiptID)
        {
            return Context.TB_Receipt
                .Include(TB_Receipt.FieldName.ReceiptDetails)
                .Include($"{TB_Receipt.FieldName.ReceiptDetails}.{TB_ReceiptDetail.FieldName.Product}")
                .Include(TB_Receipt.FieldName.ReceiptExtraFees)
                .Include($"{TB_Receipt.FieldName.ReceiptExtraFees}.{TB_ReceiptExtraFee.FieldName.FeeType}")
                .SingleOrDefault(c => c.ID == receiptID);
        }

        public void SaveReceipt(TB_Receipt receipt)
        {
            if (receipt.ID == Guid.Empty)
            {
                Context.TB_Receipt.Add(receipt);

                foreach (TB_ReceiptDetail detail in receipt.ReceiptDetails)
                {
                    if (receipt.Status == EN_ReceiptStatus.E_COMPLETED)
                    {
                        TB_Stock stock = new TB_Stock();

                        stock.UserID = detail.UserID;
                        stock.ProductID = detail.ProductID;
                        stock.Quantity = detail.Quantity;

                        detail.Stocks.Add(stock);
                    }
                }
            }
            else
            {
                TB_Receipt dbReceipt = Context.TB_Receipt
                    .Include(TB_Receipt.FieldName.ReceiptDetails)
                    .Include(TB_Receipt.FieldName.ReceiptExtraFees)
                    .Include($"{TB_Receipt.FieldName.ReceiptDetails}.{TB_ReceiptDetail.FieldName.Stocks}")
                    .Single(c => c.ID == receipt.ID);

                dbReceipt.ExternalReceiptID = receipt.ExternalReceiptID;
                dbReceipt.ExternalReceiptUrl = receipt.ExternalReceiptUrl;
                dbReceipt.TotalAmount = receipt.TotalAmount;
                dbReceipt.ReceiptDate = receipt.ReceiptDate;
                dbReceipt.Status = receipt.Status;

                dbReceipt.Descriptions = receipt.Descriptions;
                dbReceipt.IsActive = receipt.IsActive;

                Guid[] detailIDs = receipt.ReceiptDetails.Select(c => c.ID).ToArray();
                IEnumerable<TB_ReceiptDetail> deletingDetails = dbReceipt.ReceiptDetails.Where(c => !detailIDs.Contains(c.ID));

                Context.TB_ReceiptDetail.RemoveRange(deletingDetails);

                foreach (TB_ReceiptDetail detail in receipt.ReceiptDetails)
                {
                    if (detail.ID == Guid.Empty)
                    {
                        dbReceipt.ReceiptDetails.Add(detail);

                        if (dbReceipt.Status == EN_ReceiptStatus.E_COMPLETED)
                        {
                            TB_Stock stock = new TB_Stock();

                            stock.UserID = detail.UserID;
                            stock.ProductID = detail.ProductID;
                            stock.Quantity = detail.Quantity;

                            detail.Stocks.Add(stock);
                        }
                    }
                    else
                    {
                        TB_ReceiptDetail dbReceiptDetail = dbReceipt.ReceiptDetails.SingleOrDefault(c => c.ID == detail.ID);

                        if (dbReceiptDetail != null)
                        {
                            dbReceiptDetail.ProductID = detail.ProductID;
                            dbReceiptDetail.UnitPrice = detail.UnitPrice;
                            dbReceiptDetail.Quantity = detail.Quantity;
                            dbReceiptDetail.Amount = detail.Amount;
                            dbReceiptDetail.Descriptions = detail.Descriptions;

                            TB_Stock dbStock = dbReceiptDetail.Stocks.SingleOrDefault();
                            if (dbStock == null)
                            {
                                if (dbReceipt.Status == EN_ReceiptStatus.E_COMPLETED)
                                {
                                    dbStock = new TB_Stock();
                                    dbStock.UserID = detail.UserID;
                                    dbStock.ProductID = detail.ProductID;
                                    dbStock.Quantity = detail.Quantity;

                                    dbReceiptDetail.Stocks.Add(dbStock);
                                }
                            }
                            else
                            {
                                if (dbReceipt.Status == EN_ReceiptStatus.E_COMPLETED)
                                {
                                    dbStock.ProductID = detail.ProductID;
                                    dbStock.Quantity = detail.Quantity;
                                }
                                else
                                {
                                    Context.TB_Stock.RemoveRange(dbReceiptDetail.Stocks);
                                }
                            }
                        }
                    }
                }

                Guid[] extraFeeIDs = receipt.ReceiptExtraFees.Select(c => c.ID).ToArray();
                IEnumerable<TB_ReceiptExtraFee> deletingExtraFees = dbReceipt.ReceiptExtraFees.Where(c => !extraFeeIDs.Contains(c.ID));

                Context.TB_ReceiptExtraFee.RemoveRange(deletingExtraFees);

                foreach (TB_ReceiptExtraFee extraFee in receipt.ReceiptExtraFees)
                {
                    if (extraFee.ID == Guid.Empty)
                    {
                        dbReceipt.ReceiptExtraFees.Add(extraFee);
                    }
                    else
                    {
                        TB_ReceiptExtraFee dbExtraFee = dbReceipt.ReceiptExtraFees.SingleOrDefault(c => c.ID == extraFee.ID);

                        if (dbExtraFee != null)
                        {
                            dbExtraFee.FeeTypeID = extraFee.FeeTypeID;
                            dbExtraFee.Amount = extraFee.Amount;
                            dbExtraFee.Descriptions = extraFee.Descriptions;
                        }
                    }
                }
            }

            Save();
        }
    }
}
