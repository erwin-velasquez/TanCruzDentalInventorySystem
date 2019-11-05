using System;
using System.Collections.Generic;

namespace TanCruzDentalInventorySystem.Models
{
    public class SalesOrderPayment
    {
        public string SOPaymentId { get; set; }
        public long SOPaymentControlNumber { get; set; }
        public SalesOrder SalesOrder { get; set; }
        public BusinessPartner BusinessPartner { get; set; }
        public Currency Currency { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime? DocumentDate { get; set; }
        public string RefDocNumber { get; set; }
        public decimal PaymentTotal { get; set; }
        public string Remarks { get; set; }
        public string UserId { get; set; }
        public DateTime? ChangedDate { get; set; }
        public long VersionTimeStamp { get; set; }
        public IEnumerable<SalesOrderPaymentDetail> SalesOrderPaymentDetails { get; set; }
    }
}
