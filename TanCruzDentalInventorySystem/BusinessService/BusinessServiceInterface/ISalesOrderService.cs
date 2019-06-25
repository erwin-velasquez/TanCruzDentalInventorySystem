using System.Collections.Generic;
using TanCruzDentalInventorySystem.ViewModel;

namespace TanCruzDentalInventorySystem.BusinessService.BusinessServiceInterface
{
	public interface ISalesOrderService
	{
		IEnumerable<SalesOrderViewModel> GetSalesOrderList();
	}
}
