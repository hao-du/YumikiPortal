using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Entity.MoneyTrace;

namespace Yumiki.Business.MoneyTrace.Interfaces
{
    public interface ICategoryService
    {
        // <summary>
        /// Get all active Category from Database.
        /// </summary>
        /// <param name="showInactive">Show list of inactive Category or active Category.</param>
        /// <returns>List of all active Category.</returns>
        List<TB_Category> GetAllCategory(bool showInactive);

        /// <summary>
        /// Get a specific category.
        /// </summary>
        /// <param name="categoryID">Specify id for category need to be retrieved.</param>
        /// <returns>Category Object</returns>
        TB_Category GetCategory(string categoryID);

        /// <summary>
        /// Create/Update a category
        /// </summary>
        /// <param name="category">If category id is empty, then this is new category. Otherwise, this needs to be updated</param>
        void SaveCategory(TB_Category category);
    }
}
