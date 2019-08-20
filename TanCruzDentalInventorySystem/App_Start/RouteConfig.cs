﻿using System.Web.Mvc;
using System.Web.Routing;

namespace TanCruzDentalInventorySystem
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			//routes.MapMvcAttributeRoutes();

			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
				//defaults: new { controller = "SalesOrder", action = "SalesHome", id = UrlParameter.Optional }
			);
		}
	}
}
