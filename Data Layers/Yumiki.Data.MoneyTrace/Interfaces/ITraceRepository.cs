using System;
using System.Collections.Generic;
using Yumiki.Entity.MoneyTrace;
using Yumiki.Entity.MoneyTrace.ServiceObjects;

namespace Yumiki.Data.MoneyTrace.Interfaces
{
    public interface ITraceRepository
    {
        /// <summary>
        /// Get all Traces with filters from Database.
        /// </summary>
        /// <param name="request">All criaterias to filters the traces.</param>
        /// <returns>List of all Traces after filtered.</returns>
        GetTraceResponse<TB_Trace> GetAllTraces(GetTraceRequest<TB_Trace> request);

        /// <summary>
        /// Summary the trace to get total amount for each currency, 
        /// </summary>
        /// <param name="userID">User need to retrieved the records.</param>
        /// <returns></returns>
        List<TraceSummary> GetTotalAmount(Guid userID);

        /// <summary>
        /// Get summary of expense trace for each bank.
        /// </summary>
        /// <param name="userID">User need to retrieved the records.</param>
        /// <returns></returns>
        List<TraceSummary> GetBankingSummary(Guid userID);

        /// <summary>
        /// Get a specific Trace.
        /// </summary>
        /// <param name="currencyID">Specify id for Trace need to be retrieved.</param>
        /// <returns>Trace Object</returns>
        TB_Trace GetTrace(Guid traceID);

        /// <summary>
        /// Get a related log Trace base on GroupTokenID.
        /// </summary>
        /// <param name="traceID">Specify id is excluded from results.</param>
        /// /// <param name="groupTokenID">Specify groupTokenID for Trace need to be retrieved.</param>
        /// <returns>Trace Object</returns>
        TB_Trace GetLogTrace(Guid traceID, Guid groupTokenID);

        /// <summary>
        /// This is to get the back log with GroupTokenID and Transaction Type.
        /// </summary>
        /// <param name="traceID">Specify id is excluded from results.</param>
        /// <param name="groupTokenID">Specify groupTokenID for Trace need to be retrieved.</param>
        /// <param name="transactionType">
        ///     In case of a trace has more than one back log, filter back logs also with transaction type to get expected back log.
        ///     Each back log should have a different trans type.
        /// </param>
        /// <returns>Trace Object</returns>
        TB_Trace GetLogTrace(Guid traceID, Guid groupTokenID, EN_TransactionType transactionType);

        /// <summary>
        /// Create/Update a Trace
        /// </summary>
        /// <param name="trace">If Trace id is empty, then this is new Trace. Otherwise, this needs to be updated</param>
        Guid SaveTrace(TB_Trace trace, bool saveTags = true);

        /// <summary>
        /// Get tags from keyword.
        /// </summary>
        /// <param name="keyword">Keyword to filter tag results.</param>
        /// <returns>List of tags after filter.</returns>
        List<string> GetTags(string keyword);
    }
}
