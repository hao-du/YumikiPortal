using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yumiki.Business.Base;
using Yumiki.Business.Shopper.Interfaces;
using Yumiki.Commons.Dictionaries;
using Yumiki.Commons.Exceptions;
using Yumiki.Commons.Helpers;
using Yumiki.Commons.Report;
using Yumiki.Commons.Settings;
using Yumiki.Data.Shopper.Interfaces;
using Yumiki.Entity.Shopper;
using Yumiki.Entity.Shopper.ServiceObjects;

namespace Yumiki.Business.Shopper.Services
{
    public class ReportService : BaseService<IReportRepository>, IReportService
    {
        public GetReportResponse GetRevenueReport(GetReportRequest request)
        {
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
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Start Date cannot be greater than End Date.", Logger);
            }

            return Repository.GetRevenueReport(request);
        }
    }
}
