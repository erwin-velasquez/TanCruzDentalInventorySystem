using System;

namespace TanCruzDentalInventorySystem.Models
{
	public class Tax
	{
		public string TaxId { get; set; }
		public string TaxName { get; set; }
		public string TaxDescription { get; set; }
		public decimal TaxValue { get; set; }
		public bool IsDefault { get; set; }
		public string TaxStatus { get; set; }
		public string UserId { get; set; }
	}
}
