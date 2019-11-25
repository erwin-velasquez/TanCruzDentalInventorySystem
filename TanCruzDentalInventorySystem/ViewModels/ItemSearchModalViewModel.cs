using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TanCruzDentalInventorySystem.ViewModels
{
		public class ItemSearchFormViewModel
		{
				public string ItemPriceDetailsItemId { get; set; }
				public ItemViewModel Item { get; set; }
				public ItemPriceViewModel SalesOrderItemPrice { get; set; }
				public ItemPriceViewModel PurchaseOrderItemPrice { get; set; }
		}
}