using System.Threading.Tasks;
using System.Web.Mvc;
using TanCruzDentalInventorySystem.BusinessService.BusinessServiceInterface;

namespace TanCruzDentalInventorySystem.Controllers
{
    public class PurchaseController: Controller
    {
        private IPurchaseService _purchaseService;
        public PurchaseController(IPurchaseService purchaseService)
        {
            this._purchaseService = purchaseService;
        }


        public async Task<ActionResult>  Index() {
            var items = await _purchaseService.GetPurchaseList();

            return View();
        }

    }
}