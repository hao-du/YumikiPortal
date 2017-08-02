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
            ColumnNames = new Dictionary<string, EN_DataType>();
        }

        public Guid ObjectID { get; set; }

        public string LiveName { get; set; }

        public Dictionary<string, EN_DataType> ColumnNames { get; set; }

        public EnumerableRowCollection<DataRow> Datasource { get; set; }
    }
}
