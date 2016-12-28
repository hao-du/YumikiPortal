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
    [RoutePrefix("api/currency")]
    public class ApiCurrencyController : ApiBaseController<ICurrencyService>
    {
        [Route("getall", Name = RouteNames.CurrencyGetAll)]
        [HttpGet()]
        public IHttpActionResult Get(bool showInactive)
        {
            try
            {
                List<TB_Currency> currencyList = BusinessService.GetAllCurrency(showInactive, HttpSession.UserID);
                return Ok(currencyList);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("getwithshareableitems", Name = RouteNames.CurrencyGetAllWithShareableItems)]
        [HttpGet()]
        public IHttpActionResult GetWithShareableItems(bool showInactive)
        {
            try
            {
                List<TB_Currency> currencyList = BusinessService.GetAllCurrency(showInactive, HttpSession.UserID, true);
                return Ok(currencyList);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("get", Name = RouteNames.CurrencyGetByID)]
        [HttpGet()]
        public IHttpActionResult GetById(string currencyID)
        {
            try
            {
                TB_Currency currency = BusinessService.GetCurrency(currencyID);
                return Ok(currency);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("save", Name = RouteNames.CurrencySave)]
        [HttpPost()]
        public IHttpActionResult Create([FromBody] TB_Currency item)
        {
            try
            {
                item.UserID = HttpSession.UserID;

                BusinessService.SaveCurrency(item);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
