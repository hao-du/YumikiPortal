using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Yumiki.Business.MoneyTrace.Interfaces;
using Yumiki.Commons.Settings;
using Yumiki.Entity.MoneyTrace;
using Yumiki.Web.Base;
using Yumiki.Web.MoneyTrace.Constants;

namespace Yumiki.Web.MoneyTrace.Controllers
{
    [RoutePrefix("api/traceuser")]
    public class ApiUserController : ApiBaseController<IUserService>
    {
        [Route("getall", Name = RouteNames.UserGetAll)]
        [HttpGet()]
        public IHttpActionResult GetAll(bool showInactive)
        {
            try
            {
                List<TB_User> users = BusinessService.GetAllUsers(showInactive, CurrentUser.UserID);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
