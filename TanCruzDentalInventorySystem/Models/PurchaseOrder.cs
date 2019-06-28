using System;

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
		public decimal PurchaseOrderAmount { get; set; }
		public decimal SalesOrderTax { get; set; }
		public decimal SalesOrderTotal { get; set; }
		public string UserId { get; set; }
	}
}