using System.Threading.Tasks;
using System.Web.Mvc;
using TanCruzDentalInventorySystem.BusinessService.BusinessServiceInterface;

namespace TanCruzDentalInventorySystem.Controllers
{
	public class ItemController : Controller
	{
		private IItemService _itemService;

		public ItemController(IItemService itemService)
		{
			_itemService = itemService;
		}

		public async Task<ActionResult> ItemHome()
		{
			var items = await _itemService.GetItemList();

			return View(items);
		}

		public async Task<ActionResult> ItemRecord(string itemId)
		{
			var item = await _itemService.GetItem(itemId);

			return View(item);
		}

		[Authorize(Roles = "Editor")]
		public async Task<ActionResult> ItemRecordEdit(string itemId)
		{
			var itemForm = await _itemService.GetItemForm(itemId);

			return View(itemForm);
		}
	}
}