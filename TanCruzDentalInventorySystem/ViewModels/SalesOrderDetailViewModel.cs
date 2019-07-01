using System;

namespace TanCruzDentalInventorySystem.ViewModels
{
	public class SalesOrderDetailsViewModel
	{
		public int SalesOrderDetails_ID { get; set; }
		public string SalesOrderDetails_LineID { get; set; }
		public string SalesOrderDetails_SalesOrderId { get; set; }
		public string SalesOrderDetails_ItemId { get; set; }
		public decimal SalesOrderDetails_ItemPrice { get; set; }
        public decimal SalesOrderDetails_Quantity { get; set; }
        public decimal SalesOrderDetails_OnHandQuantity { get; set; }
        public decimal SalesOrderDetails_LineDiscount { get; set; }
        public decimal SalesOrderDetails_LineDiscountAmount { get; set; }
        public string SalesOrderDetails_TaxId { get; set; }
        public decimal SalesOrderDetails_LineTaxAmount { get; set; }
        public decimal SalesOrderDetails_LineTotal { get; set; }
        public string SalesOrderDetails_Remarks { get; set; }

    }
}