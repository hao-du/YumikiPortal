namespace Yumiki.Entity.WellCovered
{
    using System;
    using System.Collections.Generic;
    using Yumiki.Commons.Dictionaries;
    using Yumiki.Commons.Helpers;
    using Yumiki.Entity.Base;

    public partial class TB_App
    {
        public string LastUpdateDateUI
        {
            get
            {
                return LastUpdateDate.HasValue ? LastUpdateDate.Value.GetZonedDateTimeFromUTC().ToString(Formats.DateTime.ShortDateTime) : CreateDate.GetZonedDateTimeFromUTC().ToString(Formats.DateTime.ShortDateTime);
            }
        }

        public class FieldName
        {
            public const string AppName = "AppName";
            public const string IsShareable = "IsShareable";
            public const string Objects = "Objects";
        }
    }
}
