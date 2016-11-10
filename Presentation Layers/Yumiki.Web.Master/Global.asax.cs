using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;
using System.Configuration;
using Yumiki.Web.Master.App_Config;
using System.Reflection;

namespace Yumiki.Web.Master
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            RouteConfig.RegisterWebFormRoutes(RouteTable.Routes);

            //Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterMVCRoutes(RouteTable.Routes);
        }
    }
}