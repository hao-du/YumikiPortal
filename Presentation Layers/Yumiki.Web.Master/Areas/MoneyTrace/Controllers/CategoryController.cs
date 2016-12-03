using System.Web.Mvc;
using Yumiki.Business.MoneyTrace.Interfaces;
using Yumiki.Web.Base;

namespace Yumiki.Web.MoneyTrace.Controllers
{
    public class CategoryController : BaseController<ICategoryService>
    {
        // GET: Category
        public ActionResult Index()
        {
            return View();
        }
    }
}