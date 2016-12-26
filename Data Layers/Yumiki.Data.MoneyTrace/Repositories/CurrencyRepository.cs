using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Data.MoneyTrace.Interfaces;
using Yumiki.Entity.MoneyTrace;

namespace Yumiki.Data.MoneyTrace.Repositories
{
    public class CurrencyRepository : MoneyTraceRepository, ICurrencyRepository
    {
        /// <summary>
        /// Get all active Currency from Database.
        /// </summary>
        /// <param name="showInactive">Show list of inactive Currency or active Currency.</param>
        /// <returns>List of all active Currency.</returns>
        public List<TB_Currency> GetAllCurrency(bool showInactive, Guid userID, bool getShareable)
        {
            IQueryable<TB_Currency> queryable = Context.TB_Currency.Where(c => c.IsActive == !showInactive);

            if (getShareable)
            {
                queryable = queryable.Where(c => c.UserID == userID || c.IsShareable == true);
            }
            else
            {
                queryable = queryable.Where(c => c.UserID == userID);
            }

            return queryable.ToList();
        }

        /// <summary>
        /// Get a specific currency.
        /// </summary>
        /// <param name="currencyID">Specify id for currency need to be retrieved.</param>
        /// <returns>Currency Object</returns>
        public TB_Currency GetCurrency(Guid currencyID)
        {
            return Context.TB_Currency.SingleOrDefault(c => c.ID == currencyID);
        }

        /// <summary>
        /// Create/Update a currency
        /// </summary>
        /// <param name="currency">If currency id is empty, then this is new currency. Otherwise, this needs to be updated</param>
        public void SaveCurrency(TB_Currency currency)
        {
            if (currency.ID == Guid.Empty)
            {
                Context.TB_Currency.Add(currency);
            }
            else
            {
                TB_Currency dbCurrency = Context.TB_Currency.Single(c => c.ID == currency.ID);
                dbCurrency.CurrencyName = currency.CurrencyName;
                dbCurrency.CurrencyShortName = currency.CurrencyShortName;
                dbCurrency.UserID = currency.UserID;
                dbCurrency.IsShareable = currency.IsShareable;
                dbCurrency.Descriptions = currency.Descriptions;
                dbCurrency.IsActive = currency.IsActive;
            }

            Save();
        }
    }
}
