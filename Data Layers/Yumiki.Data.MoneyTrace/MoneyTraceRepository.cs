using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Data.Base;

namespace Yumiki.Data.MoneyTrace
{
    public class MoneyTraceRepository : BaseRepository<MoneyTraceModel>
    {
        protected MoneyTraceModel Context
        {
            get
            {
                if (context == null)
                    context = new MoneyTraceModel();
                return context;
            }
        }
    }
}
