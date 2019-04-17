using System;
using System.Collections.Generic;
using Yumiki.Data.Base;
using Yumiki.Entity.MoneyTrace;
using Yumiki.Entity.MoneyTrace.ServiceObjects;

namespace Yumiki.Data.MoneyTrace.Interfaces
{
    public interface ITraceTemplateRepository : IShareableRepository<MoneyTraceModel>
    {
        /// <summary>
        /// Get all trace templates
        /// </summary>
        /// <returns>List of Trace Template</returns>
        List<TB_TraceTemplate> GetAllTemplates(bool showInactive, Guid userID);

        /// <summary>
        /// Get a specific Trac Template.
        /// </summary>
        /// <param name="traceID">Specify id for Trace Template need to be retrieved.</param>
        /// <returns>Trace Template Object</returns>
        TB_TraceTemplate GetTraceTemplate(Guid templateID);

        /// <summary>
        /// Create/Update a Trace Template.
        /// </summary>
        /// <param name="traceTemplate">If Trace Template id is empty, then this is new Trace Template. Otherwise, this needs to be updated</param>
        Guid SaveTraceTemplate(TB_TraceTemplate traceTemplate, bool saveTags = true);

        /// <summary>
        /// Get tags from keyword.
        /// </summary>
        /// <param name="keyword">Keyword to filter tag results.</param>
        /// <returns>List of tags after filter.</returns>
        List<string> GetTags(string keyword);
    }
}
