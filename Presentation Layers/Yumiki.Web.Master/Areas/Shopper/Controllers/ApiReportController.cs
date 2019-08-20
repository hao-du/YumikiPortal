using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Yumiki.Business.Shopper.Interfaces;
using Yumiki.Commons.Settings;
using Yumiki.Entity.Shopper;
using Yumiki.Entity.Shopper.ServiceObjects;
using Yumiki.Web.Base;
using Yumiki.Web.Shopper.Constants;
using Yumiki.Commons.Helpers;
using Yumiki.Commons.Entities;
using Yumiki.Commons.Report;

namespace Yumiki.Web.Shopper.Controllers
{
    [RoutePrefix("api/shopperreport")]
    public class ApiReportController : ApiBaseController<IReportService>
    {
        [Route("getRevenueReportTypes", Name = RouteNames.ReportGetReportRevenueTypes)]
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


        [Route("generateRevenueReport", Name = RouteNames.ReportGenerateRevenueReport)]
        [HttpPost()]
        public IHttpActionResult Get([FromBody] GetReportRequest request)
        {
            try
            {
                GetReportResponse resonse = BusinessService.GetRevenueReport(request);
                return Ok(resonse);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
