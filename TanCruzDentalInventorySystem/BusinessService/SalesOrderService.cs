using AutoMapper;
using System.Collections.Generic;
using TanCruzDentalInventorySystem.BusinessService.BusinessServiceInterface;
using TanCruzDentalInventorySystem.Repository.DataServiceInterface;
using TanCruzDentalInventorySystem.ViewModels;

namespace TanCruzDentalInventorySystem.BusinessService
{
	public class SalesOrderService : ISalesOrderService
	{
		private readonly ISalesOrderRepository _salesOrderRepository;

		public SalesOrderService(IUnitOfWork unitOfWork, ISalesOrderRepository salesOrderRepository)
		{
			_salesOrderRepository = salesOrderRepository;
			_salesOrderRepository.UnitOfWork = unitOfWork;
		}

		public IEnumerable<SalesOrderViewModel> GetSalesOrderList()
		{
			var salesOrderList = _salesOrderRepository.GetSalesOrderList();
			return Mapper.Map<List<SalesOrderViewModel>>(salesOrderList);
		}
	}
}