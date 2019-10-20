using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TanCruzDentalInventorySystem.Models;
using TanCruzDentalInventorySystem.Repository.DataServiceInterface;

namespace TanCruzDentalInventorySystem.Repository
{
	public class ItemGroupRepository : IItemGroupRepository
	{
		public IUnitOfWork UnitOfWork { get; set; }

		public async Task<string> CreateItemGroup(string userId)
		{
			DynamicParameters parameters = new DynamicParameters();
			parameters.Add("@UserId", userId, System.Data.DbType.String, System.Data.ParameterDirection.Input);

			var itemId = await UnitOfWork.Connection.ExecuteScalarAsync<string>(
				sql: SP_CREATE_ITEMGROUP,
				param: parameters,
				transaction: UnitOfWork.Transaction,
				commandType: System.Data.CommandType.StoredProcedure);

			return itemId;
		}

		public async Task<ItemGroup> GetItemGroup(string itemGroupId)
		{
			var parameters = new DynamicParameters();
			parameters.Add("@ItemGroupId", itemGroupId, System.Data.DbType.String, System.Data.ParameterDirection.Input);

			var itemGroup = await UnitOfWork.Connection.QueryAsync<ItemGroup>(
				sql: SP_GET_ITEMGROUP,
				param: parameters,
				transaction: UnitOfWork.Transaction,
				commandType: System.Data.CommandType.StoredProcedure);

			var versionedItemGroup = itemGroup.AsList().SingleOrDefault();
			versionedItemGroup.VersionTimeStamp = versionedItemGroup.ChangedDate.Value.Ticks;
			return versionedItemGroup;
		}

		public async Task<IEnumerable<ItemGroup>> GetItemGroupList()
		{
            var itemGroupList = await UnitOfWork.Connection.QueryAsync<ItemGroup>(
            sql: SP_GET_ITEMGROUP_LIST,
            param: null,
            transaction: UnitOfWork.Transaction,
            commandType: System.Data.CommandType.StoredProcedure);

            return itemGroupList;

        }

		public async Task<int> SaveItemGroup(ItemGroup itemGroup)
		{
			DynamicParameters parameters = new DynamicParameters();
			parameters.Add("@ItemGroupId", itemGroup.ItemGroupId, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@ItemGroupName", itemGroup.ItemGroupName, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@ItemGroupDescription", itemGroup.ItemGroupDescription, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@UserId", itemGroup.UserId, System.Data.DbType.String, System.Data.ParameterDirection.Input);
			parameters.Add("@ChangedDate", new DateTime(itemGroup.VersionTimeStamp), System.Data.DbType.DateTime2, System.Data.ParameterDirection.Input);

			var rowsAffected = await UnitOfWork.Connection.ExecuteAsync(
				sql: SP_SAVE_ITEMGROUP,
				param: parameters,
				transaction: UnitOfWork.Transaction,
				commandType: System.Data.CommandType.StoredProcedure);

			return rowsAffected;
		}

		private const string SP_GET_ITEMGROUP = "dbo.GetItemGroup";
		private const string SP_SAVE_ITEMGROUP = "dbo.SaveItemGroup";
		private const string SP_CREATE_ITEMGROUP = "dbo.CreateItemGroup";
        private const string SP_GET_ITEMGROUP_LIST = "dbo.GetItemGroups";

    }
}