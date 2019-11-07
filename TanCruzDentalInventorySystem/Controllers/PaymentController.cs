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
    }
}