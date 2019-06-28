using System.Collections.Generic;
using System.Threading.Tasks;
using TanCruzDentalInventorySystem.Models;
using TanCruzDentalInventorySystem.Repository.DataServiceInterface;
using Dapper;

namespace TanCruzDentalInventorySystem.Repository
{
	public class PurchaseOrderRepository : IPurchaseOrderRepository
	{
		public IUnitOfWork UnitOfWork { get; set; }

		public async Task<IEnumerable<PurchaseOrder>> GetPurchaseOrderList()
		{
			var purchaseOrderList = await UnitOfWork.Connection.QueryAsync<PurchaseOrder>(
				sql: SP_GET_PURCHASEORDER_LIST,
				types:
					new[]
					{
						typeof(PurchaseOrder),
						typeof(BusinessPartner),
						typeof(Currency)
					},
				map:
					typeMap =>
					{
						if (!(typeMap[0] is PurchaseOrder purchaseOrderUnit)) return null;

						purchaseOrderUnit.BusinessPartner = typeMap[1] as BusinessPartner;
						purchaseOrderUnit.Currency = typeMap[2] as Currency;

						return purchaseOrderUnit;
					},
				param: null,
				transaction: UnitOfWork.Transaction,
				commandType: System.Data.CommandType.StoredProcedure,
				splitOn: "BusinessPartnerId, CurrencyId");

			return purchaseOrderList;

			//List<PurchaseOrder> result = new List<PurchaseOrder>();
			//         for (int x = 0; x < 100; x++)
			//         {
			//             result.Add(new PurchaseOrder()
			//             {
			//                 BP_ID = "bp_id " + x.ToString(),
			//                 CHANGE_DATE = DateTime.UtcNow,   


			//                 CHANGE_ID = "Manglinong, James P.",
			//                 CREATE_DATE = DateTime.UtcNow,
			//                 CREATE_ID = "Manglinong, James P.",
			//                 CURRENCY_ID = "PHP",
			//                 DELIVERY_DATE = DateTime.UtcNow,
			//                 DOCUMENT_DATE = DateTime.UtcNow,
			//                 ID = x,
			//                 POSTING_DATE = DateTime.UtcNow,
			//                 PO_CONTROL_NUM = x,
			//                 PO_DISCOUNT = 10,
			//                 PO_DISC_AMT = 10,
			//                 PO_STATUS = "Active",
			//                 PURCHASEORDER_ID = x.ToString(),
			//                 REFDOC_NUM = "REFDOC_NUM" + x.ToString(),
			//                 REMARKS = "Purchase sampling 101. This is a test purchase.",
			//                 SO_TAX = 1,
			//                 SO_TOTAL = 100
			//             }); ; ;
			//         }
			//         IEnumerable<PurchaseOrder> output = result;
			//         return output;
		}

		private const string SP_GET_PURCHASEORDER_LIST = "dbo.GetPurchaseOrders";
	}
}
