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
        /// <summary>
        /// Customer cancels order before products are sent to transporter
        /// In transit but Customer cancels in the middle and products are returned back to Stock.
        /// </summary>
        [Description("Cancelled")]
        E_CANCELLED,
        /// <summary>
        /// In transit but Customer cancels in the middle and products are not returned back.
        /// </summary>
        [Description("Cancelled and Lost")]
        E_CANCELLED_LOST,
        [Description("Completed")]
        E_COMPLETED,
        [Description("Unpacked")]
        E_UNPACKED
    }
}
