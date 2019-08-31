using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TanCruzDentalInventorySystem.ViewModels
{
	public class PurchaseOrderViewModel
	{
		[Display(Name = "Purchase Order Id")]
		public string PurchaseOrderId { get; set; }
		[Display(Name = "Purchase Order Control Number")]
		[Required]
		public long PurchaseOrderControlNumber { get; set; }
		[Display(Name = "Business Partner")]
		public BusinessPartnerViewModel BusinessPartner { get; set; }
		[Display(Name = "Currency")]
		public CurrencyViewModel Currency { get; set; }
		[Required]
		[Display(Name = "Purchase Order Status")]
		public string PurchaseOrderStatus { get; set; }
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
		[Display(Name = "Purchase Order Discount")]
		public decimal PurchaseOrderDiscount { get; set; }
		[Display(Name = "Purchase Order Discount Amount")]
		public decimal PurchaseOrderDiscountAmount { get; set; }
		[Display(Name = "Purchase Order Tax")]
		public decimal PurchaseOrderTax { get; set; }
		[Display(Name = "Purchase Order Total")]
		public decimal PurchaseOrderTotal { get; set; }
		public string UserId { get; set; }
		public DateTime? ChangedDate { get; set; }
		public long VersionTimeStamp { get; set; }
		public List<PurchaseOrderDetailViewModel> PurchaseOrderDetails { get; set; }
	}

	public class PurchaseOrderFormViewModel
	{
		public PurchaseOrderViewModel PurchaseOrder { get; set; }
		public IEnumerable<CurrencyViewModel> Currencies { get; set; }
		public IEnumerable<BusinessPartnerViewModel> BusinessPartners { get; set; }
	}
}