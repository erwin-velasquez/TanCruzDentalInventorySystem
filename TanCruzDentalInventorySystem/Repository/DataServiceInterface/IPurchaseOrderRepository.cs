using System.Collections.Generic;
using System.Threading.Tasks;
using TanCruzDentalInventorySystem.Models;

namespace TanCruzDentalInventorySystem.Repository.DataServiceInterface
{
	public interface IPurchaseOrderRepository
	{
		IUnitOfWork UnitOfWork { get; set; }
		Task<IEnumerable<PurchaseOrder>> GetPurchaseOrderList();
	}
}
