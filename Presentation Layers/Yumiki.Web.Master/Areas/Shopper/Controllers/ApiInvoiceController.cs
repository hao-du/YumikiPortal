using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Yumiki.Business.Shopper.Interfaces;
using Yumiki.Commons.Settings;
using Yumiki.Entity.Shopper;
using Yumiki.Web.Base;
using Yumiki.Web.Shopper.Models;
using Yumiki.Web.Shopper.Constants;

namespace Yumiki.Web.Shopper.Controllers
{
    [RoutePrefix("api/shopperinvoice")]
    public class ApiInvoiceController : ApiBaseController<IInvoiceService>
    {
        [Route("getall", Name = RouteNames.InvoiceGetAll)]
        [HttpGet()]
        public IHttpActionResult Get(bool showInactive)
        {
            try
            {
                List<TB_Invoice> invoices = BusinessService.GetInvoices(showInactive);

                IEnumerable<MD_Invoice> viewModel = invoices.Select(c => new MD_Invoice(c));

                return Ok(viewModel);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("get", Name = RouteNames.InvoiceGetByID)]
        [HttpGet()]
        public IHttpActionResult GetById(string invoiceId)
        {
            try
            {
                TB_Invoice invoice = BusinessService.GetInvoice(invoiceId);

                MD_Invoice viewModel = new MD_Invoice(invoice);

                return Ok(viewModel);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("getbyproductid", Name = RouteNames.InvoiceGetByProductID)]
        [HttpGet()]
        public IHttpActionResult GetByProductID(string productId)
        {
            try
            {
                List<TB_InvoiceDetail> invoices = BusinessService.GetInvoicesByProductID(productId);

                IEnumerable<MD_InvoiceDetail> viewModel = invoices.Select(c => new MD_InvoiceDetail(c));

                return Ok(viewModel);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("save", Name = RouteNames.InvoiceSave)]
        [HttpPost()]
        public IHttpActionResult Create([FromBody] MD_Invoice viewModel)
        {
            try
            {
                BusinessService.SaveInvoice(viewModel.ToObject());

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
