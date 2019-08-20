using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Yumiki.Commons.Dictionaries;
using Yumiki.Commons.Helpers;
using Yumiki.Data.MoneyTrace.Interfaces;
using Yumiki.Entity.MoneyTrace;
using Yumiki.Entity.MoneyTrace.ServiceObjects;
using System.Linq.Expressions;
using LinqKit;
using Yumiki.Commons.Report;

namespace Yumiki.Data.MoneyTrace.Repositories
{
    public class ReportRepository : MoneyTraceRepository, IReportRepository
    {
        /// <summary>
        /// Get Trace Report
        /// </summary>
        /// <param name="request">Request contains filters</param>
        /// <returns>Report result with label/value</returns>
        public GetReportResponse GetTraceReport(GetReportRequest request)
        {
            IQueryable<TB_Trace> reportData = Context.TB_Trace.Where(c => c.IsActive
                                                                        && c.UserID == request.UserID
                                                                        && c.CurrencyID == request.CurrencyID
                                                                        && (request.StartDate <= c.TraceDate && c.TraceDate <= request.EndDate))
                                                              .AsExpandable();

            if (!string.IsNullOrWhiteSpace(request.Tags))
            {
                //http://www.albahari.com/nutshell/predicatebuilder.aspx
                //Use false for Or, true for And
                Expression<Func<TB_Trace, bool>> tagExpression = PredicateBuilder.New<TB_Trace>(false);

                foreach (string tag in request.Tags.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    tagExpression = tagExpression.Or(c => c.Tags.Contains(tag));
                }

                reportData = reportData.Where(tagExpression);
            }

            if (request.TransactionTypes != null && request.TransactionTypes.Any())
            {
                Expression<Func<TB_Trace, bool>> transactionTypeExpression = PredicateBuilder.New<TB_Trace>(false);

                foreach (EN_TransactionType transaction in request.TransactionTypes)
                {
                    switch (transaction)
                    {
                        case EN_TransactionType.E_BANKING:
                            transactionTypeExpression = transactionTypeExpression.Or(c => c.TransactionType == EN_TransactionType.E_INCOME
                                                                                        && c.BankID != null
                                                                                        && c.GroupTokenID != null
                                                                                    );
                            break;
                        case EN_TransactionType.E_EXCHANGE:
                            transactionTypeExpression = transactionTypeExpression.Or(c => (c.TransactionType == EN_TransactionType.E_INCOME || c.TransactionType == EN_TransactionType.E_OUTCOME)
                                                                                        && c.BankID == null
                                                                                        && c.GroupTokenID != null
                                                                                    );
                            break;
                        default:
                            transactionTypeExpression = transactionTypeExpression.Or(c => c.TransactionType == transaction);
                            break;
                    }
                }

                reportData = reportData.Where(transactionTypeExpression);
            }

            string incomeLabel = "Income";
            string outcomeLabel = "Outcome";

            return new GetReportResponse()
            {
                Records = reportData
                                .AsEnumerable()
                                .GroupBy(c => string.Format("{0} {1}", request.CalculateTotal ? request.StartDate.ToString(Formats.DateTime.ServerShortYearMonth) : c.TraceDate.ToString(Formats.DateTime.ServerShortYearMonth)
                                                                    , request.SplitIncomeOutcomeView ? (c.Amount > 0 ? incomeLabel : outcomeLabel) : string.Empty).Trim())
                                .OrderBy(c => c.Key)
                                .Select(c => new ReportObject(c.Key, c.Sum(d => d.Amount))).ToList()
            };
        }
    }
}
