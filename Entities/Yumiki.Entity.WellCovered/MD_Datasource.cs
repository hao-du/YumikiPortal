using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yumiki.Entity.WellCovered
{
    public class MD_Datasource
    {
        public string ID { get; set; }

        public string DisplayText { get; set; }

        public class FieldNames
        {
            public const string ID = "ID";
            public const string DisplayText = "DisplayText";
        }
    }
}
