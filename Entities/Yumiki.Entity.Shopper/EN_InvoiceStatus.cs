using System.ComponentModel;

namespace Yumiki.Entity.Shopper
{
    public enum EN_InvoiceStatus
    {
        [Description("Created")]
        E_CREATED,
        [Description("In Process")]
        E_IN_PROCESS,
        [Description("Waiting COD")]
        E_WAITING_COD,
        [Description("Cancelled")]
        E_CANCELLED,
        [Description("Cancelled and Lost")]
        E_CANCELLED_LOST,
        [Description("Completed")]
        E_COMPLETED
    }
}
