using System;

namespace TanCruzDentalInventorySystem.Models
{
	public class Item
	{
		public string ItemId { get; set; }
		public string ItemBarCode { get; set; }
		public string ItemName { get; set; }
		public string ItemDescription { get; set; }
		public ItemGroup ItemGroup { get; set; }
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
		public DateTime? ChangedDate { get; set; }
		public long VersionTimeStamp { get; set; }
		public decimal QuantityOnHand { get; set; }
	}
}