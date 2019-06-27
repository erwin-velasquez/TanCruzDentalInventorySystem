using AutoMapper;
using System.Threading.Tasks;
using System.Web.Mvc;
using TanCruzDentalInventorySystem.BusinessService.BusinessServiceInterface;
using TanCruzDentalInventorySystem.ViewModel;

namespace TanCruzDentalInventorySystem.Controllers
{
	//	[Authorize]
	public class ItemController : Controller
	{
		private IItemService _itemService;

		public ItemController(IItemService itemService)
		{
			_itemService = itemService;
		}

		public async Task<ActionResult> Index()
		{
			var items = await _itemService.GetItemList();

			return View(items);
		}

		public async Task<ActionResult> ItemRecord(string itemId)
		{
			var item = await _itemService.GetItem(itemId);

			return View(item);
		}

		//		[Authorize(Roles = "Editor")]
		public async Task<ActionResult> EditItemRecord(string itemId)
		{
			var itemForm = await _itemService.GetItemForm(itemId);

			return View(itemForm);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> SaveItemRecord(ItemFormViewModel itemForm)
		{
			if (ModelState.IsValid)
			{
				var updatedItem = await _itemService.GetItem(itemForm.Item.ItemId);

				ItemViewModel vm = Mapper.Map<ItemViewModel>(updatedItem);

				// TODO: Save changes
				ModelState.AddModelError(string.Empty, "");
			}

			return null;
		}
	}
}