namespace Yumiki.Entity.Administration
{
    using Base;
    using Commons.Dictionaries;
    using Commons.Helpers;
    using System;
    using System.Collections.Generic;

    public partial class TB_UserAddress
    {
        public string LastUpdateDateUI
        {
            get
            {
                return LastUpdateDate.HasValue ? LastUpdateDate.Value.ToString(DateTimeHelper.ShortDateTime) : CreateDate.ToString(DateTimeHelper.ShortDateTime);
            }
        }

        public class FieldName
        {
            public const string TB_UserAddress = "TB_UserAddress";
        }
    }
}
