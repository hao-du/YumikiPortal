namespace Yumiki.Entity.Shopper
{
    using System;
    using Yumiki.Commons.Dictionaries;
    using Yumiki.Commons.Helpers;
    using Yumiki.Entity.Base;

    public partial class TB_InvoiceExtraFee
    {
        public string LastUpdateDateUI
        {
            get
            {
                return LastUpdateDate.HasValue ? LastUpdateDate.Value.GetZonedDateTimeFromUTC().ToString(Formats.DateTime.ShortDateTime) : CreateDate.GetZonedDateTimeFromUTC().ToString(Formats.DateTime.ShortDateTime);
            }
        }

        public string FeeTypeName
        {
            get
            {
                return FeeType == null ? string.Empty : FeeType.FeeTypeName;
            }
        }

        public class FieldName
        {
            public const string TB_InvoiceExtraFee = "TB_InvoiceExtraFee";

            public const string FeeType = "FeeType";
            public const string FeeTypeName = "FeeTypeName";
            public const string FeeTypeID = "FeeTypeID";
            public const string InvoiceID = "InvoiceID";
            public const string Amount = "Amount";
        }
    }
}
