namespace TanCruzDentalInventorySystem.ViewModel
{
	public class ItemViewModel
	{
		public string ItemId { get; set; }
		public string ItemName { get; set; }
		public string ItemDescription { get; set; }
		public ItemGroupViewModel ItemGroup { get; set; }
		public ItemPriceViewModel ItemPrice { get; set; }
		public CurrencyViewModel Currency { get; set; }
		public UnitOfMeasureViewModel UnitOfMeasure { get; set; }
		public BusinessPartnerViewModel BusinessPartner { get; set; }
		public PurchasingUnitOfMeasureViewModel PurchasingUnitOfMeasure { get; set; }
		public long ItemsPerUnitOfMeasure { get; set; }
		public string PurchasingRemarks { get; set; }
		public InventoryUnitOfMeasureViewModel InventoryUnitOfMeasure { get; set; }
		public long MinimumInventoryRequired { get; set; }
		public bool IsActive { get; set; }
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
		public decimal PriceAmount { get; set; }
		public string BaseCurrency { get; set; }
	}
}