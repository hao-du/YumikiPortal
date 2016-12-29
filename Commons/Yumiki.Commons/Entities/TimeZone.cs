using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yumiki.Commons.Entities
{
    public class SystemTimeZone
    {
        public string ID { get; set; }
        public string DisplayName { get; set; }

        public class FieldName
        {
            public const string ID = "ID";
            public const string DisplayName = "DisplayName";
        }
    }
}
