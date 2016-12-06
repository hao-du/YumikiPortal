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
    public class CurrencyService : BaseService<ICurrencyRepository>, ICurrencyService
    {
        /// <summary>
        /// Get all active Currency from Database.
        /// </summary>
        /// <param name="showInactive">Show list of inactive Currency or active Currency.</param>
        /// <returns>List of all active Currency.</returns>
        public List<TB_Currency> GetAllCurrency(bool showInactive)
        {
            return Repository.GetAllCurrency(showInactive);
        }

        /// <summary>
        /// Get a specific currency.
        /// </summary>
        /// <param name="currencyID">Specify id for currency need to be retrieved.</param>
        /// <returns>Currency Object</returns>
        public TB_Currency GetCurrency(string currencyID)
        {
            if (string.IsNullOrEmpty(currencyID))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Currency ID cannot be empty.", Logger);
            }

            Guid convertedCurrencyID = Guid.Empty;
            Guid.TryParse(currencyID, out convertedCurrencyID);
            if (convertedCurrencyID == Guid.Empty)
            {
                throw new YumikiException(ExceptionCode.E_WRONG_TYPE, "Currency ID must be GUID type.", Logger);
            }

            return Repository.GetCurrency(convertedCurrencyID);
        }

        /// <summary>
        /// Create/Update a currency
        /// </summary>
        /// <param name="currency">If currency id is empty, then this is new currency. Otherwise, this needs to be updated</param>
        public void SaveCurrency(TB_Currency currency)
        {
            if (string.IsNullOrEmpty(currency.CurrencyName))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Currency Name is required.", Logger);
            }

            if (string.IsNullOrEmpty(currency.CurrencyShortName))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Currency Short Name is required.", Logger);
            }

            if (currency.CurrencyShortName.Length < 3 || currency.CurrencyShortName.Length > 4)
            {
                throw new YumikiException(ExceptionCode.E_INVALID_LENGTH, "Currency Short Name allows 3 to 4 characters.", Logger);
            }

            currency.CurrencyShortName = currency.CurrencyShortName.ToUpper();

            Repository.SaveCurrency(currency);
        }
    }
}
