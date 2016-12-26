using System.Web.Mvc;
using Yumiki.Business.Master.Interfaces;
using Yumiki.Commons.Dictionaries;
using Yumiki.Web.Base;

namespace Yumiki.Web.Master.Controllers
{
    public class HomeController : BaseController<IGUIService>
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToLoginPage(false);
        }

        [ChildActionOnly]
        public PartialViewResult GetMenu()
        {
            string userID = HttpSession.UserID.ToString();
            string menu = string.Empty;

            if (!string.IsNullOrEmpty(userID))
            {
                menu = BusinessService.GetPrivilege(userID);
            }

            return PartialView("GetMenu", menu);
        }

    }
}