using System;
using System.Collections.Generic;
using System.Linq;
using Yumiki.Commons.Dictionaries;
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
        public List<TB_Trace> GetAllTraces(bool showInactive, Guid userID)
        {
            return Context.TB_Trace.Include(TB_Trace.FieldName.Currency).Where(c => c.IsActive == !showInactive && c.UserID == userID).OrderBy(c=>c.TraceDate).ThenBy(c=>c.LastUpdateDate).ToList();
        }

        /// <summary>
        /// Summary the trace to get total amount for each currency, 
        /// </summary>
        /// <param name="userID">User need to retrieved the records.</param>
        /// <returns></returns>
        public List<TraceSummary> GetTotalAmount(Guid userID)
        {
            List<TraceSummary> traces = Context.TB_Trace
                                        .Include(TB_Trace.FieldName.Currency)
                                        .Where(c =>
                                                c.IsActive
                                                && c.UserID == userID
                                                && (c.TransactionType == EN_TransactionType.E_INCOME || c.TransactionType == EN_TransactionType.E_OUTCOME))
                                        .GroupBy(g => g.Currency)
                                        .Select(s => new TraceSummary { CurrencyShortName = s.Key.CurrencyShortName,  TotalAmount = s.Sum(d => d.Amount) })
                                        .ToList();

            return traces;
        }

        /// <summary>
        /// Get a specific Trace.
        /// </summary>
        /// <param name="traceID">Specify id for Trace need to be retrieved.</param>
        /// <returns>Trace Object</returns>
        public TB_Trace GetTrace(Guid traceID)
        {
            return Context.TB_Trace.SingleOrDefault(c => c.ID == traceID);
        }

        /// <summary>
        /// Get a related log Trace base on GroupTokenID.
        /// </summary>
        /// <param name="traceID">Specify id is excluded from results.</param>
        /// /// <param name="groupTokenID">Specify groupTokenID for Trace need to be retrieved.</param>
        /// <returns>Trace Object</returns>
        public TB_Trace GetLogTrace(Guid traceID, Guid groupTokenID)
        {
            return Context.TB_Trace.SingleOrDefault(c => c.ID != traceID && c.GroupTokenID == groupTokenID);
        }

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
        public TB_Trace GetLogTrace(Guid traceID, Guid groupTokenID, EN_TransactionType transactionType)
        {
            return Context.TB_Trace.SingleOrDefault(c => c.ID != traceID && c.GroupTokenID == groupTokenID && c.TransactionType == transactionType);
        }

        /// <summary>
        /// Create/Update a Trace
        /// </summary>
        /// <param name="trace">If Trace id is empty, then this is new Trace. Otherwise, this needs to be updated</param>
        public void SaveTrace(TB_Trace trace, bool saveTags = true)
        {
            if (trace.ID == Guid.Empty)
            {
                Context.TB_Trace.Add(trace);
            }
            else
            {
                TB_Trace dbTrace = Context.TB_Trace.Single(c => c.ID == trace.ID);
                dbTrace.Amount = trace.Amount;
                dbTrace.Tags = trace.Tags;
                dbTrace.CurrencyID = trace.CurrencyID;
                dbTrace.TraceDate = trace.TraceDate;
                dbTrace.TransactionType = trace.TransactionType;
                dbTrace.GroupTokenID = trace.GroupTokenID;
                dbTrace.BankID = trace.BankID;
                dbTrace.UserID = trace.UserID;
                dbTrace.Descriptions = trace.Descriptions;
                dbTrace.IsActive = trace.IsActive;
            }

            if (saveTags)
            {
                SaveTags(trace.Tags, trace.UserID);
            }
            Save();
        }

        /// <summary>
        /// Save tags in "Tag A, Tag B, Tag C" string to TB_Tag table.
        /// </summary>
        /// <param name="tags">Tags with ',' separated char.</param>
        /// <param name="userID">User who creates tags.</param>
        private void SaveTags(string tags, Guid userID)
        {
            string[] tagList = tags.Split(new char[] { CommonValues.SeparateCharComma }, StringSplitOptions.RemoveEmptyEntries);

            //From existed tags to get the new tags which are not in db.
            IEnumerable<TB_Tag> existedTags = Context.TB_Tag.Where(c => tagList.Any(d => d.ToLower() == c.TagName.ToLower())).AsEnumerable();
            IEnumerable<string> newTags = tagList.Where(c => !existedTags.Any(d => d.TagName.Equals(c, StringComparison.InvariantCultureIgnoreCase)));

            foreach (string tag in newTags)
            {
                TB_Tag dbTag = new TB_Tag();
                dbTag.TagName = tag;
                dbTag.UserID = userID;

                Context.TB_Tag.Add(dbTag);
            }
        }
    }
}
