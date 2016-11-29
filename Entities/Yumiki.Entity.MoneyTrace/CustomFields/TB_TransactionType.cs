namespace Yumiki.Entity.MoneyTrace
{
    using Base;
    using System;
    using System.Collections.Generic;
    using Commons.Dictionaries;

    public partial class TB_TransactionType
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
            public const string TB_TransactionType = "TB_TransactionType";
            public const string TransactionTypeName = "TransactionTypeName";
            public const string IsIncome = "IsIncome";
            public const string IsTransfer = "IsTransfer";
        }
    }
}
