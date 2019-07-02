using Microsoft.AspNet.Identity;
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

			// TODO: James to create the PurchaseOrder view
			return View(purchaseOrder);
		}

		[Authorize(Roles = "Editor")]
		public async Task<ActionResult> CreatePurchaseOrder()
		{
            var purchaseOrderForm = await _purchaseOrderService.CreatePurchaseOrderForm(User.Identity.GetUserId());

            // TODO: James to create the PurchaseOrder create view
            return View(purchaseOrderForm);


        }

		[Authorize(Roles = "Editor")]
		public async Task<ActionResult> EditPurchaseOrderRecord(string purchaseOrderId)
		{
			var purchaseOrderForm = await _purchaseOrderService.GetPurchaseOrderForm(purchaseOrderId);

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
				purchaseOrderForm.PurchaseOrder.UserId = User.Identity.GetUserId();
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