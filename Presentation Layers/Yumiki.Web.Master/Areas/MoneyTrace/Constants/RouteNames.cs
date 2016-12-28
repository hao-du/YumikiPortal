using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Yumiki.Web.MoneyTrace.Constants
{
    public class RouteNames
    {
        public const string DefaultMVCRoute = "MoneyTrace_default";

        public const string CurrencyGetAll = "CurrencyGetAll";
        public const string CurrencyGetAllWithShareableItems = "CurrencyGetAllWithShareableItems";
        public const string CurrencyGetByID = "CurrencyGetByID";
        public const string CurrencySave = "CurrencySave";

        public const string BankGetAll = "BankGetAll";
        public const string BankGetAllWithShareableItems = "BankGetAllWithShareableItems";
        public const string BankGetByID = "BankGetByID";
        public const string BankSave = "BankSave";

        public const string TraceGetAll = "TraceGetAll";
        public const string TraceGetByID = "TraceGetByID";
        public const string TraceSave = "TraceSave";
    }
}