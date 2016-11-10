using System.Web.Mvc;

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
                "MoneyTrace/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}