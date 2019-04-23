using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Business.Base;
using Yumiki.Business.Shopper.Interfaces;
using Yumiki.Commons.Entities;
using Yumiki.Commons.Exceptions;
using Yumiki.Data.Shopper.Interfaces;
using Yumiki.Entity.Shopper;

namespace Yumiki.Business.Shopper.Interfaces
{
    public interface IInvoiceService
    {
        List<TB_Invoice> GetInvoices(bool showInactive);

        TB_Invoice GetInvoice(string invoiceID);

        void SaveInvoice(TB_Invoice invoice);

        List<ExtendEnum> GetStatuses();
    }
}
