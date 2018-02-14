using System.Web.Mvc;
using Yumiki.Commons.Dictionaries;

namespace Yumiki.Web.MoneyTrace
{
    public class MoneyTraceAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "MoneyTrace";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "MoneyTrace_default",
                string.Format("{0}/{1}/{2}", HttpConstants.RouteNames.WebFormAreaPrefix, AreaName, "{controller}/{action}/{id}"),
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}