namespace Yumiki.Entity.Shopper
{
    using System;
    using Yumiki.Commons.Dictionaries;
    using Yumiki.Commons.Helpers;
    using Yumiki.Entity.Base;

    public partial class TB_InvoiceDetail
    {
        public string LastUpdateDateUI
        {
            get
            {
                return LastUpdateDate.HasValue ? LastUpdateDate.Value.GetZonedDateTimeFromUTC().ToString(Formats.DateTime.ShortDateTime) : CreateDate.GetZonedDateTimeFromUTC().ToString(Formats.DateTime.ShortDateTime);
            }
        }

        public string ProductName
        {
            get
            {
                return Product == null ? string.Empty : Product.ProductName;
            }
        }

        public string ProductCode
        {
            get
            {
                return Product == null ? string.Empty : Product.ProductCode;
            }
        }

        public string InvoiceNumber
        {
            get
            {
                return Invoice == null ? string.Empty : Invoice.InvoiceNumber;
            }
        }

        public string CustomerName
        {
            get
            {
                return Invoice == null ? string.Empty : Invoice.CustomerName;
            }
        }

        public string InvoiceDateUI
        {
            get
            {
                return Invoice == null ? string.Empty : Invoice.InvoiceDateUI;
            }
        }

        public class FieldName
        {
            public const string TB_InvoiceDetail = "TB_InvoiceDetail";

            public const string Product = "Product";
            public const string ProductName = "ProductName";
            public const string ProductCode = "ProductCode";
            public const string ProductID = "ProductID";
            public const string Invoice = "Invoice";
            public const string InvoiceID = "InvoiceID";
            public const string OriginalPrice = "OriginalPrice";
            public const string UnitPrice = "UnitPrice";
            public const string Quantity = "Quantity";
            public const string Amount = "Amount";
            public const string Stocks = "Stocks";
        }
    }
}
