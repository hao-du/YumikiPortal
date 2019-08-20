using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Entity.Base;

namespace Yumiki.Commons.Report
{
    public class ReportObject
    {
        public ReportObject(string label, decimal value)
        {
            Label = label;
            Value = value;
        }

        public string Label { get; private set; }
        public decimal Value { get; private set; }
    }
}
