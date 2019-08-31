using System.Collections.Generic;
using System.Threading.Tasks;
using TanCruzDentalInventorySystem.ViewModels;

namespace TanCruzDentalInventorySystem.BusinessService.BusinessServiceInterface
{
	public interface ISalesOrderService
	{
		Task<IEnumerable<SalesOrderViewModel>> GetSalesOrderList();
		Task<SalesOrderViewModel> GetSalesOrder(string salesOrderId);
		Task<SalesOrderFormViewModel> GetSalesOrderForm(string salesOrderId);
		Task<SalesOrderFormViewModel> CreateSalesOrderForm(string userId);
        Task<int> SaveSalesOrder(SalesOrderViewModel salesOrderViewModel);
	}
}
