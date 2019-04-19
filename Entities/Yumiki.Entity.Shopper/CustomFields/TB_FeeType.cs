namespace Yumiki.Entity.Shopper
{
    using System;
    using Yumiki.Commons.Dictionaries;
    using Yumiki.Commons.Helpers;
    using Yumiki.Entity.Base;

    public partial class TB_FeeType
    {
        public string LastUpdateDateUI
        {
            get
            {
                return LastUpdateDate.HasValue ? LastUpdateDate.Value.GetZonedDateTimeFromUTC().ToString(Formats.DateTime.ShortDateTime) : CreateDate.GetZonedDateTimeFromUTC().ToString(Formats.DateTime.ShortDateTime);
            }
        }

        public class FieldName
        {
            public const string TB_FeeType = "TB_FeeType";

            public const string FeeTypeName = "FeeTypeName";
            public const string ShowInReceipt = "ShowInReceipt";
            public const string ShowInInvoice = "ShowInInvoice";
            public const string ShowInAdditionalFee = "ShowInAdditionalFee";
        }
    }
}
