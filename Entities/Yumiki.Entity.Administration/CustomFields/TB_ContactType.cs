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
                return LastUpdateDate.HasValue ? DateTimeHelper.GetUserTimeZoneFromUTC(LastUpdateDate.Value).ToString(DateTimeHelper.ShortDateTime) : DateTimeHelper.GetUserTimeZoneFromUTC(CreateDate).ToString(DateTimeHelper.ShortDateTime);
            }
        }

        public IEnumerable<TB_UserAddress> SortUserAddresses
        {
            get
            {
                return this.UserAddresses.OrderByDescending(c=>c.IsPrimary).ThenBy(c=>c.UserAddress);
            }
        }

        public class FieldName
        {
            public const string TB_ContactType = "TB_ContactType";
            public const string ID = "ID";
            public const string ContactTypeName = "ContactTypeName";
            public const string SortUserAddresses = "SortUserAddresses";
            public const string UserAddresses = "UserAddresses";
        }
    }
}
