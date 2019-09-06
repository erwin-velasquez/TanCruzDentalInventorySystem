using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using Unity.AspNet.Mvc;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(TanCruzDentalInventorySystem.UnityMvcActivator), nameof(TanCruzDentalInventorySystem.UnityMvcActivator.Start))]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(TanCruzDentalInventorySystem.UnityMvcActivator), nameof(TanCruzDentalInventorySystem.UnityMvcActivator.Shutdown))]

namespace TanCruzDentalInventorySystem
{
	/// <summary>
	/// Provides the bootstrapping for integrating Unity with ASP.NET MVC.
	/// </summary>
	public static class UnityMvcActivator
	{
		/// <summary>
		/// Integrates Unity when the application starts.
		/// </summary>
		public static void Start()
		{
			FilterProviders.Providers.Remove(FilterProviders.Providers.OfType<FilterAttributeFilterProvider>().First());
			FilterProviders.Providers.Add(new UnityFilterAttributeFilterProvider(UnityConfig.Container));

			GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(UnityConfig.Container);
			DependencyResolver.SetResolver(new UnityDependencyResolver(UnityConfig.Container));

			// TODO: Uncomment if you want to use PerRequestLifetimeManager
			// Microsoft.Web.Infrastructure.DynamicModuleHelper.DynamicModuleUtility.RegisterModule(typeof(UnityPerRequestHttpModule));
		}

		/// <summary>
		/// Disposes the Unity container when the application is shut down.
		/// </summary>
		public static void Shutdown()
		{
			UnityConfig.Container.Dispose();
		}
	}
}