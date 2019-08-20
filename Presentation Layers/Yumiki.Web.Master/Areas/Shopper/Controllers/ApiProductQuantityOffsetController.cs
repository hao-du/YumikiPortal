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
using Yumiki.Web.Shopper.Constants;
using Yumiki.Web.Shopper.Models;

namespace Yumiki.Web.Shopper.Controllers
{
    [RoutePrefix("api/shopperproductquantityoffset")]
    public class ApiProductQuantityOffsetController : ApiBaseController<IProductQuantityOffsetService>
    {
        [Route("getall", Name = RouteNames.ProductOffsetGetAll)]
        [HttpGet()]
        public IHttpActionResult Get(bool showInactive, string productID)
        {
            try
            {
                List<TB_ProductQuantityOffset> productQuantityOffsets = BusinessService.GetProductQuantityOffsets(showInactive, productID);

                IEnumerable<MD_ProductQuantityOffset> viewModel = productQuantityOffsets.Select(c => new MD_ProductQuantityOffset(c));

                return Ok(viewModel);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("get", Name = RouteNames.ProductOffsetByID)]
        [HttpGet()]
        public IHttpActionResult GetById(string offsetID)
        {
            try
            {
                TB_ProductQuantityOffset productQuantityOffset = BusinessService.GetProductQuantityOffset(offsetID);

                MD_ProductQuantityOffset viewModel = new MD_ProductQuantityOffset(productQuantityOffset);

                return Ok(viewModel);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("save", Name = RouteNames.ProductOffsetSave)]
        [HttpPost()]
        public IHttpActionResult Create([FromBody] MD_ProductQuantityOffset offset)
        {
            try
            {
                BusinessService.SaveProductQuantityOffset(offset.ToObject());

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}