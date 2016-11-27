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
        public List<TB_Currency> GetAllCurrency(bool showInactive)
        {
            return Context.TB_Currency.Where(c => c.IsActive == !showInactive).ToList();
        }

        /// <summary>
        /// Get a specific currency.
        /// </summary>
        /// <param name="currencyID">Specify id for currency need to be retrieved.</param>
        /// <returns>Currency Object</returns>
        public TB_Currency GetCurrency(Guid currencyID)
        {
            return Context.TB_Currency.Where(c => c.ID == currencyID).SingleOrDefault();
        }
    }
}
