using System.Web.Mvc;
using Yumiki.Commons.Dictionaries;

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
            context.MapRoute(
                "WellCovered_default",
                string.Format("{0}/{1}/{2}", HttpConstants.Pages.WebFormAreaPrefix, AreaName, "{controller}/{action}/{id}"),
                new { action = "List", id = UrlParameter.Optional }
            );
        }
    }
}