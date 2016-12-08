namespace Yumiki.Entity.MoneyTrace
{
    using Base;
    using System;
    using System.Collections.Generic;
    using Commons.Dictionaries;
    using Commons.Helpers;

    public partial class TB_TransactionType
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
            public const string TB_TransactionType = "TB_TransactionType";
            public const string TransactionTypeName = "TransactionTypeName";
            public const string IsIncome = "IsIncome";
            public const string IsTransfer = "IsTransfer";
        }
    }
}
