using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TanCruzDentalInventorySystem.Models;
using TanCruzDentalInventorySystem.Repository.DataServiceInterface;

namespace TanCruzDentalInventorySystem.Repository
{
	public class ItemRepository : IItemRepository
	{
		public IUnitOfWork UnitOfWork { get; set; }

		public async Task<IEnumerable<Item>> GetItemList()
		{
			var itemList = await UnitOfWork.Connection.QueryAsync<Item>(
				sql: SP_GET_ITEM_LIST,
				types:
					new[]
					{
						typeof(Item),
						typeof(ItemGroup),
						typeof(Currency),
						typeof(UnitOfMeasure),
						typeof(BusinessPartner),
						typeof(PurchasingUnitOfMeasure),
						typeof(InventoryUnitOfMeasure),
                        typeof(ItemPrice),
                        typeof(ItemPrice)
					},
				map:
					typeMap =>
					{
						if (!(typeMap[0] is Item itemUnit)) return null;

						itemUnit.ItemGroup = typeMap[1] as ItemGroup;
						itemUnit.Currency = typeMap[2] as Currency;
						itemUnit.UnitOfMeasure = typeMap[3] as UnitOfMeasure;
						itemUnit.BusinessPartner = typeMap[4] as BusinessPartner;
						itemUnit.PurchasingUnitOfMeasure = typeMap[5] as PurchasingUnitOfMeasure;
						itemUnit.InventoryUnitOfMeasure = typeMap[6] as InventoryUnitOfMeasure;
                        itemUnit.SalesOrderItemPrice = typeMap[7] as ItemPrice;
                        itemUnit.PurchaseOrderItemPrice = typeMap[8] as ItemPrice;

                        return itemUnit;
					},
				param: null,
				transaction: UnitOfWork.Transaction,
				commandType: System.Data.CommandType.StoredProcedure,
				splitOn: "ItemGroupId, CurrencyId, UnitOfMeasureId, BusinessPartnerId, PurchasingUnitOfMeasureId, InventoryUnitOfMeasureId, ItemPriceId, ItemPriceId");

			return itemList;
		}

		public async Task<Item> GetItem(string itemId)
		{
			var parameters = new DynamicParameters();
			parameters.Add("@ItemId", itemId, System.Data.DbType.String, System.Data.ParameterDirection.Input);

			var item = await UnitOfWork.Connection.QueryAsync<Item>(
				sql: SP_GET_ITEM,
				types:
					new[]
					{
						typeof(Item),
						typeof(ItemGroup),
						typeof(Currency),
						typeof(UnitOfMeasure),
						typeof(BusinessPartner),
						typeof(PurchasingUnitOfMeasure),
						typeof(InventoryUnitOfMeasure)
					},
				map:
					typeMap =>
					{
						if (!(typeMap[0] is Item itemUnit)) return null;

						itemUnit.ItemGroup = typeMap[1] as ItemGroup;
						itemUnit.Currency = typeMap[2] as Currency;
						itemUnit.UnitOfMeasure = typeMap[3] as UnitOfMeasure;
						itemUnit.BusinessPartner = typeMap[4] as BusinessPartner;
						itemUnit.PurchasingUnitOfMeasure = typeMap[5] as PurchasingUnitOfMeasure;
						itemUnit.InventoryUnitOfMeasure = typeMap[6] as InventoryUnitOfMeasure;

                        return itemUnit;
					},
				param: parameters,
				transaction: UnitOfWork.Transaction,
				commandType: System.Data.CommandType.StoredProcedure,
				splitOn: "ItemGroupId, CurrencyId, UnitOfMeasureId, BusinessPartnerId, PurchasingUnitOfMeasureId, InventoryUnitOfMeasureId");

			var versionedItem = item.AsList().SingleOrDefault();
			versionedItem.VersionTimeStamp = versionedItem.ChangedDate.Value.Ticks;
			return versionedItem;
		}

		public async Task<IEnumerable<UnitOfMeasure>> GetUnitOfMeasureList()
		{

			var unitOfMeasures = await UnitOfWork.Connection.QueryAsync<UnitOfMeasure>(
				sql: SP_GET_UNITOFMEASURE_LIST,
				param: null,
				transaction: UnitOfWork.Transaction,
				commandType: System.Data.CommandType.StoredProcedure);

			return unitOfMeasures;
		}

		public async Task<int> SaveItem(Item item)
		{
			DynamicParameters parameters = new DynamicParameters();
			parameters.Add("@ItemId", item.ItemId, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@ItemBarCode", item.ItemBarCode, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@ItemName", item.ItemName, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@ItemGroupId", item.ItemGroup.ItemGroupId, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@IsActive", item.IsActive, System.Data.DbType.Boolean, System.Data.ParameterDirection.Input);
			parameters.Add("@CurrencyId", item.Currency.CurrencyId, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@UnitOfMeasureId", item.UnitOfMeasure.UnitOfMeasureId, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@BusinessPartnerId", item.BusinessPartner.BusinessPartnerId, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@PurchasingUnitOfMeasureId", item.PurchasingUnitOfMeasure.PurchasingUnitOfMeasureId, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@ItemsPerUnitOfMeasure", item.ItemsPerUnitOfMeasure, System.Data.DbType.Int64, System.Data.ParameterDirection.Input);
			parameters.Add("@PurchasingRemarks", item.PurchasingRemarks, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@InventoryUnitOfMeasureId", item.InventoryUnitOfMeasure.InventoryUnitOfMeasureId, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@MinimumInventoryRequired", item.MinimumInventoryRequired, System.Data.DbType.Int64, System.Data.ParameterDirection.Input);
			parameters.Add("@UserId", item.UserId, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            parameters.Add("@ChangedDate", new DateTime(item.VersionTimeStamp), System.Data.DbType.DateTime2, System.Data.ParameterDirection.Input);

            var rowsAffected = await UnitOfWork.Connection.ExecuteAsync(
				sql: SP_SAVE_ITEM,
				param: parameters,
				transaction: UnitOfWork.Transaction,
				commandType: System.Data.CommandType.StoredProcedure);

			return rowsAffected;
		}

		public async Task<string> CreateItem(string userId)
		{
			DynamicParameters parameters = new DynamicParameters();
			parameters.Add("@UserId", userId, System.Data.DbType.String, System.Data.ParameterDirection.Input);

			var itemId = await UnitOfWork.Connection.ExecuteScalarAsync<string>(
				sql: SP_CREATE_ITEM,
				param: parameters,
				transaction: UnitOfWork.Transaction,
				commandType: System.Data.CommandType.StoredProcedure);

			return itemId;
		}

		private const string SP_GET_ITEM_LIST = "dbo.GetItems";
		private const string SP_GET_ITEM = "dbo.GetItem";
		private const string SP_GET_ITEMGROUP_LIST = "dbo.GetItemGroups";
		private const string SP_SAVE_ITEM = "dbo.SaveItem";
		private const string SP_GET_UNITOFMEASURE_LIST = "dbo.GetUnitOfMeasures";
		private const string SP_CREATE_ITEM = "dbo.CreateItem";
	}
}