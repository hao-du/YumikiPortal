using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Entity.Base;

namespace Yumiki.Commons.Helpers
{
    /// <summary>
    /// To calculate all paging params
    /// </summary>
    public class PagingHelper<T> where T : IEntity
    {
        public static void Paging(PagingEntity<T> pagingEntity)
        {
            //Exmaple: currentPage = 10, itemsize = 10, (startPage = 5, endPage = 4 ==> following the formular below.
            //Output: 5 6 7 8 9|10| 11 12 13 14
            int startPage = pagingEntity.CurrentPage - (pagingEntity.PagingItemSize / 2);
            int endPage = pagingEntity.CurrentPage + (pagingEntity.PagingItemSize / 2) - 1;
            if (startPage <= 0)
            {
                endPage -= (startPage - 1);
                startPage = 1;
            }
            if (endPage > pagingEntity.TotalPages)
            {
                endPage = pagingEntity.TotalPages;
                if (endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }

            pagingEntity.StartPage = startPage;
            pagingEntity.EndPage = endPage;
        }
    }
}
