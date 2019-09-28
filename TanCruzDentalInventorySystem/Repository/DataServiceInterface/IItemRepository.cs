using System.Collections.Generic;
using System.Threading.Tasks;
using TanCruzDentalInventorySystem.Models;

namespace TanCruzDentalInventorySystem.Repository.DataServiceInterface
{
	public interface IItemRepository
	{
		IUnitOfWork UnitOfWork { get; set; }
		Task<IEnumerable<Item>> GetItemList();
		Task<Item> GetItem(string itemId);
		Task<IEnumerable<UnitOfMeasure>> GetUnitOfMeasureList();
		Task<int> SaveItem(Item item);
		Task<string> CreateItem(string userId);
	}
}
