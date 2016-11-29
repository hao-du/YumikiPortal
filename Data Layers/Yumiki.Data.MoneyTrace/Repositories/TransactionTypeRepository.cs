using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Data.MoneyTrace.Interfaces;
using Yumiki.Entity.MoneyTrace;

namespace Yumiki.Data.MoneyTrace.Repositories
{
    public class TransactionTypeRepository : MoneyTraceRepository, ITransactionTypeRepository
    {
        /// <summary>
        /// Get all active Transaction Type from Database.
        /// </summary>
        /// <param name="showInactive">Show list of inactive Transaction Type or active Transaction Type.</param>
        /// <returns>List of all active Transaction Type.</returns>
        public List<TB_TransactionType> GetAllTransactionTypes(bool showInactive)
        {
            return Context.TB_TransactionType.Where(c => c.IsActive == !showInactive).ToList();
        }

        /// <summary>
        /// Get a specific Transaction Type.
        /// </summary>
        /// <param name="currencyID">Specify id for Transaction Type need to be retrieved.</param>
        /// <returns>Transaction Type Object</returns>
        public TB_TransactionType GetTransactionType(Guid transactionTypeID)
        {
            return Context.TB_TransactionType.Where(c => c.ID == transactionTypeID).SingleOrDefault();
        }

        /// <summary>
        /// Create/Update a Transaction Type
        /// </summary>
        /// <param name="user">If Transaction Type id is empty, then this is new Transaction Type. Otherwise, this needs to be updated</param>
        public void SaveTransactionType(TB_TransactionType transactionType)
        {
            if (transactionType.ID == Guid.Empty)
            {
                Context.TB_TransactionType.Add(transactionType);
            }
            else
            {
                TB_TransactionType dbTransactionType = Context.TB_TransactionType.Where(c => c.ID == transactionType.ID).Single();
                dbTransactionType.TransactionTypeName = transactionType.TransactionTypeName;
                dbTransactionType.IsIncome = transactionType.IsIncome;
                dbTransactionType.IsTransfer = transactionType.IsTransfer;
                dbTransactionType.Descriptions = transactionType.Descriptions;
                dbTransactionType.IsActive = transactionType.IsActive;
            }

            Save();
        }
    }
}
