﻿using System.Web.Mvc;
using System.Web.Routing;
using Yumiki.Commons.Dictionaries;

namespace Yumiki.Web.Master
{
    public class RouteConfig
    {
        /// <summary>
        /// Custom Route for ASP.NET Webform URLs
        /// </summary>
        /// <param name="routes">Route Collection which initialized by Global.asax</param>
        public static void RegisterWebFormRoutes(RouteCollection routes)
        {
            routes.MapPageRoute("WebForm", string.Format("{0}/{1}/{2}", HttpConstants.RouteNames.WebFormAreaPrefix, "{application}", "{page}"), "~/Areas/{application}/{page}.aspx");
            routes.MapPageRoute("Master", string.Format("{0}/{1}/{2}", HttpConstants.RouteNames.WebFormMasterPrefix, "{application}", "{page}"), "~/Pages/{application}/{page}.aspx");
        }

        /// <summary>
        /// Custom Route for ASP.NET MVC URLs
        /// </summary>
        /// <param name="routes">Route Collection which initialized by Global.asax</param>
        public static void RegisterMVCRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
