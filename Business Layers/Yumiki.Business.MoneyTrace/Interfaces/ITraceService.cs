using System;
using System.Collections.Generic;
using Yumiki.Entity.MoneyTrace;

namespace Yumiki.Business.MoneyTrace.Interfaces
{
    public interface ITraceService
    {
        /// <summary>
        /// Get all active Traces from Database.
        /// </summary>
        /// <param name="showInactive">Show list of inactive Traces or active Traces.</param>
        /// <returns>List of all active Traces.</returns>
        List<TB_Trace> GetAllTraces(bool showInactive, Guid userID, DateTime month, bool isDisplayedAll);

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
        /// <param name="traceID">Specify id for Trace need to be retrieved.</param>
        /// <returns>Trace Object</returns>
        TB_Trace GetTrace(string traceID);

        /// <summary>
        /// Create/Update a Trace
        /// </summary>
        /// <param name="trace">If Trace id is empty, then this is new Trace. Otherwise, this needs to be updated</param>
        void SaveTrace(TB_Trace trace);
    }
}
