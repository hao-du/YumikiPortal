using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Yumiki.Web.MoneyTrace.Constants
{
    public class RouteNames
    {
        public const string DefaultMVCRoute = "MoneyTrace_default";

        public const string UserGetAll = "UserGetAll";

        public const string CurrencyGetAll = "CurrencyGetAll";
        public const string CurrencyGetAllWithShareableItems = "CurrencyGetAllWithShareableItems";
        public const string CurrencyGetByID = "CurrencyGetByID";
        public const string CurrencySave = "CurrencySave";

        public const string BankGetAll = "BankGetAll";
        public const string BankGetAllWithShareableItems = "BankGetAllWithShareableItems";
        public const string BankGetByID = "BankGetByID";
        public const string BankSave = "BankSave";

        public const string BankAccountGetAll = "BankAccountGetAll";
        public const string BankAccountGetByID = "BankAccountGetByID";
        public const string BankAccountSave = "BankAccountSave";

        public const string TraceGetAll = "TraceGetAll";
        public const string TraceGetTotalSummary = "TraceGetTotalSummary";
        public const string TraceGetBankingSummary = "TraceGetBankingSummary";
        public const string TraceGetByID = "TraceGetByID";
        public const string TraceGetTags = "TraceGetTags";
        public const string TraceSave = "TraceSave";
        public const string SaveBankingWithdrawingTrace = "SaveBankingWithdrawingTrace";

        public const string TraceTemplateGetAll = "TraceTemplateGetAll";
        public const string TraceTemplateGetByID = "TraceTemplateGetByID";
        public const string TraceTemplateGetTags = "TraceTemplateGetTags";
        public const string TraceTemplateSave = "TraceTemplateSave";

        public const string ReportGenerateReport = "ReportGenerateReport";
        public const string ReportGetReportTypes = "ReportGetReportTypes";
        public const string ReportGetTransactionTypes = "ReportGetTransactionTypes";
    }
}