using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Business.Base;
using Yumiki.Business.MoneyTrace.Interfaces;
using Yumiki.Commons.Exceptions;
using Yumiki.Data.MoneyTrace.Interfaces;
using Yumiki.Entity.MoneyTrace;

namespace Yumiki.Business.MoneyTrace.Services
{
    public class CategoryService : BaseService<ICategoryRepository>, ICategoryService
    {
        /// <summary>
        /// Get all active Category from Database.
        /// </summary>
        /// <param name="showInactive">Show list of inactive Category or active Category.</param>
        /// <returns>List of all active Category.</returns>
        public List<TB_Category> GetAllCategory(bool showInactive)
        {
            return Repository.GetAllCategory(showInactive);
        }

        /// <summary>
        /// Get a specific category.
        /// </summary>
        /// <param name="categoryID">Specify id for category need to be retrieved.</param>
        /// <returns>Category Object</returns>
        public TB_Category GetCategory(string categoryID)
        {
            if (string.IsNullOrEmpty(categoryID))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Category ID cannot be empty.", Logger);
            }

            Guid convertedCategoryID = Guid.Empty;
            Guid.TryParse(categoryID, out convertedCategoryID);
            if (convertedCategoryID == Guid.Empty)
            {
                throw new YumikiException(ExceptionCode.E_WRONG_TYPE, "Category ID must be GUID type.", Logger);
            }

            return Repository.GetCategory(convertedCategoryID);
        }

        /// <summary>
        /// Create/Update a category
        /// </summary>
        /// <param name="category">If category id is empty, then this is new category. Otherwise, this needs to be updated</param>
        public void SaveCategory(TB_Category category)
        {
            if (string.IsNullOrEmpty(category.CategoryName))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Category Name is required.", Logger);
            }

            if (category.TransactionTypeID == Guid.Empty)
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Transaction Type is required.", Logger);
            }

            Repository.SaveCategory(category);
        }
    }
}
