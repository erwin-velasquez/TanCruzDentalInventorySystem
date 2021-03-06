﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TanCruzDentalInventorySystem.ViewModels;

namespace TanCruzDentalInventorySystem.BusinessService.BusinessServiceInterface
{
		public interface IItemService
		{
				Task<IEnumerable<ItemViewModel>> GetItemList();
				Task<IEnumerable<ItemSearchFormViewModel>> GetItemSearchModalList();
				Task<ItemViewModel> GetItem(string itemId);
				Task<ItemSearchFormViewModel> GetItemWithPrice(string itemId);
				Task<ItemFormViewModel> GetItemForm(string itemId);
				Task<string> CreateItem(string userId);
				Task<int> SaveItem(ItemViewModel itemViewModel);
		}
}
