using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Data.Shopper.Interfaces;
using Yumiki.Entity.Shopper;

namespace Yumiki.Data.Shopper.Repositories
{
    public class InvoiceRepository : ShopperRepository, IInvoiceRepository
    {
        public List<TB_Invoice> GetInvoices(bool showInactive)
        {
            return Context.TB_Invoice
                .Where(c => c.IsActive == !showInactive)
                .OrderByDescending(c => c.InvoiceDate)
                .ToList();
        }

        public TB_Invoice GetInvoice(Guid invoiceID)
        {
            return Context.TB_Invoice
                .Include(TB_Invoice.FieldName.InvoiceDetails)
                .Include($"{TB_Invoice.FieldName.InvoiceDetails}.{TB_InvoiceDetail.FieldName.Product}")
                .Include(TB_Invoice.FieldName.InvoiceExtraFees)
                .Include($"{TB_Invoice.FieldName.InvoiceExtraFees}.{TB_InvoiceExtraFee.FieldName.FeeType}")
                .SingleOrDefault(c => c.ID == invoiceID);
        }
        public List<TB_InvoiceDetail> GetInvoicesByProductID(Guid productID)
        {
            return Context.TB_InvoiceDetail
                .Include(TB_InvoiceDetail.FieldName.Invoice)
                .Where(c => c.ProductID == productID)
                .OrderByDescending(c => c.Invoice.InvoiceDate)
                .ToList();
        }

        public void SaveInvoice(TB_Invoice invoice)
        {
            if (invoice.ID == Guid.Empty)
            {
                Context.TB_Invoice.Add(invoice);

                foreach (TB_InvoiceDetail detail in invoice.InvoiceDetails)
                {
                    if (invoice.Status != EN_InvoiceStatus.E_CANCELLED)
                    {
                        TB_Stock stock = new TB_Stock();

                        stock.UserID = detail.UserID;
                        stock.ProductID = detail.ProductID;
                        stock.Quantity = -detail.Quantity;

                        detail.Stocks.Add(stock);
                    }
                }
            }
            else
            {
                TB_Invoice dbInvoice = Context.TB_Invoice
                    .Include(TB_Invoice.FieldName.InvoiceDetails)
                    .Include(TB_Invoice.FieldName.InvoiceExtraFees)
                    .Include($"{TB_Invoice.FieldName.InvoiceDetails}.{TB_InvoiceDetail.FieldName.Stocks}")
                    .Single(c => c.ID == invoice.ID);

                dbInvoice.InvoiceNumber = invoice.InvoiceNumber;
                dbInvoice.CustomerName = invoice.CustomerName;
                dbInvoice.CustomerAddress = invoice.CustomerAddress;
                dbInvoice.CustomerPhone = invoice.CustomerPhone;
                dbInvoice.CustomerEmail = invoice.CustomerEmail;
                dbInvoice.CustomerNote = invoice.CustomerNote;

                dbInvoice.TotalAmount = invoice.TotalAmount;
                dbInvoice.InvoiceDate = invoice.InvoiceDate;
                dbInvoice.Status = invoice.Status;

                dbInvoice.Descriptions = invoice.Descriptions;
                dbInvoice.IsActive = invoice.IsActive;

                Guid[] detailIDs = invoice.InvoiceDetails.Select(c => c.ID).ToArray();
                IEnumerable<TB_InvoiceDetail> deletingDetails = dbInvoice.InvoiceDetails.Where(c => !detailIDs.Contains(c.ID));

                Context.TB_InvoiceDetail.RemoveRange(deletingDetails);

                foreach (TB_InvoiceDetail detail in invoice.InvoiceDetails)
                {
                    if (detail.ID == Guid.Empty)
                    {
                        dbInvoice.InvoiceDetails.Add(detail);

                        if (dbInvoice.Status != EN_InvoiceStatus.E_CANCELLED)
                        {
                            TB_Stock stock = new TB_Stock();
                            detail.Stocks.Add(stock);

                            stock.UserID = detail.UserID;
                            stock.ProductID = detail.ProductID;
                            stock.Quantity = -detail.Quantity;
                        }
                    }
                    else
                    {
                        TB_InvoiceDetail dbInvoiceDetail = dbInvoice.InvoiceDetails.SingleOrDefault(c => c.ID == detail.ID);

                        if (dbInvoiceDetail != null)
                        {
                            dbInvoiceDetail.ProductID = detail.ProductID;
                            dbInvoiceDetail.OriginalPrice = detail.OriginalPrice;
                            dbInvoiceDetail.UnitPrice = detail.UnitPrice;
                            dbInvoiceDetail.Quantity = detail.Quantity;
                            dbInvoiceDetail.Amount = detail.Amount;
                            dbInvoiceDetail.Descriptions = detail.Descriptions;

                            TB_Stock dbStock = dbInvoiceDetail.Stocks.SingleOrDefault();
                            if (dbStock == null)
                            {
                                if (dbInvoice.Status != EN_InvoiceStatus.E_CANCELLED)
                                {
                                    dbStock = new TB_Stock();
                                    dbStock.UserID = detail.UserID;
                                    dbStock.ProductID = detail.ProductID;
                                    dbStock.Quantity = -detail.Quantity;

                                    dbInvoiceDetail.Stocks.Add(dbStock);
                                }
                            }
                            else
                            {
                                if (dbInvoice.Status != EN_InvoiceStatus.E_CANCELLED)
                                {
                                    dbStock.ProductID = detail.ProductID;
                                    dbStock.Quantity = -detail.Quantity;
                                }
                                else
                                {
                                    Context.TB_Stock.RemoveRange(dbInvoiceDetail.Stocks);
                                }
                            }
                        }
                    }
                }

                Guid[] extraFeeIDs = invoice.InvoiceExtraFees.Select(c => c.ID).ToArray();
                IEnumerable<TB_InvoiceExtraFee> deletingExtraFees = dbInvoice.InvoiceExtraFees.Where(c => !extraFeeIDs.Contains(c.ID));

                Context.TB_InvoiceExtraFee.RemoveRange(deletingExtraFees);

                foreach (TB_InvoiceExtraFee extraFee in invoice.InvoiceExtraFees)
                {
                    if (extraFee.ID == Guid.Empty)
                    {
                        dbInvoice.InvoiceExtraFees.Add(extraFee);
                    }
                    else
                    {
                        TB_InvoiceExtraFee dbExtraFee = dbInvoice.InvoiceExtraFees.SingleOrDefault(c => c.ID == extraFee.ID);

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
