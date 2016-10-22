namespace Yumiki.Entity.Administration
{
    using Base;
    using Common.Dictionary;
    using System;
    using System.Collections.Generic;

    public partial class TB_UserAddress
    {
        public string LastUpdateDateUI
        {
            get
            {
                return LastUpdateDate.HasValue ? LastUpdateDate.Value.ToString(DateTimeFormat.ShortDateTime) : CreateDate.ToString(DateTimeFormat.ShortDateTime);
            }
        }

        public class FieldName
        {
            public const string TB_UserAddress = "TB_UserAddress";
        }
    }
}
