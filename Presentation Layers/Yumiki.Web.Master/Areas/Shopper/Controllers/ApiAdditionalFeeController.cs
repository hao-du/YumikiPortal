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
    [RoutePrefix("api/shopperadditionalfee")]
    public class ApiAdditionalFeeController : ApiBaseController<IAdditionalFeeService>
    {
        [Route("getall", Name = RouteNames.AdditionalFeeGetAll)]
        [HttpGet()]
        public IHttpActionResult Get(bool showInactive)
        {
            try
            {
                List<TB_AdditionalFee> additionalFees = BusinessService.GetAdditionalFees(showInactive);

                return Ok(additionalFees);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("get", Name = RouteNames.AdditionalFeeGetByID)]
        [HttpGet()]
        public IHttpActionResult GetById(string additionalFeeId)
        {
            try
            {
                TB_AdditionalFee additionalFee = BusinessService.GetAdditionalFee(additionalFeeId);

                return Ok(additionalFee);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("save", Name = RouteNames.AdditionalFeeSave)]
        [HttpPost()]
        public IHttpActionResult Create([FromBody] TB_AdditionalFee additionalFee)
        {
            try
            {
                additionalFee.UserID = CurrentUser.UserID;

                BusinessService.SaveAdditionalFee(additionalFee);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
