using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Yumiki.Business.MoneyTrace.Interfaces;
using Yumiki.Commons.Dictionaries;
using Yumiki.Entity.MoneyTrace;
using Yumiki.Web.Base;
using Yumiki.Web.MoneyTrace.Constants;

namespace Yumiki.Web.MoneyTrace.Controllers
{
    [RoutePrefix("api/category")]
    public class ApiCategoryController : ApiBaseController<ICategoryService>
    {
        [Route("getall", Name = RouteNames.CategoryGetAll)]
        [HttpGet()]
        public IHttpActionResult Get(bool showInactive)
        {
            try
            {
                List<TB_Category> categoryList = BusinessService.GetAllCategory(showInactive);
                return Ok(categoryList);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("get", Name = RouteNames.CategoryGetByID)]
        [HttpGet()]
        public IHttpActionResult GetById(string categoryID)
        {
            try
            {
                TB_Category category = BusinessService.GetCategory(categoryID);
                return Ok(category);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("save", Name = RouteNames.CategoryGetSave)]
        [HttpPost()]
        public IHttpActionResult Create([FromBody] TB_Category item)
        {
            try
            {
                if(item != null && Session[HttpConstants.Session.UserID] != null)
                {
                    item.UserID = Guid.Parse(Session[HttpConstants.Session.UserID].ToString());
                }
                BusinessService.SaveCategory(item);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
