namespace Yumiki.Entity.MoneyTrace
{
    using Commons.Dictionaries;
    using Commons.Helpers;
    using System.Linq;

    public partial class TB_BankAccount
    {
        public string LastUpdateDateUI
        {
            get
            {
                return LastUpdateDate.HasValue ? LastUpdateDate.Value.GetZonedDateTimeFromUTC().ToString(Formats.DateTime.ShortDateTime) : CreateDate.GetZonedDateTimeFromUTC().ToString(Formats.DateTime.ShortDateTime);
            }
        }

        public string DepositDateUI
        {
            get
            {
                return DepositDate.GetZonedDateTimeFromUTC().ToString(Formats.DateTime.ShortDate);
            }
        }

        public string WithdrawDateUI
        {
            get
            {
                return CreateDate.GetZonedDateTimeFromUTC().ToString(Formats.DateTime.ShortDate);
            }
        }

        public string BankName
        {
            get
            {
                if (Bank == null)
                {
                    return string.Empty;
                }
                return Bank.BankName;
            }
        }

        public string CurrencyShortName
        {
            get
            {
                if (Currency == null)
                {
                    return string.Empty;
                }
                return Currency.CurrencyShortName;
            }
        }

        public bool HasDepositeReference
        {
            get
            {
                if(Traces.Any(c=>c.TransactionType == EN_TransactionType.E_OUTCOME))
                {
                    return true;
                }
                return false;
            }
        }

        public bool HasWithdrawReference
        {
            get
            {
                if (Traces.Any(c => c.TransactionType == EN_TransactionType.E_INCOME))
                {
                    return true;
                }
                return false;
            }
        }

        public class FieldName
        {
            public const string TB_BankAccount = "TB_BankAccount";

            public const string Bank = "Bank";
            public const string Currency = "Currency";

            public const string AccountNumber = "AccountNumber";
            public const string BankID = "BankID";
            public const string CurrencyID = "CurrencyID";

            public const string DepositDate = "DepositDate";
            public const string WithdrawDate = "WithdrawDate";

            public const string DepositDateUI = "DepositDateUI";
            public const string WithdrawDateUI = "WithdrawDateUI";

            public const string HasDepositeReference = "HasDepositeReference";
            public const string HasWithdrawReference = "HasWithdrawReference";

            public const string Interest = "Interest";
        }
    }
}
