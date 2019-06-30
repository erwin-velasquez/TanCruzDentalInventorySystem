using System.Collections.Generic;
using System.Threading.Tasks;
using TanCruzDentalInventorySystem.ViewModels;

namespace TanCruzDentalInventorySystem.BusinessService.BusinessServiceInterface
{
	public interface IPurchaseOrderService
	{
		Task<IEnumerable<PurchaseOrderViewModel>> GetPurchaseOrderList();
		Task<PurchaseOrderViewModel> GetPurchaseOrder(string purchaseOrderId);
		Task<PurchaseOrderFormViewModel> GetPurchaseOrderForm(string purchaseOrderId);
		Task<PurchaseOrderFormViewModel> CreatePurchaseOrderForm(string userId);
		Task<int> SavePurchaseOrder(PurchaseOrderViewModel purchaseOrderViewModel);
	}
}
