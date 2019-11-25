using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TanCruzDentalInventorySystem.ViewModels
{
		public class ItemSearchFormViewModel
		{
				public ItemViewModel Item { get; set; }
				public ItemPriceViewModel SalesOrderItemPrice { get; set; }
				public ItemPriceViewModel PurchaseOrderItemPrice { get; set; }
				public IEnumerable<ItemGroupViewModel> ItemGroups { get; set; }
				public IEnumerable<CurrencyViewModel> Currencies { get; set; }
				public IEnumerable<UnitOfMeasureViewModel> UnitOfMeasures { get; set; }
				public IEnumerable<BusinessPartnerViewModel> BusinessPartners { get; set; }
				public IEnumerable<PurchasingUnitOfMeasureViewModel> PurchasingUnitOfMeasures { get; set; }
				public IEnumerable<InventoryUnitOfMeasureViewModel> InventoryUnitOfMeasures { get; set; }
		}
}