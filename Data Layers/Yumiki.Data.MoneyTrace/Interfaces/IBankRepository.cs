using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Entity.MoneyTrace;

namespace Yumiki.Data.MoneyTrace.Interfaces
{
    public interface IBankRepository
    {
        /// <summary>
        /// Get all active Banks from Database.
        /// </summary>
        /// <param name="showInactive">Show list of inactive Banks or active Banks.</param>
        /// <returns>List of all active Banks.</returns>
        List<TB_Bank> GetAllBanks(bool showInactive, Guid userID, bool getShareable);

        /// <summary>
        /// Get a specific bank.
        /// </summary>
        /// <param name="bankID">Specify id for bank need to be retrieved.</param>
        /// <returns>Bank Object</returns>
        TB_Bank GetBank(Guid bankID);

        /// <summary>
        /// Create/Update a bank
        /// </summary>
        /// <param name="bank">If bank id is empty, then this is new bank. Otherwise, this needs to be updated</param>
        void SaveBank(TB_Bank bank);
    }
}
