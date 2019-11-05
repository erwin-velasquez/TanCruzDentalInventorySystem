using System;

namespace TanCruzDentalInventorySystem.Models
{
	public class SalesOrderPaymentDetail
	{
		public string SalesOrderPaymentDetailId { get; set; }
		public string SalesOrderPaymentId { get; set; }
        public string PaymentType { get; set; }
        public string CheckNumber { get; set; }
        public decimal SalesOrderPaymentDetailTotal { get; set; }
		public string Remarks { get; set; }
		public string UserId { get; set; }
		public DateTime? ChangedDate { get; set; }
		public long VersionTimeStamp { get; set; }
	}
}
