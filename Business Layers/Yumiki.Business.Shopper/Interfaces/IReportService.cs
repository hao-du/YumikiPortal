using System;
using System.Collections.Generic;
using Yumiki.Entity.Shopper;
using Yumiki.Entity.Shopper.ServiceObjects;

namespace Yumiki.Business.Shopper.Interfaces
{
    public interface IReportService
    {
        GetReportResponse GetRevenueReport(GetReportRequest request);
    }
}
