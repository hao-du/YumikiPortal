﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Data.MoneyTrace.Interfaces;
using Yumiki.Entity.MoneyTrace;

namespace Yumiki.Data.MoneyTrace.Interfaces
{
    public interface ITraceRepository
    {
        /// <summary>
        /// Get all active Traces from Database.
        /// </summary>
        /// <param name="showInactive">Show list of inactive Traces or active Traces.</param>
        /// <returns>List of all active Traces.</returns>
        List<TB_Trace> GetAllTraces(bool showInactive, Guid userID);

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
        void SaveTrace(TB_Trace trace, bool saveTags = true);
    }
}