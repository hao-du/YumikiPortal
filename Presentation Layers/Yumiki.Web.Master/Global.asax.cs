using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;
using Yumiki.Commons.Settings;

namespace Yumiki.Web.Master
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            //Custom Configuration. 
            YumikiConfigLoader.GetInstance();

            RouteConfig.RegisterWebFormRoutes(RouteTable.Routes);

            //Code that runs on application startup.
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterMVCRoutes(RouteTable.Routes);

            //Loading Json for WebAPI.
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);
        }

        protected void Application_PostAuthorizeRequest()
        {
            //Enable session in WebAPI.
            System.Web.HttpContext.Current.SetSessionStateBehavior(System.Web.SessionState.SessionStateBehavior.Required);
        }
    }
}