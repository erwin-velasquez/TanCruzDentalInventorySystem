using System;

namespace TanCruzDentalInventorySystem.Models
{
	public class BusinessPartnerDetail
	{
		public string BusinessPartnerDetailId { get; set; }
		public string BusinessPartnerId { get; set; }
		public string TelephoneNumber1 { get; set; }
		public string TelephoneNumber2 { get; set; }
		public string MobileNumber { get; set; }
		public string Fax { get; set; }
		public string Email { get; set; }
		public string WebSite { get; set; }
		public string ShippingType { get; set; }
		public string Address1 { get; set; }
		public string Address2 { get; set; }
		public string ZipCode { get; set; }
		public string Barangay { get; set; }
		public string Province { get; set; }
		public string City { get; set; }
		public string UserId { get; set; }
		public DateTime? ChangedDate { get; set; }
		public long VersionTimeStamp { get; set; }
	}
}