using System;
using System.ComponentModel.DataAnnotations;

namespace TanCruzDentalInventorySystem.ViewModels
{
	public class BusinessPartnerViewModel
	{
        [Required(ErrorMessage ="Business Partner is Required")]
        [Display(Name ="Business Partner")]
		public string BusinessPartnerId { get; set; }

		[Display(Name = "Preferred Vendor")]
		public string BusinessPartnerName { get; set; }
		public string BusinessPartnerType { get; set; }
		public string BusinessPartnerAlias { get; set; }
		public string BusinessPartnerFirstName { get; set; }
		public string BusinessPartnerMiddleName { get; set; }
		public string BusinessPartnerLastName { get; set; }
		public string UserId { get; set; }
		public DateTime? ChangedDate { get; set; }
		public long VersionTimeStamp { get; set; }
	}

	public class BusinessPartnerFormViewModel
	{
		public BusinessPartnerViewModel BusinessPartner { get; set; }
	}
}