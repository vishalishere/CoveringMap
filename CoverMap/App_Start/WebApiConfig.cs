using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CoverMap
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "GetDatapointsWithinBounds",
                routeTemplate: "GetDatapointsWithinBounds",
                defaults: new { controller = "Covers", action = "GetDatapointsWithinBounds" }
            );

            config.Routes.MapHttpRoute(
                name: "createCoverRoute",
                routeTemplate: "api/createcover",
                defaults: new { controller = "Covers", action = "PostCover" }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: new { id = "[0-9]+"}
            );

            config.Routes.MapHttpRoute("DefaultApiWithAction", "Api/{controller}/{action}");
           
            var formatters = GlobalConfiguration.Configuration.Formatters;
            formatters.Remove(formatters.XmlFormatter);
        }
    }
}
