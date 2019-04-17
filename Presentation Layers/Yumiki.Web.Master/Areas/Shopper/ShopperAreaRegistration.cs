using System.Web.Mvc;
using Yumiki.Commons.Dictionaries;

namespace Yumiki.Web.Shopper
{
    public class ShopperAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Shopper";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Shopper_default",
                string.Format("{0}/{1}/{2}", HttpConstants.RouteNames.WebFormAreaPrefix, AreaName, "{controller}/{action}/{id}"),
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}