using System;

namespace TanCruzDentalInventorySystem.ViewModels
{
	public class ScheduledPaymentDetailViewModel
	{
		public string ScheduledPaymentDetailId { get; set; }
		public ScheduledPaymentViewModel ScheduledPayment { get; set; }
		public CurrencyViewModel Currency { get; set; }
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