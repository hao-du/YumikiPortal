using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yumiki.Commons.Comparers
{
    public class LowerCaseStringComparer : IEqualityComparer<string>
    {
        public bool Equals(string x, string y)
        {
            if(x == null || y == null)
            {
                return false;
            }

            return x.Equals(y, StringComparison.InvariantCultureIgnoreCase);
        }

        public int GetHashCode(string obj)
        {
            return obj.GetHashCode();
        }
    }
}
