namespace Yumiki.Entity.Shopper
{
    using System;
    using Yumiki.Commons.Dictionaries;
    using Yumiki.Commons.Helpers;
    using Yumiki.Entity.Base;

    public partial class TB_AdditionalFee
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

        public string FeeIssueDateUI
        {
            get
            {
                return FeeIssueDate.ToString(Formats.DateTime.LongDate);
            }
        }

        public class FieldName
        {
            public const string TB_AdditionalFee = "TB_AdditionalFee";

            public const string FeeType = "FeeType";
            public const string FeeTypeName = "FeeTypeName";
            public const string FeeTypeID = "FeeTypeID";
            public const string Amount = "Amount";
            public const string FeeIssueDate = "FeeIssueDate";
            public const string FeeIssueDateUI = "FeeIssueDateUI";
        }
    }
}
