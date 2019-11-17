namespace TanCruzDentalInventorySystem.Models
{
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