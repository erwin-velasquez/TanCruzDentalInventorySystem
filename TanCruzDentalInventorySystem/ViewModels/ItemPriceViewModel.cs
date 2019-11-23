using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TanCruzDentalInventorySystem.ViewModels
{
	public class ItemPriceViewModel
	{
		public string ItemPriceId { get; set; }
		public string ItemPriceName { get; set; }
		public string ItemPriceDescription { get; set; }
		public string ItemId { get; set; }
		public bool IsDefault { get; set; }

		public string Type { get; set; }

		[Display(Name = "Item Price")]
		public decimal PriceAmount { get; set; }
		public CurrencyViewModel BaseCurrency { get; set; }
		public string UserId { get; set; }
		public DateTime? ChangedDate { get; set; }
		public long VersionTimeStamp { get; set; }
	}

	public class ItemPriceFormViewModel
	{
		public ItemPriceViewModel ItemPrice { get; set; }
		public IEnumerable<CurrencyViewModel> Currencies { get; set; }
	}
}