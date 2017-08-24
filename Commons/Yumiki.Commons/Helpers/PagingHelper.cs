using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yumiki.Commons.Helpers
{
    /// <summary>
    /// To calculate all paging params
    /// </summary>
    public class PagingHelper
    {
        public PagingHelper(int totalItems, int currentPage, int itemSize, int pageSize)
        {
            int totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
            currentPage = currentPage > 0 ? currentPage : 1;
            //Exmaple: currentPage = 10, itemsize = 10, (startPage = 5, endPage = 4 ==> following the formular below.
            //Output: 5 6 7 8 9|10| 11 12 13 14
            int startPage = currentPage - (itemSize / 2);
            int endPage = currentPage + (itemSize / 2) - 1;
            if (startPage <= 0)
            {
                endPage -= (startPage - 1);
                startPage = 1;
            }
            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }

            TotalItems = totalItems;
            CurrentPage = currentPage.;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;
        }

        public int TotalItems { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }
    }
}
