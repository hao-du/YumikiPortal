using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Data.MoneyTrace.Interfaces;
using Yumiki.Entity.MoneyTrace;

namespace Yumiki.Data.MoneyTrace.Repositories
{
    public class CategoryRepository : MoneyTraceRepository, ICategoryRepository
    {
        /// <summary>
        /// Get all active Category from Database.
        /// </summary>
        /// <param name="showInactive">Show list of inactive Category or active Category.</param>
        /// <returns>List of all active Category.</returns>
        public List<TB_Category> GetAllCategory(bool showInactive)
        {
            return Context.TB_Category.Include(TB_TransactionType.FieldName.TB_TransactionType).Where(c => c.IsActive == !showInactive).ToList();
        }

        /// <summary>
        /// Get a specific category.
        /// </summary>
        /// <param name="categoryID">Specify id for category need to be retrieved.</param>
        /// <returns>Category Object</returns>
        public TB_Category GetCategory(Guid categoryID)
        {
            return Context.TB_Category.Where(c => c.ID == categoryID).SingleOrDefault();
        }

        /// <summary>
        /// Create/Update a category.
        /// </summary>
        /// <param name="category">If category id is empty, then this is new category. Otherwise, this needs to be updated.</param>
        public void SaveCategory(TB_Category category)
        {
            if (category.ID == Guid.Empty)
            {
                Context.TB_Category.Add(category);
            }
            else
            {
                TB_Category dbCategory = Context.TB_Category.Where(c => c.ID == category.ID).Single();
                dbCategory.CategoryName = category.CategoryName;
                dbCategory.TransactionTypeID = category.TransactionTypeID;
                dbCategory.Descriptions = category.Descriptions;
                dbCategory.IsActive = category.IsActive;
            }

            Save();
        }
    }
}
