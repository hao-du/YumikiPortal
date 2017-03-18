using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yumiki.Entity.Base
{
    public abstract class PagingEntity<T> where T : IEntity
    {
        public bool EnablePaging { get; set; } = true;

        public int TotalItems
        {
            get; set;
        }

        public IEnumerable<T> Records
        {
            get; set;
        }

        private int _itemsPerPage = int.MaxValue;
        public int ItemsPerPage
        {
            get
            {
                if (_itemsPerPage < 0)
                {
                    _itemsPerPage = int.MaxValue;
                }
                return _itemsPerPage;
            }
            set
            {
                _itemsPerPage = value;
            }
        }

        private int _currentPage = 1;
        /// <summary>
        /// Smallest current page index is always 1.
        /// </summary>
        public int CurrentPage
        {
            get
            {
                if (_currentPage <= 0)
                {
                    _currentPage = 1;
                }
                return _currentPage;
            }
            set
            {
                _currentPage = value;
            }
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
