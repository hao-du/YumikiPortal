using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Entity.Base;

namespace Yumiki.Entity.MoneyTrace.ServiceObjects
{
    public class GetReportRequest : PagingEntity<TraceReport> {
        public EN_ReportType ReportType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Tags { get; set; }
        public EN_TransactionType[] TransactionType { get; set; }
        public Guid UserID { get; set; }
        public Guid CurrencyID { get; set; }

        public class FieldName
        {
            public const string ReportType = "ReportType";
            public const string StartDate = "StartDate";
            public const string EndDate = "EndDate";
            public const string Tags = "Tags";
            public const string TransactionType = "TransactionType";
            public const string CurrencyID = "CurrencyID";
        }
    }
}
