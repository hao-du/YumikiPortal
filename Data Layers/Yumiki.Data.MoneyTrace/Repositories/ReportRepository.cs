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
            switch (request.ReportType)
            {
                case EN_ReportPeriodType.E_YEAR:
                    break;
                case EN_ReportPeriodType.E_PERIOD:
                    break;
                case EN_ReportPeriodType.E_MONTH:
                default:

                    break;
            }

            return null;
        }
    }
}
