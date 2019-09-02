using Microsoft.AspNet.Identity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Collections;
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
		public async Task<ActionResult> Index()
		{
            var salesOrders = await _salesOrderService.GetSalesOrderList();

            return View(salesOrders);
		}

		[HttpGet]
		public async Task<ActionResult> SalesOrderRecord(string salesOrderId)
		{
			var salesOrder = await _salesOrderService.GetSalesOrder(salesOrderId);

			return View(salesOrder);
		}

		[Authorize(Roles = "Editor")]
		public async Task<ActionResult> CreateSalesOrder()
		{
			var salesOrderForm = await _salesOrderService.CreateSalesOrderForm(User.Identity.GetUserId());

			return View(salesOrderForm);
		}

		[HttpGet]
		[Authorize(Roles = "Editor")]
		public async Task<ActionResult> EditSalesOrderRecord(string salesOrderId)
		{
			var salesOrderForm = await _salesOrderService.GetSalesOrderForm(salesOrderId);

			return View(salesOrderForm);
		}

		[HttpGet]
		public async Task<ActionResult> GetSalesOrderList()
		{
			var salesOrders = await _salesOrderService.GetSalesOrderList();

			return View(salesOrders);
		}

		[HttpPost]
		[Authorize(Roles = "Editor")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> SaveSalesOrderRecord(SalesOrderFormViewModel salesOrderForm)
		{
            ArrayList errorList = new ArrayList();

            if (TryValidateModel(salesOrderForm))
			{
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

            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    errorList.Add(error.ErrorMessage);
                }
            }

            foreach (string error in errorList)
            {
                ModelState.AddModelError(string.Empty, error);
            }
            salesOrderForm = await _salesOrderService.GetSalesOrderForm(salesOrderForm.SalesOrder.SalesOrderId);

			return View((ViewBag.FormMode = "Create" )? "CreateSalesOrder" : "SalesOrderEdit", salesOrderForm);
		}

	}
}