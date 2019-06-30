using System.ComponentModel.DataAnnotations;

namespace TanCruzDentalInventorySystem.ViewModels
{
	public class UnitOfMeasureViewModel
	{
		public string UnitOfMeasureId { get; set; }

		[Display(Name = "Unit Of Measure")]
		public string UnitOfMeasureDescription { get; set; }
	}

	public class PurchasingUnitOfMeasureViewModel
	{
		public string PurchasingUnitOfMeasureId { get; set; }

		[Display(Name = "Purchasing Unit of Measure")]
		public string PurchasingUnitOfMeasureDescription { get; set; }
	}

	public class InventoryUnitOfMeasureViewModel
	{
		public string InventoryUnitOfMeasureId { get; set; }

		[Display(Name = "Inventory Unit of Measure")]
		public string InventoryUnitOfMeasureDescription { get; set; }
	}
}