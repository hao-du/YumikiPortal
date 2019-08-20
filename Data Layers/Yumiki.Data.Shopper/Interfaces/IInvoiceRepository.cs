using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Commons.Entities.Parameters;
using Yumiki.Data.Base;
using Yumiki.Entity.Shopper.ServiceObjects;
using Yumiki.Entity.Shopper;

namespace Yumiki.Data.Shopper.Interfaces
{
    public interface IInvoiceRepository
    {
        GetResponse<TB_Invoice> GetInvoices(GetShopperRequest<TB_Invoice> request);

        TB_Invoice GetInvoice(Guid invoiceID);

        List<TB_InvoiceDetail> GetInvoicesByProductID(Guid productID);

        void SaveInvoice(TB_Invoice invoice);
    }
}
