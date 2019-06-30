using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using TanCruzDentalInventorySystem.Models;

namespace TanCruzDentalInventorySystem.Repository.DataServiceInterface
{
	public class CurrencyRepository : ICurrencyRepository
	{
		public IUnitOfWork UnitOfWork { get; set; }
		public async Task<IEnumerable<Currency>> GetCurrencyList()
		{

			var currencies = await UnitOfWork.Connection.QueryAsync<Currency>(
				sql: SP_GET_CURRENCY_LIST,
				param: null,
				transaction: UnitOfWork.Transaction,
				commandType: System.Data.CommandType.StoredProcedure);

			return currencies;
		}

		private const string SP_GET_CURRENCY_LIST = "dbo.GetCurrencies";
	}
}