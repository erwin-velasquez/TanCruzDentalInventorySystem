using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TanCruzDentalInventorySystem.Models;
using TanCruzDentalInventorySystem.Repository.DataServiceInterface;

namespace TanCruzDentalInventorySystem.Repository
{
	public class SalesOrderRepository : ISalesOrderRepository
	{
		public IUnitOfWork UnitOfWork { get; set; }

		public async Task<IEnumerable<SalesOrder>> GetSalesOrderList()
		{
			var salesOrderList = await UnitOfWork.Connection.QueryAsync<SalesOrder>(
				sql: SP_GET_SALESORDER_LIST,
				types:
					new[]
					{
						typeof(SalesOrder),
						typeof(BusinessPartner),
						typeof(Currency)
					},
				map:
					typeMap =>
					{
						if (!(typeMap[0] is SalesOrder salesOrderUnit)) return null;

						salesOrderUnit.BusinessPartner = typeMap[1] as BusinessPartner;
						salesOrderUnit.Currency = typeMap[2] as Currency;

						return salesOrderUnit;
					},
				param: null,
				transaction: UnitOfWork.Transaction,
				commandType: System.Data.CommandType.StoredProcedure,
				splitOn: "BusinessPartnerId, CurrencyId");

			return salesOrderList;

			//List<SalesOrder> result = new List<SalesOrder>();
			//         for (int x = 0; x < 100; x++)
			//         {
			//             result.Add(new SalesOrder()
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
			//         IEnumerable<SalesOrder> output = result;
			//         return output;
		}

		public async Task<int> SaveSalesOrder(SalesOrder salesOrder)
		{
			DynamicParameters parameters = new DynamicParameters();
			parameters.Add("@SalesOrderId", salesOrder.SalesOrderId, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@SalesOrderControlNumber", salesOrder.SalesOrderControlNumber, System.Data.DbType.Int64, System.Data.ParameterDirection.Input);
			parameters.Add("@BusinessPartnerId", salesOrder.BusinessPartner.BusinessPartnerId, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@CurrencyId", salesOrder.Currency.CurrencyId, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@SalesOrderStatus", salesOrder.SalesOrderStatus, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@DeliveryDate", salesOrder.DeliveryDate, System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);
			parameters.Add("@PostingDate", salesOrder.PostingDate, System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);
			parameters.Add("@DocumentDate", salesOrder.DocumentDate, System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);
			parameters.Add("@Remarks", salesOrder.Remarks, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@RefDocNumber", salesOrder.RefDocNumber, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@SalesOrderDiscount", salesOrder.SalesOrderDiscount, System.Data.DbType.Decimal, System.Data.ParameterDirection.Input);
			parameters.Add("@SalesOrderDiscountAmount", salesOrder.SalesOrderDiscountAmount, System.Data.DbType.Decimal, System.Data.ParameterDirection.Input);
			parameters.Add("@SalesOrderTax", salesOrder.SalesOrderDiscountAmount, System.Data.DbType.Decimal, System.Data.ParameterDirection.Input);
			parameters.Add("@SalesOrderTotal", salesOrder.SalesOrderDiscountAmount, System.Data.DbType.Decimal, System.Data.ParameterDirection.Input);
			parameters.Add("@UserId", salesOrder.UserId, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@ChangedDate", new DateTime(salesOrder.VersionTimeStamp), System.Data.DbType.DateTime2, System.Data.ParameterDirection.Input);

			var rowsAffected = await UnitOfWork.Connection.ExecuteAsync(
				sql: SP_SAVE_SALESORDER,
				param: parameters,
				transaction: UnitOfWork.Transaction,
				commandType: System.Data.CommandType.StoredProcedure);

			return rowsAffected;
		}

		public async Task<SalesOrder> GetSalesOrder(string salesOrderId)
		{
			var parameters = new DynamicParameters();
			parameters.Add("@SalesOrderId", salesOrderId, System.Data.DbType.String, System.Data.ParameterDirection.Input);

			var salesOrder = await UnitOfWork.Connection.QueryAsync<SalesOrder>(
				sql: SP_GET_SALESORDER,
				types:
					new[]
					{
						typeof(SalesOrder),
						typeof(BusinessPartner),
						typeof(Currency)
					},
				map:
					typeMap =>
					{
						if (!(typeMap[0] is SalesOrder salesOrderUnit)) return null;

						salesOrderUnit.BusinessPartner = typeMap[1] as BusinessPartner;
						salesOrderUnit.Currency = typeMap[2] as Currency;

						return salesOrderUnit;
					},
				param: parameters,
				transaction: UnitOfWork.Transaction,
				commandType: System.Data.CommandType.StoredProcedure,
				splitOn: "BusinessPartnerId, CurrencyId");

			var versionedSalesOrder = salesOrder.AsList().SingleOrDefault();
			versionedSalesOrder.VersionTimeStamp = versionedSalesOrder.ChangedDate.Value.Ticks;
			return versionedSalesOrder;
		}

		public async Task<string> CreateSalesOrderAsync(string userId)
		{
			DynamicParameters parameters = new DynamicParameters();
			parameters.Add("@UserId", userId, System.Data.DbType.String, System.Data.ParameterDirection.Input);

			var salesOrderId = await UnitOfWork.Connection.ExecuteScalarAsync<string>(
				sql: SP_CREATE_SALESORDER,
				param: parameters,
				transaction: UnitOfWork.Transaction,
				commandType: System.Data.CommandType.StoredProcedure);

			return salesOrderId;
		}

		private const string SP_GET_SALESORDER_LIST = "dbo.GetSalesOrders";
		private const string SP_SAVE_SALESORDER = "dbo.SaveSalesOrder";
		private const string SP_GET_SALESORDER = "dbo.GetSalesOrder";
		private const string SP_CREATE_SALESORDER = "dbo.CreateSalesOrder";
	}
}


		//public IEnumerable<SalesOrder> GetSalesOrderList()
		//{
			//List<SalesOrder> result = new List<SalesOrder>();
			//for (int x = 0; x < 100; x++)
			//{
			//	result.Add(new SalesOrder()
			//	{
			//		BP_ID = "bp_id " + x.ToString(),
			//		CHANGE_DATE = DateTime.UtcNow,


			//		CHANGE_ID = "Manglinong, James P.",
			//		CREATE_DATE = DateTime.UtcNow,
			//		CREATE_ID = "Manglinong, James P.",
			//		CURRENCY_ID = "PHP",
			//		DELIVERY_DATE = DateTime.UtcNow,
			//		DOCUMENT_DATE = DateTime.UtcNow,
			//		ID = x,
			//		POSTING_DATE = DateTime.UtcNow,
			//		SO_CONTROL_NUM = x,
			//		SO_DISCOUNT = 10,
			//		SO_DISC_AMT = 10,
			//		SO_STATUS = "Active",
			//		SALESORDER_ID = x.ToString(),
			//		REFDOC_NUM = "REFDOC_NUM" + x.ToString(),
			//		REMARKS = "Sales sampling 101. This is a test Sales.",
			//		SO_TAX = 1,
			//		SO_TOTAL = 100
			//	}); ; ;
			//}
			//IEnumerable<SalesOrder> output = result;
			//return output;
		//}