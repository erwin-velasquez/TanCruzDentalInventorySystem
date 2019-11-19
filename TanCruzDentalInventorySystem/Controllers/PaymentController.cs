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
    public class PaymentController : Controller
    {
        private IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        // GET: Payment
        [HttpGet]
        [Authorize(Roles = "Editor")]
        public async Task<ActionResult> CreateSalesOrderPayment(string salesOrderId)
        {
            var paymentForm = await _paymentService.CreateSalesOrderPaymentForm(User.Identity.GetUserId(), salesOrderId);

            return View(paymentForm);
        }

        [HttpGet]
        [Authorize(Roles = "Editor")]
        public ActionResult CreateSalesOrderPaymentDetailModal()
        {
            var formViewModel = new SalesOrderPaymentDetailViewModel();
            formViewModel.salesOrderPaymenFormDropDownValues = new SalesOrderPaymentFormDropDownValues();

            return View("~/Views/Payment/CreateSalesOrderPaymentDetailModal.cshtml", formViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Editor")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SaveSalesOrderPayment(SalesOrderPaymentFormViewModel salesOrderPaymentFormViewModel)
        {
            ModelState["SalesOrderPayment.SalesOrder.SalesOrderStatus"].Errors.Clear();
            ModelState["SalesOrderPayment.SalesOrder.SalesOrderDetails"].Errors.Clear();
            ModelState["SalesOrderPayment.BusinessPartner.BusinessPartnerName"].Errors.Clear();

            if (TryValidateModel(salesOrderPaymentFormViewModel))
            {
                salesOrderPaymentFormViewModel.SalesOrderPayment.UserId = User.Identity.GetUserId();
                salesOrderPaymentFormViewModel.SalesOrderPayment.SalesOrderPaymentDetails?.Select
                    (detail =>
                    {
                        detail.UserId = User.Identity.GetUserId();
                        detail.SalesOrderPaymentId = salesOrderPaymentFormViewModel.SalesOrderPayment.SOPaymentId;
                        return detail;
                    }).ToList();

                var recordsSaved = await _paymentService.SaveSalesOrderPayment(salesOrderPaymentFormViewModel.SalesOrderPayment);

                if (recordsSaved == 0)
                {
                    ModelState.AddModelError(string.Empty, "There was a problem and the SalesOrder was not saved.");
                }
               
            }

            return RedirectToAction("SalesOrderPaymentList", "SalesOrder");
        }


    }
}