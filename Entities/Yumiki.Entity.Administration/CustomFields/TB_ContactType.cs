﻿using System.Collections.Generic;
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
                return LastUpdateDate.HasValue ? LastUpdateDate.Value.ToString(DateTimeHelper.ShortDateTime) : CreateDate.ToString(DateTimeHelper.ShortDateTime);
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
