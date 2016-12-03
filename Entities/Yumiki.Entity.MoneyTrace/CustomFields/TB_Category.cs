namespace Yumiki.Entity.MoneyTrace
{
    using Base;
    using Commons.Dictionaries;
    using System;
    using System.Collections.Generic;

    public partial class TB_Category
    {
        public string LastUpdateDateUI
        {
            get
            {
                return LastUpdateDate.HasValue ? LastUpdateDate.Value.ToString(DateTimeFormat.ShortDateTime) : CreateDate.ToString(DateTimeFormat.ShortDateTime);
            }
        }

        public string TransactionTypeName
        {
            get
            {
                if(TB_TransactionType != null)
                {
                    return TB_TransactionType.TransactionTypeName;
                }
                return string.Empty;
            }
        }

        public class FieldName
        {
            public const string TB_Category = "TB_Category";
            public const string CategoryName = "CategoryName";
            public const string TransactionTypeName = "TransactionTypeName";
            public const string TransactionTypeID = "TransactionTypeID";
        }
    }
}
