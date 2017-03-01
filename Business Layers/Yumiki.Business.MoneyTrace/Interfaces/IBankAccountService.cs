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
using Yumiki.Entity.MoneyTrace.ServiceObjects;

namespace Yumiki.Business.MoneyTrace.Interfaces
{
    public interface IBankAccountService
    {
        /// <summary>
        /// Get all active Bank Accounts from Database.
        /// </summary>
        /// <param name="request">Request includes all filter parameters</param>
        /// <returns>List of all active/inactive Bank Accounts.</returns>
        GetBankAccountResponse<TB_BankAccount> GetAllBankAccounts(GetBankAccountRequest<TB_BankAccount> request);

        /// <summary>
        /// Get a specific bank account.
        /// </summary>
        /// <param name="bankID">Specify id for bank account need to be obtained.</param>
        /// <returns>Bank Object</returns>
        TB_BankAccount GetBankAccount(string bankAccountID);

        /// <summary>
        /// Create/Update a bank account
        /// </summary>
        /// <param name="bank">If bank id is empty, then this is new bank. Otherwise, this needs to be updated</param>
        void SaveBankAccount(TB_BankAccount bankAccount);
    }
}
