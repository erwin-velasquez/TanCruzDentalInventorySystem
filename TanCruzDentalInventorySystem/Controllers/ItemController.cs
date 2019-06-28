﻿using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Web.Mvc;
using TanCruzDentalInventorySystem.BusinessService.BusinessServiceInterface;
using TanCruzDentalInventorySystem.ViewModel;

namespace TanCruzDentalInventorySystem.Controllers
{
	[Authorize]
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

		[Authorize(Roles = "Editor")]
		public async Task<ActionResult> EditItemRecord(string itemId)
		{
			var itemForm = await _itemService.GetItemForm(itemId);

			return View(itemForm);
		}

		[HttpPost]
		[Authorize(Roles = "Editor")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> SaveItemRecord(ItemFormViewModel itemForm)
		{
			if (ModelState.IsValid)
			{
				itemForm.Item.UserId = User.Identity.GetUserId();
				var recordsSaved = await _itemService.SaveItem(itemForm.Item);

				if (recordsSaved <= 0)
					ModelState.AddModelError(string.Empty, "There was a problem and the Item was not saved.");

				var item = await _itemService.GetItem(itemForm.Item.ItemId);
				return View("ItemRecord", item);
			}
			itemForm = await _itemService.GetItemForm(itemForm.Item.ItemId);
			return View("EditItemRecord", itemForm);
		}
	}
}