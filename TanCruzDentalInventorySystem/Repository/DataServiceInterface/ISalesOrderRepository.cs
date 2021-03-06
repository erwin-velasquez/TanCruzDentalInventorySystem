﻿using System.Collections.Generic;
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
		Task<string> CreateSalesOrder(string userId);
        Task<string> CreateSalesOrderPayment(string userId, string salesOrderId);
        Task<int> SaveSalesOrderDetail(SalesOrderDetail salesOrderDetail);
		Task<string> CreateSalesOrderDetail(string userId);
		Task<SalesOrderDetail> GetSalesOrderDetail(string salesOrderDetailId);
        Task<ScheduledPayment> GetSalesOrderPayment(string ScheduledPaymentId);

    }
}
