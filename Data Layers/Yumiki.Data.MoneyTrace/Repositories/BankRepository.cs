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
    public class BankRepository : MoneyTraceRepository, IBankRepository
    {
        /// <summary>
        /// Get all active Banks from Database.
        /// </summary>
        /// <param name="request">Request include all filter parameters</param>
        /// <returns>List of all active Banks.</returns>
        public GetBankResponse<TB_Bank> GetAllBanks(GetBankRequest<TB_Bank> request)
        {
            IQueryable<TB_Bank> queryable = Context.TB_Bank.Where(c => c.IsActive == !request.ShowInactive);

            if (request.GetShareable)
            {
                queryable = queryable.Where(c => c.UserID == request.UserID || c.IsShareable);
            }
            else
            {
                queryable = queryable.Where(c => c.UserID == request.UserID);
            }

            return new GetBankResponse<TB_Bank>() {
                Records = queryable.OrderBy(c=>c.BankName).Skip((request.CurrentPage - 1) * request.ItemsPerPage).Take(request.ItemsPerPage).ToList(),
                CurrentPage = request.CurrentPage,
                ItemsPerPage = request.ItemsPerPage,
                TotalItems = queryable.Count()
            };
        }

        /// <summary>
        /// Get a specific bank.
        /// </summary>
        /// <param name="bankID">Specify id for bank need to be retrieved.</param>
        /// <returns>Bank Object</returns>
        public TB_Bank GetBank(Guid bankID)
        {
            return Context.TB_Bank.SingleOrDefault(c => c.ID == bankID);
        }

        /// <summary>
        /// Create/Update a bank
        /// </summary>
        /// <param name="bank">If bank id is empty, then this is new bank. Otherwise, this needs to be updated</param>
        public void SaveBank(TB_Bank bank)
        {
            if (bank.ID == Guid.Empty)
            {
                Context.TB_Bank.Add(bank);
            }
            else
            {
                TB_Bank dbBank = Context.TB_Bank.Single(c => c.ID == bank.ID);
                dbBank.BankName = bank.BankName;
                dbBank.UserID = bank.UserID;
                dbBank.IsShareable = bank.IsShareable;
                dbBank.Descriptions = bank.Descriptions;
                dbBank.IsActive = bank.IsActive;
            }

            Save();
        }
    }
}
