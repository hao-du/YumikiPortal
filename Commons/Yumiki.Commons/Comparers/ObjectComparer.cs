using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Entity.Base;

namespace Yumiki.Commons.Comparers
{
    public class ObjectComparer : IEqualityComparer<IEntity>
    {
        public bool Equals(IEntity x, IEntity y)
        {
            if(x == null || y == null)
            {
                return false;
            }

            return x.ID == y.ID;
        }

        public int GetHashCode(IEntity obj)
        {
            return obj.GetHashCode();
        }
    }
}
