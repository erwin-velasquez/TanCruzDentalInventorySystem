using System;
using System.Collections.Generic;

namespace TanCruzDentalInventorySystem.Models
{
	public class PurchaseOrder
	{
		public string PurchaseOrderId { get; set; }
		public long PurchaseOrderControlNumber { get; set; }
		public BusinessPartner BusinessPartner { get; set; }
		public Currency Currency { get; set; }
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
		public DateTime? ChangedDate { get; set; }
		public long VersionTimeStamp { get; set; }
		public IEnumerable<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
	}
}