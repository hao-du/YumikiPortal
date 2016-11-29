using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Yumiki.Business.MoneyTrace.Interfaces;
using Yumiki.Entity.MoneyTrace;
using Yumiki.Web.Base;
using Yumiki.Web.MoneyTrace.Constants;

namespace Yumiki.Web.MoneyTrace.Controllers
{
    [RoutePrefix("api/transactiontype")]
    public class ApiTransactionTypeController : ApiBaseController<ITransactionTypeService>
    {
        [Route("getall", Name = RouteNames.TransactionTypeGetAll)]
        [HttpGet()]
        public IHttpActionResult Get(bool showInactive)
        {
            try
            {
                List<TB_TransactionType> transactionTypes = BusinessService.GetAllTransactionTypes(showInactive);
                return Ok(transactionTypes);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("get", Name = RouteNames.TransactionTypeGetByID)]
        [HttpGet()]
        public IHttpActionResult GetById(string transactionTypeID)
        {
            try
            {
                TB_TransactionType transactionType = BusinessService.GetTransactionType(transactionTypeID);
                return Ok(transactionType);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("save", Name = RouteNames.TransactionTypeGetSave)]
        [HttpPost()]
        public IHttpActionResult Create([FromBody] TB_TransactionType item)
        {
            try
            {
                BusinessService.SaveTransactionType(item);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
