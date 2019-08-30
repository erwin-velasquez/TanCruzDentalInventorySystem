using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TanCruzDentalInventorySystem.Models;
using TanCruzDentalInventorySystem.Repository.DataServiceInterface;

namespace TanCruzDentalInventorySystem.Repository
{
	public class ScheduledPaymentRepository : IScheduledPaymentRepository
	{
		public IUnitOfWork UnitOfWork { get; set; }

		public async Task<IEnumerable<ScheduledPayment>> GetScheduledPaymentList()
		{
			var scheduledPaymentList = await UnitOfWork.Connection.QueryAsync<ScheduledPayment>(
				sql: SP_GET_SCHEDULEDPAYMENT_LIST,
				types:
					new[]
					{
						typeof(ScheduledPayment),
						typeof(BusinessPartner),
						typeof(Currency)
					},
				map:
					typeMap =>
					{
						if (!(typeMap[0] is ScheduledPayment scheduledPaymentUnit)) return null;

						scheduledPaymentUnit.BusinessPartner = typeMap[1] as BusinessPartner;

						return scheduledPaymentUnit;
					},
				param: null,
				transaction: UnitOfWork.Transaction,
				commandType: System.Data.CommandType.StoredProcedure,
				splitOn: "BusinessPartnerId, CurrencyId");

			return scheduledPaymentList;
		}

		public async Task<int> SaveScheduledPayment(ScheduledPayment scheduledPayment)
		{
			//DynamicParameters parameters = new DynamicParameters();
			//parameters.Add("@ScheduledPaymentId", scheduledPayment.ScheduledPaymentId, System.Data.DbType.String, System.Data.ParameterDirection.Input);

			//parameters.Add("@UserId", scheduledPayment.UserId, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			//parameters.Add("@ChangedDate", new DateTime(scheduledPayment.VersionTimeStamp), System.Data.DbType.DateTime2, System.Data.ParameterDirection.Input);

			//var rowsAffected = await UnitOfWork.Connection.ExecuteAsync(
			//	sql: SP_SAVE_SCHEDULEDPAYMENT,
			//	param: parameters,
			//	transaction: UnitOfWork.Transaction,
			//	commandType: System.Data.CommandType.StoredProcedure);

			//return rowsAffected;

			return 0;
		}

		public async Task<ScheduledPayment> GetScheduledPayment(string scheduledPaymentId)
		{
			var parameters = new DynamicParameters();
			parameters.Add("@ScheduledPaymentId", scheduledPaymentId, System.Data.DbType.String, System.Data.ParameterDirection.Input);

			var scheduledPayment = await UnitOfWork.Connection.QueryAsync<ScheduledPayment>(
				sql: SP_GET_SCHEDULEDPAYMENT,
				types:
					new[]
					{
						typeof(ScheduledPayment),
						typeof(BusinessPartner),
						typeof(Currency)
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
				splitOn: "BusinessPartnerId, CurrencyId");

			var versionedScheduledPayment = scheduledPayment.AsList().SingleOrDefault();
			versionedScheduledPayment.ScheduledPaymentDetails = await GetScheduledPaymentDetailList(versionedScheduledPayment.ScheduledPaymentId);
			versionedScheduledPayment.VersionTimeStamp = versionedScheduledPayment.ChangedDate.Value.Ticks;
			return versionedScheduledPayment;
		}

		public async Task<ScheduledPaymentDetail> GetScheduledPaymentDetail(string scheduledPaymentDetailId)
		{
			//var parameters = new DynamicParameters();
			//parameters.Add("@ScheduledPaymentDetailId", scheduledPaymentDetailId, System.Data.DbType.String, System.Data.ParameterDirection.Input);

			//var scheduledPaymentDetail = await UnitOfWork.Connection.QueryAsync<ScheduledPaymentDetail>(
			//	sql: SP_GET_SCHEDULEDPAYMENTDETAIL,
			//	types:
			//		new[]
			//		{
			//			typeof(ScheduledPaymentDetail),
			//			typeof(Item),
			//			typeof(ItemPrice),
			//			typeof(Tax)
			//		},
			//	map:
			//		typeMap =>
			//		{
			//			if (!(typeMap[0] is ScheduledPaymentDetail scheduledPaymentDetailUnit)) return null;

			//			scheduledPaymentDetailUnit.Item = typeMap[1] as Item;
			//			scheduledPaymentDetailUnit.ItemPrice = typeMap[2] as ItemPrice;
			//			scheduledPaymentDetailUnit.Tax = typeMap[3] as Tax;

			//			return scheduledPaymentDetailUnit;
			//		},
			//	param: parameters,
			//	transaction: UnitOfWork.Transaction,
			//	commandType: System.Data.CommandType.StoredProcedure,
			//	splitOn: "ItemId, ItemPriceId, TaxId");

			//var versionedScheduledPaymentDetail = scheduledPaymentDetail.AsList().SingleOrDefault();
			//versionedScheduledPaymentDetail.VersionTimeStamp = versionedScheduledPaymentDetail.ChangedDate.Value.Ticks;
			//return versionedScheduledPaymentDetail;

			return null;
		}

		private async Task<IEnumerable<ScheduledPaymentDetail>> GetScheduledPaymentDetailList(string scheduledPaymentId)
		{
			//var parameters = new DynamicParameters();
			//parameters.Add("@ScheduledPaymentId", scheduledPaymentId, System.Data.DbType.String, System.Data.ParameterDirection.Input);

			//var scheduledPaymentDetailList = await UnitOfWork.Connection.QueryAsync<ScheduledPaymentDetail>(
			//	sql: SP_GET_SCHEDULEDPAYMENTDETAIL_LIST,
			//	types:
			//		new[]
			//		{
			//			typeof(ScheduledPaymentDetail),
			//			typeof(Item),
			//			typeof(ItemPrice),
			//			typeof(Tax)
			//		},
			//	map:
			//		typeMap =>
			//		{
			//			if (!(typeMap[0] is ScheduledPaymentDetail scheduledPaymentDetailUnit)) return null;

			//			scheduledPaymentDetailUnit.Item = typeMap[1] as Item;
			//			scheduledPaymentDetailUnit.ItemPrice = typeMap[2] as ItemPrice;
			//			scheduledPaymentDetailUnit.Tax = typeMap[3] as Tax;

			//			return scheduledPaymentDetailUnit;
			//		},
			//	param: parameters,
			//	transaction: UnitOfWork.Transaction,
			//	commandType: System.Data.CommandType.StoredProcedure,
			//	splitOn: "ItemId, ItemPriceId, TaxId");

			//scheduledPaymentDetailList.Select(detail => detail.VersionTimeStamp = detail.ChangedDate.Value.Ticks).ToList();
			//return scheduledPaymentDetailList;

			return null;
		}

		public async Task<string> CreateScheduledPayment(string userId)
		{
			//DynamicParameters parameters = new DynamicParameters();
			//parameters.Add("@UserId", userId, System.Data.DbType.String, System.Data.ParameterDirection.Input);

			//var scheduledPaymentId = await UnitOfWork.Connection.ExecuteScalarAsync<string>(
			//	sql: SP_CREATE_SCHEDULEDPAYMENT,
			//	param: parameters,
			//	transaction: UnitOfWork.Transaction,
			//	commandType: System.Data.CommandType.StoredProcedure);

			//return scheduledPaymentId;

			return null;
		}

		public async Task<int> SaveScheduledPaymentDetail(ScheduledPaymentDetail scheduledPaymentDetail)
		{
			//DynamicParameters parameters = new DynamicParameters();
			//parameters.Add("@ScheduledPaymentDetailId", scheduledPaymentDetail.ScheduledPaymentDetailId, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			//parameters.Add("@ScheduledPaymentId", scheduledPaymentDetail.ScheduledPaymentId, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			//parameters.Add("@UserId", scheduledPaymentDetail.UserId, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			//parameters.Add("@ChangedDate", new DateTime(scheduledPaymentDetail.VersionTimeStamp), System.Data.DbType.DateTime2, System.Data.ParameterDirection.Input);

			//var rowsAffected = await UnitOfWork.Connection.ExecuteAsync(
			//	sql: SP_SAVE_SCHEDULEDPAYMENTDETAIL,
			//	param: parameters,
			//	transaction: UnitOfWork.Transaction,
			//	commandType: System.Data.CommandType.StoredProcedure);

			//return rowsAffected;

			return 0;
		}

		public async Task<string> CreateScheduledPaymentDetail(string userId)
		{
			//DynamicParameters parameters = new DynamicParameters();
			//parameters.Add("@UserId", userId, System.Data.DbType.String, System.Data.ParameterDirection.Input);

			//var scheduledPaymentDetailId = await UnitOfWork.Connection.ExecuteScalarAsync<string>(
			//	sql: SP_CREATE_SCHEDULEDPAYMENTDETAIL,
			//	param: parameters,
			//	transaction: UnitOfWork.Transaction,
			//	commandType: System.Data.CommandType.StoredProcedure);

			//return scheduledPaymentDetailId;

			return null;
		}

		private const string SP_GET_SCHEDULEDPAYMENT_LIST = "dbo.GetScheduledPayments";
		private const string SP_SAVE_SCHEDULEDPAYMENT = "dbo.SaveScheduledPayment";
		private const string SP_GET_SCHEDULEDPAYMENT = "dbo.GetScheduledPayment";
		private const string SP_GET_SCHEDULEDPAYMENTDETAIL_LIST = "dbo.GetScheduledPaymentDetails";
		private const string SP_GET_SCHEDULEDPAYMENTDETAIL = "dbo.GetScheduledPaymentDetail";
		private const string SP_CREATE_SCHEDULEDPAYMENT = "dbo.CreateScheduledPayment";
		private const string SP_CREATE_SCHEDULEDPAYMENTDETAIL = "dbo.CreateScheduledPaymentDetail";
		private const string SP_SAVE_SCHEDULEDPAYMENTDETAIL = "dbo.SaveScheduledPaymentDetail";
	}
}
