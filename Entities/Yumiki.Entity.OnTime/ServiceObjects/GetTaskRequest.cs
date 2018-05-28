using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Entity.Base;

namespace Yumiki.Entity.OnTime.ServiceObjects
{
    public class GetTaskRequest : PagingEntity<TB_Task>
    {
        public GetTaskRequest()
        {
        }

        public Guid UserID { get; set; }
        public Guid PhaseID { get; set; }
        public Guid ProjectID { get; set; }
    }
}
