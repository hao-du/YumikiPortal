﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Entity.MoneyTrace;

namespace Yumiki.Data.MoneyTrace.Interfaces
{
    public interface ICurrencyRepository
    {
        /// <summary>
        /// Get all active Currency from Database.
        /// </summary>
        /// <param name="showInactive">Show list of inactive Currency or active Currency.</param>
        /// <returns>List of all active Currency.</returns>
        List<TB_Currency> GetAllCurrency(bool showInactive);

        /// <summary>
        /// Get a specific currency.
        /// </summary>
        /// <param name="currencyID">Specify id for currency need to be retrieved.</param>
        /// <returns>Currency Object</returns>
        TB_Currency GetCurrency(Guid currencyID);
    }
}
