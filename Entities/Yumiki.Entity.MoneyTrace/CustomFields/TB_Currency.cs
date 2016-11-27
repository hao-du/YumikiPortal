namespace Yumiki.Entity.MoneyTrace
{
    using Base;
    using System;
    using System.Collections.Generic;
    using Commons.Dictionaries;

    public partial class TB_Currency : IEntity
    {
        public string LastUpdateDateUI
        {
            get
            {
                return LastUpdateDate.HasValue ? LastUpdateDate.Value.ToString(DateTimeFormat.ShortDateTime) : CreateDate.ToString(DateTimeFormat.ShortDateTime);
            }
        }

        public class FieldName
        {
            public const string TB_Currency = "TB_Currency";
            public const string CurrencyName = "CurrencyName";
            public const string CurrencyShortName = "CurrencyShortName";
        }
    }
}
