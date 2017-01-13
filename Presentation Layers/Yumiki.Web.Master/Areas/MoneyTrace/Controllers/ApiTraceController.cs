using System;
using System.Collections.Generic;
using System.Web.Http;
using Yumiki.Business.MoneyTrace.Interfaces;
using Yumiki.Commons.Settings;
using Yumiki.Entity.MoneyTrace;
using Yumiki.Web.Base;
using Yumiki.Web.MoneyTrace.Constants;

namespace Yumiki.Web.MoneyTrace.Controllers
{
    [RoutePrefix("api/trace")]
    public class ApiTraceController : ApiBaseController<ITraceService>
    {
        [Route("getall", Name = RouteNames.TraceGetAll)]
        [HttpGet()]
        public IHttpActionResult Get(bool showInactive, string month, bool isDisplayedAll)
        {
            try
            {
                string[] monthArray = month.Split('-');

                List<TB_Trace> traces = BusinessService.GetAllTraces(showInactive, 
                                                                    HttpSession.UserID,
                                                                    new DateTime(int.Parse(monthArray[1].Trim()), int.Parse(monthArray[0].Trim()), 1),
                                                                    isDisplayedAll);
                return Ok(traces);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("gettotalsummary", Name = RouteNames.TraceGetTotalSummary)]
        [HttpGet()]
        public IHttpActionResult GetTotalSummary()
        {
            try
            {
                List<TraceSummary> traces = BusinessService.GetTotalAmount(HttpSession.UserID);
                return Ok(traces);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("getbankingsummary", Name = RouteNames.TraceGetBankingSummary)]
        [HttpGet()]
        public IHttpActionResult GetBankingSummary()
        {
            try
            {
                List<TraceSummary> traces = BusinessService.GetBankingSummary(HttpSession.UserID);
                return Ok(traces);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("get", Name = RouteNames.TraceGetByID)]
        [HttpGet()]
        public IHttpActionResult GetById(string traceID)
        {
            try
            {
                TB_Trace trace = BusinessService.GetTrace(traceID);
                return Ok(trace);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("save", Name = RouteNames.TraceSave)]
        [HttpPost()]
        public IHttpActionResult Save([FromBody] TB_Trace item)
        {
            try
            {
                item.UserID = HttpSession.UserID;

                BusinessService.SaveTrace(item);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
