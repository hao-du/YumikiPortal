﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Yumiki.Web.Master
{
    public class RouteConfig
    {
        public static void RegisterWebFormRoutes(RouteCollection routes)
        {
            routes.MapPageRoute("WebForm", "{application}/{page}", "~/Areas/{application}/{page}.aspx");
        }

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
