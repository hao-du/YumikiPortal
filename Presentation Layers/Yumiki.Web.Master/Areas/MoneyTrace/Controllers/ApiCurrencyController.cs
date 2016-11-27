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
            List<TB_Currency> currencyList = BusinessService.GetAllCurrency(showInactive);

            return Ok(currencyList);
        }

        [Route("get", Name = RouteNames.CurrencyGetByID)]
        [HttpGet()]
        public IHttpActionResult GetById(string currencyID)
        {
            TB_Currency currency = BusinessService.GetCurrency(currencyID);

            return Ok(currency);
        }

        [Route("save", Name = RouteNames.CurrencyGetSave)]
        [HttpPost]
        public IHttpActionResult Create([FromBody] TB_Currency item)
        {


            //item.IsActive = true;
            //item.UserID = 1;
            //_context.Tbl_Category.Add(item);
            //_context.SaveChanges();

            //List<TB_Category> categoryList = _context.Tbl_Category.Where(c => c.IsActive).OrderBy(c => c.CategoryName).ToList();
            return Ok();
        }

        //[HttpPut("{id}")]
        public IEnumerable<TB_Category> Update(int id, [FromBody] TB_Category item)
        {
            //TB_Category dbItem = _context.Tbl_Category.Where(c => c.Id == id).FirstOrDefault();
            //dbItem.CategoryName = item.CategoryName;
            //dbItem.Description = item.Description;
            //_context.SaveChanges();

            //List<TB_Category> categoryList = _context.Tbl_Category.Where(c => c.IsActive).OrderBy(c => c.CategoryName).ToList();
            return null;
        }
    }
}
