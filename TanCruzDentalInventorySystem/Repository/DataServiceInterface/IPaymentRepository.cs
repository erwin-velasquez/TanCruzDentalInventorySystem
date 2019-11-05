using System.Collections.Generic;
using System.Threading.Tasks;
using TanCruzDentalInventorySystem.Models;


namespace TanCruzDentalInventorySystem.Repository.DataServiceInterface
{
    public interface IPaymentRepository
    {
        IUnitOfWork UnitOfWork { get; set; }

        Task<SalesOrderPayment> GetSalesOrderPayment(string salesOrderPaymentId);
        Task<IEnumerable<SalesOrderPaymentDetail>> GetSalesOrderPaymentDetailList(string salesOrderPaymentId);
        Task<string> CreateSalesOrderPayment(string userId);
        Task<string> CreateSalesOrderPaymentDetail(string userId);
        Task<int> SaveSalesOrderPayment(SalesOrderPayment salesOrderPayment);
        Task<int> SaveSalesOrderPaymentDetail(SalesOrderPaymentDetail salesOrderPaymentDetail);
        Task<SalesOrderPaymentDetail> GetSalesOrderPaymentDetail(string salesOrderPaymentDetailId);
    }
}
