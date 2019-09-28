using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TanCruzDentalInventorySystem.BusinessService.BusinessServiceInterface;
using TanCruzDentalInventorySystem.Models;
using TanCruzDentalInventorySystem.Repository.DataServiceInterface;
using TanCruzDentalInventorySystem.ViewModels;

namespace TanCruzDentalInventorySystem.BusinessService
{
	public class ItemService : IItemService
	{
		private readonly IItemRepository _itemRepository;
		private readonly ICurrencyRepository _currencyRepository;
		private readonly IBusinessPartnerRepository _businessPartnerRepository;
		private readonly IItemGroupRepository _itemGroupRepository;

		public ItemService(IUnitOfWork unitOfWork,
			IItemRepository itemRepository,
			ICurrencyRepository currencyRepository,
			IBusinessPartnerRepository businessPartnerRepository,
			IItemGroupRepository itemGroupRepository)
		{
			_itemRepository = itemRepository;
			_itemRepository.UnitOfWork = unitOfWork;

			_currencyRepository = currencyRepository;
			_currencyRepository.UnitOfWork = unitOfWork;

			_businessPartnerRepository = businessPartnerRepository;
			_businessPartnerRepository.UnitOfWork = unitOfWork;

			_itemGroupRepository = itemGroupRepository;
			_itemGroupRepository.UnitOfWork = unitOfWork;
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
				ItemGroups = Mapper.Map<IEnumerable<ItemGroupViewModel>>(await _itemGroupRepository.GetItemGroupList()),
				Currencies = Mapper.Map<IEnumerable<CurrencyViewModel>>(await _currencyRepository.GetCurrencyList()),
				UnitOfMeasures = baseUnitOfMeasures,
				BusinessPartners = Mapper.Map<IEnumerable<BusinessPartnerViewModel>>(await _businessPartnerRepository.GetBusinessPartnerList()),
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

		public async Task<string> CreateItem(string userId)
		{
			string itemId = await _itemRepository.CreateItem(userId);

			return itemId;
		}

		public async Task<int> SaveItem(ItemViewModel itemViewModel)
		{
			var item = Mapper.Map<Item>(itemViewModel);
			return await _itemRepository.SaveItem(item);
		}

		// DO NOT ERASE
		// DO NOT ERASE
		// DO NOT ERASE
		// DO NOT ERASE
		// DO NOT ERASE
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