using System;
using System.Collections.Generic;
using Yumiki.Entity.MoneyTrace;
using Yumiki.Entity.MoneyTrace.ServiceObjects;

namespace Yumiki.Business.MoneyTrace.Interfaces
{
    public interface IReportService
    {
        /// <summary>
        /// Get Trace Report
        /// </summary>
        /// <param name="request">Request contains filters</param>
        /// <returns>Report result with label/value</returns>
        GetReportResponse GetTraceReport(GetReportRequest request);
    }
}
