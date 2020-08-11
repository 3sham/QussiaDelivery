using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace QussiaDelivery
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.Add(config.Formatters.JsonFormatter);
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
              name: "Filter",
              routeTemplate: "api/{controller}/{action}/{id}/{genderID}/{typeID}",
              defaults: new { id = RouteParameter.Optional , genderID = RouteParameter.Optional , typeID = RouteParameter.Optional }
          );
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
