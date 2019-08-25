using System;

namespace TanCruzDentalInventorySystem.Models
{
	public class PurchaseOrderDetail
	{
		public string PurchaseOrderDetailId { get; set; }
		public string PurchaseOrderId { get; set; }
		public Item Item { get; set; }
		public ItemPrice ItemPrice { get; set; }
		public decimal ItemPriceAmount { get; set; }
		public decimal Quantity { get; set; }
		public decimal QuantityOnHand { get; set; }
		public decimal PurchaseOrderDetailDiscount { get; set; }
		public decimal PurchaseOrderDetailDiscountAmount { get; set; }
		public Tax Tax { get; set; }
		public decimal PurchaseOrderDetailTax { get; set; }
		public decimal PurchaseOrderDetailTotal { get; set; }
		public string Remarks { get; set; }
		public string UserId { get; set; }
		public DateTime? ChangedDate { get; set; }
		public long VersionTimeStamp { get; set; }
	}
}
