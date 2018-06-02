using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Yumiki.Business.OnTime.Interfaces;
using Yumiki.Commons.Settings;
using Yumiki.Entity.OnTime;
using Yumiki.Web.Base;
using Yumiki.Web.OnTime.Models;

namespace Yumiki.Web.OnTime.Controllers
{
    [RoutePrefix("api/ontime/phase")]
    public class ApiPhaseController : ApiBaseController<IPhaseService>
    {
        [Route("getall", Name = "GetAllPhases")]
        [HttpGet()]
        public IHttpActionResult Get(bool isActive)
        {
            try
            {
                IEnumerable<MD_Phase> phases = BusinessService.GetAllPhases(isActive).Select(c => new MD_Phase(c));

                return Ok(phases);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("get", Name = "GetPhase")]
        [HttpGet()]
        public IHttpActionResult Get(string id)
        {
            try
            {
                MD_Phase phase = new MD_Phase(BusinessService.GetPhase(id));

                return Ok(phase);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("save", Name = "SavePhase")]
        [HttpPost()]
        public IHttpActionResult Save(MD_Phase phase)
        {
            try
            {
                BusinessService.SavePhase(phase.ToObject());

                return Ok(phase);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
