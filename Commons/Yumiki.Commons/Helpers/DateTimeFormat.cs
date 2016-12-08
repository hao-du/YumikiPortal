using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yumiki.Commons.Helpers
{
    public class DateTimeHelper
    {
        public const string ShortDate = "MM/dd/yyyy";
        public const string LongDate = "dd MMM yyyy";
        public const string ShortDateTime = "MM/dd/yyyy HH:mm:ss";
        public const string LongDateTime = "dd MMM yyyy HH:mm:ss";

        public static DateTime GetSystemDatetime
        {
            get
            {
                return DateTime.Now;
            }
        }
    }
}
