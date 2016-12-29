using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yumiki.Entity.MoneyTrace
{
    public class TraceSummary
    {
        public string CurrencyShortName { get; set; }
        public decimal TotalAmount { get; set; }

        public class FieldName
        {
            public const string CurrencyShortName = "CurrencyShortName";
            public const string TotalAmount = "TotalAmount";
        }
    }
}
