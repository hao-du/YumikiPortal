namespace Yumiki.Entity.MoneyTrace
{
    using Commons.Dictionaries;
    using Commons.Helpers;
    using Resources;
    using System;

    public partial class TB_TraceTemplate
    {
        public string LastUpdateDateUI
        {
            get
            {
                return LastUpdateDate.HasValue ? DateTimeExtension.GetZonedDateTimeFromUTC(LastUpdateDate.Value).ToString(Formats.DateTime.ShortDateTime) : CreateDate.GetZonedDateTimeFromUTC().ToString(Formats.DateTime.ShortDateTime);
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

        public string TransferredUserName
        {
            get
            {
                if (TransferredUser != null)
                {
                    return TransferredUser.FullName;
                }
                return CommonValues.EmptyValue;
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
                    case EN_TransactionType.E_TRANSFER:
                        transactionName = FieldNames.E_TRANSFER;
                        break;
                    default:
                        transactionName = FieldNames.E_INVALID;
                        break;
                }
                return transactionName;
            }
        }

        public class FieldName
        {
            public const string TB_TraceTemplate = "TB_TraceTemplate";

            public const string TemplateName = "TemplateName";
           
            public const string Amount = "Amount";
            public const string Tags = "Tags";

            public const string TransferredUser = "TransferredUser";
            public const string TransferredUserID = "TransferredUserID";
            public const string TransferredUserName = "TransferredUserName";

            public const string Currency = "Currency";
            public const string CurrencyID = "CurrencyID";
            public const string CurrencyName = "CurrencyName";

            public const string TransactionType = "TransactionType";
            public const string TransactionTypeName = "TransactionTypeName";
        }
    }
}
