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
			versionedSalesOrder.SalesOrderDetails = await GetSalesOrderDetailList(versionedSalesOrder.SalesOrderId);
			versionedSalesOrder.ScheduledPayments = await GetScheduledPaymentList(versionedSalesOrder.SalesOrderId);
			versionedSalesOrder.VersionTimeStamp = versionedSalesOrder.ChangedDate.Value.Ticks;
			return versionedSalesOrder;
		}

		public async Task<SalesOrderDetail> GetSalesOrderDetail(string salesOrderDetailId)
		{
			var parameters = new DynamicParameters();
			parameters.Add("@SalesOrderDetailId", salesOrderDetailId, System.Data.DbType.String, System.Data.ParameterDirection.Input);

			var salesOrderDetail = await UnitOfWork.Connection.QueryAsync<SalesOrderDetail>(
				sql: SP_GET_SALESORDERDETAIL,
				types:
					new[]
					{
						typeof(SalesOrderDetail),
						typeof(Item),
						typeof(ItemPrice),
						typeof(Tax)
					},
				map:
					typeMap =>
					{
						if (!(typeMap[0] is SalesOrderDetail salesOrderDetailUnit)) return null;

						salesOrderDetailUnit.Item = typeMap[1] as Item;
						salesOrderDetailUnit.ItemPrice = typeMap[2] as ItemPrice;
						salesOrderDetailUnit.Tax = typeMap[3] as Tax;

						return salesOrderDetailUnit;
					},
				param: parameters,
				transaction: UnitOfWork.Transaction,
				commandType: System.Data.CommandType.StoredProcedure,
				splitOn: "ItemId, ItemPriceId, TaxId");

			var versionedSalesOrderDetail = salesOrderDetail.AsList().SingleOrDefault();
			versionedSalesOrderDetail.VersionTimeStamp = versionedSalesOrderDetail.ChangedDate.Value.Ticks;
			return versionedSalesOrderDetail;
		}

        private async Task<IEnumerable<SalesOrderDetail>> GetSalesOrderDetailList(string salesOrderId)
		{
			var parameters = new DynamicParameters();
			parameters.Add("@SalesOrderId", salesOrderId, System.Data.DbType.String, System.Data.ParameterDirection.Input);

			var salesOrderDetailList = await UnitOfWork.Connection.QueryAsync<SalesOrderDetail>(
				sql: SP_GET_SALESORDERDETAIL_LIST,
				types:
					new[]
					{
						typeof(SalesOrderDetail),
						typeof(Item),
						typeof(ItemPrice),
						typeof(Tax)
					},
				map:
					typeMap =>
					{
						if (!(typeMap[0] is SalesOrderDetail salesOrderDetailUnit)) return null;

						salesOrderDetailUnit.Item = typeMap[1] as Item;
						salesOrderDetailUnit.ItemPrice = typeMap[2] as ItemPrice;
						salesOrderDetailUnit.Tax = typeMap[3] as Tax;

						return salesOrderDetailUnit;
					},
				param: parameters,
				transaction: UnitOfWork.Transaction,
				commandType: System.Data.CommandType.StoredProcedure,
				splitOn: "ItemId, ItemPriceId, TaxId");

			salesOrderDetailList.Select(detail => detail.VersionTimeStamp = detail.ChangedDate.Value.Ticks).ToList();
			return salesOrderDetailList;
		}

        public async Task<ScheduledPayment> GetSalesOrderPayment(string ScheduledPaymentId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ScheduledPaymentId", ScheduledPaymentId, System.Data.DbType.String, System.Data.ParameterDirection.Input);

            var salesOrderPayment = await UnitOfWork.Connection.QueryAsync<ScheduledPayment>(
                sql: SP_GET_SALESORDERPAYMENT,
                types:
                    new[]
                    {
                        typeof(ScheduledPayment),
                        typeof(BusinessPartner)
                    },
                map:
                    typeMap =>
                    {
                        if (!(typeMap[0] is ScheduledPayment salesOrderPaymentUnit)) return null;

                        salesOrderPaymentUnit.BusinessPartner = typeMap[1] as BusinessPartner;

                        return salesOrderPaymentUnit;
                    },
                param: parameters,
                transaction: UnitOfWork.Transaction,
                commandType: System.Data.CommandType.StoredProcedure,
                splitOn: "BusinessPartnerId");

            var versionedSalesOrderPayment = salesOrderPayment.AsList().SingleOrDefault();
            versionedSalesOrderPayment.ScheduledPaymentDetails = await GetSalesOrderPaymentDetailList(versionedSalesOrderPayment.ScheduledPaymentId);
            versionedSalesOrderPayment.VersionTimeStamp = versionedSalesOrderPayment.ChangedDate.Value.Ticks;
            return versionedSalesOrderPayment;
        }

        private async Task<IEnumerable<ScheduledPaymentDetail>> GetSalesOrderPaymentDetailList(string ScheduledPaymentId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ScheduledPaymentId", ScheduledPaymentId, System.Data.DbType.String, System.Data.ParameterDirection.Input);

            var salesOrderPaymentDetailList = await UnitOfWork.Connection.QueryAsync<ScheduledPaymentDetail>(
                sql: SP_GET_SALESORDERPAYMENTDETAIL_LIST,
                types:
                    new[]
                    {
                        typeof(SalesOrderDetail),
                        typeof(Currency)
                    },
                map:
                    typeMap =>
                    {
                        if (!(typeMap[0] is ScheduledPaymentDetail salesOrderPaymentDetailUnit)) return null;

                        salesOrderPaymentDetailUnit.Currency = typeMap[1] as Currency;

                        return salesOrderPaymentDetailUnit;
                    },
                param: parameters,
                transaction: UnitOfWork.Transaction,
                commandType: System.Data.CommandType.StoredProcedure,
                splitOn: "CurrencyId");

            salesOrderPaymentDetailList.Select(detail => detail.VersionTimeStamp = detail.ChangedDate.Value.Ticks).ToList();
            return salesOrderPaymentDetailList;
        }

        private async Task<IEnumerable<ScheduledPayment>> GetScheduledPaymentList(string salesOrderId)
		{
			var parameters = new DynamicParameters();
			parameters.Add("@OrderId", salesOrderId, System.Data.DbType.String, System.Data.ParameterDirection.Input);

			var scheduledPaymentList = await UnitOfWork.Connection.QueryAsync<ScheduledPayment>(
				sql: SP_GET_SCHEDULEDPAYMENT_LIST,
				types:
					new[]
					{
						typeof(ScheduledPayment),
						typeof(BusinessPartner)
					},
				map:
					typeMap =>
					{
						if (!(typeMap[0] is ScheduledPayment scheduledPaymentUnit)) return null;

						scheduledPaymentUnit.BusinessPartner = typeMap[1] as BusinessPartner;

						return scheduledPaymentUnit;
					},
				param: parameters,
				transaction: UnitOfWork.Transaction,
				commandType: System.Data.CommandType.StoredProcedure,
				splitOn: "BusinessPartnerId");

			scheduledPaymentList.Select(detail => detail.VersionTimeStamp = detail.ChangedDate.Value.Ticks).ToList();
			return scheduledPaymentList;
		}

		public async Task<string> CreateSalesOrder(string userId)
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

        public async Task<string> CreateSalesOrderPayment(string userId, string salesOrderId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@UserId", userId, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            parameters.Add("@SalesOrderId", salesOrderId, System.Data.DbType.String, System.Data.ParameterDirection.Input);

            var salesOrderPaymentId = await UnitOfWork.Connection.ExecuteScalarAsync<string>(
                sql: SP_CREATE_SALESORDERPAYMENT,
                param: parameters,
                transaction: UnitOfWork.Transaction,
                commandType: System.Data.CommandType.StoredProcedure);

            return salesOrderPaymentId;
        }

        public async Task<int> SaveSalesOrderDetail(SalesOrderDetail salesOrderDetail)
		{
			DynamicParameters parameters = new DynamicParameters();
			parameters.Add("@SalesOrderDetailId", salesOrderDetail.SalesOrderDetailId, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@SalesOrderId", salesOrderDetail.SalesOrderId, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@ItemId", salesOrderDetail.Item.ItemId, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            parameters.Add("@ItemPriceId", null, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            parameters.Add("@ItemPriceAmount", salesOrderDetail.ItemPriceAmount, System.Data.DbType.Decimal, System.Data.ParameterDirection.Input);
			parameters.Add("@Quantity", salesOrderDetail.Quantity, System.Data.DbType.Decimal, System.Data.ParameterDirection.Input);
			parameters.Add("@QuantityOnHand", salesOrderDetail.QuantityOnHand, System.Data.DbType.Decimal, System.Data.ParameterDirection.Input);
			parameters.Add("@SalesOrderDetailDiscount", salesOrderDetail.SalesOrderDetailDiscount, System.Data.DbType.Decimal, System.Data.ParameterDirection.Input);
			parameters.Add("@SalesOrderDetailDiscountAmount", salesOrderDetail.SalesOrderDetailDiscountAmount, System.Data.DbType.Decimal, System.Data.ParameterDirection.Input);
            parameters.Add("@TaxId", null, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            parameters.Add("@SalesOrderDetailTax", salesOrderDetail.SalesOrderDetailTax, System.Data.DbType.Decimal, System.Data.ParameterDirection.Input);
			parameters.Add("@SalesOrderDetailTotal", salesOrderDetail.SalesOrderDetailTotal, System.Data.DbType.Decimal, System.Data.ParameterDirection.Input);
			parameters.Add("@Remarks", salesOrderDetail.Remarks, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@UserId", salesOrderDetail.UserId, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@ChangedDate", new DateTime(salesOrderDetail.VersionTimeStamp), System.Data.DbType.DateTime2, System.Data.ParameterDirection.Input);

			var rowsAffected = await UnitOfWork.Connection.ExecuteAsync(
				sql: SP_SAVE_SALESORDERDETAIL,
				param: parameters,
				transaction: UnitOfWork.Transaction,
				commandType: System.Data.CommandType.StoredProcedure);

			return rowsAffected;
		}

		public async Task<string> CreateSalesOrderDetail(string userId)
		{
			DynamicParameters parameters = new DynamicParameters();
			parameters.Add("@UserId", userId, System.Data.DbType.String, System.Data.ParameterDirection.Input);

			var salesOrderDetailId = await UnitOfWork.Connection.ExecuteScalarAsync<string>(
				sql: SP_CREATE_SALESORDERDETAIL,
				param: parameters,
				transaction: UnitOfWork.Transaction,
				commandType: System.Data.CommandType.StoredProcedure);

			return salesOrderDetailId;
		}

		private const string SP_GET_SALESORDER_LIST = "dbo.GetSalesOrders";
		private const string SP_SAVE_SALESORDER = "dbo.SaveSalesOrder";
		private const string SP_GET_SALESORDER = "dbo.GetSalesOrder";
		private const string SP_GET_SALESORDERDETAIL_LIST = "dbo.GetSalesOrderDetails";
		private const string SP_GET_SALESORDERDETAIL = "dbo.GetSalesOrderDetail";
		private const string SP_CREATE_SALESORDER = "dbo.CreateSalesOrder";
		private const string SP_CREATE_SALESORDERDETAIL = "dbo.CreateSalesOrderDetail";
		private const string SP_SAVE_SALESORDERDETAIL = "dbo.SaveSalesOrderDetail";
		private const string SP_GET_SCHEDULEDPAYMENT_LIST = "dbo.GetScheduledPayments";
        
        private const string SP_GET_SALESORDERPAYMENT = "dbo.GetSalesOrderPayment";
        private const string SP_GET_SALESORDERPAYMENTDETAIL_LIST = "dbo.GetSalesOrderPaymentDetails";
        private const string SP_CREATE_SALESORDERPAYMENT = "dbo.CreateSalesOrderPayment";
    }
}
