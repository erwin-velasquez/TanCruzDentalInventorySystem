using System.Collections.Generic;
using System.Threading.Tasks;
using TanCruzDentalInventorySystem.ViewModels;

namespace TanCruzDentalInventorySystem.BusinessService.BusinessServiceInterface
{
	public interface IScheduledPaymentService
	{
		Task<IEnumerable<ScheduledPaymentViewModel>> GetScheduledPaymentList();
		Task<ScheduledPaymentViewModel> GetScheduledPayment(string scheduledPaymentId);
		Task<ScheduledPaymentFormViewModel> GetScheduledPaymentForm(string scheduledPaymentId);
		Task<ScheduledPaymentFormViewModel> CreateScheduledPaymentForm(string userId);
        Task<ScheduledPaymentFormViewModel> UpdateScheduledPaymentForm(ScheduledPaymentFormViewModel scheduledPaymentFormViewModel);
        Task<int> SaveScheduledPayment(ScheduledPaymentViewModel scheduledPaymentViewModel);
	}
}
