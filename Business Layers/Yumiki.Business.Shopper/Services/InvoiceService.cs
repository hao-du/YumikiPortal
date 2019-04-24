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
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Invoice ID is required.", Logger);
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
            if (string.IsNullOrWhiteSpace(invoice.InvoiceNumber))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Invoice Number is required.", Logger);
            }

            if (string.IsNullOrWhiteSpace(invoice.CustomerName))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Customer Name is required.", Logger);
            }

            if (string.IsNullOrWhiteSpace(invoice.CustomerAddress))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Customer Address is required.", Logger);
            }

            if (string.IsNullOrWhiteSpace(invoice.CustomerPhone))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Customer Phone is required.", Logger);
            }

            if (invoice.InvoiceDate == DateTime.MinValue)
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Invoice Date is required.", Logger);
            }

            if (!invoice.InvoiceDetails.Any())
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Invoice Details is required.", Logger);
            }

            if (invoice.Status <= 0)
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "Status is required.", Logger);
            }

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
