using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Entity.Base;

namespace Yumiki.Entity.MoneyTrace.ServiceObjects
{
    public class GetTraceRequest<T> : PagingEntity<T> where T : IEntity
    {
        public bool ShowInactive { get; set; } = false;
        public Guid UserID { get; set; }

        public DateTime Month { get; set; }
        public bool IsDisplayedAll { get; set; } = true;

        public Guid? BankAccountID { get; set; }
        
        /// <summary>
        /// NULL: Not filter, TRUE: Filter log traces only, FASLE: Filter traces only. 
        /// </summary>
        public bool? GetLogTraceLOnly { get; set; } = null;

        public EN_TransactionType? TransactionLogType { get; set; }
        public EN_TransactionType? TransactionType { get; set; }
        public bool GetOnlyGroupRecords { get; set; } = false;
    }
}
