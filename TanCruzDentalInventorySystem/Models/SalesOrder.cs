using System;

namespace TanCruzDentalInventorySystem.Models
{
	public class SalesOrder
	{
		public SalesOrder()
		{

		}

		public int ID { get; set; }
		public string SALESORDER_ID { get; set; }
		public long SO_CONTROL_NUM { get; set; }
		public string BP_ID { get; set; }
		public string CURRENCY_ID { get; set; }
		public string SO_STATUS { get; set; }
		public DateTime? DELIVERY_DATE { get; set; }
		public DateTime? POSTING_DATE { get; set; }
		public DateTime? DOCUMENT_DATE { get; set; }
		public string REMARKS { get; set; }
		public string REFDOC_NUM { get; set; }
		public Decimal? SO_DISCOUNT { get; set; }
		public Decimal? SO_DISC_AMT { get; set; }
		public Decimal? SO_TAX { get; set; }
		public Decimal? SO_TOTAL { get; set; }
		public string CREATE_ID { get; set; }
		public DateTime? CREATE_DATE { get; set; }
		public string CHANGE_ID { get; set; }
		public DateTime? CHANGE_DATE { get; set; }
	}
}
