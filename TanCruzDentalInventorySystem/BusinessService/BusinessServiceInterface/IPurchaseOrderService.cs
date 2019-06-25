using System.Collections.Generic;
using System.Threading.Tasks;
using TanCruzDentalInventorySystem.ViewModel;

namespace TanCruzDentalInventorySystem.BusinessService.BusinessServiceInterface
{
	public interface IPurchaseOrderService
	{
		Task<IEnumerable<PurchaseOrderViewModel>> GetPurchaseList();
	}
}
