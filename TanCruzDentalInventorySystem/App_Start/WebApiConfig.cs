﻿using System.Web.Http;

namespace TanCruzDentalInventorySystem
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = RouteParameter.Optional }
			//defaults: new { id = RouteParameter.Optional }
			);
        }
    }
}
