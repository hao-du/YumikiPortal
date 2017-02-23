using System.Collections.Generic;
using System.Linq;
using Yumiki.Commons.Dictionaries;
using Yumiki.Commons.Helpers;

namespace Yumiki.Entity.Administration
{
    public partial class TB_ContactType
    {
        public string LastUpdateDateUI
        {
            get
            {
                return LastUpdateDate.HasValue ? DateTimeExtension.GetZonedDateTimeFromUTC(LastUpdateDate.Value).ToString(DateTimeExtension.ShortDateTime) : DateTimeExtension.GetZonedDateTimeFromUTC(CreateDate).ToString(DateTimeExtension.ShortDateTime);
            }
        }

        public class FieldName
        {
            public const string TB_ContactType = "TB_ContactType";
            public const string ID = "ID";
            public const string ContactTypeName = "ContactTypeName";
            public const string UserAddresses = "UserAddresses";
        }
    }
}
