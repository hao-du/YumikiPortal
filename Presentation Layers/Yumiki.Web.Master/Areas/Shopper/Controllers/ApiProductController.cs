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

namespace Yumiki.Web.Shopper.Controllers
{
    [RoutePrefix("api/shopperproduct")]
    public class ApiProductController : ApiBaseController<IProductService>
    {
        [Route("getall", Name = RouteNames.ProductGetAll)]
        [HttpGet()]
        public IHttpActionResult Get(bool showInactive)
        {
            try
            {
                List<TB_Product> products = BusinessService.GetProducts(showInactive);

                return Ok(products);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("getbyterm", Name = RouteNames.ProductGetByTerm)]
        [HttpGet()]
        public IHttpActionResult Get(string term)
        {
            try
            {
                List<TB_Product> products = BusinessService.GetProducts(term);

                return Ok(products);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("get", Name = RouteNames.ProductGetByID)]
        [HttpGet()]
        public IHttpActionResult GetById(string productId)
        {
            try
            {
                TB_Product product = BusinessService.GetProduct(productId);

                return Ok(product);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("save", Name = RouteNames.ProductSave)]
        [HttpPost()]
        public IHttpActionResult Create([FromBody] TB_Product product)
        {
            try
            {
                product.UserID = CurrentUser.UserID;

                BusinessService.SaveProduct(product);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
