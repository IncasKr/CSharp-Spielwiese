using Owin;
using System.Collections.Generic;
using System.Web.Http;

namespace OwinSelfhostSample
{
    public class Startup
    {
        public static Dictionary<int, string> ValuesList { get; set; }

        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            // Init list of data.
            ValuesList = new Dictionary<int, string>();
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            appBuilder.UseWebApi(config);
        }

    }
}
