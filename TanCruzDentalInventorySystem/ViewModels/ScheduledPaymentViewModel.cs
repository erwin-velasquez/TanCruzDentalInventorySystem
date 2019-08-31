using System;
using System.Collections.Generic;

namespace TanCruzDentalInventorySystem.ViewModels
{
    public class ScheduledPaymentViewModel
	{
		public string ScheduledPaymentId { get; set; }
		public PurchaseOrderViewModel PurchaseOrder { get; set; }
		public SalesOrderViewModel SalesOrder { get; set; }
		public BusinessPartnerViewModel BusinessPartner { get; set; }
		public string ScheduledPaymentStatus { get; set; }
		public string ModeOfPayment { get; set; }
		public DateTime PostingDate { get; set; }
		public string Remarks { get; set; }
		public string RefDocNumber { get; set; }
		public decimal ScheduledPaymentDiscount { get; set; }
		public decimal ScheduledPaymentDiscountAmount { get; set; }
		public string UserId { get; set; }
		public DateTime? ChangedDate { get; set; }
		public long VersionTimeStamp { get; set; }
		public IEnumerable<ScheduledPaymentDetailViewModel> ScheduledPaymentDetails { get; set; }
	}

	public class ScheduledPaymentFormViewModel
	{
		public ScheduledPaymentViewModel ScheduledPayment { get; set; }
		public IEnumerable<PurchaseOrderViewModel> PurchaseOrders { get; set; }
		public IEnumerable<SalesOrderViewModel> SalesOrders { get; set; }
		public IEnumerable<BusinessPartnerViewModel> BusinessPartners { get; set; }
	}
}