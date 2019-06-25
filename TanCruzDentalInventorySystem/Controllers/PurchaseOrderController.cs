using System.Threading.Tasks;
using System.Web.Mvc;
using TanCruzDentalInventorySystem.BusinessService.BusinessServiceInterface;

namespace TanCruzDentalInventorySystem.Controllers
{
	public class PurchaseOrderController : Controller
	{
		private IPurchaseOrderService _purchaseService;

		public PurchaseOrderController(IPurchaseOrderService purchaseService)
		{
			_purchaseService = purchaseService;
		}

		public async Task<ActionResult> Index()
		{
			var purchaseOrders = await _purchaseService.GetPurchaseList();

			return View();
		}


		public async Task<ActionResult> GetData()
		{
			var purchaseOrders = await _purchaseService.GetPurchaseList();

			return Json(new { data = purchaseOrders }, JsonRequestBehavior.AllowGet);
		}
	}
}