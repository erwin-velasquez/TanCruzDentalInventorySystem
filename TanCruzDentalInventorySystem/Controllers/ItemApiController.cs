using System.Threading.Tasks;
using System.Web.Http;
using TanCruzDentalInventorySystem.BusinessService.BusinessServiceInterface;

namespace TanCruzDentalInventorySystem.Controllers
{
	[Authorize]
	public class ItemApiController : ApiController
    {
		private IItemService _itemService;

		public ItemApiController(IItemService itemService)
		{
			_itemService = itemService;
		}

		public async Task<IHttpActionResult> GetItems()
		{
			var items = await _itemService.GetItemList();

			return Ok(new { items = items });
		}
	}
}
