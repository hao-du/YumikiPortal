using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yumiki.Business.Base;
using Yumiki.Business.MoneyTrace.Interfaces;
using Yumiki.Commons.Dictionaries;
using Yumiki.Commons.Exceptions;
using Yumiki.Commons.Helpers;
using Yumiki.Commons.Settings;
using Yumiki.Data.MoneyTrace.Interfaces;
using Yumiki.Entity.MoneyTrace;
using Yumiki.Entity.MoneyTrace.ServiceObjects;

namespace Yumiki.Business.MoneyTrace.Services
{
    public class TraceTemplateService : BaseService<ITraceTemplateRepository>, ITraceTemplateService
    {
        /// <summary>
        /// Get all trace templates
        /// </summary>
        /// <returns>List of Trace Template</returns>
        public List<TB_TraceTemplate> GetAllTemplates(bool showInactive, Guid userID)
        {
            return Repository.GetAllTemplates(showInactive, userID);
        }

        /// <summary>
        /// Get a specific Trac Template.
        /// </summary>
        /// <param name="traceID">Specify id for Trace Template need to be retrieved.</param>
        /// <returns>Trace Template Object</returns>
        public TB_TraceTemplate GetTraceTemplate(string templateID)
        {
            if (string.IsNullOrWhiteSpace(templateID))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Template ID cannot be empty.", Logger);
            }

            Guid convertedTemplateID = Guid.Empty;
            Guid.TryParse(templateID, out convertedTemplateID);
            if (convertedTemplateID == Guid.Empty)
            {
                throw new YumikiException(ExceptionCode.E_WRONG_TYPE, "Template ID must be GUID type.", Logger);
            }

            return Repository.GetTraceTemplate(convertedTemplateID);
        }

        /// <summary>
        /// Create/Update a Trace Template.
        /// </summary>
        /// <param name="traceTemplate">If Trace Template id is empty, then this is new Trace Template. Otherwise, this needs to be updated</param>
        public Guid SaveTraceTemplate(TB_TraceTemplate traceTemplate)
        {
            if (traceTemplate.Amount == decimal.Zero)
            {
                throw new YumikiException(ExceptionCode.E_WRONG_VALUE, "Amount cannot be zero.", Logger);
            }

            if (traceTemplate.CurrencyID == Guid.Empty)
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Currency is required.", Logger);
            }

            if (traceTemplate.TransferredUserID.HasValue)
            {
                traceTemplate.TransactionType = EN_TransactionType.E_TRANSFER;
            }
            else if (traceTemplate.Amount > decimal.Zero)
            {
                traceTemplate.TransactionType = EN_TransactionType.E_INCOME;
            }
            else
            {
                traceTemplate.TransactionType = EN_TransactionType.E_OUTCOME;
            }

            return SaveTraceTemplate(traceTemplate, true);
        }

        private Guid SaveTraceTemplate(TB_TraceTemplate traceTemplate, bool saveTags = true)
        {
            if (saveTags && !string.IsNullOrWhiteSpace(traceTemplate.Tags))
            {
                //Decorate tags string by trim all whitespace and remove empty/duplicated tags
                IEnumerable<string> tags = traceTemplate.Tags
                    .Split(new char[] { CommonValues.SeparateCharComma, CommonValues.SeparateCharPeriodComma }, StringSplitOptions.RemoveEmptyEntries)
                    .Where(c => !string.IsNullOrWhiteSpace(c))
                    .Select(c => c.Trim())
                    .Distinct();

                if (tags.Count() > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < tags.Count(); i++)
                    {
                        sb.Append(tags.ElementAt(i).Trim());
                        if (i < tags.Count() - 1)
                        {
                            sb.Append(CommonValues.SeparateCharComma);
                        }
                    }
                    traceTemplate.Tags = sb.ToString();
                }
            }
            return Repository.SaveTraceTemplate(traceTemplate, saveTags);
        }

        /// <summary>
        /// Get tags from keyword.
        /// </summary>
        /// <param name="keyword">Keyword to filter tag results.</param>
        /// <returns>List of tags after filter.</returns>
        public List<string> GetTags(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return new List<string>();
            }

            return Repository.GetTags(keyword);
        }
    }
}
