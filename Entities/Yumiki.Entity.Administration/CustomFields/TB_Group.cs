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
                return LastUpdateDate.HasValue ? DateTimeExtension.GetZonedDateTimeFromUTC(LastUpdateDate.Value).ToString(Formats.DateTime.ShortDateTime) : DateTimeExtension.GetZonedDateTimeFromUTC(CreateDate).ToString(DateTimeExtension.ShortDateTime);
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
