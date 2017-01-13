using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Commons.Dictionaries;
using Yumiki.Commons.Helpers;

namespace Yumiki.Entity.Administration
{
    public partial class TB_Group
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
            public const string GroupName = "GroupName";
            public const string Users = "Users";
            public const string Privileges = "Privileges";
        }
    }
}
