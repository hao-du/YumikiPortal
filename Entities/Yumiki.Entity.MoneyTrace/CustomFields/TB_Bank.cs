namespace Yumiki.Entity.MoneyTrace
{
    using Commons.Helpers;

    public partial class TB_Bank
    {
        public string LastUpdateDateUI
        {
            get
            {
                return LastUpdateDate.HasValue ? LastUpdateDate.Value.GetZonedDateTimeFromUTC().ToString(DateTimeExtension.ShortDateTime) : CreateDate.GetZonedDateTimeFromUTC().ToString(DateTimeExtension.ShortDateTime);
            }
        }

        public class FieldName
        {
            public const string TB_Bank = "TB_Bank";
            public const string BankName = "BankName";
            public const string IsShareable = "IsShareable";
        }
    }
}
