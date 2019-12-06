using System;

namespace TanCruzDentalInventorySystem.Models
{
	public class ItemPrice
	{
		public string ItemPriceId { get; set; }
		public string ItemPriceName { get; set; }
		public string ItemPriceDescription { get; set; }
		public Item Item { get; set; }
		public bool IsDefault { get; set; }
		public string Type { get; set; }
		public decimal PriceAmount { get; set; }
		public Currency BaseCurrency { get; set; }
		public string UserId { get; set; }
		public DateTime? ChangedDate { get; set; }
		public long VersionTimeStamp { get; set; }
	}
}