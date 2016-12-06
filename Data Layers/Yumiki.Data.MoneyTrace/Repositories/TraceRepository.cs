using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Data.MoneyTrace.Interfaces;
using Yumiki.Entity.MoneyTrace;

namespace Yumiki.Data.MoneyTrace.Repositories
{
    public class TraceRepository : MoneyTraceRepository, ITraceRepository
    {
        /// <summary>
        /// Get all active Traces from Database.
        /// </summary>
        /// <param name="showInactive">Show list of inactive Traces or active Traces.</param>
        /// <returns>List of all active Traces.</returns>
        public List<TB_Trace> GetAllTraces(bool showInactive)
        {
            return Context.TB_Trace.Where(c => c.IsActive == !showInactive).OrderByDescending(c=>c.TraceDate).ToList();
        }

        /// <summary>
        /// Get a specific Trace.
        /// </summary>
        /// <param name="traceID">Specify id for Trace need to be retrieved.</param>
        /// <returns>Trace Object</returns>
        public TB_Trace GetTrace(Guid traceID)
        {
            return Context.TB_Trace.Where(c => c.ID == traceID).SingleOrDefault();
        }

        /// <summary>
        /// Create/Update a Trace
        /// </summary>
        /// <param name="trace">If Trace id is empty, then this is new Trace. Otherwise, this needs to be updated</param>
        public void SaveTrace(TB_Trace trace)
        {
            if (trace.ID == Guid.Empty)
            {
                Context.TB_Trace.Add(trace);
            }
            else
            {
                TB_Trace dbTrace = Context.TB_Trace.Where(c => c.ID == trace.ID).Single();
                dbTrace.Amount = trace.Amount;
                dbTrace.CategoryID = trace.CategoryID;
                dbTrace.CurrencyID = trace.CurrencyID;
                dbTrace.TraceDate = trace.TraceDate;
                dbTrace.Descriptions = trace.Descriptions;
                dbTrace.IsActive = trace.IsActive;
            }

            Save();
        }
    }
}
