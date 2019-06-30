using Dapper;
using System.Collections.Generic;
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

		private const string SP_GET_BUSINESSPARTNER_LIST = "dbo.GetBusinessPartners";
	}
}