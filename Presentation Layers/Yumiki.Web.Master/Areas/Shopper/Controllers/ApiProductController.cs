using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Yumiki.Business.Shopper.Interfaces;
using Yumiki.Commons.Entities.Parameters;
using Yumiki.Commons.Settings;
using Yumiki.Entity.Shopper;
using Yumiki.Entity.Shopper.ServiceObjects;
using Yumiki.Web.Base;
using Yumiki.Web.Shopper.Constants;

namespace Yumiki.Web.Shopper.Controllers
{
    [RoutePrefix("api/shopperproduct")]
    public class ApiProductController : ApiBaseController<IProductService>
    {
        [Route("getall", Name = RouteNames.ProductGetAll)]
        [HttpGet()]
        public IHttpActionResult Get(bool showInactive, int currentPage, int itemsPerPage)
        {
            try
            {
                GetShopperRequest<TB_Product> request = new GetShopperRequest<TB_Product>
                {
                    ShowInactive = showInactive,
                    CurrentPage = currentPage,
                    ItemsPerPage = itemsPerPage
                };

                GetResponse<TB_Product> response = BusinessService.GetProducts(request);

                return Ok(response);
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
                GetShopperRequest<TB_Product> request = new GetShopperRequest<TB_Product>
                {
                    ShowInactive = false,
                    EnablePaging = false,
                    ProductTerm = term
                };

                GetResponse<TB_Product> response = BusinessService.GetProducts(request);

                return Ok(response.Records);
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
