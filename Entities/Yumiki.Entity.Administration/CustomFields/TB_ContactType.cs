using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Common.Dictionary;

namespace Yumiki.Entity.Administration
{
    public partial class TB_ContactType
    {
        public string LastUpdateDateUI
        {
            get
            {
                return LastUpdateDate.HasValue ? LastUpdateDate.Value.ToString(DateTimeFormat.ShortDateTime) : CreateDate.ToString(DateTimeFormat.ShortDateTime);
            }
        }

        public IEnumerable<TB_UserAddress> SortUserAddresses
        {
            get
            {
                return this.TB_UserAddress.OrderByDescending(c=>c.IsPrimary).ThenBy(c=>c.UserAddress);
            }
        }

        public class FieldName
        {
            public const string TB_ContactType = "TB_ContactType";
            public const string ID = "ID";
            public const string ContactTypeName = "ContactTypeName";
            public const string SortUserAddresses = "SortUserAddresses";
        }
    }
}
