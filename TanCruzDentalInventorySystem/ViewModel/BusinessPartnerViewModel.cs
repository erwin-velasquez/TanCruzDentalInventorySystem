using System.ComponentModel.DataAnnotations;

namespace TanCruzDentalInventorySystem.ViewModel
{
	public class BusinessPartnerViewModel
	{
		public string BusinessPartnerId { get; set; }

		[Display(Name = "Preferred Vendor")]
		public string BusinessPartnerName { get; set; }
		public string BusinessPartnerType { get; set; }
		public string BusinessPartnerAlias { get; set; }
		public string BusinessPartnerFirstName { get; set; }
		public string BusinessPartnerMiddleName { get; set; }
		public string BusinessPartnerLastName { get; set; }
	}
}