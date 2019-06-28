using System.Threading.Tasks;
using System.Web.Mvc;
using TanCruzDentalInventorySystem.BusinessService.BusinessServiceInterface;

namespace TanCruzDentalInventorySystem.Controllers
{
	public class PurchaseOrderController : Controller
	{
		private IPurchaseOrderService _purchaseOrderService;

		public PurchaseOrderController(IPurchaseOrderService purchaseService)
		{
			_purchaseOrderService = purchaseService;
		}

		public async Task<ActionResult> Index()
		{
			var purchaseOrders = await _purchaseOrderService.GetPurchaseOrderList();

			return View();
		}


		public async Task<ActionResult> GetData()
		{
			var purchaseOrders = await _purchaseOrderService.GetPurchaseOrderList();

			return Json(new { data = purchaseOrders }, JsonRequestBehavior.AllowGet);
		}
	}
}