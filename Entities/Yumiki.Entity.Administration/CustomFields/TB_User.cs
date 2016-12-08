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
                return LastUpdateDate.HasValue ? LastUpdateDate.Value.ToString(DateTimeHelper.ShortDateTime) : CreateDate.ToString(DateTimeHelper.ShortDateTime);
            }
        }

        public string FullName
        {
            get
            {
                string lastName = string.IsNullOrEmpty(LastName) ? string.Empty : LastName;

                return string.IsNullOrEmpty(FirstName) ? lastName : FirstName + " " + LastName;
            }
        }

        public class FieldName
        {
            public const string TB_User = "TB_User";
        }
    }
}
