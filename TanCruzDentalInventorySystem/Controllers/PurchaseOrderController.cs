﻿using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using TanCruzDentalInventorySystem.BusinessService.BusinessServiceInterface;
using TanCruzDentalInventorySystem.ViewModels;

namespace TanCruzDentalInventorySystem.Controllers
{
	[Authorize]
    public class PurchaseOrderController : Controller
    {
        private IPurchaseOrderService _purchaseOrderService;

        public PurchaseOrderController(IPurchaseOrderService purchaseService)
        {
            _purchaseOrderService = purchaseService;
        }

		[HttpGet]
		public ActionResult Index()
        {
            return View();
        }

		[HttpGet]
		public async Task<ActionResult> PurchaseOrderRecord(string purchaseOrderId)
        {
            var purchaseOrder = await _purchaseOrderService.GetPurchaseOrder(purchaseOrderId);


			// ====================================================================================
			// ------------------------------------------------------------------------------------
			// Remove these lines of codes from here
			// Use the PurchaseOrderRecord action exposed from PurchaseOrderApiController instead
			// ------------------------------------------------------------------------------------
			purchaseOrder.PurchaseOrderDetailsJson = JsonConvert.SerializeObject(purchaseOrder.PurchaseOrderDetails);
			// ====================================================================================


			return View(purchaseOrder);
        }

		[Authorize(Roles = "Editor")]
		public async Task<ActionResult> CreatePurchaseOrder()
        {
            var purchaseOrderForm = await _purchaseOrderService.CreatePurchaseOrderForm(User.Identity.GetUserId());


			// =========================================================================
			// -------------------------------------------------------------------------
			// Remove these lines of codes from here
			// PurchaseOrderDetails is not expected to have anything at this point yet
			// -------------------------------------------------------------------------
			purchaseOrderForm.PurchaseOrder.PurchaseOrderDetailsJson = JsonConvert.SerializeObject(purchaseOrderForm.PurchaseOrder.PurchaseOrderDetails);
			// =========================================================================


			return View(purchaseOrderForm);
        }

		[HttpGet]
		[Authorize(Roles = "Editor")]
        public async Task<ActionResult> EditPurchaseOrderRecord(string purchaseOrderId)
        {
            var purchaseOrderForm = await _purchaseOrderService.GetPurchaseOrderForm(purchaseOrderId);


			// =======================================================================================================================
			// -----------------------------------------------------------------------------------------------------------------------
			// Remove these lines of codes from here
			// If you need just Json objects, use the EditPurchaseOrderRecord action exposed from PurchaseOrderApiController instead
			// -----------------------------------------------------------------------------------------------------------------------
			purchaseOrderForm.PurchaseOrder.PurchaseOrderDetailsJson = JsonConvert.SerializeObject(purchaseOrderForm.PurchaseOrder.PurchaseOrderDetails);
			// =======================================================================================================================


			return View(purchaseOrderForm);
        }

		[HttpGet]
		public async Task<ActionResult> GetPurchaseOrderList()
        {
            var purchaseOrders = await _purchaseOrderService.GetPurchaseOrderList();

            return Json(new { data = purchaseOrders }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize(Roles = "Editor")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SavePurchaseOrderRecord(PurchaseOrderFormViewModel purchaseOrderForm)
        {
            if (ModelState.IsValid)
            {
				// =======================================================================================
				// ---------------------------------------------------------------------------------------
				// Remove these lines of codes from here
				// Implement the persistence of data in the object model from the UI (as Miko explained)
				// ---------------------------------------------------------------------------------------
				purchaseOrderForm.PurchaseOrder.PurchaseOrderDetails = JsonConvert.DeserializeObject<List<PurchaseOrderDetailViewModel>>(purchaseOrderForm.PurchaseOrder.PurchaseOrderDetailsJson);
				// =======================================================================================


				purchaseOrderForm.PurchaseOrder.UserId = User.Identity.GetUserId();
				purchaseOrderForm.PurchaseOrder.PurchaseOrderDetails?.Select
					(detail =>
					{
						detail.UserId = User.Identity.GetUserId();
						detail.PurchaseOrderId = purchaseOrderForm.PurchaseOrder.PurchaseOrderId;
						return detail;
					}).ToList();

                var recordsSaved = await _purchaseOrderService.SavePurchaseOrder(purchaseOrderForm.PurchaseOrder);

                if (recordsSaved >= 1)
                {
                    var purchaseOrder = await _purchaseOrderService.GetPurchaseOrder(purchaseOrderForm.PurchaseOrder.PurchaseOrderId);


					// ====================================================================================
					// ------------------------------------------------------------------------------------
					// Remove these lines of codes from here
					// 
					// ------------------------------------------------------------------------------------
					purchaseOrder.PurchaseOrderDetailsJson = JsonConvert.SerializeObject(purchaseOrderForm.PurchaseOrder.PurchaseOrderDetails);
					// ====================================================================================



					return View("PurchaseOrderRecord", purchaseOrder);
                }
                ModelState.AddModelError(string.Empty, "There was a problem and the PurchaseOrder was not saved.");
            }

            purchaseOrderForm = await _purchaseOrderService.GetPurchaseOrderForm(purchaseOrderForm.PurchaseOrder.PurchaseOrderId);

            return View("EditPurchaseOrderRecord", purchaseOrderForm);
        }

    }
}