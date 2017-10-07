using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Yumiki.Business.MoneyTrace.Interfaces;
using Yumiki.Commons.Settings;
using Yumiki.Entity.MoneyTrace;
using Yumiki.Entity.MoneyTrace.ServiceObjects;
using Yumiki.Web.Base;
using Yumiki.Web.MoneyTrace.Constants;

namespace Yumiki.Web.MoneyTrace.Controllers
{
    [RoutePrefix("api/report")]
    public class ApiReportController : ApiBaseController<IReportService>
    {
        [Route("get", Name = RouteNames.ReportGet)]
        [HttpGet()]
        public IHttpActionResult Get()
        {
            try
            {
                GetReportRequest request = new GetReportRequest()
                {
                    ReportType = EN_ReportPeriodType.E_MONTH
                };


                GetReportResponse resonse = BusinessService.GetTraceReport(request);
                return Ok(resonse);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
