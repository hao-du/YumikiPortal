using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yumiki.Entity.Base
{
    public abstract class PagingEntity<T> where T : IEntity
    {
        public int TotalItems
        {
            get; set;
        }

        public IEnumerable<T> Records
        {
            get; set;
        }

        public int ItemsPerPage
        {
            get; set;
        }

        public int CurrentPage
        {
            get; set;
        }

        public int TotalPages
        {
            get
            {
                return TotalItems / ItemsPerPage;
            }
        }
    }
}
