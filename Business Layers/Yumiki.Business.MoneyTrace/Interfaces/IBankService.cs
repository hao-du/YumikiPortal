using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Entity.MoneyTrace;
using Yumiki.Entity.MoneyTrace.ServiceObjects;

namespace Yumiki.Business.MoneyTrace.Interfaces
{
    public interface IBankService
    {
        /// <summary>
        /// Get all active Banks from Database.
        /// </summary>
        /// <param name="request">Request include all filter parameters</param>
        /// <returns>List of all active Banks.</returns>
        GetBankResponse<TB_Bank> GetAllBanks(GetBankRequest<TB_Bank> request);

        /// <summary>
        /// Get a specific bank.
        /// </summary>
        /// <param name="bankID">Specify id for bank need to be retrieved.</param>
        /// <returns>Bank Object</returns>
        TB_Bank GetBank(string bankID);

        /// <summary>
        /// Create/Update a bank
        /// </summary>
        /// <param name="bank">If bank id is empty, then this is new bank. Otherwise, this needs to be updated</param>
        void SaveBank(TB_Bank bank);
    }
}
