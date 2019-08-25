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
			parameters.Add("@PostingDate", purchaseOrder.PostingDate, System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);
			parameters.Add("@DocumentDate", purchaseOrder.DocumentDate, System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);
			parameters.Add("@Remarks", purchaseOrder.Remarks, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@RefDocNumber", purchaseOrder.RefDocNumber, System.Data.DbType.String, System.Data.ParameterDirection.Input);
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
			versionedPurchaseOrder.PurchaseOrderDetails = await GetPurchaseOrderDetailList(versionedPurchaseOrder.PurchaseOrderId);
			versionedPurchaseOrder.VersionTimeStamp = versionedPurchaseOrder.ChangedDate.Value.Ticks;
			return versionedPurchaseOrder;
		}

		public async Task<PurchaseOrderDetail> GetPurchaseOrderDetail(string purchaseOrderDetailId)
		{
			var parameters = new DynamicParameters();
			parameters.Add("@PurchaseOrderDetailId", purchaseOrderDetailId, System.Data.DbType.String, System.Data.ParameterDirection.Input);

			var purchaseOrderDetail = await UnitOfWork.Connection.QueryAsync<PurchaseOrderDetail>(
				sql: SP_GET_PURCHASEORDERDETAIL,
				types:
					new[]
					{
						typeof(PurchaseOrderDetail),
						typeof(Item),
						typeof(ItemPrice),
						typeof(Tax)
					},
				map:
					typeMap =>
					{
						if (!(typeMap[0] is PurchaseOrderDetail purchaseOrderDetailUnit)) return null;

						purchaseOrderDetailUnit.Item = typeMap[1] as Item;
						purchaseOrderDetailUnit.ItemPrice = typeMap[2] as ItemPrice;
						purchaseOrderDetailUnit.Tax = typeMap[3] as Tax;

						return purchaseOrderDetailUnit;
					},
				param: parameters,
				transaction: UnitOfWork.Transaction,
				commandType: System.Data.CommandType.StoredProcedure,
				splitOn: "ItemId, ItemPriceId, TaxId");

			var versionedPurchaseOrderDetail = purchaseOrderDetail.AsList().SingleOrDefault();
			versionedPurchaseOrderDetail.VersionTimeStamp = versionedPurchaseOrderDetail.ChangedDate.Value.Ticks;
			return versionedPurchaseOrderDetail;
		}

		private async Task<IEnumerable<PurchaseOrderDetail>> GetPurchaseOrderDetailList(string purchaseOrderId)
		{
			var parameters = new DynamicParameters();
			parameters.Add("@PurchaseOrderId", purchaseOrderId, System.Data.DbType.String, System.Data.ParameterDirection.Input);

			var purchaseOrderDetailList = await UnitOfWork.Connection.QueryAsync<PurchaseOrderDetail>(
				sql: SP_GET_PURCHASEORDERDETAIL_LIST,
				types:
					new[]
					{
						typeof(PurchaseOrderDetail),
						typeof(Item),
						typeof(ItemPrice),
						typeof(Tax)
					},
				map:
					typeMap =>
					{
						if (!(typeMap[0] is PurchaseOrderDetail purchaseOrderDetailUnit)) return null;

						purchaseOrderDetailUnit.Item = typeMap[1] as Item;
						purchaseOrderDetailUnit.ItemPrice = typeMap[2] as ItemPrice;
						purchaseOrderDetailUnit.Tax = typeMap[3] as Tax;

						return purchaseOrderDetailUnit;
					},
				param: parameters,
				transaction: UnitOfWork.Transaction,
				commandType: System.Data.CommandType.StoredProcedure,
				splitOn: "ItemId, ItemPriceId, TaxId");

			purchaseOrderDetailList.Select(detail => detail.VersionTimeStamp = detail.ChangedDate.Value.Ticks).ToList();
			return purchaseOrderDetailList;
		}

		public async Task<string> CreatePurchaseOrder(string userId)
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

		public async Task<int> SavePurchaseOrderDetail(PurchaseOrderDetail purchaseOrderDetail)
		{
			DynamicParameters parameters = new DynamicParameters();
			parameters.Add("@PurchaseOrderDetailId", purchaseOrderDetail.PurchaseOrderDetailId, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@PurchaseOrderId", purchaseOrderDetail.PurchaseOrderId, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@ItemId", purchaseOrderDetail.Item.ItemId, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            parameters.Add("@ItemPriceId", purchaseOrderDetail.ItemPrice.ItemPriceId, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            parameters.Add("@ItemPriceAmount", purchaseOrderDetail.ItemPriceAmount, System.Data.DbType.Decimal, System.Data.ParameterDirection.Input);
			parameters.Add("@Quantity", purchaseOrderDetail.Quantity, System.Data.DbType.Decimal, System.Data.ParameterDirection.Input);
			parameters.Add("@QuantityOnHand", purchaseOrderDetail.QuantityOnHand, System.Data.DbType.Decimal, System.Data.ParameterDirection.Input);
			parameters.Add("@SalesOrderDetailDiscount", purchaseOrderDetail.PurchaseOrderDetailDiscount, System.Data.DbType.Decimal, System.Data.ParameterDirection.Input);
			parameters.Add("@SalesOrderDetailDiscountAmount", purchaseOrderDetail.PurchaseOrderDetailDiscountAmount, System.Data.DbType.Decimal, System.Data.ParameterDirection.Input);
            parameters.Add("@TaxId", purchaseOrderDetail.Tax.TaxId, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            parameters.Add("@SalesOrderDetailTax", purchaseOrderDetail.PurchaseOrderDetailTax, System.Data.DbType.Decimal, System.Data.ParameterDirection.Input);
			parameters.Add("@SalesOrderDetailTotal", purchaseOrderDetail.PurchaseOrderDetailTotal, System.Data.DbType.Decimal, System.Data.ParameterDirection.Input);
			parameters.Add("@Remarks", purchaseOrderDetail.Remarks, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@UserId", purchaseOrderDetail.UserId, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@ChangedDate", new DateTime(purchaseOrderDetail.VersionTimeStamp), System.Data.DbType.DateTime2, System.Data.ParameterDirection.Input);

			var rowsAffected = await UnitOfWork.Connection.ExecuteAsync(
				sql: SP_SAVE_PURCHASEORDERDETAIL,
				param: parameters,
				transaction: UnitOfWork.Transaction,
				commandType: System.Data.CommandType.StoredProcedure);

			return rowsAffected;
		}

		public async Task<string> CreatePurchaseOrderDetail(string userId)
		{
			DynamicParameters parameters = new DynamicParameters();
			parameters.Add("@UserId", userId, System.Data.DbType.String, System.Data.ParameterDirection.Input);

			var purchaseOrderDetailId = await UnitOfWork.Connection.ExecuteScalarAsync<string>(
				sql: SP_CREATE_PURCHASEORDERDETAIL,
				param: parameters,
				transaction: UnitOfWork.Transaction,
				commandType: System.Data.CommandType.StoredProcedure);

			return purchaseOrderDetailId;
		}
		private const string SP_GET_PURCHASEORDER_LIST = "dbo.GetPurchaseOrders";
		private const string SP_SAVE_PURCHASEORDER = "dbo.SavePurchaseOrder";
		private const string SP_GET_PURCHASEORDER = "dbo.GetPurchaseOrder";
		private const string SP_GET_PURCHASEORDERDETAIL_LIST = "dbo.GetPurchaseOrderDetails";
		private const string SP_GET_PURCHASEORDERDETAIL = "dbo.GetPurchaseOrderDetail";
		private const string SP_CREATE_PURCHASEORDER = "dbo.CreatePurchaseOrder";
		private const string SP_CREATE_PURCHASEORDERDETAIL = "dbo.CreatePurchaseOrderDetail";
		private const string SP_SAVE_PURCHASEORDERDETAIL = "dbo.SavePurchaseOrderDetail";
	}
}
