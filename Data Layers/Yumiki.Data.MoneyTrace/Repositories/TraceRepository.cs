﻿using System;
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
    public class TraceRepository : TagRepository, ITraceRepository
    {
        /// <summary>
        /// Get all Traces with filters from Database.
        /// </summary>
        /// <param name="request">All criaterias to filters the traces.</param>
        /// <returns>List of all Traces after filtered.</returns>
        public GetTraceResponse<TB_Trace> GetAllTraces(GetTraceRequest<TB_Trace> request)
        {
            DateTime startDate = DateTimeExtension.GetStartDateOfMonth(request.Month);
            DateTime endDate = DateTimeExtension.GetEndDateOfMonth(request.Month);

            IQueryable<TB_Trace> query = Context.TB_Trace
                    .Include(TB_Trace.FieldName.Currency)
                    .Where(
                            c => c.IsActive == !request.ShowInactive
                            && c.UserID == request.UserID
                        );

            if (!request.IsDisplayedAll)
            {
                query = query.Where(c => startDate <= c.TraceDate && c.TraceDate <= endDate);
            }

            if (request.BankAccountID.HasValue)
            {
                query = query.Where(c => c.BankAccountID == request.BankAccountID);
            }

            if (request.TransactionType.HasValue && request.TransactionLogType.HasValue)
            {
                query = query.Where(c => c.TransactionType == request.TransactionType.Value || c.TransactionType == request.TransactionLogType.Value);
            }
            else if (request.TransactionType.HasValue)
            {
                query = query.Where(c => c.TransactionType == request.TransactionType.Value);
            }
            else if (request.TransactionLogType.HasValue)
            {
                query = query.Where(c => c.TransactionType == request.TransactionLogType.Value);
            }

            if (request.GetLogTraceLOnly.HasValue)
            {
                query = query.Where(c => (c.GroupTokenID.HasValue && (c.TransactionType == EN_TransactionType.E_INCOME || c.TransactionType == EN_TransactionType.E_OUTCOME)) == request.GetLogTraceLOnly.Value);
            }

            if (request.GetOnlyGroupRecords)
            {
                query = query.GroupBy(c => c.GroupTokenID)
                            .Where(c => c.Count() > 1)
                            .SelectMany(c => c);
            }

            int count = query.Count();
            if (request.EnablePaging)
            {
                query = query.OrderBy(c => c.TraceDate).ThenBy(c => c.LastUpdateDate).Skip((request.CurrentPage - 1) * request.ItemsPerPage).Take(request.ItemsPerPage);
            }

            return new GetTraceResponse<TB_Trace>
            {
                Records = query.ToList(),
                CurrentPage = request.CurrentPage,
                ItemsPerPage = request.ItemsPerPage,
                TotalItems = count
            };
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
                                                    && (
                                                            c.TransactionType == EN_TransactionType.E_INCOME
                                                            || c.TransactionType == EN_TransactionType.E_OUTCOME
                                                            || c.TransactionType == EN_TransactionType.E_TRANSFER
                                                        )
                                                )
                                        .GroupBy(g => g.Currency)
                                        .Select(s => new TraceSummary { CurrencyShortName = s.Key.CurrencyShortName, TotalAmount = s.Sum(d => d.Amount) })
                                        .ToList();

            return traces;
        }

        /// <summary>
        /// Get summary of expense trace for each bank
        /// </summary>
        /// <param name="userID">User need to retrieved the records.</param>
        /// <returns></returns>
        public List<TraceSummary> GetBankingSummary(Guid userID)
        {
            List<TraceSummary> traces = Context.TB_Trace
                                        .Include(TB_Trace.FieldName.Currency)
                                        .Include(TB_Trace.FieldName.Bank)
                                        .Where(c =>
                                                c.IsActive
                                                && c.UserID == userID
                                                && c.TransactionType == EN_TransactionType.E_BANKING)
                                        .GroupBy(g => new { g.Bank, g.Currency })
                                        .Where(g => g.Sum(d => d.Amount) != 0)
                                        .Select(s => new TraceSummary { BankName = s.Key.Bank.BankName, CurrencyShortName = s.Key.Currency.CurrencyShortName, TotalAmount = s.Sum(d => d.Amount) })
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
        public Guid SaveTrace(TB_Trace trace, bool saveTags = true)
        {
            TB_Trace dbTrace;
            if (trace.ID == Guid.Empty)
            {
                dbTrace = trace;
                Context.TB_Trace.Add(dbTrace);
            }
            else
            {
                dbTrace = Context.TB_Trace.Single(c => c.ID == trace.ID);
                dbTrace.Amount = trace.Amount;
                dbTrace.Tags = trace.Tags;
                dbTrace.CurrencyID = trace.CurrencyID;
                //No need to store as UTC
                dbTrace.TraceDate = trace.TraceDate;
                dbTrace.TransactionType = trace.TransactionType;
                dbTrace.GroupTokenID = trace.GroupTokenID;
                dbTrace.BankID = trace.BankID;
                dbTrace.BankAccountID = trace.BankAccountID;
                dbTrace.UserID = trace.UserID;
                dbTrace.TransferredUserID = trace.TransferredUserID;
                dbTrace.Descriptions = trace.Descriptions;
                dbTrace.IsActive = trace.IsActive;
            }

            if (saveTags)
            {
                SaveTags(trace.Tags, trace.UserID);
            }
            Save();

            return dbTrace.ID;
        }

        /// <summary>
        /// Get a specific Trac Template.
        /// </summary>
        /// <param name="traceID">Specify id for Trace Template need to be retrieved.</param>
        /// <returns>Trace Template Object</returns>
        public TB_TraceTemplate GetTraceTemplate(Guid templateID)
        {
            ITraceTemplateRepository repo = GetAlternativeRepository<TraceTemplateRepository>();

            return repo.GetTraceTemplate(templateID);
        }
    }
}
