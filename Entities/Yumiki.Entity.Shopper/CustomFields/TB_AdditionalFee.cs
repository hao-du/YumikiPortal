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
                return FeeIssueDate.ToString(Formats.DateTime.ShortDate);
            }
        }

        public class FieldName
        {
            public const string TB_AdditionalFee = "TB_AdditionalFee";
            public const string FeeTypeName = "FeeIssueDateUI";
            public const string FeeIssueDateUI = "FeeIssueDateUI";
        }
    }
}
