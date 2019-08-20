using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Commons.Report;
using Yumiki.Entity.Base;

namespace Yumiki.Entity.Shopper.ServiceObjects
{
    public class GetReportRequest : PagingEntity<ReportObject> {
        public EN_ReportType ReportType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool CalculateTotal { get; set; }
        public bool SplitIncomeOutcomeView { get; set; }
        public class FieldName
        {
            public const string ReportType = "ReportType";
            public const string StartDate = "StartDate";
            public const string EndDate = "EndDate";
            public const string CalculateTotal = "CalculateTotal";
            public const string SplitIncomeOutcomeView = "SplitIncomeOutcomeView";
        }
    }
}
