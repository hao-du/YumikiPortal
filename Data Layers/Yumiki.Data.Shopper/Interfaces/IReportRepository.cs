using System;
using System.Collections.Generic;
using Yumiki.Entity.Shopper.ServiceObjects;

namespace Yumiki.Data.Shopper.Interfaces
{
    public interface IReportRepository
    {
        GetReportResponse GetRevenueReport(GetReportRequest request);
    }
}
