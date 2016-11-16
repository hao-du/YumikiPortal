using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Yumiki.Business.MoneyTrace.Interfaces;
using Yumiki.Entity.MoneyTrace;
using Yumiki.Web.Base;

namespace Yumiki.Web.MoneyTrace.Controllers
{
    public class ApiCurrencyController : ApiBaseController<ICurrencyService>
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<TB_Currency> Get()
        {
            List<TB_Currency> currencyList = BusinessService.GetAllCurrency(true);

            return null;//categoryList;
        }

        // GET api/values/1
        //[HttpGet("{id}")]
        public TB_Category GetById(int id)
        {
            //TB_Category category = _context.Tbl_Category.Where(c => c.Id == id).FirstOrDefault();

            return null;
        }

        [HttpPost]
        public IEnumerable<TB_Category> Create([FromBody] TB_Category item)
        {
            //item.IsActive = true;
            //item.UserID = 1;
            //_context.Tbl_Category.Add(item);
            //_context.SaveChanges();

            //List<TB_Category> categoryList = _context.Tbl_Category.Where(c => c.IsActive).OrderBy(c => c.CategoryName).ToList();
            return null;
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
