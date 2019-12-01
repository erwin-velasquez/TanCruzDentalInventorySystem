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

		public async Task<string> CreateItemPrice(string itemId, string userId)
		{
			string itemPriceId = await _itemPriceRepository.CreateItemPrice(itemId, userId);

			return itemPriceId;
		}

		public async Task<ItemPriceFormViewModel> GetItemPriceForm(string itemPriceId)
		{
			var itemPriceForm = new ItemPriceFormViewModel()
			{
				ItemPrice = Mapper.Map<ItemPriceViewModel>(await _itemPriceRepository.GetItemPrice(itemPriceId)),
				Currencies = Mapper.Map<IEnumerable<CurrencyViewModel>>(await _currencyRepository.GetCurrencyList()),
				ItemPriceTypes = new List<string>() { "SO", "PO" }
			};
			return itemPriceForm;
		}

		public async Task<IEnumerable<ItemPriceViewModel>> GetItemPriceList(string itemId)
		{
			var itemPriceList = await _itemPriceRepository.GetItemPriceList(itemId);
			return Mapper.Map<List<ItemPriceViewModel>>(itemPriceList);
		}

		public async Task<IEnumerable<ItemDefaultPriceViewModel>> GetItemsDefaultPrices()
		{
			var itemPriceList = Mapper.Map<List<ItemPriceViewModel>>(await _itemPriceRepository.GetItemsDefaultPrices());

			List<ItemDefaultPriceViewModel> itemDefaultPriceList = new List<ItemDefaultPriceViewModel>();
			foreach (var itemPrice in itemPriceList)
			{
				if (itemDefaultPriceList.Any(i => i.Item.ItemId == itemPrice.Item.ItemId))
					continue;

				var itemDefaultPrice = new ItemDefaultPriceViewModel();
				itemDefaultPrice.Item = itemPrice.Item;
				itemDefaultPrice.PODefaultPrice = itemPriceList.FirstOrDefault(i => i.Item.ItemId == itemPrice.Item.ItemId && i.Type == "PO");
				itemDefaultPrice.SODefaultPrice = itemPriceList.FirstOrDefault(i => i.Item.ItemId == itemPrice.Item.ItemId && i.Type == "SO");

				itemDefaultPriceList.Add(itemDefaultPrice);
			}

			return itemDefaultPriceList;
		}

		public async Task<int> SaveItemPrice(ItemPriceViewModel itemPriceViewModel)
		{
			var itemPrice = Mapper.Map<ItemPrice>(itemPriceViewModel);
			return await _itemPriceRepository.SaveItemPrice(itemPrice);
		}
	}
}