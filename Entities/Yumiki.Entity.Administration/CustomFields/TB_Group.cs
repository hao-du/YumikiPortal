using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Commons.Dictionaries;

namespace Yumiki.Entity.Administration
{
    public partial class TB_Group
    {
        public string LastUpdateDateUI
        {
            get
            {
                return LastUpdateDate.HasValue ? LastUpdateDate.Value.ToString(DateTimeFormat.ShortDateTime) : CreateDate.ToString(DateTimeFormat.ShortDateTime);
            }
        }

        public class FieldName
        {
            public const string GroupName = "GroupName";
        }
    }
}
