using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TanCruzDentalInventorySystem.Models;
using TanCruzDentalInventorySystem.Repository.DataServiceInterface;

namespace TanCruzDentalInventorySystem.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        public IUnitOfWork UnitOfWork { get; set; }

        public async Task<SalesOrderPayment> GetSalesOrderPayment(string salesOrderPaymentId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@SalesOrderId", salesOrderPaymentId, System.Data.DbType.String, System.Data.ParameterDirection.Input);

            var salesOrderPayment = await UnitOfWork.Connection.QueryAsync<SalesOrderPayment>(
                sql: SP_GET_SO_PAYMENT,
                types:
                    new[]
                    {
                        typeof(SalesOrderPayment),
                        typeof(SalesOrder),
                        typeof(BusinessPartner),
                        typeof(Currency)
                    },
                map:
                    typeMap =>
                    {
                        if (!(typeMap[0] is SalesOrderPayment salesOrderPaymentUnit)) return null;

                        salesOrderPaymentUnit.SalesOrder = typeMap[1] as SalesOrder;
                        salesOrderPaymentUnit.BusinessPartner = typeMap[2] as BusinessPartner;
                        salesOrderPaymentUnit.Currency = typeMap[3] as Currency;

                        return salesOrderPaymentUnit;
                    },
                param: parameters,
                transaction: UnitOfWork.Transaction,
                commandType: System.Data.CommandType.StoredProcedure,
                splitOn: "SalesOrderId, BusinessPartnerId, CurrencyId");

            var versionedSalesOrderPayment = salesOrderPayment.AsList().SingleOrDefault();
            versionedSalesOrderPayment.SalesOrderPaymentDetails = await GetSalesOrderPaymentDetailList(versionedSalesOrderPayment.SOPaymentId);
            versionedSalesOrderPayment.VersionTimeStamp = versionedSalesOrderPayment.ChangedDate.Value.Ticks;
            return versionedSalesOrderPayment;
        }

        public async Task<IEnumerable<SalesOrderPaymentDetail>> GetSalesOrderPaymentDetailList(string salesOrderPaymentId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@SalesOrderPaymentId", salesOrderPaymentId, System.Data.DbType.String, System.Data.ParameterDirection.Input);

            var salesOrderDetailList = await UnitOfWork.Connection.QueryAsync<SalesOrderPaymentDetail>(
                sql: SP_GET_SO_PAYMENTDETAIL_LIST,
                types:
                    new[]
                    {
                        typeof(SalesOrderPaymentDetail)
                    },
                map:
                    typeMap =>
                    {
                        if (!(typeMap[0] is SalesOrderPaymentDetail salesOrderPaymentDetail)) return null;

                        return salesOrderPaymentDetail;
                    },
                param: parameters,
                transaction: UnitOfWork.Transaction,
                commandType: System.Data.CommandType.StoredProcedure,
                splitOn: "");

            salesOrderDetailList.Select(detail => detail.VersionTimeStamp = detail.ChangedDate.Value.Ticks).ToList();
            return salesOrderDetailList;
        }

        public async Task<string> CreateSalesOrderPayment(string userId, string salesOrderId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@UserId", userId, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            parameters.Add("@SalesOrderId", userId, System.Data.DbType.String, System.Data.ParameterDirection.Input);

            var salesOrderPaymentId = await UnitOfWork.Connection.ExecuteScalarAsync<string>(
                sql: SP_CREATE_SO_PAYMENT,
                param: parameters,
                transaction: UnitOfWork.Transaction,
                commandType: System.Data.CommandType.StoredProcedure);

            return salesOrderPaymentId;
        }

        public async Task<string> CreateSalesOrderPaymentDetail(string userId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@UserId", userId, System.Data.DbType.String, System.Data.ParameterDirection.Input);

            var salesOrderPaymentDetailId = await UnitOfWork.Connection.ExecuteScalarAsync<string>(
                sql: SP_CREATE_SO_PAYMENTDETAIL,
                param: parameters,
                transaction: UnitOfWork.Transaction,
                commandType: System.Data.CommandType.StoredProcedure);

            return salesOrderPaymentDetailId;
        }

        public async Task<int> SaveSalesOrderPayment(SalesOrderPayment salesOrderPayment)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@SalesOrderPaymentId", salesOrderPayment.SOPaymentId, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            parameters.Add("@PaymentControlNum", salesOrderPayment.SOPaymentControlNumber, System.Data.DbType.Int64, System.Data.ParameterDirection.Input);
            parameters.Add("@SalesOrderId", salesOrderPayment.SalesOrder.SalesOrderId, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            parameters.Add("@BusinessPartnerId", salesOrderPayment.BusinessPartner.BusinessPartnerId, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            parameters.Add("@CurrencyId", salesOrderPayment.Currency.CurrencyId, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            parameters.Add("@PaymentStatus", salesOrderPayment.PaymentStatus, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            parameters.Add("@PaymentDate", salesOrderPayment.PaymentDate, System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);
            parameters.Add("@DocumentDate", salesOrderPayment.DocumentDate, System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);
            parameters.Add("@RefDocNumber", salesOrderPayment.RefDocNumber, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            parameters.Add("@PaymentTotal", salesOrderPayment.PaymentTotal, System.Data.DbType.Decimal, System.Data.ParameterDirection.Input);
            parameters.Add("@Remarks", salesOrderPayment.Remarks, System.Data.DbType.Decimal, System.Data.ParameterDirection.Input);
            parameters.Add("@UserId", salesOrderPayment.UserId, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            parameters.Add("@ChangedDate", new DateTime(salesOrderPayment.VersionTimeStamp), System.Data.DbType.DateTime2, System.Data.ParameterDirection.Input);

            var rowsAffected = await UnitOfWork.Connection.ExecuteAsync(
                sql: SP_SAVE_SO_PAYMENT,
                param: parameters,
                transaction: UnitOfWork.Transaction,
                commandType: System.Data.CommandType.StoredProcedure);

            return rowsAffected;
        }

        public async Task<int> SaveSalesOrderPaymentDetail(SalesOrderPaymentDetail salesOrderPaymentDetail)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@SalesOrderPaymentDetailId", salesOrderPaymentDetail.SalesOrderPaymentDetailId, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            parameters.Add("@SalesOrderPaymentId", salesOrderPaymentDetail.SalesOrderPaymentId, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            parameters.Add("@PaymentType", salesOrderPaymentDetail.PaymentType, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            parameters.Add("@CheckNumber", salesOrderPaymentDetail.CheckNumber, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            parameters.Add("@LineTotal", salesOrderPaymentDetail.SalesOrderPaymentDetailTotal, System.Data.DbType.Decimal, System.Data.ParameterDirection.Input);
            parameters.Add("@Remarks", salesOrderPaymentDetail.Remarks, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            parameters.Add("@UserId", salesOrderPaymentDetail.UserId, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            parameters.Add("@ChangedDate", new DateTime(salesOrderPaymentDetail.VersionTimeStamp), System.Data.DbType.DateTime2, System.Data.ParameterDirection.Input);

            var rowsAffected = await UnitOfWork.Connection.ExecuteAsync(
                sql: SP_SAVE_SO_PAYMENTDETAIL,
                param: parameters,
                transaction: UnitOfWork.Transaction,
                commandType: System.Data.CommandType.StoredProcedure);

            return rowsAffected;
        }

        public async Task<SalesOrderPaymentDetail> GetSalesOrderPaymentDetail(string salesOrderPaymentDetailId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@SalesOrderPaymentDetailId", salesOrderPaymentDetailId, System.Data.DbType.String, System.Data.ParameterDirection.Input);

            var salesOrderPaymentDetail = await UnitOfWork.Connection.QueryAsync<SalesOrderPaymentDetail>(
                sql: SP_GET_SO_PAYMENT_DETAIL,
                types:
                    new[]
                    {
                        typeof(SalesOrderPaymentDetail)
                    },
                map:
                    typeMap =>
                    {
                        if (!(typeMap[0] is SalesOrderPaymentDetail salesOrderPaymentDetailUnit)) return null;

                        return salesOrderPaymentDetailUnit;
                    },
                param: parameters,
                transaction: UnitOfWork.Transaction,
                commandType: System.Data.CommandType.StoredProcedure,
                splitOn: "");

            var versionedSalesOrderPaymentDetail = salesOrderPaymentDetail.AsList().SingleOrDefault();
            versionedSalesOrderPaymentDetail.VersionTimeStamp = versionedSalesOrderPaymentDetail.ChangedDate.Value.Ticks;
            return versionedSalesOrderPaymentDetail;
        }

        private const string SP_GET_SO_PAYMENT_LIST = "dbo.GetPayments";
        private const string SP_GET_SO_PAYMENT = "dbo.GetSalesOrderPayment";
        private const string SP_GET_SO_PAYMENT_DETAIL = "dbo.GetSalesOrderPaymentDetail";
        private const string SP_GET_SO_PAYMENTDETAIL_LIST = "dbo.GetSalesOrderPaymentDetails";
        private const string SP_GET_SO_PAYMENTDETAIL = "dbo.GetPaymentDetail";
        private const string SP_CREATE_SO_PAYMENT = "dbo.CreateSalesOrderPayment";
        private const string SP_CREATE_SO_PAYMENTDETAIL = "dbo.CreateSalesOrderPaymentDetail";
        private const string SP_SAVE_SO_PAYMENT = "dbo.SaveSalesOrderPayment";
        private const string SP_SAVE_SO_PAYMENTDETAIL = "dbo.SaveSalesOrderPaymentDetail";
        private const string SP_GET_SCHEDULEDPAYMENT_LIST = "dbo.GetScheduledPayments";
    }
}
