namespace Yumiki.Entity.WellCovered
{
    using System;
    using System.Collections.Generic;
    using Yumiki.Commons.Dictionaries;
    using Yumiki.Commons.Helpers;
    using Yumiki.Entity.Base;

    public partial class TB_Object
    {
        public string LastUpdateDateUI
        {
            get
            {
                return LastUpdateDate.HasValue ? LastUpdateDate.Value.GetZonedDateTimeFromUTC().ToString(Formats.DateTime.ShortDateTime) : CreateDate.GetZonedDateTimeFromUTC().ToString(Formats.DateTime.ShortDateTime);
            }
        }

        public string AppName
        {
            get
            {
                return App == null ? string.Empty : App.AppName;
            }
        }

        public class FieldName
        {
            public const string ObjectName = "ObjectName";
            public const string DisplayName = "DisplayName";
            public const string ApiName = "ApiName";
            public const string App = "App";
            public const string AppName = "AppName";
            public const string Fields = "Fields";

            public const string IsShareable = "IsShareable";
        }
    }
}
