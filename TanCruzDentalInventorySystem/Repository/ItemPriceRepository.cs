using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
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

		public async Task<string> CreateItemPrice(string itemId, string userId)
		{
			DynamicParameters parameters = new DynamicParameters();
			parameters.Add("@ItemId", itemId, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@UserId", userId, System.Data.DbType.String, System.Data.ParameterDirection.Input);

			var itemPriceId = await UnitOfWork.Connection.ExecuteScalarAsync<string>(
				sql: SP_CREATE_ITEMPRICE,
				param: parameters,
				transaction: UnitOfWork.Transaction,
				commandType: System.Data.CommandType.StoredProcedure);

			return itemPriceId;
		}

		public async Task<ItemPrice> GetItemPrice(string itemPriceId)
		{
			var parameters = new DynamicParameters();
			parameters.Add("@ItemPriceId", itemPriceId, System.Data.DbType.String, System.Data.ParameterDirection.Input);

			var itemPrice = await UnitOfWork.Connection.QueryAsync<ItemPrice>(
				sql: SP_GET_ITEMPRICE,
				types:
					new[]
					{
						typeof(ItemPrice),
						typeof(Currency),
					},
				map:
					typeMap =>
					{
						if (!(typeMap[0] is ItemPrice itemUnit)) return null;

						itemUnit.BaseCurrency = typeMap[1] as Currency;

						return itemUnit;
					},
				param: parameters,
				transaction: UnitOfWork.Transaction,
				commandType: System.Data.CommandType.StoredProcedure,
				splitOn: "CurrencyId");

			var versionedItem = itemPrice.AsList().SingleOrDefault();
			versionedItem.VersionTimeStamp = versionedItem.ChangedDate.Value.Ticks;
			return versionedItem;
		}

		public async Task<IEnumerable<ItemPrice>> GetItemPriceList(string itemId)
		{
			var parameters = new DynamicParameters();
			parameters.Add("@ItemId", itemId, System.Data.DbType.String, System.Data.ParameterDirection.Input);

			var itemPriceList = await UnitOfWork.Connection.QueryAsync<ItemPrice>(
				sql: SP_GET_ITEMPRICE_LIST,
				types:
					new[]
					{
						typeof(ItemPrice),
						typeof(Currency)
					},
				map:
					typeMap =>
					{
						if (!(typeMap[0] is ItemPrice itemPriceUnit)) return null;

						itemPriceUnit.BaseCurrency = typeMap[1] as Currency;

						return itemPriceUnit;
					},
				param: parameters,
				transaction: UnitOfWork.Transaction,
				commandType: System.Data.CommandType.StoredProcedure,
				splitOn: "CurrencyId");

			return itemPriceList;
		}

		private const string SP_GET_ITEMPRICE_LIST = "dbo.GetItemPrices";
		private const string SP_GET_ITEMPRICE = "dbo.GetItemPrice";
		private const string SP_SAVE_ITEMPRICE = "dbo.SaveItemPrice";
		private const string SP_CREATE_ITEMPRICE = "dbo.CreateItemPrice";
	}
}