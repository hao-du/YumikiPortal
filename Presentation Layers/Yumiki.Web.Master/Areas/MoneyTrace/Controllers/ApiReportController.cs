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
using Yumiki.Commons.Helpers;
using Yumiki.Commons.Entities;

namespace Yumiki.Web.MoneyTrace.Controllers
{
    [RoutePrefix("api/report")]
    public class ApiReportController : ApiBaseController<IReportService>
    {
        [Route("getReportTypes", Name = RouteNames.ReportGetReportTypes)]
        [HttpGet()]
        public IHttpActionResult GetReportTypes()
        {
            try
            {
                List<ExtendEnum> reportType = EnumHelper.GetDatasource<EN_ReportType>();
                return Ok(reportType);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("generateReport", Name = RouteNames.ReportGenerateReport)]
        [HttpPost()]
        public IHttpActionResult Get([FromBody] GetReportRequest request)
        {
            try
            {
                request = CurrentUser.UserID;

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
