namespace Yumiki.Commons.Dictionaries
{
    public class Formats
    {
        public class DateTime
        {
            public const string ShortDate = "MM/dd/yyyy";
            public const string LongDate = "dd MMM yyyy";
            public const string ShortDateTime = "MM/dd/yyyy HH:mm:ss";
            public const string LongDateTime = "dd MMM yyyy HH:mm:ss";
            public const string LongDateTime2 = "dd MMM yyyy HH:mm";
            public const string ClientMomentLongDateTime = "DD MMM YYYY HH:mm";
            public const string ClientMomentLongDate = "DD MMM YYYY";
            public const string MonthYear = "MM-YYYY";
            public const string Hour = "HH:ss";
           
        }

        public class Number
        {
            public const string Integer = "#,##0.##";
            public const string Decimal = "#,##0.##";
        }
    }
}
