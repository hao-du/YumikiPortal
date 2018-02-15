using System.Web.Mvc;
using System.Web.Routing;
using Yumiki.Commons.Dictionaries;

namespace Yumiki.Web.OnTime
{
    public class OnTimedAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "OnTime";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "OnTime_default",
                string.Format("{0}/{1}/{2}", HttpConstants.RouteNames.WebFormAreaPrefix, AreaName, "{controller}/{action}/{id}"),
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}