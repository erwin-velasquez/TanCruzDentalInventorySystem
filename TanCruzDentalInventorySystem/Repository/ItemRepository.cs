using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
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
						typeof(ItemPrice),
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
						itemUnit.ItemPrice = typeMap[2] as ItemPrice;
						itemUnit.Currency = typeMap[3] as Currency;
						itemUnit.UnitOfMeasure = typeMap[4] as UnitOfMeasure;
						itemUnit.BusinessPartner = typeMap[5] as BusinessPartner;
						itemUnit.PurchasingUnitOfMeasure = typeMap[6] as PurchasingUnitOfMeasure;
						itemUnit.InventoryUnitOfMeasure = typeMap[7] as InventoryUnitOfMeasure;

						return itemUnit;
					},
				param: null,
				transaction: UnitOfWork.Transaction,
				commandType: System.Data.CommandType.StoredProcedure,
				splitOn: "ItemGroupId, ItemPriceId, CurrencyId, UnitOfMeasureId, BusinessPartnerId, PurchasingUnitOfMeasureId, InventoryUnitOfMeasureId");

			return itemList;
		}

		public async Task<Item> GetItem(string itemId)
		{
			var parameters = new DynamicParameters();
			parameters.Add("ITEM_ID", itemId, System.Data.DbType.String, System.Data.ParameterDirection.Input);

			var item = await UnitOfWork.Connection.QueryAsync<Item>(
				sql: SP_GET_ITEM,
				types:
					new[]
					{
						typeof(Item),
						typeof(ItemGroup),
						typeof(ItemPrice),
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
						itemUnit.ItemPrice = typeMap[2] as ItemPrice;
						itemUnit.Currency = typeMap[3] as Currency;
						itemUnit.UnitOfMeasure = typeMap[4] as UnitOfMeasure;
						itemUnit.BusinessPartner = typeMap[5] as BusinessPartner;
						itemUnit.PurchasingUnitOfMeasure = typeMap[6] as PurchasingUnitOfMeasure;
						itemUnit.InventoryUnitOfMeasure = typeMap[7] as InventoryUnitOfMeasure;

						return itemUnit;
					},
				param: parameters,
				transaction: UnitOfWork.Transaction,
				commandType: System.Data.CommandType.StoredProcedure,
				splitOn: "ItemGroupId, ItemPriceId, CurrencyId, UnitOfMeasureId, BusinessPartnerId, PurchasingUnitOfMeasureId, InventoryUnitOfMeasureId");

			//var item = await UnitOfWork.Connection.QueryAsync<Item, ItemGroup, ItemPrice, Currency, UnitOfMeasure, BusinessPartner, Item>(
			//	sql: SP_GET_ITEM,
			//	(itemUnit, itemGroup, itemPrice, currency, unitOfMeasure, businessPartner) =>
			//	{
			//		itemUnit.ItemGroup = itemGroup;
			//		itemUnit.ItemPrice = itemPrice;
			//		itemUnit.Currency = currency;
			//		itemUnit.UnitOfMeasure = unitOfMeasure;
			//		itemUnit.BusinessPartner = businessPartner;
			//		return itemUnit;
			//	},
			//	param: parameters,
			//	transaction: UnitOfWork.Transaction,
			//	commandType: System.Data.CommandType.StoredProcedure,
			//	splitOn: "ItemGroupId,ItemPriceId,CurrencyId,UnitOfMeasureId,BusinessPartnerId");

			return item.AsList().SingleOrDefault();
		}

		public async Task<IEnumerable<ItemGroup>> GetItemGroupList()
		{

			var itemGroups = await UnitOfWork.Connection.QueryAsync<ItemGroup>(
				sql: SP_GET_ITEMGROUP_LIST,
				param: null,
				transaction: UnitOfWork.Transaction,
				commandType: System.Data.CommandType.Text);

			return itemGroups;
		}

		public async Task<IEnumerable<Currency>> GetCurrencyList()
		{

			var currencies = await UnitOfWork.Connection.QueryAsync<Currency>(
				sql: SP_GET_CURRENCY_LIST,
				param: null,
				transaction: UnitOfWork.Transaction,
				commandType: System.Data.CommandType.Text);

			return currencies;
		}

		public async Task<IEnumerable<UnitOfMeasure>> GetUnitOfMeasureList()
		{

			var unitOfMeasures = await UnitOfWork.Connection.QueryAsync<UnitOfMeasure>(
				sql: SP_GET_UNITOFMEASURE_LIST,
				param: null,
				transaction: UnitOfWork.Transaction,
				commandType: System.Data.CommandType.Text);

			return unitOfMeasures;
		}

		public async Task<IEnumerable<BusinessPartner>> GetBusinessPartnerList()
		{

			var businessPartners = await UnitOfWork.Connection.QueryAsync<BusinessPartner>(
				sql: SP_GET_BUSINESSPARTNER_LIST,
				param: null,
				transaction: UnitOfWork.Transaction,
				commandType: System.Data.CommandType.Text);

			return businessPartners;
		}

		//public UserProfile Login(string userName, string password)
		//      {
		//          var parameters = new DynamicParameters();
		//          parameters.Add("@USER_NAME", userName, System.Data.DbType.String, System.Data.ParameterDirection.Input);

		//          var userProfile = UnitOfWork.Connection.QuerySingleOrDefault<UserProfile>(
		//              sql: GET_USERPROFILE,
		//              param: parameters,
		//              transaction: UnitOfWork.Transaction,
		//              commandType: System.Data.CommandType.Text);

		//          return userProfile;
		//      }


		private const string SP_GET_ITEM_LIST = "dbo.GetItems";
		private const string SP_GET_ITEM = "dbo.GetItem";
		private const string SP_GET_ITEMGROUP_LIST = "dbo.GetItemGroups";
		private const string SP_GET_CURRENCY_LIST = "dbo.GetCurrencies";
		private const string SP_GET_UNITOFMEASURE_LIST = "dbo.GetUnitOfMeasures";
		private const string SP_GET_BUSINESSPARTNER_LIST = "dbo.GetBusinessPartners";
	}
}