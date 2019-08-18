using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Data.Base;
using Yumiki.Entity.Shopper;

namespace Yumiki.Data.Shopper.Interfaces
{
    public interface IInvoiceRepository
    {
        List<TB_Invoice> GetInvoices(bool showInactive);

        TB_Invoice GetInvoice(Guid invoiceID);

        List<TB_InvoiceDetail> GetInvoicesByProductID(Guid productID);

        void SaveInvoice(TB_Invoice invoice);
    }
}
