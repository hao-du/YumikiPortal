using System.ComponentModel;

namespace Yumiki.Entity.MoneyTrace
{
    public enum EN_TransactionType
    {
        [Description("Income")]
        E_INCOME,
        [Description("Outcome")]
        E_OUTCOME,
        [Description("Banking")]
        E_BANKING,
        [Description("Exchange")]
        E_EXCHANGE,
        [Description("Transfer")]
        E_TRANSFER
    }
}
