namespace Yumiki.Entity.MoneyTrace
{
    using Commons.Dictionaries;
    using Commons.Helpers;

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

        public class FieldName
        {
            public const string TB_BankAccount = "TB_BankAccount";

            public const string AccountNumber = "AccountNumber";
            public const string BankID = "BankID";

            public const string DepositDate = "DepositDate";
            public const string WithdrawDate = "WithdrawDate";

            public const string DepositDateUI = "DepositDateUI";
            public const string WithdrawDateUI = "WithdrawDateUI";

            public const string Interest = "Interest";
        }
    }
}
