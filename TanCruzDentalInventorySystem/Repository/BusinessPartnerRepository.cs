using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TanCruzDentalInventorySystem.Models;
using TanCruzDentalInventorySystem.Repository.DataServiceInterface;

namespace TanCruzDentalInventorySystem.Repository
{
	public class BusinessPartnerRepository : IBusinessPartnerRepository
	{
		public IUnitOfWork UnitOfWork { get; set; }

		public async Task<IEnumerable<BusinessPartner>> GetBusinessPartnerList()
		{
			var businessPartners = await UnitOfWork.Connection.QueryAsync<BusinessPartner>(
				sql: SP_GET_BUSINESSPARTNER_LIST,
				param: null,
				transaction: UnitOfWork.Transaction,
				commandType: System.Data.CommandType.StoredProcedure);

			return businessPartners;
		}

		public async Task<BusinessPartner> GetBusinessPartner(string businessPartnerId)
		{
			var parameters = new DynamicParameters();
			parameters.Add("@BusinessPartnerId", businessPartnerId, System.Data.DbType.String, System.Data.ParameterDirection.Input);

			var businessPartner = await UnitOfWork.Connection.QueryAsync<BusinessPartner>(
				sql: SP_GET_BUSINESSPARTNER,
				param: parameters,
				transaction: UnitOfWork.Transaction,
				commandType: System.Data.CommandType.StoredProcedure);

			var versionedBusinessPartner = businessPartner.AsList().SingleOrDefault();
			versionedBusinessPartner.BusinessPartnerDetails = await GetBusinessPartnerDetailList(versionedBusinessPartner.BusinessPartnerId);
			versionedBusinessPartner.VersionTimeStamp = versionedBusinessPartner.ChangedDate.Value.Ticks;
			return versionedBusinessPartner;
		}

		private async Task<IEnumerable<BusinessPartnerDetail>> GetBusinessPartnerDetailList(string businessPartnerId)
		{
			var parameters = new DynamicParameters();
			parameters.Add("@BusinessPartnerId", businessPartnerId, System.Data.DbType.String, System.Data.ParameterDirection.Input);

			var businessPartnerIdDetailList = await UnitOfWork.Connection.QueryAsync<BusinessPartnerDetail>(
				sql: SP_GET_BUSINESSPARTNERDETAIL_LIST,
				param: parameters,
				transaction: UnitOfWork.Transaction,
				commandType: System.Data.CommandType.StoredProcedure);

			businessPartnerIdDetailList.Select(detail => detail.VersionTimeStamp = detail.ChangedDate.Value.Ticks).ToList();
			return businessPartnerIdDetailList;
		}

		public async Task<string> CreateBusinessPartner(string userId)
		{
			DynamicParameters parameters = new DynamicParameters();
			parameters.Add("@UserId", userId, System.Data.DbType.String, System.Data.ParameterDirection.Input);

			var businessPartnerId = await UnitOfWork.Connection.ExecuteScalarAsync<string>(
				sql: SP_CREATE_BUSINESSPARTNER,
				param: parameters,
				transaction: UnitOfWork.Transaction,
				commandType: System.Data.CommandType.StoredProcedure);

			return businessPartnerId;
		}

		public async Task<int> SaveBusinessPartner(BusinessPartner businessPartner)
		{
			DynamicParameters parameters = new DynamicParameters();
			parameters.Add("@BusinessPartnerId", businessPartner.BusinessPartnerId, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@BusinessPartnerName", businessPartner.BusinessPartnerName, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@BusinessPartnerType", businessPartner.BusinessPartnerType, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@BusinessPartnerAlias", businessPartner.BusinessPartnerAlias, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@BusinessPartnerFirstName", businessPartner.BusinessPartnerFirstName, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@BusinessPartnerMiddleName", businessPartner.BusinessPartnerMiddleName, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@BusinessPartnerLastName", businessPartner.BusinessPartnerLastName, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@UserId", businessPartner.UserId, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@ChangedDate", new DateTime(businessPartner.VersionTimeStamp), System.Data.DbType.DateTime2, System.Data.ParameterDirection.Input);

			var rowsAffected = await UnitOfWork.Connection.ExecuteAsync(
				sql: SP_SAVE_BUSINESSPARTNER,
				param: parameters,
				transaction: UnitOfWork.Transaction,
				commandType: System.Data.CommandType.StoredProcedure);

			return rowsAffected;
		}

		public async Task<string> CreateBusinessPartnerDetail(string userId)
		{
			DynamicParameters parameters = new DynamicParameters();
			parameters.Add("@UserId", userId, System.Data.DbType.String, System.Data.ParameterDirection.Input);

			var businessPartnerDetailId = await UnitOfWork.Connection.ExecuteScalarAsync<string>(
				sql: SP_CREATE_BUSINESSPARTNERDETAIL,
				param: parameters,
				transaction: UnitOfWork.Transaction,
				commandType: System.Data.CommandType.StoredProcedure);

			return businessPartnerDetailId;
		}

		public async Task<BusinessPartnerDetail> GetBusinessPartnerDetail(string businessPartnerDetailId)
		{
			var parameters = new DynamicParameters();
			parameters.Add("@BusinessPartnerDetailId", businessPartnerDetailId, System.Data.DbType.String, System.Data.ParameterDirection.Input);

			var businessPartnerDetail = await UnitOfWork.Connection.QueryAsync<BusinessPartnerDetail>(
				sql: SP_GET_BUSINESSPARTNERDETAIL,
				param: parameters,
				transaction: UnitOfWork.Transaction,
				commandType: System.Data.CommandType.StoredProcedure);

			var versionedBusinessPartnerDetail = businessPartnerDetail.AsList().SingleOrDefault();
			versionedBusinessPartnerDetail.VersionTimeStamp = versionedBusinessPartnerDetail.ChangedDate.Value.Ticks;
			return versionedBusinessPartnerDetail;
		}

		public async Task<int> SaveBusinessPartnerDetail(BusinessPartnerDetail businessPartnerDetail)
		{
			DynamicParameters parameters = new DynamicParameters();
			parameters.Add("@BusinessPartnerDetailId", businessPartnerDetail.BusinessPartnerDetailId, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@BusinessPartnerId", businessPartnerDetail.BusinessPartnerId, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@TelephoneNumber1", businessPartnerDetail.TelephoneNumber1, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@TelephoneNumber2", businessPartnerDetail.TelephoneNumber2, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@MobileNumber", businessPartnerDetail.MobileNumber, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@Fax", businessPartnerDetail.Fax, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@Email", businessPartnerDetail.Email, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@WebSite", businessPartnerDetail.WebSite, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@ShippingType", businessPartnerDetail.ShippingType, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@Address1", businessPartnerDetail.Address1, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@Address2", businessPartnerDetail.Address2, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@ZipCode", businessPartnerDetail.ZipCode, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@Barangay", businessPartnerDetail.Barangay, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@Province", businessPartnerDetail.Province, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@City", businessPartnerDetail.City, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@UserId", businessPartnerDetail.UserId, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@ChangedDate", new DateTime(businessPartnerDetail.VersionTimeStamp), System.Data.DbType.DateTime2, System.Data.ParameterDirection.Input);

			var rowsAffected = await UnitOfWork.Connection.ExecuteAsync(
				sql: SP_SAVE_BUSINESSPARTNERDETAIL,
				param: parameters,
				transaction: UnitOfWork.Transaction,
				commandType: System.Data.CommandType.StoredProcedure);

			return rowsAffected;
		}

		private const string SP_GET_BUSINESSPARTNER_LIST = "dbo.GetBusinessPartners";
		private const string SP_GET_BUSINESSPARTNER = "dbo.GetBusinessPartner";
		private const string SP_GET_BUSINESSPARTNERDETAIL_LIST = "dbo.GetBusinessPartnerDetails";
		private const string SP_CREATE_BUSINESSPARTNER = "dbo.CreateBusinessPartner";
		private const string SP_SAVE_BUSINESSPARTNER = "dbo.SaveBusinessPartner";
		private const string SP_CREATE_BUSINESSPARTNERDETAIL = "dbo.CreateBusinessPartnerDetail";
		private const string SP_GET_BUSINESSPARTNERDETAIL = "dbo.GetBusinessPartnerDetail";
		private const string SP_SAVE_BUSINESSPARTNERDETAIL = "dbo.SaveBusinessPartnerDetail";
	}
}