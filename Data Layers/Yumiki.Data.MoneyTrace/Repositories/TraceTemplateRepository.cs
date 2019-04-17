using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Yumiki.Commons.Dictionaries;
using Yumiki.Commons.Helpers;
using Yumiki.Data.MoneyTrace.Interfaces;
using Yumiki.Entity.MoneyTrace;
using Yumiki.Entity.MoneyTrace.ServiceObjects;

namespace Yumiki.Data.MoneyTrace.Repositories
{
    public class TraceTemplateRepository : TagRepository, ITraceTemplateRepository
    {
        /// <summary>
        /// Get all trace templates
        /// </summary>
        /// <returns>List of Trace Template</returns>
        public List<TB_TraceTemplate> GetAllTemplates(bool showInactive, Guid userID)
        {
            IQueryable<TB_TraceTemplate> queryable = Context.TB_TraceTemplate
                                                                        .Include(TB_TraceTemplate.FieldName.Currency)
                                                                        .Include(TB_TraceTemplate.FieldName.TransferredUser)
                                                                        .Where(c => c.IsActive == !showInactive && c.UserID == userID);

            return queryable.ToList();
        }

        /// <summary>
        /// Get a specific Trac Template.
        /// </summary>
        /// <param name="traceID">Specify id for Trace Template need to be retrieved.</param>
        /// <returns>Trace Template Object</returns>
        public TB_TraceTemplate GetTraceTemplate(Guid templateID)
        {
            return Context.TB_TraceTemplate.SingleOrDefault(c => c.ID == templateID);
        }

        /// <summary>
        /// Create/Update a Trace Template.
        /// </summary>
        /// <param name="traceTemplate">If Trace Template id is empty, then this is new Trace Template. Otherwise, this needs to be updated</param>
        public Guid SaveTraceTemplate(TB_TraceTemplate traceTemplate, bool saveTags = true)
        {
            TB_TraceTemplate dbTraceTemplate;
            if (traceTemplate.ID == Guid.Empty)
            {
                dbTraceTemplate = traceTemplate;
                Context.TB_TraceTemplate.Add(dbTraceTemplate);
            }
            else
            {
                dbTraceTemplate = Context.TB_TraceTemplate.Single(c => c.ID == traceTemplate.ID);
                dbTraceTemplate.TemplateName = traceTemplate.TemplateName;
                dbTraceTemplate.Amount = traceTemplate.Amount;
                dbTraceTemplate.Tags = traceTemplate.Tags;
                dbTraceTemplate.CurrencyID = traceTemplate.CurrencyID;
                dbTraceTemplate.TransactionType = traceTemplate.TransactionType;
                dbTraceTemplate.UserID = traceTemplate.UserID;
                dbTraceTemplate.TransferredUserID = traceTemplate.TransferredUserID;
                dbTraceTemplate.Descriptions = traceTemplate.Descriptions;
                dbTraceTemplate.IsActive = traceTemplate.IsActive;
            }

            if (saveTags)
            {
                SaveTags(traceTemplate.Tags, traceTemplate.UserID);
            }
            Save();

            return dbTraceTemplate.ID;
        }
    }
}
