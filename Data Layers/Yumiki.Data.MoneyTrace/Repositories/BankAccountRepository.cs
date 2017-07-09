using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Data.MoneyTrace.Interfaces;
using Yumiki.Entity.MoneyTrace;
using Yumiki.Entity.MoneyTrace.ServiceObjects;

namespace Yumiki.Data.MoneyTrace.Repositories
{
    public class BankAccountRepository : MoneyTraceRepository, IBankAccountRepository
    {
        /// <summary>
        /// Get all active Bank Accounts from Database.
        /// </summary>
        /// <param name="request">Request includes all filter parameters</param>
        /// <returns>List of all active/inactive Bank Accounts.</returns>
        public GetBankAccountResponse<TB_BankAccount> GetAllBankAccounts(GetBankAccountRequest<TB_BankAccount> request)
        {
            IQueryable<TB_BankAccount> queryable = Context.TB_BankAccount
                                                                        .Include(TB_BankAccount.FieldName.Currency)
                                                                        .Include(TB_BankAccount.FieldName.Bank)
                                                                        .Where(c => c.IsActive == !request.ShowInactive && c.UserID == request.UserID);

            return new GetBankAccountResponse<TB_BankAccount>()
            {
                Records = queryable.OrderBy(c => c.DepositDate).Skip((request.CurrentPage - 1) * request.ItemsPerPage).Take(request.ItemsPerPage).ToList(),
                CurrentPage = request.CurrentPage,
                ItemsPerPage = request.ItemsPerPage,
                TotalItems = queryable.Count()
            };
        }

        /// <summary>
        /// Get a specific bank account.
        /// </summary>
        /// <param name="bankID">Specify id for bank account need to be obtained.</param>
        /// <returns>Bank Object</returns>
        public TB_BankAccount GetBankAccount(Guid bankAccountID)
        {
            return Context.TB_BankAccount.Include(TB_BankAccount.FieldName.Currency)
                                        .Include(TB_BankAccount.FieldName.Bank)
                                        .SingleOrDefault(c => c.ID == bankAccountID);
        }

        /// <summary>
        /// Create/Update a bank account
        /// </summary>
        /// <param name="bank">If bank id is empty, then this is new bank. Otherwise, this needs to be updated</param>
        public void SaveBankAccount(TB_BankAccount bankAccount)
        {
            if (bankAccount.ID == Guid.Empty)
            {
                Context.TB_BankAccount.Add(bankAccount);
            }
            else
            {
                TB_BankAccount dbBankAccount = Context.TB_BankAccount.Single(c => c.ID == bankAccount.ID);
                dbBankAccount.AccountNumber = bankAccount.AccountNumber;
                dbBankAccount.Amount = bankAccount.Amount;
                dbBankAccount.UserID = bankAccount.UserID;
                dbBankAccount.BankID = bankAccount.BankID;
                dbBankAccount.CurrencyID = bankAccount.CurrencyID;
                dbBankAccount.DepositDate = bankAccount.DepositDate;
                dbBankAccount.WithdrawDate = bankAccount.WithdrawDate;
                dbBankAccount.Interest = bankAccount.Interest;
                dbBankAccount.Descriptions = bankAccount.Descriptions;
                dbBankAccount.IsActive = bankAccount.IsActive;
            }

            Save();
        }

        /// <summary>
        /// Create Bank Account from Trace
        /// </summary>
        /// <param name="trace">Banking Trace</param>
        public void SaveBankAccount(TB_Trace trace)
        {
            TB_Trace dbTrace = Context.TB_Trace.Include(TB_Trace.FieldName.BankAccount).SingleOrDefault(c => c.ID == trace.ID);

            TB_BankAccount dbBankAccount = dbTrace.BankAccount;
            if (dbBankAccount == null)
            {
                dbBankAccount = new TB_BankAccount();
                Context.TB_BankAccount.Add(dbBankAccount);

                dbBankAccount.Descriptions = trace.Descriptions;
                dbBankAccount.Traces.Add(dbTrace);
            }

            dbBankAccount.Amount = trace.Amount;
            dbBankAccount.BankID = trace.BankID.Value;
            dbBankAccount.DepositDate = trace.TraceDate;
            dbBankAccount.UserID = trace.UserID;
            dbBankAccount.CurrencyID = trace.CurrencyID;
            dbBankAccount.Tags = trace.Tags;
            dbBankAccount.IsActive = trace.IsActive;

            Save();
        }
    }
}
