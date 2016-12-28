﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Business.Base;
using Yumiki.Business.MoneyTrace.Interfaces;
using Yumiki.Commons.Dictionaries;
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
        public List<TB_Trace> GetAllTraces(bool showInactive, Guid userID)
        {
            return Repository.GetAllTraces(showInactive, userID);
        }

        /// <summary>
        /// Get a specific Trace.
        /// </summary>
        /// <param name="traceID">Specify id for Trace need to be retrieved.</param>
        /// <returns>Trace Object</returns>
        public TB_Trace GetTrace(string traceID)
        {
            if (string.IsNullOrWhiteSpace(traceID))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Trace cannot be empty.");
            }

            Guid convertedTraceID = Guid.Empty;
            Guid.TryParse(traceID, out convertedTraceID);
            if (convertedTraceID == Guid.Empty)
            {
                throw new YumikiException(ExceptionCode.E_WRONG_TYPE, "Trace ID must be GUID type.");
            }

            TB_Trace trace = Repository.GetTrace(convertedTraceID);
            if (trace != null)
            {
                if (trace.TransactionType == EN_TransactionType.E_EXCHANGE)
                {
                    TB_Trace logTrace = Repository.GetLogTrace(trace.ID, trace.GroupTokenID.Value, EN_TransactionType.E_INCOME);

                    trace.ExchangeAmount = logTrace.Amount;
                    trace.ExchangeCurrencyID = logTrace.CurrencyID;
                }
            }
            return trace;
        }

        /// <summary>
        /// Create/Update a Trace
        /// </summary>
        /// <param name="trace">If Trace id is empty, then this is new Trace. Otherwise, this needs to be updated</param>
        public void SaveTrace(TB_Trace trace)
        {
            if (trace.Amount == decimal.Zero)
            {
                throw new YumikiException(ExceptionCode.E_WRONG_VALUE, "Amount cannot be zero.");
            }

            if (trace.CurrencyID == Guid.Empty)
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Currency is required.");
            }

            if (trace.TraceDate == DateTime.MinValue)
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Trace Date is required.");
            }

            switch (trace.TransactionType)
            {
                //If trace is income (monthly salary) or outcome (shopping) (one way transaction), only save a trace record to db.
                case EN_TransactionType.E_INCOME:
                    if (trace.Amount < decimal.Zero)
                    {
                        throw new YumikiException(ExceptionCode.E_WRONG_VALUE, "Amount cannot be negative number with Income type.");
                    }
                    SaveTrace(trace, true);
                    break;
                case EN_TransactionType.E_OUTCOME:
                    if (trace.Amount > decimal.Zero)
                    {
                        throw new YumikiException(ExceptionCode.E_WRONG_VALUE, "Amount cannot be positive number with Outcome type.");
                    }
                    SaveTrace(trace, true);
                    break;
                //Two way transaction, withdraw money from personal wallet and deposite to Bank, save 2 records with GroupTokenId to reconize the trace relationship.
                case EN_TransactionType.E_BANKING:
                    if (!trace.BankID.HasValue || trace.BankID.Value == Guid.Empty)
                    {
                        throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Bank is required.");
                    }

                    if (!trace.GroupTokenID.HasValue)
                    {
                        trace.GroupTokenID = Guid.NewGuid();
                    }

                    SaveTrace(trace, true);

                    //Save the backLog record.
                    TB_Trace logTrace = Repository.GetLogTrace(trace.ID, trace.GroupTokenID.Value);
                    if (logTrace == null)
                    {
                        logTrace = new TB_Trace();
                    }
                    logTrace.Amount = -trace.Amount;
                    logTrace.Tags = trace.Tags;
                    logTrace.CurrencyID = trace.CurrencyID;
                    logTrace.BankID = trace.BankID;
                    logTrace.TraceDate = trace.TraceDate;
                    logTrace.TransactionType = logTrace.Amount < decimal.Zero ? EN_TransactionType.E_OUTCOME : EN_TransactionType.E_INCOME;
                    logTrace.GroupTokenID = trace.GroupTokenID;
                    logTrace.UserID = trace.UserID;
                    logTrace.Descriptions = trace.Descriptions;

                    SaveTrace(logTrace, false);
                    break;
                case EN_TransactionType.E_EXCHANGE:
                    if (trace.Amount > decimal.Zero)
                    {
                        throw new YumikiException(ExceptionCode.E_WRONG_VALUE, "Amount cannot be positive number with Exchange type.");
                    }

                    if (trace.ExchangeAmount < decimal.Zero)
                    {
                        throw new YumikiException(ExceptionCode.E_WRONG_VALUE, "Exchange Amount cannot be negative number with Exchange type.");
                    }

                    if (!trace.ExchangeCurrencyID.HasValue)
                    {
                        throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Exchange Currency is required.");
                    }

                    if (!trace.GroupTokenID.HasValue)
                    {
                        trace.GroupTokenID = Guid.NewGuid();
                    }

                    SaveTrace(trace, true);

                    //Save income backLog record.
                    logTrace = Repository.GetLogTrace(trace.ID, trace.GroupTokenID.Value, EN_TransactionType.E_INCOME);
                    if (logTrace == null)
                    {
                        logTrace = new TB_Trace();
                    }
                    logTrace.Amount = trace.ExchangeAmount;
                    logTrace.Tags = trace.Tags;
                    logTrace.CurrencyID = trace.ExchangeCurrencyID.Value;
                    logTrace.TraceDate = trace.TraceDate;
                    logTrace.TransactionType = EN_TransactionType.E_INCOME;
                    logTrace.GroupTokenID = trace.GroupTokenID;
                    logTrace.UserID = trace.UserID;
                    logTrace.Descriptions = trace.Descriptions;

                    SaveTrace(logTrace, false);

                    //Save outcome backLog record.
                    logTrace = Repository.GetLogTrace(trace.ID, trace.GroupTokenID.Value, EN_TransactionType.E_OUTCOME);
                    if (logTrace == null)
                    {
                        logTrace = new TB_Trace();
                    }
                    logTrace.Amount = trace.Amount;
                    logTrace.Tags = trace.Tags;
                    logTrace.CurrencyID = trace.CurrencyID;
                    logTrace.TraceDate = trace.TraceDate;
                    logTrace.TransactionType = EN_TransactionType.E_OUTCOME;
                    logTrace.GroupTokenID = trace.GroupTokenID;
                    logTrace.UserID = trace.UserID;
                    logTrace.Descriptions = trace.Descriptions;

                    SaveTrace(logTrace, false);
                    break;
            }
        }

        private void SaveTrace(TB_Trace trace, bool saveTags = true)
        {
            if (saveTags && !string.IsNullOrWhiteSpace(trace.Tags))
            {
                //Decorate tags string by trim all whitespace and remove empty/duplicated tags
                IEnumerable<string> tags = trace.Tags
                    .Split(new char[] { CommonValues.SeparateCharComma, CommonValues.SeparateCharPeriodComma }, StringSplitOptions.RemoveEmptyEntries)
                    .Where(c=> !string.IsNullOrWhiteSpace(c))
                    .Select(c=>c.Trim())
                    .Distinct();

                if(tags.Count() > 0)
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
                    trace.Tags = sb.ToString();
                }
            }
            Repository.SaveTrace(trace, saveTags);
        }
    }
}