namespace Yumiki.Entity.MoneyTrace
{
    using Commons.Dictionaries;
    using Commons.Helpers;
    using Resources;
    using System;

    public partial class TB_Trace
    {
        public string LastUpdateDateUI
        {
            get
            {
                return LastUpdateDate.HasValue ? LastUpdateDate.Value.ToLocalTime().ToString(DateTimeHelper.ShortDateTime) : CreateDate.ToLocalTime().ToString(DateTimeHelper.ShortDateTime);
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
                if(Currency != null)
                {
                    return Currency.CurrencyShortName;
                }
                return CommonValues.EmptyValue;
            }
        }

        public string BankName
        {
            get
            {
                if (Bank != null)
                {
                    return Bank.BankName;
                }
                return CommonValues.EmptyValue;
            }
        }

        public bool IsLogTrace
        {
            get
            {
                return GroupTokenID.HasValue && (TransactionType == EN_TransactionType.E_INCOME || TransactionType == EN_TransactionType.E_OUTCOME);
            }
        }

        public string TransactionTypeName
        {
            get
            {
                string transactionName = string.Empty;
                switch (TransactionType)
                {
                    case EN_TransactionType.E_INCOME:
                        transactionName = FieldNames.E_INCOME;
                        break;
                    case EN_TransactionType.E_OUTCOME:
                        transactionName = FieldNames.E_OUTCOME;
                        break;
                    case EN_TransactionType.E_BANKING:
                        transactionName = FieldNames.E_BANKING;
                        break;
                    case EN_TransactionType.E_EXCHANGE:
                        transactionName = FieldNames.E_EXCHANGE;
                        break;
                }
                return transactionName;
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
            public const string IsLogTrace = "IsLogTrace";

            public const string Bank = "Bank";
            public const string BankID = "BankID";
            public const string BankName = "BankName";

            public const string Currency = "Currency";
            public const string CurrencyID = "CurrencyID";
            public const string CurrencyName = "CurrencyName";

            public const string TransactionType = "TransactionType";
            public const string TransactionTypeName = "TransactionTypeName";

            public const string ExchangeAmount = "ExchangeAmount";
            public const string ExchangeCurrencyID = "ExchangeCurrencyID";
        }
    }
}
