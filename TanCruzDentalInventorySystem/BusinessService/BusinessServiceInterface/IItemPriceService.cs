using System.Collections.Generic;
using System.Threading.Tasks;
using TanCruzDentalInventorySystem.ViewModels;

namespace TanCruzDentalInventorySystem.BusinessService.BusinessServiceInterface
{
	public interface IItemPriceService
	{
		Task<IEnumerable<ItemPriceViewModel>> GetItemPriceList(string itemId);
		Task<ItemPriceFormViewModel> GetItemPriceForm(string itemPriceId);
		Task<string> CreateItemPrice(string itemId, string userId);
		Task<int> SaveItemPrice(ItemPriceViewModel itemPriceViewModel);
		Task<IEnumerable<ItemDefaultPriceViewModel>> GetItemsDefaultPrices();
	}
}