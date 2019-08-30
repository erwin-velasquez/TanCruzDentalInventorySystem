using System.Collections.Generic;
using System.Threading.Tasks;
using TanCruzDentalInventorySystem.Models;

namespace TanCruzDentalInventorySystem.Repository.DataServiceInterface
{
	public interface IScheduledPaymentRepository
	{
		IUnitOfWork UnitOfWork { get; set; }
		Task<IEnumerable<ScheduledPayment>> GetScheduledPaymentList();
		Task<ScheduledPayment> GetScheduledPayment(string scheduledPaymentId);
		Task<int> SaveScheduledPayment(ScheduledPayment scheduledPayment);
		Task<string> CreateScheduledPayment(string userId);
		Task<int> SaveScheduledPaymentDetail(ScheduledPaymentDetail scheduledPaymentDetail);
		Task<string> CreateScheduledPaymentDetail(string userId);
		Task<ScheduledPaymentDetail> GetScheduledPaymentDetail(string scheduledPaymentDetailId);
	}
}
