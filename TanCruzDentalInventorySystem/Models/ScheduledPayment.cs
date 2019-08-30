using System;
using System.Collections.Generic;

namespace TanCruzDentalInventorySystem.Models
{
	public class ScheduledPayment
	{
		public string ScheduledPaymentId { get; set; }
		public string PurchaseOrderId { get; set; }
		public string SalesOrderId { get; set; }
		public BusinessPartner BusinessPartner { get; set; }
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
		public IEnumerable<ScheduledPaymentDetail> ScheduledPaymentDetails { get; set; }
	}
}