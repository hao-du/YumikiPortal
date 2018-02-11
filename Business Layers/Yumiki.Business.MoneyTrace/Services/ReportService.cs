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
            if(request.CurrencyID == Guid.Empty)
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Currency is required.");
            }

            switch (request.ReportType)
            {
                case EN_ReportType.E_MONTH:
                    request.StartDate = request.StartDate.GetStartDateOfMonth();
                    request.EndDate = request.StartDate.GetEndDateOfMonth();
                    break;
                case EN_ReportType.E_YEAR:
                    request.StartDate = request.StartDate.GetStartDateOfYear();
                    request.EndDate = request.StartDate.GetEndDateOfYear();
                    break;
                case EN_ReportType.E_PERIOD:
                    request.StartDate = request.StartDate.GetDateWithBeginOfDayTime();
                    request.EndDate = request.EndDate.GetDateWithEndOfDayTime();
                    break;
            }

            if (request.StartDate > request.EndDate)
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Start Date cannot be greater than End Date.");
            }

            return Repository.GetTraceReport(request);
        }
    }
}
