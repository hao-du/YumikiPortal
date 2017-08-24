using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Entity.Base;
using Yumiki.Entity.WellCovered;

namespace Yumiki.Entity.MoneyTrace.ServiceObjects
{
    public class GetSearchIndexRequest<T> : PagingEntity<TB_Index>
    {
        public string Keywords { get; set; }
        public Guid UserID { get; set; }
    }
}
