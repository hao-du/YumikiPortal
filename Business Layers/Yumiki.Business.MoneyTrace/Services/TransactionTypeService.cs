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

namespace Yumiki.Business.MoneyTrace.Services
{
    public class TransactionTypeService : BaseService<ITransactionTypeRepository>, ITransactionTypeService
    {
        /// <summary>
        /// Get all active Transaction Type from Database.
        /// </summary>
        /// <param name="showInactive">Show list of inactive Transaction Type or active Transaction Type.</param>
        /// <returns>List of all active Transaction Type.</returns>
        public List<TB_TransactionType> GetAllTransactionTypes(bool showInactive)
        {
            return Repository.GetAllTransactionTypes(showInactive);
        }

        /// <summary>
        /// Get a specific Transaction Type.
        /// </summary>
        /// <param name="transactionTypeID">Specify id for Transaction Type need to be retrieved.</param>
        /// <returns>Transaction Type Object</returns>
        public TB_TransactionType GetTransactionType(string transactionTypeID)
        {
            if (string.IsNullOrEmpty(transactionTypeID))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Transaction Type cannot be empty.");
            }

            Guid convertedTransactionTypeID = Guid.Empty;
            Guid.TryParse(transactionTypeID, out convertedTransactionTypeID);
            if (convertedTransactionTypeID == Guid.Empty)
            {
                throw new YumikiException(ExceptionCode.E_WRONG_TYPE, "Transaction Type ID must be GUID type.");
            }

            return Repository.GetTransactionType(convertedTransactionTypeID);
        }

        /// <summary>
        /// Create/Update a Transaction Type
        /// </summary>
        /// <param name="transactionType">If Transaction Type id is empty, then this is new Transaction Type. Otherwise, this needs to be updated</param>
        public void SaveTransactionType(TB_TransactionType transactionType)
        {
            if (string.IsNullOrEmpty(transactionType.TransactionTypeName))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Transaction Type Name is required.");
            }

            Repository.SaveTransactionType(transactionType);
        }
    }
}
