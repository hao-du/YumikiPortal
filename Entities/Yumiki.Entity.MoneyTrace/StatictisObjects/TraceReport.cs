using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Entity.Base;

namespace Yumiki.Entity.MoneyTrace
{
    public class TraceReport
    {
        public TraceReport(string label, decimal value)
        {
            Label = label;
            Value = value;
        }

        public string Label { get; private set; }
        public decimal Value { get; private set; }
    }
}
