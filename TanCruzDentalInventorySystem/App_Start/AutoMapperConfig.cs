using AutoMapper;
using TanCruzDentalInventorySystem.Models;
using TanCruzDentalInventorySystem.ViewModel;

namespace TanCruzDentalInventorySystem
{
	public static class AutoMapperConfig
	{
		public static void RegisterComponents()
		{
			Mapper.Initialize(cfg =>
			{
				cfg.CreateMap<Item, ItemViewModel>().ReverseMap();
				cfg.CreateMap<ItemGroup, ItemGroupViewModel>().ReverseMap();
				cfg.CreateMap<ItemPrice, ItemPriceViewModel>().ReverseMap();
				cfg.CreateMap<Currency, CurrencyViewModel>().ReverseMap();
				cfg.CreateMap<UnitOfMeasure, UnitOfMeasureViewModel>().ReverseMap();
				cfg.CreateMap<BusinessPartner, BusinessPartnerViewModel>().ReverseMap();
				cfg.CreateMap<PurchasingUnitOfMeasure, PurchasingUnitOfMeasureViewModel>().ReverseMap();
				cfg.CreateMap<InventoryUnitOfMeasure, InventoryUnitOfMeasureViewModel>().ReverseMap();

				cfg.CreateMap<PurchaseOrder, PurchaseOrderViewModel>().ReverseMap();
			});
		}
	}
}