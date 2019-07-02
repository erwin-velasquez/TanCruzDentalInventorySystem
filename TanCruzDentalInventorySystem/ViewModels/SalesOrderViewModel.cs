using System;
using System.Collections;
using System.Collections.Generic;

namespace TanCruzDentalInventorySystem.ViewModels
{
    public class SalesOrderViewModel
    {
        public int SalesOrder_Id { get; set; }
        public string SalesOrder_SalesOrderId { get; set; }
        public long SalesOrder_SOControlNum { get; set; }
        public string SalesOrder_BusinessPartnerId { get; set; }
        public string SalesOrder_CurrencyId { get; set; }
        public string SalesOrder_SalesOrderStatus { get; set; }
        public DateTime? SalesOrder_DeliveryDate { get; set; }
        public DateTime? SalesOrder_PostingDate { get; set; }
        public DateTime? SalesOrder_DocumentDate { get; set; }
        public string SalesOrder_Remarks { get; set; }
        public string SalesOrder_RefDocNum { get; set; }
        public string SalesOrder_Discount { get; set; }
        public Decimal? SalesOrder_DiscountAmount { get; set; }
        public string SalesOrder_TaxId { get; set; }
        public Decimal? SalesOrder_TaxAmount { get; set; }
        public string SalesOrder_CreateId { get; set; }
        public DateTime? SalesOrder_CreateDate { get; set; }
        public string SalesOrder_ChangeId { get; set; }
        public DateTime? SalesOrder_ChangeDate { get; set; }
        public ICollection<SalesOrderDetailsViewModel> SalesOrer_SalesOrderDetailList { get; set; }
	}
}