using System;
using System.Collections.Generic;
using System.Web.Http;
using Yumiki.Business.MoneyTrace.Interfaces;
using Yumiki.Commons.Settings;
using Yumiki.Entity.MoneyTrace;
using Yumiki.Entity.MoneyTrace.ServiceObjects;
using Yumiki.Web.Base;
using Yumiki.Web.MoneyTrace.Constants;

namespace Yumiki.Web.MoneyTrace.Controllers
{
    [RoutePrefix("api/trace")]
    public class ApiTraceController : ApiBaseController<ITraceService>
    {
        [Route("getall", Name = RouteNames.TraceGetAll)]
        [HttpGet()]
        public IHttpActionResult Get(bool showInactive, string month, bool isDisplayedAll, int currentPage, int itemsPerPage)
        {
            try
            {
                string[] monthArray = month.Split('-');

                GetTraceRequest<TB_Trace> traceRequest = new GetTraceRequest<TB_Trace>
                {
                    IsDisplayedAll = isDisplayedAll,
                    Month = new DateTime(int.Parse(monthArray[1].Trim()), int.Parse(monthArray[0].Trim()), 1),
                    UserID = HttpSession.UserID,
                    ShowInactive = showInactive,
                    CurrentPage = currentPage,
                    ItemsPerPage = itemsPerPage
                };

                GetTraceResponse<TB_Trace> traceResponse = BusinessService.GetAllTraces(traceRequest);
                return Ok(traceResponse);
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

        [Route("gettags", Name = RouteNames.TraceGetTags)]
        [HttpGet()]
        public IHttpActionResult GetTags(string keyword)
        {
            try
            {
                List<string> tags = BusinessService.GetTags(keyword);
                return Ok(tags);
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
