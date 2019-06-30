using System.Collections.Generic;
using TanCruzDentalInventorySystem.ViewModels;

namespace TanCruzDentalInventorySystem.BusinessService.BusinessServiceInterface
{
	public interface ISalesOrderService
	{
		IEnumerable<SalesOrderViewModel> GetSalesOrderList();
	}
}
