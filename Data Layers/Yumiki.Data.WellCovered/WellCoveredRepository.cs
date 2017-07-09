using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Data.Base;

namespace Yumiki.Entity.WellCovered
{
    public class WellCoveredRepository : BaseRepository<WellCoveredModel>
    {
        protected WellCoveredModel Context
        {
            get
            {
                if (context == null)
                    context = new WellCoveredModel();
                return context;
            }
        }
    }
}
