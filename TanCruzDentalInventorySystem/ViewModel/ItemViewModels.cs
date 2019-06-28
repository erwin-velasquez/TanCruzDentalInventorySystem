using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TanCruzDentalInventorySystem.ViewModel
{
	public class ItemViewModel
	{
		[Display(Name = "Item Number")]
		public string ItemId { get; set; }

		[Display(Name = "Item Name")]
		public string ItemName { get; set; }
		public string ItemDescription { get; set; }

		[Display(Name = "Item Type")]
		public ItemGroupViewModel ItemGroup { get; set; }
		public ItemPriceViewModel ItemPrice { get; set; }

		[Display(Name = "Currency")]
		public CurrencyViewModel Currency { get; set; }
		public UnitOfMeasureViewModel UnitOfMeasure { get; set; }
		public BusinessPartnerViewModel BusinessPartner { get; set; }
		public PurchasingUnitOfMeasureViewModel PurchasingUnitOfMeasure { get; set; }

		[Display(Name = "Items per Unit of Measure")]
		public long ItemsPerUnitOfMeasure { get; set; }

		[Display(Name = "Purchasing Remarks")]
		public string PurchasingRemarks { get; set; }
		public InventoryUnitOfMeasureViewModel InventoryUnitOfMeasure { get; set; }

		[Display(Name = "Minimum Inventory Required")]
		public long MinimumInventoryRequired { get; set; }
		public bool IsActive { get; set; }
		public string UserId { get; set; }
	}

	public class ItemGroupViewModel
	{
		public string ItemGroupId { get; set; }
		public string ItemGroupName { get; set; }
		public string ItemGroupDescription { get; set; }
	}

	public class ItemPriceViewModel
	{
		public string ItemPriceId { get; set; }
		public string ItemPriceName { get; set; }
		public string ItemPriceDescription { get; set; }
		public string Type { get; set; }

		[Display(Name = "Item Price")]
		public decimal PriceAmount { get; set; }
		public string BaseCurrency { get; set; }
	}

	public class ItemFormViewModel
	{
		public ItemViewModel Item { get; set; }
		public IEnumerable<ItemGroupViewModel> ItemGroups { get; set; }
		public IEnumerable<CurrencyViewModel> Currencies { get; set; }
		public IEnumerable<UnitOfMeasureViewModel> UnitOfMeasures { get; set; }
		public IEnumerable<BusinessPartnerViewModel> BusinessPartners { get; set; }
		public IEnumerable<PurchasingUnitOfMeasureViewModel> PurchasingUnitOfMeasures { get; set; }
		public IEnumerable<InventoryUnitOfMeasureViewModel> InventoryUnitOfMeasures { get; set; }
	}
}