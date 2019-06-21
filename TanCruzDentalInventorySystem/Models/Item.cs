namespace TanCruzDentalInventorySystem.Models
{
	public class Item
	{
		public string ItemId { get; set; }
		public string ItemName { get; set; }
		public string ItemDescription { get; set; }
		public ItemGroup ItemGroup { get; set; }
		public ItemPrice ItemPrice { get; set; }
		public Currency Currency { get; set; }
		public UnitOfMeasure UnitOfMeasure { get; set; }
		public BusinessPartner BusinessPartner { get; set; }
		public PurchasingUnitOfMeasure PurchasingUnitOfMeasure { get; set; }
		public long ItemsPerUnitOfMeasure { get; set; }
		public string PurchasingRemarks { get; set; }
		public InventoryUnitOfMeasure InventoryUnitOfMeasure { get; set; }
		public long MinimumInventoryRequired { get; set; }
		public bool IsActive { get; set; }
		public string UserId { get; set; }
	}

	public class ItemGroup
	{
		public string ItemGroupId { get; set; }
		public string ItemGroupName { get; set; }
		public string ItemGroupDescription { get; set; }
		public string UserId { get; set; }
	}

	public class ItemPrice
	{
		public string ItemPriceId { get; set; }
		public string ItemPriceName { get; set; }
		public string ItemPriceDescription { get; set; }
		public string Type { get; set; }
		public decimal PriceAmount { get; set; }
		public string BaseCurrency { get; set; }
		public string UserId { get; set; }
	}
}