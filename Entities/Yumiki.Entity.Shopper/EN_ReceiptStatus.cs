using System.ComponentModel;

namespace Yumiki.Entity.Shopper
{
    public enum EN_ReceiptStatus
    {
        [Description("Ordered")]
        E_ORDERED,
        [Description("Waiting")]
        E_WAITING,
        [Description("Completed")]
        E_COMPLETED,
        [Description("Cancelled")]
        E_CANCELLED
    }
}
