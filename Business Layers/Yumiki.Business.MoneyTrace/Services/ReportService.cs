using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yumiki.Business.Base;
using Yumiki.Business.MoneyTrace.Interfaces;
using Yumiki.Commons.Dictionaries;
using Yumiki.Commons.Exceptions;
using Yumiki.Commons.Helpers;
using Yumiki.Commons.Settings;
using Yumiki.Data.MoneyTrace.Interfaces;
using Yumiki.Entity.MoneyTrace;
using Yumiki.Entity.MoneyTrace.ServiceObjects;

namespace Yumiki.Business.MoneyTrace.Services
{
    public class ReportService : BaseService<IReportRepository>, IReportService
    {
        /// <summary>
        /// Get Trace Report
        /// </summary>
        /// <param name="request">Request contains filters</param>
        /// <returns>Report result with label/value</returns>
        public GetReportResponse GetTraceReport(GetReportRequest request)
        {
            return Repository.GetTraceReport(request);
        }
    }
}
