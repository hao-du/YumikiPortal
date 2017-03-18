using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yumiki.Business.Base;
using Yumiki.Business.MoneyTrace.Interfaces;
using Yumiki.Commons.Dictionaries;
using Yumiki.Commons.Exceptions;
using Yumiki.Commons.Helpers;
using Yumiki.Data.MoneyTrace.Interfaces;
using Yumiki.Entity.MoneyTrace;
using Yumiki.Entity.MoneyTrace.ServiceObjects;

namespace Yumiki.Business.MoneyTrace.Services
{
    public class TraceService : BaseService<ITraceRepository>, ITraceService
    {
        /// <summary>
        /// Get all Traces with filters from Database.
        /// </summary>
        /// <param name="request">All criaterias to filters the traces.</param>
        /// <returns>List of all Traces after filtered.</returns>
        public GetTraceResponse<TB_Trace> GetAllTraces(GetTraceRequest<TB_Trace> request)
        {
            return Repository.GetAllTraces(request);
        }

        /// <summary>
        /// Summary the trace to get total amount for each currency, 
        /// </summary>
        /// <param name="userID">User need to retrieved the records.</param>
        /// <returns></returns>
        public List<TraceSummary> GetTotalAmount(Guid userID)
        {
            return Repository.GetTotalAmount(userID);
        }

        /// <summary>
        /// Get summary of expense trace for each bank.
        /// </summary>
        /// <param name="userID">User need to retrieved the records.</param>
        /// <returns></returns>
        public List<TraceSummary> GetBankingSummary(Guid userID)
        {
            return Repository.GetBankingSummary(userID);
        }

        /// <summary>
        /// Get a specific Trace.
        /// NOTE: Cannot retrieve the back log trace.
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
                //Cannot get the back log traces.
                if(trace.GroupTokenID.HasValue && (trace.TransactionType == EN_TransactionType.E_INCOME || trace.TransactionType == EN_TransactionType.E_OUTCOME))
                {
                    throw new YumikiException(ExceptionCode.E_INVALID_VALUE, "Cannot get the back log trace.");
                }

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

        /// <summary>
        /// Create/Update a Trace
        /// </summary>
        /// <param name="trace">If Trace id is empty, then this is new Trace. Otherwise, this needs to be updated</param>
        public Guid SaveTrace(TB_Trace trace)
        {
            Guid traceID = Guid.Empty;
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
                    traceID = SaveTrace(trace, true);
                    break;
                case EN_TransactionType.E_OUTCOME:
                    if (trace.Amount > decimal.Zero)
                    {
                        throw new YumikiException(ExceptionCode.E_WRONG_VALUE, "Amount cannot be positive number with Outcome type.");
                    }
                    traceID = SaveTrace(trace, true);
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

                    traceID = SaveTrace(trace, true);

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
                    logTrace.BankAccountID = trace.BankAccountID;
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

                    traceID = SaveTrace(trace, true);

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
                case EN_TransactionType.E_TRANSFER:
                    if(!trace.TransferredUserID.HasValue)
                    {
                        throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Transferred User is required.");
                    }

                    traceID = SaveTrace(trace, true);
                    break;
            }

            return traceID;
        }

        private Guid SaveTrace(TB_Trace trace, bool saveTags = true)
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
            return Repository.SaveTrace(trace, saveTags);
        }

        /// <summary>
        /// Create/Update a banking withdrawing trace and logs from BankAccount.
        /// Create INCOME trace if Interest > 0.
        /// </summary>
        public void SaveBankingWithdrawingTrace(GetTraceRequest<TB_Trace> bankingTraceRequest, GetTraceRequest<TB_Trace> interestTraceRequest, TB_BankAccount bankAccount)
        {
            if(!bankAccount.WithdrawDate.HasValue 
                || bankAccount.WithdrawDate == DateTimeExtension.GetSystemMinDate()
                || bankAccount.WithdrawDate == DateTimeExtension.GetSystemMaxDate())
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Withdraw Date is required.");
            }

            if (interestTraceRequest != null && bankAccount.Interest < decimal.Zero)
            {
                throw new YumikiException(ExceptionCode.E_WRONG_VALUE, "Interest must greater than zero.");
            }

            IEnumerable<TB_Trace> traces = Repository.GetAllTraces(bankingTraceRequest).Records;

            TB_Trace bankingTrace = traces.SingleOrDefault(c => c.TransactionType == EN_TransactionType.E_BANKING);

            if(bankingTrace == null)
            {
                bankingTrace = new TB_Trace();

                bankingTrace.TransactionType = EN_TransactionType.E_BANKING;
                bankingTrace.UserID = bankAccount.UserID;
                bankingTrace.BankAccountID = bankAccount.ID;
            }

            bankingTrace.Amount = -bankAccount.Amount;
            bankingTrace.BankID = bankAccount.BankID;
            bankingTrace.CurrencyID = bankAccount.CurrencyID;
            bankingTrace.TraceDate = bankAccount.WithdrawDate.Value;
            bankingTrace.Descriptions = bankAccount.Descriptions;
            bankingTrace.Tags = bankAccount.Tags;

            SaveTrace(bankingTrace);

            if(interestTraceRequest != null && bankAccount.Interest > decimal.Zero)
            {
                TB_Trace interestTrace = Repository.GetAllTraces(interestTraceRequest).Records.SingleOrDefault();

                if (interestTrace == null)
                {
                    interestTrace = new TB_Trace();

                    interestTrace.TransactionType = EN_TransactionType.E_INCOME;
                    interestTrace.UserID = bankAccount.UserID;
                    interestTrace.BankAccountID = bankAccount.ID;
                }

                interestTrace.Amount = bankAccount.Interest.Value;
                interestTrace.BankID = bankAccount.BankID;
                interestTrace.CurrencyID = bankAccount.CurrencyID;
                interestTrace.TraceDate = bankAccount.WithdrawDate.Value;
                interestTrace.Descriptions = bankAccount.Descriptions;
                interestTrace.Tags = bankAccount.Tags;

                SaveTrace(interestTrace);
            }
        }
    }
}
