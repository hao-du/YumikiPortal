namespace Yumiki.Entity.MoneyTrace
{
    using Base;
    using System;
    using System.Collections.Generic;
    using Commons.Dictionaries;
    using Commons.Helpers;

    public partial class TB_Currency
    {
        public string LastUpdateDateUI
        {
            get
            {
                return LastUpdateDate.HasValue ? LastUpdateDate.Value.ToString(DateTimeHelper.ShortDateTime) : CreateDate.ToString(DateTimeHelper.ShortDateTime);
            }
        }

        public class FieldName
        {
            public const string TB_Currency = "TB_Currency";
            public const string CurrencyName = "CurrencyName";
            public const string CurrencyShortName = "CurrencyShortName";
            public const string IsShareable = "IsShareable";
        }
    }
}
