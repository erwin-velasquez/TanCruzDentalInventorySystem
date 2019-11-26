using System.Threading.Tasks;
using System.Web.Http;
using TanCruzDentalInventorySystem.BusinessService.BusinessServiceInterface;

namespace TanCruzDentalInventorySystem.Controllers
{
		[Authorize]
		[RoutePrefix("api/ItemApi")]
		public class ItemApiController : ApiController
		{
				private IItemService _itemService;


				public ItemApiController(IItemService itemService)
				{
						_itemService = itemService;
				}

				[HttpGet]
				// GET api/ItemApi/GetItems
				public async Task<IHttpActionResult> GetItems()
				{
						var items = await _itemService.GetItemList();

						return Ok(new { items = items });
				}

				[HttpGet]
				// GET api/ItemApi/ItemRecord?itemId=IT00000001
				public async Task<IHttpActionResult> ItemRecord(string itemId)
				{
						var item = await _itemService.GetItem(itemId);

						return Ok(new { item = item });
				}

				[HttpGet]
				// GET api/ItemApi/ItemRecord?itemId=IT00000001
				public async Task<IHttpActionResult> ItemRecordWithPrice(string itemId)
				{
						var item = await _itemService.GetItemWithPrice(itemId);

						return Ok(new { item = item });
				}
		}
}
