using Microsoft.Owin;
using Owin;
using System.Web.Http;

[assembly: OwinStartup(typeof(OwinLib.Startup))]
namespace OwinLib
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Configure Web API for self-host. 
            // Note: I prefer my routes to be "api/{controller}/{action}" instead of "api/{controller}/{id}"
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}/{val}/",
                defaults: new { id = RouteParameter.Optional, val = RouteParameter.Optional }
            );
            app.UseWebApi(config);
        }
    }
}