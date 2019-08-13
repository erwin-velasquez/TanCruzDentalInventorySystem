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

		//public int SalesOrderDetails_ID { get; set; }
		//public string SalesOrderDetails_LineID { get; set; }
		//public string SalesOrderDetails_SalesOrderId { get; set; }
		//public string SalesOrderDetails_ItemId { get; set; }
		//public decimal SalesOrderDetails_ItemPrice { get; set; }
		//      public decimal SalesOrderDetails_Quantity { get; set; }
		//      public decimal SalesOrderDetails_OnHandQuantity { get; set; }
		//      public decimal SalesOrderDetails_LineDiscount { get; set; }
		//      public decimal SalesOrderDetails_LineDiscountAmount { get; set; }
		//      public string SalesOrderDetails_TaxId { get; set; }
		//      public decimal SalesOrderDetails_LineTaxAmount { get; set; }
		//      public decimal SalesOrderDetails_LineTotal { get; set; }
		//      public string SalesOrderDetails_Remarks { get; set; }
	}
}