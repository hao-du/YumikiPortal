using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Entity.Base;

namespace Yumiki.Entity.OnTime.ServiceObjects
{
    public class GetTaskResponse : PagingEntity<TB_Task>
    {
        public GetTaskResponse()
        {
            Records = new List<TB_Task>();
        }
    }
}
