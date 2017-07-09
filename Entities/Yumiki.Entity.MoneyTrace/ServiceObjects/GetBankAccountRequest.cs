using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Entity.Base;

namespace Yumiki.Entity.MoneyTrace.ServiceObjects
{
    public class GetBankAccountRequest<T> : PagingEntity<T> where T : IEntity
    {
        public bool ShowInactive { get; set; }
        public Guid UserID { get; set; }
    }
}
