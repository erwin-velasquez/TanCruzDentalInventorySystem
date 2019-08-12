using System;
using System.Collections.Generic;

namespace TanCruzDentalInventorySystem.Models
{
	public class SalesOrder
	{
		public string SalesOrderId { get; set; }
		public long SalesOrderControlNumber { get; set; }
		public BusinessPartner BusinessPartner { get; set; }
		public Currency Currency { get; set; }
		public string SalesOrderStatus { get; set; }
		public DateTime? DeliveryDate { get; set; }
		public DateTime? PostingDate { get; set; }
		public DateTime? DocumentDate { get; set; }
		public string Remarks { get; set; }
		public string RefDocNumber { get; set; }
		public decimal SalesOrderDiscount { get; set; }
		public decimal SalesOrderDiscountAmount { get; set; }
		public decimal SalesOrderTax { get; set; }
		public decimal SalesOrderTotal { get; set; }
		public string UserId { get; set; }
		public DateTime? ChangedDate { get; set; }
		public long VersionTimeStamp { get; set; }
		public IEnumerable<SalesOrderDetail> SalesOrderDetails { get; set; }

        public string SalesOrderDetailsJson { get; set; }
    }
}
