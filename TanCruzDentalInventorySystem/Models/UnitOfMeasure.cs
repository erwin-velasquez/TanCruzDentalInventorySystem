namespace TanCruzDentalInventorySystem.Models
{
	public class UnitOfMeasure
	{
		public string UnitOfMeasureId { get; set; }
		public string UnitOfMeasureDescription { get; set; }
		public string UserId { get; set; }
	}

	public class PurchasingUnitOfMeasure
	{
		public string PurchasingUnitOfMeasureId { get; set; }
		public string PurchasingUnitOfMeasureDescription { get; set; }
		public string UserId { get; set; }
	}

	public class InventoryUnitOfMeasure
	{
		public string InventoryUnitOfMeasureId { get; set; }
		public string InventoryUnitOfMeasureDescription { get; set; }
		public string UserId { get; set; }
	}
}