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
    [RoutePrefix("api/shopperreceipt")]
    public class ApiReceiptController : ApiBaseController<IReceiptService>
    {
        [Route("getall", Name = RouteNames.ReceiptGetAll)]
        [HttpGet()]
        public IHttpActionResult Get(bool showInactive)
        {
            try
            {
                List<TB_Receipt> receipts = BusinessService.GetReceipts(showInactive);

                IEnumerable<MD_Receipt> viewModel = receipts.Select(c => new MD_Receipt(c));

                return Ok(viewModel);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("get", Name = RouteNames.ReceiptGetByID)]
        [HttpGet()]
        public IHttpActionResult GetById(string receiptId)
        {
            try
            {
                TB_Receipt receipt = BusinessService.GetReceipt(receiptId);

                MD_Receipt viewModel = new MD_Receipt(receipt);

                return Ok(viewModel);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("getbyproductid", Name = RouteNames.ReceiptGetByProductID)]
        [HttpGet()]
        public IHttpActionResult GetByProductID(string productId)
        {
            try
            {
                List<TB_ReceiptDetail> receipts = BusinessService.GetReceiptsByProductID(productId);

                IEnumerable<MD_ReceiptDetail> viewModel = receipts.Select(c => new MD_ReceiptDetail(c));

                return Ok(viewModel);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("save", Name = RouteNames.ReceiptSave)]
        [HttpPost()]
        public IHttpActionResult Create([FromBody] MD_Receipt viewModel)
        {
            try
            {
                BusinessService.SaveReceipt(viewModel.ToObject());

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
