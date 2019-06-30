using System;
using System.Collections.Generic;

namespace TanCruzDentalInventorySystem.ViewModels
{
	public class PurchaseOrderViewModel
    {
		public string PurchaseOrderId { get; set; }
		public long PurchaseOrderControlNumber { get; set; }
		public BusinessPartnerViewModel BusinessPartner { get; set; }
		public CurrencyViewModel Currency { get; set; }
		public string PurchaseOrderStatus { get; set; }
		public DateTime? DeliveryDate { get; set; }
		public DateTime? PostingDate { get; set; }
		public DateTime? DocumentDate { get; set; }
		public string Remarks { get; set; }
		public string RefDocNumber { get; set; }
		public decimal PurchaseOrderDiscount { get; set; }
		public decimal PurchaseOrderDiscountAmount { get; set; }
		public decimal PurchaseOrderTax { get; set; }
		public decimal PurchaseOrderTotal { get; set; }
		public string UserId { get; set; }
	}

	public class PurchaseOrderFormViewModel
	{
		public PurchaseOrderViewModel PurchaseOrder { get; set; }
		public IEnumerable<CurrencyViewModel> Currencies { get; set; }
		public IEnumerable<BusinessPartnerViewModel> BusinessPartners { get; set; }
	}
}