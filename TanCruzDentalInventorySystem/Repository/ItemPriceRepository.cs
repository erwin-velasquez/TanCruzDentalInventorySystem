using Dapper;
using System;
using System.Threading.Tasks;
using TanCruzDentalInventorySystem.Models;
using TanCruzDentalInventorySystem.Repository.DataServiceInterface;

namespace TanCruzDentalInventorySystem.Repository
{
	public class ItemPriceRepository : IItemPriceRepository
	{
		public IUnitOfWork UnitOfWork { get; set; }

		public async Task<int> SaveItemPrice(ItemPrice itemPrice)
		{
			DynamicParameters parameters = new DynamicParameters();
			parameters.Add("@ItemPriceId", itemPrice.ItemPriceId, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@ItemPriceName", itemPrice.ItemPriceName, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@ItemPriceDescription", itemPrice.ItemPriceDescription, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@ItemId", itemPrice.ItemId, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@IsDefault", itemPrice.IsDefault, System.Data.DbType.Boolean, System.Data.ParameterDirection.Input);
			parameters.Add("@Type", itemPrice.Type, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@PriceAmount", itemPrice.PriceAmount, System.Data.DbType.Decimal, System.Data.ParameterDirection.Input);
			parameters.Add("@BaseCurrencyId", itemPrice.BaseCurrency.CurrencyId, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@UserId", itemPrice.UserId, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@ChangedDate", new DateTime(itemPrice.VersionTimeStamp), System.Data.DbType.DateTime2, System.Data.ParameterDirection.Input);

			var rowsAffected = await UnitOfWork.Connection.ExecuteAsync(
				sql: SP_SAVE_ITEMPRICE,
				param: parameters,
				transaction: UnitOfWork.Transaction,
				commandType: System.Data.CommandType.StoredProcedure);

			return rowsAffected;
		}

		public async Task<string> CreateItemPrice(string userId)
		{
			DynamicParameters parameters = new DynamicParameters();
			parameters.Add("@UserId", userId, System.Data.DbType.String, System.Data.ParameterDirection.Input);

			var itemId = await UnitOfWork.Connection.ExecuteScalarAsync<string>(
				sql: SP_CREATE_ITEMPRICE,
				param: parameters,
				transaction: UnitOfWork.Transaction,
				commandType: System.Data.CommandType.StoredProcedure);

			return itemId;
		}

		private const string SP_SAVE_ITEMPRICE = "dbo.SaveItemPrice";
		private const string SP_CREATE_ITEMPRICE = "dbo.CreateItemPrice";
	}
}