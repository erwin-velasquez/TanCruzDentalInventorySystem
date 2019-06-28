using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using TanCruzDentalInventorySystem.BusinessService.BusinessServiceInterface;
using TanCruzDentalInventorySystem.Repository.DataServiceInterface;
using TanCruzDentalInventorySystem.ViewModel;

namespace TanCruzDentalInventorySystem.BusinessService
{
	public class PurchaseOrderService : IPurchaseOrderService
	{
		private readonly IPurchaseOrderRepository _purchaseOrderRepository;

		public PurchaseOrderService(IUnitOfWork unitOfWork, IPurchaseOrderRepository purchaseOrderRepository)
		{
			_purchaseOrderRepository = purchaseOrderRepository;
			_purchaseOrderRepository.UnitOfWork = unitOfWork;
		}

		public async Task<IEnumerable<PurchaseOrderViewModel>> GetPurchaseOrderList()
		{
			var purchaseOrderList = await _purchaseOrderRepository.GetPurchaseOrderList();
			return Mapper.Map<List<PurchaseOrderViewModel>>(purchaseOrderList);
		}
	}
}