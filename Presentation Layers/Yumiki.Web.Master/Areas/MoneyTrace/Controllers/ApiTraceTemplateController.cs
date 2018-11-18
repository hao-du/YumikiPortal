using System;
using System.Collections.Generic;
using System.Web.Http;
using Yumiki.Business.MoneyTrace.Interfaces;
using Yumiki.Commons.Exceptions;
using Yumiki.Commons.Settings;
using Yumiki.Entity.MoneyTrace;
using Yumiki.Entity.MoneyTrace.ServiceObjects;
using Yumiki.Web.Base;
using Yumiki.Web.MoneyTrace.Constants;

namespace Yumiki.Web.MoneyTrace.Controllers
{
    [RoutePrefix("api/tracetemplate")]
    public class ApiTraceTemplateController : ApiBaseController<ITraceTemplateService>
    {
        [Route("getall", Name = RouteNames.TraceTemplateGetAll)]
        [HttpGet()]
        public IHttpActionResult Get(bool showInactive)
        {
            try
            {
                List<TB_TraceTemplate> templates = BusinessService.GetAllTemplates(showInactive, CurrentUser.UserID);
                return Ok(templates);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("get", Name = RouteNames.TraceTemplateGetByID)]
        [HttpGet()]
        public IHttpActionResult GetById(string traceTemplateID)
        {
            try
            {
                TB_TraceTemplate traceTemplate = BusinessService.GetTraceTemplate(traceTemplateID);
                return Ok(traceTemplate);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("gettags", Name = RouteNames.TraceTemplateGetTags)]
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

        [Route("save", Name = RouteNames.TraceTemplateSave)]
        [HttpPost()]
        public IHttpActionResult Save([FromBody] TB_TraceTemplate item)
        {
            try
            {
                item.UserID = CurrentUser.UserID;

                item.ID = BusinessService.SaveTraceTemplate(item);

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
