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

		public ActionResult Index()
		{
			return View();
		}

		public async Task<ActionResult> SalesOrderRecord(string salesOrderId)
		{
			var salesOrder = await _salesOrderService.GetSalesOrder(salesOrderId);

			// TODO: James to create the SalesOrder view
			return View(salesOrder);
		}

		[Authorize(Roles = "Editor")]
		public async Task<ActionResult> CreateSalesOrder()
		{
			var salesOrderForm = await _salesOrderService.CreateSalesOrderForm(User.Identity.GetUserId());
			salesOrderForm.SalesOrder.SalesOrderDetailsJson = JsonConvert.SerializeObject(salesOrderForm.SalesOrder.SalesOrderDetails);
			// TODO: James to create the SalesOrder create view
			return View(salesOrderForm);
		}

		[Authorize(Roles = "Editor")]
		public async Task<ActionResult> EditSalesOrderRecord(string salesOrderId)
		{
			var salesOrderForm = await _salesOrderService.GetSalesOrderForm(salesOrderId);
			salesOrderForm.SalesOrder.SalesOrderDetailsJson = JsonConvert.SerializeObject(salesOrderForm.SalesOrder.SalesOrderDetails);
			return View(salesOrderForm);
		}

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
				// TODO: James, include the ChangeDate property value of each so detail when submitting if available.

				//// test
				//salesOrderForm = await _salesOrderService.GetSalesOrderForm("SO00000001");
				////

				///Deserialized SalesOrderDetailsJason
				//var t = JsonConvert.DeserializeObject<List<SalesOrderDetailViewModel>>(salesOrderForm.SalesOrder.SalesOrderDetailsJson);
				//var t = JsonConvert.DeserializeObject<dynamic>(salesOrderForm.sale.SalesOrder.SalesOrderDetailsJson);
				salesOrderForm.SalesOrder.SalesOrderDetails = JsonConvert.DeserializeObject<List<SalesOrderDetailViewModel>>(salesOrderForm.SalesOrder.SalesOrderDetailsJson);
				salesOrderForm.SalesOrder.UserId = User.Identity.GetUserId();
				salesOrderForm.SalesOrder.SalesOrderDetails?.Select
					(detail =>
					{
						detail.UserId = User.Identity.GetUserId();
						detail.SalesOrderId = salesOrderForm.SalesOrder.SalesOrderId;
						return detail;
					}).ToList();

				//// test sample
				//var salesOrder = await _salesOrderService.GetSalesOrder(salesOrderForm.SalesOrder.SalesOrderId);
				//var recordsSaved = await _salesOrderService.SaveSalesOrder(salesOrderForm.SalesOrder);
				////

				var recordsSaved = await _salesOrderService.SaveSalesOrder(salesOrderForm.SalesOrder);

				if (recordsSaved >= 1)
				{
					var salesOrder = await _salesOrderService.GetSalesOrder(salesOrderForm.SalesOrder.SalesOrderId);
					return View("SalesOrderRecord", salesOrder);
				}
				ModelState.AddModelError(string.Empty, "There was a problem and the SalesOrder was not saved.");
			}

			salesOrderForm = await _salesOrderService.GetSalesOrderForm(salesOrderForm.SalesOrder.SalesOrderId);

			// TODO: James to create the SalesOrder input view
			return View("EditSalesOrderRecord", salesOrderForm);
		}

	}
}