using System.ComponentModel.DataAnnotations;

namespace TanCruzDentalInventorySystem.ViewModels
{
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

}