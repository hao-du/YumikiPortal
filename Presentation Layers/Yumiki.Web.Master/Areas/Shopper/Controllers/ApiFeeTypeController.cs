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
    [RoutePrefix("api/shopperfeetype")]
    public class ApiFeeTypeController : ApiBaseController<IFeeTypeService>
    {
        [Route("getall", Name = RouteNames.FeeTypeGetAll)]
        [HttpGet()]
        public IHttpActionResult Get(bool showInactive, bool forReceipt = false, bool forInvoice = false, bool forAdditionFee = false)
        {
            try
            {
                List<TB_FeeType> feeTypes = BusinessService.GetFeeTypes(showInactive, forReceipt, forInvoice, forAdditionFee);

                return Ok(feeTypes);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("getbyterm", Name = RouteNames.FeeTypeGetByTerm)]
        [HttpGet()]
        public IHttpActionResult Get(string term, bool forReceipt, bool forInvoice, bool forAdditionFee)
        {
            try
            {
                List<TB_FeeType> feeTypes = BusinessService.GetFeeTypes(term, forReceipt, forInvoice, forAdditionFee);

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
        public IHttpActionResult Create([FromBody] TB_FeeType feeType)
        {
            try
            {
                feeType.UserID = CurrentUser.UserID;

                BusinessService.SaveFeeType(feeType);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
