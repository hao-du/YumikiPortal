using System.IO;
using System.Web.Mvc;
using Yumiki.Business.OnTime.Interfaces;
using Yumiki.Web.Base;

namespace Yumiki.Web.OnTime.Controllers
{
    public class HtmlController : Controller
    {
        // GET: App
        public ActionResult Get(string page)
        {
            return new FilePathResult($"../../ontime-clients/views/{page}.html", "text/html");
        }
    }
}
