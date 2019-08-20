using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Business.Base;
using Yumiki.Business.Shopper.Interfaces;
using Yumiki.Commons.Entities;
using Yumiki.Commons.Entities.Parameters;
using Yumiki.Commons.Exceptions;
using Yumiki.Data.Shopper.Interfaces;
using Yumiki.Entity.Shopper;
using Yumiki.Entity.Shopper.ServiceObjects;

namespace Yumiki.Business.Shopper.Interfaces
{
    public interface IInvoiceService
    {
        GetResponse<TB_Invoice> GetInvoices(GetShopperRequest<TB_Invoice> request);

        TB_Invoice GetInvoice(string invoiceID);

        List<TB_InvoiceDetail> GetInvoicesByProductID(string productID);

        void SaveInvoice(TB_Invoice invoice);

        List<ExtendEnum> GetStatuses();
    }
}
