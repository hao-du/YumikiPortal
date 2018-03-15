using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Business.Base;
using Yumiki.Business.MoneyTrace.Interfaces;
using Yumiki.Commons.Exceptions;
using Yumiki.Commons.Helpers;
using Yumiki.Data.MoneyTrace.Interfaces;
using Yumiki.Entity.MoneyTrace;
using Yumiki.Entity.MoneyTrace.ServiceObjects;

namespace Yumiki.Business.MoneyTrace.Services
{
    public class BankAccountService : BaseService<IBankAccountRepository>, IBankAccountService
    {
        /// <summary>
        /// Get all active Bank Accounts from Database.
        /// </summary>
        /// <param name="request">Request includes all filter parameters</param>
        /// <returns>List of all active/inactive Bank Accounts.</returns>
        public GetBankAccountResponse<TB_BankAccount> GetAllBankAccounts(GetBankAccountRequest<TB_BankAccount> request)
        {
            return Repository.GetAllBankAccounts(request);
        }

        /// <summary>
        /// Get a specific bank account.
        /// </summary>
        /// <param name="bankID">Specify id for bank account need to be obtained.</param>
        /// <returns>Bank Object</returns>
        public TB_BankAccount GetBankAccount(string bankAccountID)
        {
            if (string.IsNullOrWhiteSpace(bankAccountID))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Bank Account ID cannot be empty.", Logger);
            }

            Guid convertedBankAccountID = Guid.Empty;
            Guid.TryParse(bankAccountID, out convertedBankAccountID);
            if (convertedBankAccountID == Guid.Empty)
            {
                throw new YumikiException(ExceptionCode.E_WRONG_TYPE, "Bank Account ID must be GUID type.", Logger);
            }

            return Repository.GetBankAccount(convertedBankAccountID);
        }

        /// <summary>
        /// Create/Update a bank account
        /// </summary>
        /// <param name="bank">If bank id is empty, then this is new bank. Otherwise, this needs to be updated</param>
        public void SaveBankAccount(TB_BankAccount bankAccount)
        {
            if (bankAccount.Amount == decimal.Zero)
            {
                throw new YumikiException(ExceptionCode.E_WRONG_VALUE, "Amount cannot be zero.", Logger);
            }

            if (bankAccount.Interest.HasValue && bankAccount.Interest.Value < decimal.Zero)
            {
                throw new YumikiException(ExceptionCode.E_WRONG_VALUE, "Interest must be greater than zero.", Logger);
            }

            if (bankAccount.DepositDate == DateTimeExtension.GetSystemMinDate())
            {
                bankAccount.DepositDate = null;
            }

            if (bankAccount.WithdrawDate == DateTimeExtension.GetSystemMinDate())
            {
                bankAccount.WithdrawDate = null;
            }

            if (bankAccount.BankID == Guid.Empty)
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Bank is required.", Logger);
            }

            Repository.SaveBankAccount(bankAccount);
        }

        /// <summary>
        /// Create Bank Account from Trace
        /// </summary>
        /// <param name="trace">Banking Trace</param>
        public void SaveBankAccount(TB_Trace trace)
        {
            Repository.SaveBankAccount(trace);
        }
    }
}
