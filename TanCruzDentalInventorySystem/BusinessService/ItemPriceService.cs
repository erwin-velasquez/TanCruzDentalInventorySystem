using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using TanCruzDentalInventorySystem.BusinessService.BusinessServiceInterface;
using TanCruzDentalInventorySystem.Models;
using TanCruzDentalInventorySystem.Repository.DataServiceInterface;
using TanCruzDentalInventorySystem.ViewModels;

namespace TanCruzDentalInventorySystem.BusinessService
{
	public class ItemPriceService : IItemPriceService
	{
		private readonly IItemPriceRepository _itemPriceRepository;
		private readonly ICurrencyRepository _currencyRepository;

		public ItemPriceService(IUnitOfWork unitOfWork,
			IItemPriceRepository itemPriceRepository,
			ICurrencyRepository currencyRepository)
		{
			_itemPriceRepository = itemPriceRepository;
			_itemPriceRepository.UnitOfWork = unitOfWork;

			_currencyRepository = currencyRepository;
			_currencyRepository.UnitOfWork = unitOfWork;
		}

		public async Task<string> CreateItemPrice(string userId)
		{
			string itemId = await _itemPriceRepository.CreateItemPrice(userId);

			return itemId;
		}

		public async Task<ItemPriceFormViewModel> GetItemPriceForm(string itemPriceId)
		{
			var itemPriceForm = new ItemPriceFormViewModel()
			{
				ItemPrice = Mapper.Map<ItemPriceViewModel>(await _itemPriceRepository.GetItemPrice(itemPriceId)),
				Currencies = Mapper.Map<IEnumerable<CurrencyViewModel>>(await _currencyRepository.GetCurrencyList()),
			};
			return itemPriceForm;
		}

		public async Task<IEnumerable<ItemPriceViewModel>> GetItemPriceList(string itemId)
		{
			var itemPriceList = await _itemPriceRepository.GetItemPriceList(itemId);
			return Mapper.Map<List<ItemPriceViewModel>>(itemPriceList);
		}

		public async Task<int> SaveItemPrice(ItemPriceViewModel itemPriceViewModel)
		{
			var item = Mapper.Map<ItemPrice>(itemPriceViewModel);
			return await _itemPriceRepository.SaveItemPrice(item);
		}
	}
}