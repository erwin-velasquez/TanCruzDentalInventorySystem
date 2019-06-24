using System.Threading.Tasks;
using System.Web.Mvc;
using TanCruzDentalInventorySystem.BusinessService.BusinessServiceInterface;

namespace TanCruzDentalInventorySystem.Controllers
{
	public class PurchaseController : Controller
	{
		private IPurchaseService _purchaseService;

		public PurchaseController(IPurchaseService purchaseService)
		{
			_purchaseService = purchaseService;
		}

		public ActionResult Index()
		{
			var items = _purchaseService.GetPurchaseList();

			return View();
		}


        public ActionResult GetData() {
            var items =  _purchaseService.GetPurchaseList();
            return Json(new { data = items },JsonRequestBehavior.AllowGet);

        }


    }
}