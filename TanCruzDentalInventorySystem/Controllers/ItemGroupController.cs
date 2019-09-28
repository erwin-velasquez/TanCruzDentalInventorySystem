using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Web.Mvc;
using TanCruzDentalInventorySystem.BusinessService.BusinessServiceInterface;
using TanCruzDentalInventorySystem.ViewModels;

namespace TanCruzDentalInventorySystem.Controllers
{
	[Authorize]
	public class ItemGroupController : Controller
	{
		private IItemGroupService _itemGroupService;

		public ItemGroupController(IItemGroupService itemGroupService)
		{
			_itemGroupService = itemGroupService;
		}

		[HttpGet]
		public async Task<ActionResult> Index()
		{
			var itemGroups = await _itemGroupService.GetItemGroupList();

			return View(itemGroups);
		}

		[HttpGet]
		public async Task<ActionResult> ItemGroupRecord(string itemGroupId)
		{
			var itemGroup = await _itemGroupService.GetItemGroup(itemGroupId);

			return View(itemGroup);
		}

		[Authorize(Roles = "Editor")]
		public async Task<ActionResult> CreateItemGroup()
		{
			var itemGroupId = await _itemGroupService.CreateItemGroup(User.Identity.GetUserId());
			var itemGroupForm = await _itemGroupService.GetItemGroupForm(itemGroupId);

			return View(itemGroupForm);
		}

		[HttpPost]
		[Authorize(Roles = "Editor")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> SaveItemGroupRecord(ItemGroupFormViewModel itemGroupForm)
		{
			if (ModelState.IsValid)
			{
				itemGroupForm.ItemGroup.UserId = User.Identity.GetUserId();
				var recordsSaved = await _itemGroupService.SaveItemGroup(itemGroupForm.ItemGroup);

				if (recordsSaved >= 1)
				{
					var itemGroup = await _itemGroupService.GetItemGroup(itemGroupForm.ItemGroup.ItemGroupId);
					return View("ItemGroupRecord", itemGroup);
				}
				ModelState.AddModelError(string.Empty, "There was a problem and the ItemGroup was not saved.");
			}

			itemGroupForm = await _itemGroupService.GetItemGroupForm(itemGroupForm.ItemGroup.ItemGroupId);
			return View("EditItemGroupRecord", itemGroupForm);
		}

		[HttpGet]
		[Authorize(Roles = "Editor")]
		public async Task<ActionResult> EditItemGroupRecord(string itemGroupId)
		{
			var itemGroupForm = await _itemGroupService.GetItemGroupForm(itemGroupId);

			return View(itemGroupForm);
		}
	}
}