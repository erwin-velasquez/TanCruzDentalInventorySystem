using System;
using System.Collections;
using System.Collections.Generic;


namespace TanCruzDentalInventorySystem.ViewModels
{
	public class SalesOrderPaymentDetailViewModel
	{
		public string SalesOrderPaymentDetailId { get; set; }
		public string SalesOrderPaymentId { get; set; }
        public string PaymentType { get; set; }
		public decimal SalesOrderPaymentDetailTotal { get; set; }
		public string Remarks { get; set; }
		public string UserId { get; set; }
		public DateTime? ChangedDate { get; set; }
		public long VersionTimeStamp { get; set; }
	}
}