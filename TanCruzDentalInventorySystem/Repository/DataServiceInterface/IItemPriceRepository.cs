using System.Collections.Generic;
using System.Threading.Tasks;
using TanCruzDentalInventorySystem.Models;

namespace TanCruzDentalInventorySystem.Repository.DataServiceInterface
{
	public interface IItemPriceRepository
	{
		IUnitOfWork UnitOfWork { get; set; }

		Task<IEnumerable<ItemPrice>> GetItemPriceList(string itemId);
		Task<ItemPrice> GetItemPrice(string itemPriceId);
		Task<int> SaveItemPrice(ItemPrice itemPrice);
		Task<string> CreateItemPrice(string itemId, string userId);
		Task<IEnumerable<ItemPrice>> GetItemsDefaultPrices();
	}
}
