using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Commons.Dictionaries;
using Yumiki.Commons.Helpers;

namespace Yumiki.Entity.Administration
{
    public partial class TB_User
    {
        public string LastUpdateDateUI
        {
            get
            {
                return LastUpdateDate.HasValue ? DateTimeExtension.GetZonedDateTimeFromUTC(LastUpdateDate.Value).ToString(Formats.DateTime.ShortDateTime) : DateTimeExtension.GetZonedDateTimeFromUTC(CreateDate).ToString(Formats.DateTime.ShortDateTime);
            }
        }

        public string FullName
        {
            get
            {
                string lastName = string.IsNullOrWhiteSpace(LastName) ? string.Empty : LastName;

                return string.IsNullOrWhiteSpace(FirstName) ? lastName : FirstName + " " + LastName;
            }
        }

        public class FieldName
        {
            public const string TB_User = "TB_User";
        }
    }
}
