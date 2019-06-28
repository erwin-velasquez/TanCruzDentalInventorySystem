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
		Task<IEnumerable<ItemGroup>> GetItemGroupList();
		Task<IEnumerable<Currency>> GetCurrencyList();
		Task<IEnumerable<UnitOfMeasure>> GetUnitOfMeasureList();
		Task<IEnumerable<BusinessPartner>> GetBusinessPartnerList();
		Task<int> SaveItem(Item item);
	}
}
