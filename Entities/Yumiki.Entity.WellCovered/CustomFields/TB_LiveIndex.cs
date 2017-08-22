using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yumiki.Entity.WellCovered
{
    public partial class TB_LiveIndex
    {
        public string Rank { get; set; }

        public class FieldName
        {
            public const string AppID = "AppID";
            public const string ObjectID = "ObjectID";
            public const string LiveID = "LiveID";
            public const string FullTextIndex = "FullTextIndex";
            public const string DisplayContent = "DisplayContent";
        }
    }
}
