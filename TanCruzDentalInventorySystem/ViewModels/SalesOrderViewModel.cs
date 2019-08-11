using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TanCruzDentalInventorySystem.ViewModels
{
    public class SalesOrderViewModel
    {
		[Display(Name = "Sales Order Id")]
		public string SalesOrderId { get; set; }
		[Display(Name = "Sales Order Control Number")]
		[Required]
		public long SalesOrderControlNumber { get; set; }
		[Display(Name = "Business Partner")]
		public BusinessPartnerViewModel BusinessPartner { get; set; }
		[Display(Name = "Currency")]
		public CurrencyViewModel Currency { get; set; }
		[Required]
		[Display(Name = "Sales Order Status")]
		public string SalesOrderStatus { get; set; }
		[Display(Name = "Delivery Date")]
		public DateTime? DeliveryDate { get; set; }
		[Display(Name = "Posting Date")]
		public DateTime? PostingDate { get; set; }
		[Display(Name = "Document Date")]
		public DateTime? DocumentDate { get; set; }
		[Display(Name = "Remarks")]
		public string Remarks { get; set; }
		[Display(Name = "Reference Document Number")]
		public string RefDocNumber { get; set; }
		[Display(Name = "Sales Order Discount")]
		public decimal SalesOrderDiscount { get; set; }
		[Display(Name = "Sales Order Discount Amount")]
		public decimal SalesOrderDiscountAmount { get; set; }
		[Display(Name = "Sales Order Tax")]
		public decimal SalesOrderTax { get; set; }
		[Display(Name = "Sales Order Total")]
		public decimal SalesOrderTotal { get; set; }
		public string UserId { get; set; }
		public DateTime? ChangedDate { get; set; }
		public long VersionTimeStamp { get; set; }
		public List<SalesOrderDetailViewModel> SalesOrderDetails { get; set; }
	}

	public class SalesOrderFormViewModel
	{
		public SalesOrderViewModel SalesOrder { get; set; }
		public IEnumerable<CurrencyViewModel> Currencies { get; set; }
		public IEnumerable<BusinessPartnerViewModel> BusinessPartners { get; set; }
	}
}