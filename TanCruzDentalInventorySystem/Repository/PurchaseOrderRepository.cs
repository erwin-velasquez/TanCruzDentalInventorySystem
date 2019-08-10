using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TanCruzDentalInventorySystem.Models;
using TanCruzDentalInventorySystem.Repository.DataServiceInterface;

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

		public async Task<int> SavePurchaseOrder(PurchaseOrder purchaseOrder)
		{
			DynamicParameters parameters = new DynamicParameters();
			parameters.Add("@PurchaseOrderId", purchaseOrder.PurchaseOrderId, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@PurchaseOrderControlNumber", purchaseOrder.PurchaseOrderControlNumber, System.Data.DbType.Int64, System.Data.ParameterDirection.Input);
			parameters.Add("@BusinessPartnerId", purchaseOrder.BusinessPartner.BusinessPartnerId, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@CurrencyId", purchaseOrder.Currency.CurrencyId, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@PurchaseOrderStatus", purchaseOrder.PurchaseOrderStatus, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@DeliveryDate", purchaseOrder.DeliveryDate, System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);
			parameters.Add("@PostingDate", purchaseOrder.DeliveryDate, System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);
			parameters.Add("@DocumentDate", purchaseOrder.DeliveryDate, System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);
			parameters.Add("@Remarks", purchaseOrder.DeliveryDate, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@RefDocNumber", purchaseOrder.DeliveryDate, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@PurchaseOrderDiscount", purchaseOrder.PurchaseOrderDiscount, System.Data.DbType.Decimal, System.Data.ParameterDirection.Input);
			parameters.Add("@PurchaseOrderDiscountAmount", purchaseOrder.PurchaseOrderDiscountAmount, System.Data.DbType.Decimal, System.Data.ParameterDirection.Input);
			parameters.Add("@PurchaseOrderTax", purchaseOrder.PurchaseOrderDiscountAmount, System.Data.DbType.Decimal, System.Data.ParameterDirection.Input);
			parameters.Add("@PurchaseOrderTotal", purchaseOrder.PurchaseOrderDiscountAmount, System.Data.DbType.Decimal, System.Data.ParameterDirection.Input);
			parameters.Add("@UserId", purchaseOrder.UserId, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@ChangedDate", new DateTime(purchaseOrder.VersionTimeStamp), System.Data.DbType.DateTime2, System.Data.ParameterDirection.Input);

			var rowsAffected = await UnitOfWork.Connection.ExecuteAsync(
				sql: SP_SAVE_PURCHASEORDER,
				param: parameters,
				transaction: UnitOfWork.Transaction,
				commandType: System.Data.CommandType.StoredProcedure);

			return rowsAffected;
		}

		public async Task<PurchaseOrder> GetPurchaseOrder(string purchaseOrderId)
		{
			var parameters = new DynamicParameters();
			parameters.Add("@PurchaseOrderId", purchaseOrderId, System.Data.DbType.String, System.Data.ParameterDirection.Input);

			var purchaseOrder = await UnitOfWork.Connection.QueryAsync<PurchaseOrder>(
				sql: SP_GET_PURCHASEORDER,
				types:
					new[]
					{
						typeof(PurchaseOrder),
						typeof(BusinessPartner),
						typeof(Currency),
					},
				map:
					typeMap =>
					{
						if (!(typeMap[0] is PurchaseOrder purchaseOrderUnit)) return null;

						purchaseOrderUnit.BusinessPartner = typeMap[1] as BusinessPartner;
						purchaseOrderUnit.Currency = typeMap[2] as Currency;

						return purchaseOrderUnit;
					},
				param: parameters,
				transaction: UnitOfWork.Transaction,
				commandType: System.Data.CommandType.StoredProcedure,
				splitOn: "BusinessPartnerId, CurrencyId");

			var versionedPurchaseOrder = purchaseOrder.AsList().SingleOrDefault();
			versionedPurchaseOrder.VersionTimeStamp = versionedPurchaseOrder.ChangedDate.Value.Ticks;
			return versionedPurchaseOrder;
		}

		public async Task<string> CreatePurchaseOrderAsync(string userId)
		{
			DynamicParameters parameters = new DynamicParameters();
			parameters.Add("@UserId", userId, System.Data.DbType.String, System.Data.ParameterDirection.Input);

			var purchaseOrderId = await UnitOfWork.Connection.ExecuteScalarAsync<string>(
				sql: SP_CREATE_PURCHASEORDER,
				param: parameters,
				transaction: UnitOfWork.Transaction,
				commandType: System.Data.CommandType.StoredProcedure);

			return purchaseOrderId;
		}

		private const string SP_GET_PURCHASEORDER_LIST = "dbo.GetPurchaseOrders";
		private const string SP_SAVE_PURCHASEORDER = "dbo.SavePurchaseOrder";
		private const string SP_GET_PURCHASEORDER = "dbo.GetPurchaseOrder";
		private const string SP_CREATE_PURCHASEORDER = "dbo.CreatePurchaseOrder";
	}
}
