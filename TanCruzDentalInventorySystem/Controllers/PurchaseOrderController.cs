using Microsoft.AspNet.Identity;
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

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> PurchaseOrderRecord(string purchaseOrderId)
        {
            var purchaseOrder = await _purchaseOrderService.GetPurchaseOrder(purchaseOrderId);
            purchaseOrder.PurchaseOrderDetailsJson = JsonConvert.SerializeObject(purchaseOrder.PurchaseOrderDetails);

            return View(purchaseOrder);
        }

        [Authorize(Roles = "Editor")]
        public async Task<ActionResult> CreatePurchaseOrder()
        {
            var purchaseOrderForm = await _purchaseOrderService.CreatePurchaseOrderForm(User.Identity.GetUserId());
			purchaseOrderForm.PurchaseOrder.PurchaseOrderDetailsJson = JsonConvert.SerializeObject(purchaseOrderForm.PurchaseOrder.PurchaseOrderDetails);

            return View(purchaseOrderForm);
        }

        [Authorize(Roles = "Editor")]
        public async Task<ActionResult> EditPurchaseOrderRecord(string purchaseOrderId)
        {
            var purchaseOrderForm = await _purchaseOrderService.GetPurchaseOrderForm(purchaseOrderId);
			purchaseOrderForm.PurchaseOrder.PurchaseOrderDetailsJson = JsonConvert.SerializeObject(purchaseOrderForm.PurchaseOrder.PurchaseOrderDetails);
            return View(purchaseOrderForm);
        }

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
				purchaseOrderForm.PurchaseOrder.PurchaseOrderDetails = JsonConvert.DeserializeObject<List<PurchaseOrderDetailViewModel>>(purchaseOrderForm.PurchaseOrder.PurchaseOrderDetailsJson);
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
                    return View("PurchaseOrderRecord", purchaseOrder);
                }
                ModelState.AddModelError(string.Empty, "There was a problem and the PurchaseOrder was not saved.");
            }

            purchaseOrderForm = await _purchaseOrderService.GetPurchaseOrderForm(purchaseOrderForm.PurchaseOrder.PurchaseOrderId);

            // TODO: James to create the PurchaseOrder input view
            return View("EditPurchaseOrderRecord", purchaseOrderForm);
        }

    }
}