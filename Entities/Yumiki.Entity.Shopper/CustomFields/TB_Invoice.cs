namespace Yumiki.Entity.Shopper
{
    using System;
    using Yumiki.Commons.Dictionaries;
    using Yumiki.Commons.Helpers;
    using Yumiki.Entity.Base;

    public partial class TB_Invoice
    {
        public string LastUpdateDateUI
        {
            get
            {
                return LastUpdateDate.HasValue ? LastUpdateDate.Value.GetZonedDateTimeFromUTC().ToString(Formats.DateTime.ShortDateTime) : CreateDate.GetZonedDateTimeFromUTC().ToString(Formats.DateTime.ShortDateTime);
            }
        }

        public string InvoiceDateUI
        {
            get
            {
                return InvoiceDate.ToString(Formats.DateTime.LongDate);
            }
        }

        public class FieldName
        {
            public const string TB_Invoice = "TB_Invoice";

            public const string InvoiceNumber = "InvoiceNumber";
            public const string CustomerName = "CustomerName";
            public const string CustomerAddress = "CustomerAddress";
            public const string CustomerPhone = "CustomerPhone";
            public const string CustomerEmail = "CustomerEmail";
            public const string CustomerNote = "CustomerNote";
            public const string TotalAmount = "TotalAmount";
            public const string InvoiceDate = "InvoiceDate";
            public const string Status = "Status";
            public const string InvoiceDetails = "InvoiceDetails";
            public const string InvoiceExtraFees = "InvoiceExtraFees";
        }
    }
}
