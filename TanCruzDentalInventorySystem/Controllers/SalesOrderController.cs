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
        public async Task<ActionResult> SalesOrderPaymentList()
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

            salesOrderForm.ViewMode = "Create";

			return View(salesOrderForm);
		}

        [Authorize(Roles = "Editor")]
        public async Task<ActionResult> CreateSalesOrderPayment(string salesOrderId)
        {
            var salesOrderPaymentForm = await _salesOrderService.CreateSalesOrderPaymentForm(User.Identity.GetUserId(), salesOrderId);

            salesOrderPaymentForm.ScheduledPayment.PostingDate = System.DateTime.Now;

            return View(salesOrderPaymentForm);
        }

        [HttpGet]
		[Authorize(Roles = "Editor")]
		public async Task<ActionResult> EditSalesOrderRecord(string salesOrderId)
		{
			var salesOrderForm = await _salesOrderService.GetSalesOrderForm(salesOrderId);

            salesOrderForm.ViewMode = "Edit";

            return View(salesOrderForm);
		}

		[HttpGet]
		public async Task<ActionResult> GetSalesOrderList()
		{
			var salesOrders = await _salesOrderService.GetSalesOrderList();

			return View(salesOrders);
		}

        [HttpGet]
        public async Task<ActionResult> PrintSalesOrderReceipt()
        {
            return View();
        }

		[HttpPost]
		[Authorize(Roles = "Editor")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> SaveSalesOrderRecord(SalesOrderFormViewModel salesOrderForm)
		{
            ArrayList errorList = new ArrayList();
            string _viewMode = salesOrderForm.ViewMode;

            if ( salesOrderForm.SalesOrder.SalesOrderDetails != null)
            foreach (SalesOrderDetailViewModel salesOrderDetail in salesOrderForm.SalesOrder.SalesOrderDetails)
            {
                if (salesOrderDetail.QuantityOnHand < salesOrderDetail.Quantity)
                {
                    ModelState.AddModelError(string.Empty, "Item " + salesOrderDetail.Item.ItemName + " will be depleted!");
                }
            }

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

            salesOrderForm = await _salesOrderService.GetSalesOrderForm(salesOrderForm.SalesOrder.SalesOrderId);
            salesOrderForm.ViewMode = _viewMode;


            return View((salesOrderForm.ViewMode == "Create" )? "~/Views/SalesOrder/CreateSalesOrder.cshtml" : "~/Views/SalesOrder/EditSalesOrderRecord.cshtml", salesOrderForm);
		}

	}
}