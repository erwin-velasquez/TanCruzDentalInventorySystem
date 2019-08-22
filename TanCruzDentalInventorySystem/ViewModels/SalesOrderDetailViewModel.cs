using System;

namespace TanCruzDentalInventorySystem.ViewModels
{
	public class SalesOrderDetailViewModel
	{
		public string SalesOrderDetailId { get; set; }
		public string SalesOrderId { get; set; }
		public ItemViewModel Item { get; set; }
		public ItemPriceViewModel ItemPrice { get; set; }
		public decimal ItemPriceAmount { get; set; }
		public decimal Quantity { get; set; }
		public decimal QuantityOnHand { get; set; }
		public decimal SalesOrderDetailDiscount { get; set; }
		public decimal SalesOrderDetailDiscountAmount { get; set; }
		public TaxViewModel Tax { get; set; }
		public decimal SalesOrderDetailTax { get; set; }
		public decimal SalesOrderDetailTotal { get; set; }
		public string Remarks { get; set; }
		public string UserId { get; set; }
		public DateTime? ChangedDate { get; set; }
		public long VersionTimeStamp { get; set; }
	}
}