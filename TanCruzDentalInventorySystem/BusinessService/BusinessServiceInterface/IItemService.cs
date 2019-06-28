using System.Collections.Generic;
using System.Threading.Tasks;
using TanCruzDentalInventorySystem.ViewModel;

namespace TanCruzDentalInventorySystem.BusinessService.BusinessServiceInterface
{
	public interface IItemService
	{
		Task<IEnumerable<ItemViewModel>> GetItemList();
		Task<ItemViewModel> GetItem(string itemId);
		Task<ItemFormViewModel> GetItemForm(string itemId);
		Task<int> SaveItem(ItemViewModel itemViewModel);
	}
}
