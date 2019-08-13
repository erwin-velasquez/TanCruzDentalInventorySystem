using System.Collections.Generic;
using System.Threading.Tasks;
using TanCruzDentalInventorySystem.Models;

namespace TanCruzDentalInventorySystem.Repository.DataServiceInterface
{
	public interface ISalesOrderRepository
	{
		IUnitOfWork UnitOfWork { get; set; }
		Task<IEnumerable<SalesOrder>> GetSalesOrderList();
		Task<SalesOrder> GetSalesOrder(string salesOrderId);
		Task<int> SaveSalesOrder(SalesOrder salesOrder);
		Task<string> CreateSalesOrderAsync(string userId);
//		Task<IEnumerable<SalesOrderDetail>> GetSalesOrderDetailList(string salesOrderId);
	}
}
