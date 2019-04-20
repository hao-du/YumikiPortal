namespace Yumiki.Entity.Shopper
{
    using System;
    using Yumiki.Commons.Dictionaries;
    using Yumiki.Commons.Helpers;
    using Yumiki.Entity.Base;

    public partial class TB_Receipt
    {
        public string LastUpdateDateUI
        {
            get
            {
                return LastUpdateDate.HasValue ? LastUpdateDate.Value.GetZonedDateTimeFromUTC().ToString(Formats.DateTime.ShortDateTime) : CreateDate.GetZonedDateTimeFromUTC().ToString(Formats.DateTime.ShortDateTime);
            }
        }

        public string ReceiptDateUI
        {
            get
            {
                return ReceiptDate.ToString(Formats.DateTime.LongDate);
            }
        }

        public class FieldName
        {
            public const string TB_Receipt = "TB_Receipt";

            public const string ExternalReceiptID = "ExternalReceiptID";
            public const string ExternalReceiptUrl = "ExternalReceiptUrl";
            public const string TotalAmount = "TotalAmount";
            public const string ReceiptDate = "ReceiptDate";
            public const string ReceiptDateUI = "ReceiptDateUI";
            public const string Status = "Status";
            public const string ReceiptDetails = "ReceiptDetails";
            public const string ReceiptExtraFees = "ReceiptExtraFees";
        }
    }
}
