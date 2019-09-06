using System.Collections.Generic;
using System.Threading.Tasks;
using TanCruzDentalInventorySystem.ViewModels;

namespace TanCruzDentalInventorySystem.BusinessService.BusinessServiceInterface
{
	public interface IItemService
	{
		Task<IEnumerable<ItemViewModel>> GetItemList();
		Task<ItemViewModel> GetItem(string itemId);
		Task<ItemFormViewModel> GetItemForm(string itemId);
		Task<string> CreateItem(string userId);
		Task<int> SaveItem(ItemViewModel itemViewModel);
	}
}
