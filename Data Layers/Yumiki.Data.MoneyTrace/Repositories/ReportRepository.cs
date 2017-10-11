using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Yumiki.Commons.Dictionaries;
using Yumiki.Commons.Helpers;
using Yumiki.Data.MoneyTrace.Interfaces;
using Yumiki.Entity.MoneyTrace;
using Yumiki.Entity.MoneyTrace.ServiceObjects;

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
            IEnumerable<TraceReport> report = new List<TraceReport>();
            switch (request.ReportType)
            {
                case EN_ReportType.E_YEAR:
                    break;
                case EN_ReportType.E_PERIOD:
                    break;
                case EN_ReportType.E_MONTH:
                    report = Context.TB_Trace.Where(c => c.IsActive
                                            && c.UserID == request.UserID
                                            && c.CurrencyID == request.CurrencyID
                                            && (
                                                    //Only INCOME, OUTCOME AND TRANSFER IS ACTUAL MONNEY
                                                    c.TransactionType == EN_TransactionType.E_INCOME
                                                    || c.TransactionType == EN_TransactionType.E_OUTCOME
                                                    || c.TransactionType == EN_TransactionType.E_TRANSFER
                                                ))
                                    .AsEnumerable()
                                    .GroupBy(c => c.TraceDate.ToString(Formats.DateTime.ServerShortYearMonth))
                                    .OrderBy(c=>c.Key)
                                    .Select(c => new TraceReport() { Label = c.First().TraceDate.ToString(Formats.DateTime.ServerShortMonthYear)
                                                                    , Value = c.Sum(d=>d.Amount)
                                                                    });
                    break;
            }

            return new GetReportResponse()
            {
                Records = report
            };
        }
    }
}
