using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TanCruzDentalInventorySystem.Models
{
    public class Purchase
    {
        public Purchase()
        {

        }
        public int ID { get; set; }
        public string PURCHASEORDER_ID { get; set; }
        public long PO_CONTROL_NUM { get; set; }
        public string BP_ID { get; set; }
        public string CURRENCY_ID { get; set; }
        public string PO_STATUS { get; set; }
        public DateTime? DELIVERY_DATE { get; set; }
        public DateTime? POSTING_DATE { get; set; }
        public DateTime? DOCUMENT_DATE { get; set; }
        public string REMARKS { get; set; }
        public string REFDOC_NUM { get; set; }
        public Decimal? PO_DISCOUNT { get; set; }
        public Decimal? PO_DISC_AMT { get; set; }
        public Decimal? SO_TAX { get; set; }
        public Decimal? SO_TOTAL { get; set; }
        public string CREATE_ID { get; set; }
        public DateTime? CREATE_DATE { get; set; }
        public string CHANGE_ID { get; set; }
        public DateTime? CHANGE_DATE { get; set; }
    }
}