﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Commons.Comparers;
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
        public List<TB_Trace> GetAllTraces(bool showInactive)
        {
            return Context.TB_Trace.Include(TB_Tag.FieldName.TB_Tag).Include(TB_Currency.FieldName.TB_Currency).Where(c => c.IsActive == !showInactive).OrderByDescending(c=>c.TraceDate).ToList();
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
            IEnumerable<TB_Tag> existedTags = Context.TB_Tag.Where(c => tagList.Contains(c.TagName, new LowerCaseStringComparer())).AsEnumerable();
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
