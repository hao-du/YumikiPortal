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
    public class TraceService : BaseService<ITraceRepository>, ITraceService
    {
        /// <summary>
        /// Get all active Trace from Database.
        /// </summary>
        /// <param name="showInactive">Show list of inactive Trace or active Trace.</param>
        /// <returns>List of all active Trace.</returns>
        public List<TB_Trace> GetAllTraces(bool showInactive)
        {
            return Repository.GetAllTraces(showInactive);
        }

        /// <summary>
        /// Get a specific Trace.
        /// </summary>
        /// <param name="traceID">Specify id for Trace need to be retrieved.</param>
        /// <returns>Trace Object</returns>
        public TB_Trace GetTrace(string traceID)
        {
            if (string.IsNullOrEmpty(traceID))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Trace cannot be empty.");
            }

            Guid convertedTraceID = Guid.Empty;
            Guid.TryParse(traceID, out convertedTraceID);
            if (convertedTraceID == Guid.Empty)
            {
                throw new YumikiException(ExceptionCode.E_WRONG_TYPE, "Trace ID must be GUID type.");
            }

            return Repository.GetTrace(convertedTraceID);
        }

        /// <summary>
        /// Create/Update a Trace
        /// </summary>
        /// <param name="trace">If Trace id is empty, then this is new Trace. Otherwise, this needs to be updated</param>
        public void SaveTrace(TB_Trace trace)
        {
            if (trace.Amount == decimal.Zero)
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Amount cannot be zero.");
            }
            if (trace.CategoryID == Guid.Empty)
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Category is required.");
            }
            if (trace.CurrencyID == Guid.Empty)
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Currency is required.");
            }
            if (trace.TraceDate == DateTime.MinValue)
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Trace Date is required.");
            }

            Repository.SaveTrace(trace);
        }
    }
}
