using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using TanCruzDentalInventorySystem.BusinessService.BusinessServiceInterface;
using TanCruzDentalInventorySystem.Models;
using TanCruzDentalInventorySystem.Repository.DataServiceInterface;
using TanCruzDentalInventorySystem.ViewModels;

namespace TanCruzDentalInventorySystem.BusinessService
{
	public class ItemGroupService : IItemGroupService
	{
		private readonly IItemGroupRepository _itemGroupRepository;

		public ItemGroupService(IUnitOfWork unitOfWork,
			IItemGroupRepository itemGroupRepository)
		{
			_itemGroupRepository = itemGroupRepository;
			_itemGroupRepository.UnitOfWork = unitOfWork;
		}

		public async Task<string> CreateItemGroup(string userId)
		{
			string itemGroupId = await _itemGroupRepository.CreateItemGroup(userId);

			return itemGroupId;
		}

		public async Task<ItemGroupViewModel> GetItemGroup(string itemGroupId)
		{
			var itemGroup = await _itemGroupRepository.GetItemGroup(itemGroupId);
			return Mapper.Map<ItemGroupViewModel>(itemGroup);
		}

		public async Task<ItemGroupFormViewModel> GetItemGroupForm(string itemGroupId)
		{
			var itemGroupForm = new ItemGroupFormViewModel()
			{
				ItemGroup = Mapper.Map<ItemGroupViewModel>(await _itemGroupRepository.GetItemGroup(itemGroupId))
			};
			return itemGroupForm;
		}

		public async Task<IEnumerable<ItemGroupViewModel>> GetItemGroupList()
		{
			var itemGroupList = await _itemGroupRepository.GetItemGroupList();
			return Mapper.Map<List<ItemGroupViewModel>>(itemGroupList);
		}

		public async Task<int> SaveItemGroup(ItemGroupViewModel itemGroupViewModel)
		{
			var itemGroup = Mapper.Map<ItemGroup>(itemGroupViewModel);
			return await _itemGroupRepository.SaveItemGroup(itemGroup);
		}
	}
}