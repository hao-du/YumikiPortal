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

        private int _itemsPerPage = 10;
        public int ItemsPerPage
        {
            get
            {
                if (_itemsPerPage < 0)
                {
                    _itemsPerPage = 10;
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
                return (TotalItems % ItemsPerPage) != 0 ? TotalItems / ItemsPerPage + 1 : TotalItems / ItemsPerPage;
            }
        }

        private int _pagingItemSize = 10;
        public int PagingItemSize
        {
            get
            {
                if (_pagingItemSize <= 0)
                {
                    _pagingItemSize = 10;
                }
                return _pagingItemSize;
            }
            set
            {
                _pagingItemSize = value;
            }
        }


        public int StartPage { get; set; }
        public int EndPage { get; set; }
    }
}
