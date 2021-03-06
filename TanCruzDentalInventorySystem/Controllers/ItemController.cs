﻿using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Web.Mvc;
using TanCruzDentalInventorySystem.BusinessService.BusinessServiceInterface;
using TanCruzDentalInventorySystem.ViewModels;

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

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var items = await _itemService.GetItemList();

            return View(items);
        }

        [HttpGet]
        public async Task<ActionResult> ItemRecord(string itemId)
        {
            var item = await _itemService.GetItem(itemId);

            return View(item);
        }

        [Authorize(Roles = "Editor")]
        public async Task<ActionResult> CreateItem()
        {
            var itemId = await _itemService.CreateItem(User.Identity.GetUserId());
			var itemForm = await _itemService.GetItemForm(itemId);

			return View(itemForm);
        }

        [HttpGet]
        [Authorize(Roles = "Editor")]
        public async Task<ActionResult> EditItemRecord(string itemId)
        {
            var itemForm = await _itemService.GetItemForm(itemId);

            return View(itemForm);
        }

        [HttpGet]
        [Authorize(Roles = "Editor")]
        public async Task<PartialViewResult> ItemSearchModal()
        {
            var itemList = await _itemService.GetItemSearchModalList();

            return PartialView("~/Views/Shared/ItemSearchModal.cshtml", itemList);
        }

        public class ReturnArgs
        {
            public ReturnArgs()
            {
            }

            public int Status { get; set; }
            public PartialViewResult View { get; set; }
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

				if (recordsSaved >= 1)
				{
					var item = await _itemService.GetItem(itemForm.Item.ItemId);
					return View("ItemRecord", item);
				}
				ModelState.AddModelError(string.Empty, "There was a problem and the Item was not saved.");
			}
			
			itemForm = await _itemService.GetItemForm(itemForm.Item.ItemId);
			return View("EditItemRecord", itemForm);
		}
	}
}