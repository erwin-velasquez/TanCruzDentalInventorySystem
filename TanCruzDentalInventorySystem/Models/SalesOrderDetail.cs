using System;

namespace TanCruzDentalInventorySystem.Models
{
	public class SalesOrderDetail
	{
		public string SalesOrderDetailId { get; set; }
		public string SalesOrderId { get; set; }
		public string ItemId { get; set; }
		public ItemPrice ItemPrice { get; set; }
		public decimal Quantity { get; set; }
		public decimal QuantityOnHand { get; set; }
		public decimal SalesOrderDetailDiscount { get; set; }
		public decimal SalesOrderDetailDiscountAmount { get; set; }
		public string TaxId { get; set; }
		public decimal SalesOrderDetailTax { get; set; }
		public decimal SalesOrderDetailTotal { get; set; }
		public string Remarks { get; set; }
		public string UserId { get; set; }
		public DateTime? ChangedDate { get; set; }
		public long VersionTimeStamp { get; set; }
	}
}
