namespace Yumiki.Entity.MoneyTrace
{
    using Commons.Dictionaries;
    using Commons.Helpers;
    using System;

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

        public decimal ExchangeAmount { get; set; }

        public Guid? ExchangeCurrencyID { get; set; }

        public class FieldName
        {
            public const string TB_Trace = "TB_Trace";
            public const string Amount = "Amount";
            public const string TraceDate = "TraceDate";
            public const string TraceDateUI = "TraceDateUI";
            public const string Tags = "Tags";
            public const string CurrencyID = "CurrencyID";
            public const string CurrencyName = "CurrencyName";
            public const string ExchangeAmount = "ExchangeAmount";
            public const string ExchangeCurrencyID = "ExchangeCurrencyID";
        }
    }
}
