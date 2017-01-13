namespace Yumiki.Entity.MoneyTrace
{
    using Base;
    using Commons.Dictionaries;
    using Commons.Helpers;
    using System;
    using System.Collections.Generic;

    public partial class TB_Tag
    {
        public string LastUpdateDateUI
        {
            get
            {
                return LastUpdateDate.HasValue ? DateTimeHelper.GetUserTimeZoneFromUTC(LastUpdateDate.Value).ToString(DateTimeHelper.ShortDateTime) : DateTimeHelper.GetUserTimeZoneFromUTC(CreateDate).ToString(DateTimeHelper.ShortDateTime);
            }
        }

        public class FieldName
        {
            public const string TB_Tag = "TB_Tag";
            public const string TagName = "TagName";
        }
    }
}
