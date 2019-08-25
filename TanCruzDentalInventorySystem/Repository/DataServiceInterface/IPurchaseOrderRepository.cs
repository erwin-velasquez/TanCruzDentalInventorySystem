using System.Collections.Generic;
using System.Threading.Tasks;
using TanCruzDentalInventorySystem.Models;

namespace TanCruzDentalInventorySystem.Repository.DataServiceInterface
{
	public interface IPurchaseOrderRepository
	{
		IUnitOfWork UnitOfWork { get; set; }
		Task<IEnumerable<PurchaseOrder>> GetPurchaseOrderList();
		Task<PurchaseOrder> GetPurchaseOrder(string purchaseOrderId);
		Task<int> SavePurchaseOrder(PurchaseOrder purchaseOrder);
		Task<string> CreatePurchaseOrder(string userId);
		Task<int> SavePurchaseOrderDetail(PurchaseOrderDetail purchaseOrderDetail);
		Task<string> CreatePurchaseOrderDetail(string userId);
		Task<PurchaseOrderDetail> GetPurchaseOrderDetail(string purchaseOrderDetailId);
	}
}
