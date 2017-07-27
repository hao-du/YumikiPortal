using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yumiki.Entity.WellCovered
{
    public class MD_Live
    {
        public MD_Live()
        {
            ColumnNames = new List<string>();
        }

        public Guid ObjectID { get; set; }

        public string LiveName { get; set; }

        public List<string> ColumnNames { get; set; }

        public EnumerableRowCollection<DataRow> Datasource { get; set; }
    }
}
