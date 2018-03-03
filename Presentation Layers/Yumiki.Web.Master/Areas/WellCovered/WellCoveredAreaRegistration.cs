using System.Web.Mvc;
using System.Web.Routing;
using Yumiki.Commons.Dictionaries;
using Yumiki.Web.Base;

namespace Yumiki.Web.WellCovered
{
    public class WellCoveredAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "WellCovered";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            //This rount is no longer used as it impact to the main routes in Global.asax
            //context.Routes.Add(new Route($"{HttpConstants.RouteNames.WebHandlerAreaPrefix}/{AreaName}/{HttpConstants.Pages.Downloader}", new HttpHandlerRoute($"~/Areas/{AreaName}/WellCoveredDownloader.ashx")));

            context.MapRoute(
                "WellCovered_default",
                string.Format("{0}/{1}/{2}", HttpConstants.RouteNames.WebFormAreaPrefix, AreaName, "{controller}/{action}/{id}"),
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}