using System;

namespace TanCruzDentalInventorySystem.ViewModels
{
	public class PurchaseOrderDetailViewModel
	{
		public string PurchaseOrderDetailId { get; set; }
		public string PurchaseOrderId { get; set; }
		public ItemViewModel Item { get; set; }
		public ItemPriceViewModel ItemPrice { get; set; }
		public decimal ItemPriceAmount { get; set; }
		public decimal Quantity { get; set; }
		public decimal QuantityOnHand { get; set; }
		public decimal PurchaseOrderDetailDiscount { get; set; }
		public decimal PurchaseOrderDetailDiscountAmount { get; set; }
		public TaxViewModel Tax { get; set; }
		public decimal PurchaseOrderDetailTax { get; set; }
		public decimal PurchaseOrderDetailTotal { get; set; }
		public string Remarks { get; set; }
		public string UserId { get; set; }
		public DateTime? ChangedDate { get; set; }
		public long VersionTimeStamp { get; set; }
        public decimal Total { get; set; }
	}
}