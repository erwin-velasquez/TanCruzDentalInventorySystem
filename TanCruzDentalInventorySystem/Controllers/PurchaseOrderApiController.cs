using System.Threading.Tasks;
using System.Web.Http;
using TanCruzDentalInventorySystem.BusinessService.BusinessServiceInterface;

namespace TanCruzDentalInventorySystem.Controllers
{
	[Authorize]
	public class PurchaseOrderApiController : ApiController
	{
		private IPurchaseOrderService _purchaseOrderService;

		public PurchaseOrderApiController(IPurchaseOrderService purchaseOrderService)
		{
			_purchaseOrderService = purchaseOrderService;
		}

		[HttpGet]
		// GET api/PurchaseOrderApi/PurchaseOrderRecord?purchaseOrderId=PO00000001
		public async Task<IHttpActionResult> PurchaseOrderRecord(string purchaseOrderId)
		{
			var purchaseOrder = await _purchaseOrderService.GetPurchaseOrder(purchaseOrderId);

			return Ok(new { purchaseOrder = purchaseOrder });
		}

		[HttpGet]
		// GET api/PurchaseOrderApi/EditPurchaseOrderRecord?purchaseOrderId=PO00000001
		[Authorize(Roles = "Editor")]
		public async Task<IHttpActionResult> EditPurchaseOrderRecord(string purchaseOrderId)
		{
			var purchaseOrderForm = await _purchaseOrderService.GetPurchaseOrderForm(purchaseOrderId);

			return Ok(new { purchaseOrderForm = purchaseOrderForm });
		}

		[HttpGet]
		// GET api/PurchaseOrderApi/GetPurchaseOrderList
		public async Task<IHttpActionResult> GetPurchaseOrderList()
		{
			var purchaseOrders = await _purchaseOrderService.GetPurchaseOrderList();

			return Ok(new { purchaseOrders = purchaseOrders });
		}
	}
}