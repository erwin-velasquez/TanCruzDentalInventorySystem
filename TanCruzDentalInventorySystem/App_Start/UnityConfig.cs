using System.Web.Mvc;
using TanCruzDentalInventorySystem.BusinessService;
using TanCruzDentalInventorySystem.BusinessService.BusinessServiceInterface;
using TanCruzDentalInventorySystem.Repository;
using TanCruzDentalInventorySystem.Repository.DataServiceInterface;
using Unity;
using Unity.Mvc5;

namespace TanCruzDentalInventorySystem
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IItemService, ItemService>();

            container.RegisterType<IItemRepository, ItemRepository>();

            container.RegisterType<IUnitOfWork, UnitOfWork>();

            container.RegisterType<IPurchaseService, PurchaseService>();

            container.RegisterType<IPurchaseRepository, PurchaseRepository>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}