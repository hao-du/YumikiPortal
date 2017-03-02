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
            if (string.IsNullOrWhiteSpace(bankAccount.AccountNumber))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Account Number is required.", Logger);
            }

            if (bankAccount.DepositDate == DateTimeExtension.GetSystemMinDate())
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Deposit Date is required.", Logger);
            }

            if (bankAccount.WithdrawDate == DateTimeExtension.GetSystemMinDate())
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Withdraw Date is required.", Logger);
            }

            if (bankAccount.BankID == Guid.Empty)
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Bank is required.", Logger);
            }

            Repository.SaveBankAccount(bankAccount);
        }
    }
}
