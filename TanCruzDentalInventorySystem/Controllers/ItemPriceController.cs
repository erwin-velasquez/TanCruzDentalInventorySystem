﻿using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Web.Mvc;
using TanCruzDentalInventorySystem.BusinessService.BusinessServiceInterface;
using TanCruzDentalInventorySystem.ViewModels;

namespace TanCruzDentalInventorySystem.Controllers
{
	[Authorize]
	public class ItemPriceController : Controller
	{
		private IItemPriceService _itemPriceService;

		public ItemPriceController(IItemPriceService itemPriceService)
		{
			_itemPriceService = itemPriceService;
		}

		[HttpGet]
		[Authorize(Roles = "Editor")]
		public async Task<ActionResult> EditItemRecord(string itemPriceId)
		{
			var itemPriceForm = await _itemPriceService.GetItemPriceForm(itemPriceId);

			return View(itemPriceForm);
		}

		[HttpGet]
		public async Task<ActionResult> GetItemPrices(string itemId)
		{
			var itemPrices = await _itemPriceService.GetItemPriceList(itemId);

			return View(itemPrices);
		}

		[Authorize(Roles = "Editor")]
		public async Task<ActionResult> CreateItemPrice()
		{
			var itemPriceId = await _itemPriceService.CreateItemPrice(User.Identity.GetUserId());
			var itemPriceForm = await _itemPriceService.GetItemPriceForm(itemPriceId);

			return View(itemPriceForm);
		}

		[HttpGet]
		public async Task<ActionResult> ItemPriceRecord(string itemPriceId)
		{
			var itemPrice = await _itemPriceService.GetItemPriceForm(itemPriceId);

			return View(itemPrice);
		}

		[HttpPost]
		[Authorize(Roles = "Editor")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> SaveItemPriceRecord(ItemPriceFormViewModel itemPriceForm)
		{
			if (ModelState.IsValid)
			{
				itemPriceForm.ItemPrice.UserId = User.Identity.GetUserId();
				var recordsSaved = await _itemPriceService.SaveItemPrice(itemPriceForm.ItemPrice);

				if (recordsSaved >= 1)
				{
					var itemPrice = await _itemPriceService.GetItemPriceForm(itemPriceForm.ItemPrice.ItemPriceId);
					return View("ItemPriceRecord", itemPrice);
				}
				ModelState.AddModelError(string.Empty, "There was a problem and the ItemPrice was not saved.");
			}

			itemPriceForm = await _itemPriceService.GetItemPriceForm(itemPriceForm.ItemPrice.ItemPriceId);
			return View("EditItemPriceRecord", itemPriceForm);
		}
	}
}