using System.Threading.Tasks;
using System.Web.Http;
using TanCruzDentalInventorySystem.BusinessService.BusinessServiceInterface;

namespace TanCruzDentalInventorySystem.Controllers
{
	[Authorize]
	public class SalesOrderApiController : ApiController
	{
		private ISalesOrderService _salesOrderService;

		public SalesOrderApiController(ISalesOrderService salesOrderService)
		{
			_salesOrderService = salesOrderService;
		}

		[HttpGet]
		// GET api/SalesOrderApi/SalesOrderRecord?salesOrderId=SO00000001
		public async Task<IHttpActionResult> SalesOrderRecord(string salesOrderId)
		{
			var salesOrder = await _salesOrderService.GetSalesOrder(salesOrderId);

			return Ok(new { salesOrder = salesOrder });
		}

		[HttpGet]
		// GET api/SalesOrderApi/EditSalesOrderRecord?salesOrderId=SO00000001
		[Authorize(Roles = "Editor")]
		public async Task<IHttpActionResult> EditSalesOrderRecord(string salesOrderId)
		{
			var salesOrderForm = await _salesOrderService.GetSalesOrderForm(salesOrderId);

			return Ok(new { salesOrderForm = salesOrderForm });
		}

		[HttpGet]
		// GET api/SalesOrderApi/GetSalesOrderList
		public async Task<IHttpActionResult> GetSalesOrderList()
		{
			var salesOrders = await _salesOrderService.GetSalesOrderList();

			return Ok(new { salesOrders = salesOrders });
		}
	}
}