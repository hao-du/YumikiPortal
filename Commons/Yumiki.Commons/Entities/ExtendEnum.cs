using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yumiki.Commons.Entities
{
    public class ExtendEnum
    {
        public string Text { get; set; }
        public string DisplayText { get; set; }
        public int Value { get; set; }

        public class FieldName
        {
            public const string Text = "Text";
            public const string Value = "Value";
            public const string DisplayText = "DisplayText";
        }
    }
}
