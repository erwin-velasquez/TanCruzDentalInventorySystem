using System.Collections.Generic;
using System.Threading.Tasks;
using TanCruzDentalInventorySystem.ViewModels;

namespace TanCruzDentalInventorySystem.BusinessService.BusinessServiceInterface
{
	public interface IItemGroupService
	{
		Task<IEnumerable<ItemGroupViewModel>> GetItemGroupList();
		Task<ItemGroupViewModel> GetItemGroup(string itemGroupId);
		Task<ItemGroupFormViewModel> GetItemGroupForm(string itemGroupId);
		Task<string> CreateItemGroup(string userId);
		Task<int> SaveItemGroup(ItemGroupViewModel itemGroupViewModel);
	}
}
