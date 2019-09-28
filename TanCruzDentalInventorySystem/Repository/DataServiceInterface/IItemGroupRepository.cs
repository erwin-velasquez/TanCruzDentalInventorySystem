using System.Collections.Generic;
using System.Threading.Tasks;
using TanCruzDentalInventorySystem.Models;

namespace TanCruzDentalInventorySystem.Repository.DataServiceInterface
{
	public interface IItemGroupRepository
	{
		IUnitOfWork UnitOfWork { get; set; }
		Task<IEnumerable<ItemGroup>> GetItemGroupList();
		Task<ItemGroup> GetItemGroup(string itemGroupId);
		Task<int> SaveItemGroup(ItemGroup itemGroup);
		Task<string> CreateItemGroup(string userId);
	}
}