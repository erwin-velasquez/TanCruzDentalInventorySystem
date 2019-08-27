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
	public class SalesOrderController : Controller
	{
		private ISalesOrderService _salesOrderService;

		public SalesOrderController(ISalesOrderService salesOrderService)
		{
			_salesOrderService = salesOrderService;
		}

		[HttpGet]
		public ActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public async Task<ActionResult> SalesOrderRecord(string salesOrderId)
		{
			var salesOrder = await _salesOrderService.GetSalesOrder(salesOrderId);


			// ==============================================================================
			// ------------------------------------------------------------------------------
			// Remove these lines of codes from here
			// Use the SalesOrderRecord action exposed from SalesOrderApiController instead
			// ------------------------------------------------------------------------------
			salesOrder.SalesOrderDetailsJson = JsonConvert.SerializeObject(salesOrder.SalesOrderDetails);
			// ==============================================================================


			return View(salesOrder);
		}

		[Authorize(Roles = "Editor")]
		public async Task<ActionResult> CreateSalesOrder()
		{
			var salesOrderForm = await _salesOrderService.CreateSalesOrderForm(User.Identity.GetUserId());


			// ======================================================================
			// ----------------------------------------------------------------------
			// Remove these lines of codes from here
			// SalesOrderDetails is not expected to have anything at this point yet
			// ----------------------------------------------------------------------
			salesOrderForm.SalesOrder.SalesOrderDetailsJson = JsonConvert.SerializeObject(salesOrderForm.SalesOrder.SalesOrderDetails);
			// ======================================================================


			return View(salesOrderForm);
		}

		[HttpGet]
		[Authorize(Roles = "Editor")]
		public async Task<ActionResult> EditSalesOrderRecord(string salesOrderId)
		{
			var salesOrderForm = await _salesOrderService.GetSalesOrderForm(salesOrderId);


			// =================================================================================================================
			// -----------------------------------------------------------------------------------------------------------------
			// Remove these lines of codes from here
			// If you need just Json objects, use the EditSalesOrderRecord action exposed from SalesOrderApiController instead
			// -----------------------------------------------------------------------------------------------------------------
			salesOrderForm.SalesOrder.SalesOrderDetailsJson = JsonConvert.SerializeObject(salesOrderForm.SalesOrder.SalesOrderDetails);
			// =================================================================================================================


			return View(salesOrderForm);
		}

		[HttpGet]
		public async Task<ActionResult> GetSalesOrderList()
		{
			var salesOrders = await _salesOrderService.GetSalesOrderList();

			return Json(new { data = salesOrders }, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		[Authorize(Roles = "Editor")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> SaveSalesOrderRecord(SalesOrderFormViewModel salesOrderForm)
		{
			if (ModelState.IsValid)
			{
				// =======================================================================================
				// ---------------------------------------------------------------------------------------
				// Remove these lines of codes from here
				// Implement the persistence of data in the object model from the UI (as Miko explained)
				// ---------------------------------------------------------------------------------------
				salesOrderForm.SalesOrder.SalesOrderDetails = JsonConvert.DeserializeObject<List<SalesOrderDetailViewModel>>(salesOrderForm.SalesOrder.SalesOrderDetailsJson);
				// =======================================================================================


				salesOrderForm.SalesOrder.UserId = User.Identity.GetUserId();
				salesOrderForm.SalesOrder.SalesOrderDetails?.Select
					(detail =>
					{
						detail.UserId = User.Identity.GetUserId();
						detail.SalesOrderId = salesOrderForm.SalesOrder.SalesOrderId;
						return detail;
					}).ToList();

				var recordsSaved = await _salesOrderService.SaveSalesOrder(salesOrderForm.SalesOrder);

				if (recordsSaved >= 1)
				{
					var salesOrder = await _salesOrderService.GetSalesOrder(salesOrderForm.SalesOrder.SalesOrderId);
					return View("SalesOrderRecord", salesOrder);
				}
				ModelState.AddModelError(string.Empty, "There was a problem and the SalesOrder was not saved.");
			}

			salesOrderForm = await _salesOrderService.GetSalesOrderForm(salesOrderForm.SalesOrder.SalesOrderId);

			return View("EditSalesOrderRecord", salesOrderForm);
		}

	}
}