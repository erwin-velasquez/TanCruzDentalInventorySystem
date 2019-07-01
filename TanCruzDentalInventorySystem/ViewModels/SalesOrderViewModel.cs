using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace TanCruzDentalInventorySystem.ViewModels
{
    public class SalesOrderViewModel
    {

        public int SalesOrder_Id { get; set; }

        [Display(Name = "Sales Order Id")]
        public string SalesOrder_SalesOrderId { get; set; }

        [Display(Name = "Control Number")]
        public long SalesOrder_SOControlNum { get; set; }

        [Display(Name = "Customer ID")]
        public string SalesOrder_BusinessPartnerId { get; set; }

        [Display(Name = "Currency ID")]
        public string SalesOrder_CurrencyId { get; set; }

        [Display(Name = "Sales Order Status")]
        public string SalesOrder_SalesOrderStatus { get; set; }

        [Display(Name = "Delivery Date")]
        public DateTime? SalesOrder_DeliveryDate { get; set; }

        [Display(Name = "Posting Date")]
        public DateTime? SalesOrder_PostingDate { get; set; }

        [Display(Name = "Document Date")]
        public DateTime? SalesOrder_DocumentDate { get; set; }

        [Display(Name = "Remarks")]
        public string SalesOrder_Remarks { get; set; }

        [Display(Name = "Reference Document Number")]
        public string SalesOrder_RefDocNum { get; set; }

        [Display(Name = "Discount ID")]
        public string SalesOrder_Discount { get; set; }

        [Display(Name = "Discount Amount")]
        public Decimal? SalesOrder_DiscountAmount { get; set; }

        [Display(Name = "Tax ID")]
        public string SalesOrder_TaxId { get; set; }

        [Display(Name = "Tax Amount")]
        public Decimal? SalesOrder_TaxAmount { get; set; }

        public string SalesOrder_CreateId { get; set; }
        public DateTime? SalesOrder_CreateDate { get; set; }
        public string SalesOrder_ChangeId { get; set; }
        public DateTime? SalesOrder_ChangeDate { get; set; }
        public ICollection<SalesOrderDetailsViewModel> SalesOrer_SalesOrderDetailList { get; set; }
	}
}