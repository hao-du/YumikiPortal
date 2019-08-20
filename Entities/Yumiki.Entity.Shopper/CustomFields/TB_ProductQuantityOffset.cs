namespace Yumiki.Entity.Shopper
{
    using System;
    using Yumiki.Commons.Dictionaries;
    using Yumiki.Commons.Helpers;
    using Yumiki.Entity.Base;

    public partial class TB_ProductQuantityOffset
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

        public string OffsetDateUI
        {
            get
            {
                return (LastUpdateDate.HasValue ? LastUpdateDate.Value : CreateDate).ToString(Formats.DateTime.LongDate);
            }
        }

        public class FieldName
        {
            public const string TB_ProductQuantityOffset = "TB_ProductQuantityOffset";

            public const string Product = "Product";
            public const string ProductName = "ProductName";
            public const string ProductCode = "ProductCode";
            public const string ProductID = "ProductID";
            public const string Quantity = "Quantity";
            public const string OffsetDateUI = "OffsetDateUI";
            public const string Stocks = "Stocks";
        }
    }
}
