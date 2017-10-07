using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Entity.Base;

namespace Yumiki.Entity.MoneyTrace.ServiceObjects
{
    public class GetReportRequest : PagingEntity<TraceReport> {
        public EN_ReportPeriodType ReportType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Tags { get; set; }
        public EN_TransactionType TransactionType { get; set; }
        public Guid CurrencyID { get; set; }
    }
}
