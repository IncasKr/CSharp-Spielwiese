using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HelloWorld
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Add",
                url: "Add/{value1}/{value2}",
                defaults: new { controller = "Calculator", action = "Add", value1 = 0, value2 = 0 }
            );

            routes.MapRoute(
                name: "Diff",
                url: "Diff/{value1}/{value2}",
                defaults: new { controller = "Calculator", action = "Diff", value1 = 0, value2 = 0 }
            );

            routes.MapRoute(
                name: "Div",
                url: "Div/{value1}/{value2}",
                defaults: new { controller = "Calculator", action = "Div", value1 = 0, value2 = 0 }
            );

            routes.MapRoute(
                name: "Mult",
                url: "Mult/{value1}/{value2}",
                defaults: new { controller = "Calculator", action = "Mult", value1 = 0, value2 = 0 }
            );

            routes.MapRoute(
                name: "Weather",
                url: "{day}/{month}/{year}",
                defaults: new { controller = "Weather", action = "Display" },
                constraints: new { day = @"\d{1,2}", month = @"\d{1,2}" , year = @"\d+" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Start", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "GetAll",
                url: "{controller}/{action}/{*id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
