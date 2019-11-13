using System;
using System.ComponentModel.DataAnnotations;

namespace TanCruzDentalInventorySystem.ViewModels
{
	public class BusinessPartnerViewModel
	{
        [Required(ErrorMessage ="Business Partner is Required")]
        [Display(Name ="Business Partner")]
		public string BusinessPartnerId { get; set; }

        [Required(ErrorMessage = "Business Partner Name is Required")]
        [Display(Name = "Vendor Name")]
		public string BusinessPartnerName { get; set; }
        [Display(Name = "Type")]
        public string BusinessPartnerType { get; set; }

        [Display(Name = "Display Name")]
        public string BusinessPartnerAlias { get; set; }

        [Display(Name = "First Name")]
        public string BusinessPartnerFirstName { get; set; }

        [Display(Name = "Middle Name")]
        public string BusinessPartnerMiddleName { get; set; }
        [Display(Name = "Last Name")]
        public string BusinessPartnerLastName { get; set; }
		public string UserId { get; set; }
		public DateTime? ChangedDate { get; set; }
		public long VersionTimeStamp { get; set; }
	}

	public class BusinessPartnerInFormViewModel
	{
		[Required(ErrorMessage = "Business Partner is Required")]
		[Display(Name = "Business Partner")]
		public string BusinessPartnerId { get; set; }

		[Display(Name = "Preferred Vendor")]
		public string BusinessPartnerName { get; set; }
		[Display(Name = "Type")]
		public string BusinessPartnerType { get; set; }

		[Display(Name = "Display Name")]
		public string BusinessPartnerAlias { get; set; }

		[Display(Name = "First Name")]
		public string BusinessPartnerFirstName { get; set; }

		[Display(Name = "Middle Name")]
		public string BusinessPartnerMiddleName { get; set; }
		[Display(Name = "Last Name")]
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