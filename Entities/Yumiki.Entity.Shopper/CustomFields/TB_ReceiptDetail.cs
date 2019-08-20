namespace Yumiki.Entity.Shopper
{
    using System;
    using Yumiki.Commons.Dictionaries;
    using Yumiki.Commons.Helpers;
    using Yumiki.Entity.Base;

    public partial class TB_ReceiptDetail
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

        public string ExternalReceiptID
        {
            get
            {
                return Receipt == null ? string.Empty : Receipt.ExternalReceiptID;
            }
        }

        public string ReceiptDateUI
        {
            get
            {
                return Receipt == null ? string.Empty : Receipt.ReceiptDateUI;
            }
        }

        public class FieldName
        {
            public const string TB_ReceiptDetail = "TB_ReceiptDetail";

            public const string Receipt = "Receipt";
            public const string Product = "Product";
            public const string ProductName = "ProductName";
            public const string ProductCode = "ProductCode";
            public const string ProductID = "ProductID";
            public const string ReceiptID = "ReceiptID";
            public const string UnitPrice = "UnitPrice";
            public const string Quantity = "Quantity";
            public const string Amount = "Amount";
            public const string Stocks = "Stocks";
        }
    }
}
