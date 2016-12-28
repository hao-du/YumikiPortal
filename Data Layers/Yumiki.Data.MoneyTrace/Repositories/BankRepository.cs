using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Data.MoneyTrace.Interfaces;
using Yumiki.Entity.MoneyTrace;

namespace Yumiki.Data.MoneyTrace.Repositories
{
    public class BankRepository : MoneyTraceRepository, IBankRepository
    {
        /// <summary>
        /// Get all active Banks from Database.
        /// </summary>
        /// <param name="showInactive">Show list of inactive Banks or active Banks.</param>
        /// <returns>List of all active Banks.</returns>
        public List<TB_Bank> GetAllBanks(bool showInactive, Guid userID, bool getShareable)
        {
            IQueryable<TB_Bank> queryable = Context.TB_Bank.Where(c => c.IsActive == !showInactive);

            if (getShareable)
            {
                queryable = queryable.Where(c => c.UserID == userID || c.IsShareable == true);
            }
            else
            {
                queryable = queryable.Where(c => c.UserID == userID);
            }

            return queryable.ToList();
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
