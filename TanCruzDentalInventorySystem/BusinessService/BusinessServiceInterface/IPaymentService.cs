using System.Collections.Generic;
using System.Threading.Tasks;
using TanCruzDentalInventorySystem.ViewModels;

namespace TanCruzDentalInventorySystem.BusinessService.BusinessServiceInterface
{
	public interface IPaymentService
	{
        Task<SalesOrderPaymentFormViewModel> CreateSalesOrderPaymentForm(string userId, string salesOrderId);
        Task<SalesOrderViewModel> GetSalesOrderPayment(string salesOrderPaymentId);
        Task<int> SaveSalesOrderPayment(SalesOrderPaymentViewModel salesOrderPaymentViewModel);
    }
}
