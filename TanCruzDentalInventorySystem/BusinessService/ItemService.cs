using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TanCruzDentalInventorySystem.BusinessService.BusinessServiceInterface;
using TanCruzDentalInventorySystem.Repository.DataServiceInterface;
using TanCruzDentalInventorySystem.ViewModel;

namespace TanCruzDentalInventorySystem.BusinessService
{
	public class ItemService : IItemService
	{
		private readonly IItemRepository _itemRepository;

		public ItemService(IUnitOfWork unitOfWork, IItemRepository itemRepository)
		{
			_itemRepository = itemRepository;
			_itemRepository.UnitOfWork = unitOfWork;
		}

		public async Task<ItemViewModel> GetItem(string itemId)
		{
			var item = await _itemRepository.GetItem(itemId);
			return Mapper.Map<ItemViewModel>(item);
		}

		public async Task<IEnumerable<ItemViewModel>> GetItemList()
		{
			var itemList = await _itemRepository.GetItemList();
			return Mapper.Map<List<ItemViewModel>>(itemList);
		}

		public async Task<ItemFormViewModel> GetItemForm(string itemId)
		{

			var baseUnitOfMeasures = Mapper.Map<IEnumerable<UnitOfMeasureViewModel>>(await _itemRepository.GetUnitOfMeasureList());

			var itemForm = new ItemFormViewModel()
			{
				Item = Mapper.Map<ItemViewModel>(await _itemRepository.GetItem(itemId)),
				ItemGroups = Mapper.Map<IEnumerable<ItemGroupViewModel>>(await _itemRepository.GetItemGroupList()),
				Currencies = Mapper.Map<IEnumerable<CurrencyViewModel>>(await _itemRepository.GetCurrencyList()),
				UnitOfMeasures = baseUnitOfMeasures,
				BusinessPartners = Mapper.Map<IEnumerable<BusinessPartnerViewModel>>(await _itemRepository.GetBusinessPartnerList()),
				PurchasingUnitOfMeasures = baseUnitOfMeasures
					.Select(uom => new PurchasingUnitOfMeasureViewModel()
					{
						PurchasingUnitOfMeasureId = uom.UnitOfMeasureId,
						PurchasingUnitOfMeasureDescription = uom.UnitOfMeasureDescription
					}).ToList(),
				InventoryUnitOfMeasures = baseUnitOfMeasures
					.Select(uom => new InventoryUnitOfMeasureViewModel()
					{
						InventoryUnitOfMeasureId = uom.UnitOfMeasureId,
						InventoryUnitOfMeasureDescription = uom.UnitOfMeasureDescription
					}).ToList()
			};
			return itemForm;
		}


		//public UserProfileViewModel Login(LoginViewModel loginInfo)
		//{
		//    _accountRepository.UnitOfWork = _unitOfWork;
		//    // null;

		//    return Mapper.Map<UserProfileViewModel>(_accountRepository.Login(loginInfo.UserName, loginInfo.Password));

		//    //_unitOfWork.Begin();
		//    //try
		//    //{
		//    //    _accountRepository.Login(loginInfo.UserName, loginInfo.Password);

		//    //    _unitOfWork.Commit();
		//    //}
		//    //catch (Exception ex)
		//    //{
		//    //    _unitOfWork.Rollback();
		//    //    throw;
		//    //}

		//    //return false;
		//}
	}
}