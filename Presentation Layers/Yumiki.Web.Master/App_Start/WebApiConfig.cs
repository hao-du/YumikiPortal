using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Yumiki.Web.Master
{
    public static class WebApiConfig
    {
        /// <summary>
        /// Custom Route for ASP.NET Web API URLs
        /// </summary>
        /// <param name="config">HTTP config which initialized by Global.asax</param>
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
