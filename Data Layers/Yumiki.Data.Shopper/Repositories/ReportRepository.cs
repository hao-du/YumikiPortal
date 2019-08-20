using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Data.Shopper.Interfaces;
using Yumiki.Entity.Shopper;
using System.Linq.Expressions;
using LinqKit;
using Yumiki.Entity.Shopper.ServiceObjects;
using Yumiki.Commons.Report;
using Yumiki.Commons.Dictionaries;

namespace Yumiki.Data.Shopper.Repositories
{
    public class ReportRepository : ShopperRepository, IReportRepository
    {
        public GetReportResponse GetRevenueReport(GetReportRequest request)
        {
            IQueryable<TB_Invoice> invoiceQuery = Context.TB_Invoice.Where(c => c.IsActive
                                                                        && (request.StartDate <= c.InvoiceDate && c.InvoiceDate <= request.EndDate))
                                                              .AsExpandable();

            IEnumerable<ReportObject> invoiceData = invoiceQuery
                                .AsEnumerable()
                                .GroupBy(c => string.Format("{0} {1}", request.CalculateTotal ? request.StartDate.ToString(Formats.DateTime.ServerShortYearMonth) : c.InvoiceDate.ToString(Formats.DateTime.ServerShortYearMonth)
                                                                    , request.SplitIncomeOutcomeView ? "Outcome" : string.Empty).Trim())
                                .OrderBy(c => c.Key)
                                .Select(c => new ReportObject(c.Key, -c.Sum(d => d.TotalAmount))).ToList();


            IQueryable<TB_Receipt> receiptQuery = Context.TB_Receipt.Where(c => c.IsActive
                                                                        && (request.StartDate <= c.ReceiptDate && c.ReceiptDate <= request.EndDate))
                                                              .AsExpandable();

            IEnumerable<ReportObject> receiptData = receiptQuery
                                .AsEnumerable()
                                .GroupBy(c => string.Format("{0} {1}", request.CalculateTotal ? request.StartDate.ToString(Formats.DateTime.ServerShortYearMonth) : c.ReceiptDate.ToString(Formats.DateTime.ServerShortYearMonth)
                                                                    , request.SplitIncomeOutcomeView ? "Income" : string.Empty).Trim())
                                .OrderBy(c => c.Key)
                                .Select(c => new ReportObject(c.Key, c.Sum(d => d.TotalAmount))).ToList();

            return new GetReportResponse()
            {
                Records = invoiceData.Concat(receiptData).OrderBy(c => c.Label)
            };
        }
    }
}
