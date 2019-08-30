using System;

namespace TanCruzDentalInventorySystem.Models
{
	public class ScheduledPaymentDetail
	{
		public string ScheduledPaymentDetailId { get; set; }
		public string ScheduledPaymentId { get; set; }
		public Currency Currency { get; set; }
		public string ScheduledPaymentDetailStatus { get; set; }
		public string ModeOfPayment { get; set; }
		public DateTime DocumentDate { get; set; }
		public DateTime PostingDate { get; set; }
		public DateTime DueDate { get; set; }
		public string CheckNumber { get; set; }
		public string Bank { get; set; }
		public decimal PaymentOwed { get; set; }
		public decimal PaymentAmount { get; set; }
		public string UserId { get; set; }
		public DateTime? ChangedDate { get; set; }
		public long VersionTimeStamp { get; set; }
	}
}
