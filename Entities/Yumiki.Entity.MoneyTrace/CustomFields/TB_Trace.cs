namespace Yumiki.Entity.MoneyTrace
{
    using Commons.Dictionaries;
    using Commons.Helpers;

    public partial class TB_Trace
    {
        public string LastUpdateDateUI
        {
            get
            {
                return LastUpdateDate.HasValue ? LastUpdateDate.Value.ToString(DateTimeHelper.ShortDateTime) : CreateDate.ToString(DateTimeHelper.ShortDateTime);
            }
        }

        public string TraceDateUI
        {
            get
            {
                return TraceDate.ToString(DateTimeHelper.ShortDate);
            }
        }

        public string CurrencyName
        {
            get
            {
                if(TB_Currency != null)
                {
                    return string.Format("{0}-{1}", TB_Currency.CurrencyShortName, TB_Currency.CurrencyName);
                }
                return CommonValues.EmptyValue;
            }
        }

        public string CategoryName
        {
            get
            {
                if (TB_Category != null)
                {
                    return TB_Category.CategoryName;
                }
                return CommonValues.EmptyValue;
            }
        }

        public class FieldName
        {
            public const string TB_Trace = "TB_Trace";
            public const string Amount = "Amount";
            public const string TraceDate = "TraceDate";
            public const string TraceDateUI = "TraceDateUI";
            public const string CategoryID = "CategoryID";
            public const string CurrencyID = "CurrencyID";
            public const string CurrencyName = "CurrencyName";
            public const string CategoryName = "CategoryName";
        }
    }
}
