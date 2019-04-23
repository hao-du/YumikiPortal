using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yumiki.Business.Base;
using Yumiki.Business.Shopper.Interfaces;
using Yumiki.Commons.Entities;
using Yumiki.Commons.Exceptions;
using Yumiki.Commons.Helpers;
using Yumiki.Data.Shopper.Interfaces;
using Yumiki.Entity.Shopper;

namespace Yumiki.Business.Shopper.Services
{
    public class InvoiceService : BaseService<IInvoiceRepository>, IInvoiceService
    {
        public List<TB_Invoice> GetInvoices(bool showInactive)
        {
            return Repository.GetInvoices(showInactive);
        }

        public TB_Invoice GetInvoice(string invoiceID)
        {
            if (string.IsNullOrWhiteSpace(invoiceID))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Invoice ID cannot be empty.", Logger);
            }

            Guid convertedInvoiceID = Guid.Empty;
            Guid.TryParse(invoiceID, out convertedInvoiceID);
            if (convertedInvoiceID == Guid.Empty)
            {
                throw new YumikiException(ExceptionCode.E_WRONG_TYPE, "Invoice ID must be GUID type.", Logger);
            }

            return Repository.GetInvoice(convertedInvoiceID);
        }

        public void SaveInvoice(TB_Invoice invoice)
        {
            if (invoice.TotalAmount <= decimal.Zero)
            {
                throw new YumikiException(ExceptionCode.E_WRONG_VALUE, "Total Amount must be greater than 0.", Logger);
            }

            Repository.SaveInvoice(invoice);
        }

        public List<ExtendEnum> GetStatuses()
        {
            return EnumHelper.GetDatasource<EN_InvoiceStatus>();
        }
    }
}
