using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TanCruzDentalInventorySystem.ViewModels
{
	public class ItemPriceViewModel
	{
        [Display(Name = "Item Price Id")]
        public string ItemPriceId { get; set; }
        [Display(Name = "Item Price Name")]
        public string ItemPriceName { get; set; }
        [Display(Name = "Item Price Description")]
        public string ItemPriceDescription { get; set; }
        [Display(Name = "Item Id")]
        public string ItemId { get; set; }
        [Display(Name = "Default")]
        public bool IsDefault { get; set; }
        [Display(Name = "Item Price Type")]
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
		public List<string> ItemPriceTypes { get; set; }
	}
}