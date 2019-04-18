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
    public class ApiFeeTypeController : ApiBaseController<IFeeTypeService>
    {
        [Route("getall", Name = RouteNames.FeeTypeGetAll)]
        [HttpGet()]
        public IHttpActionResult Get(bool showInactive)
        {
            try
            {
                List<TB_FeeType> feeTypes = BusinessService.GetFeeTypes(showInactive);

                return Ok(feeTypes);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("get", Name = RouteNames.FeeTypeGetByID)]
        [HttpGet()]
        public IHttpActionResult GetById(string feeTypeId)
        {
            try
            {
                TB_FeeType feeType = BusinessService.GetFeeType(feeTypeId);

                return Ok(feeType);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("save", Name = RouteNames.FeeTypeSave)]
        [HttpPost()]
        public IHttpActionResult Create([FromBody] object item)
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
