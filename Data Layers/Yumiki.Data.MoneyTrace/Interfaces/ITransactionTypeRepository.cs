using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Entity.MoneyTrace;

namespace Yumiki.Data.MoneyTrace.Interfaces
{
    public interface ITransactionTypeRepository
    {
        /// <summary>
        /// Get all active Transaction Type from Database.
        /// </summary>
        /// <param name="showInactive">Show list of inactive Transaction Type or active Transaction Type.</param>
        /// <returns>List of all active Transaction Type.</returns>
        List<TB_TransactionType> GetAllTransactionTypes(bool showInactive);

        /// <summary>
        /// Get a specific Transaction Type.
        /// </summary>
        /// <param name="transactionTypeID">Specify id for Transaction Type need to be retrieved.</param>
        /// <returns>Transaction Type Object</returns>
        TB_TransactionType GetTransactionType(Guid transactionTypeID);

        /// <summary>
        /// Create/Update a Transaction Type
        /// </summary>
        /// <param name="transactionType">If Transaction Type id is empty, then this is new Transaction Type. Otherwise, this needs to be updated</param>
        void SaveTransactionType(TB_TransactionType transactionType);
    }
}
