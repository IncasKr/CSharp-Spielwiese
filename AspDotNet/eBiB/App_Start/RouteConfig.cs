using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace eBiB
{
    /// <summary>
    /// Uri route definitions class for application management.
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        /// Stores routes uri.
        /// </summary>
        /// <param name="routes">List of routes to record.</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Afficher", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
