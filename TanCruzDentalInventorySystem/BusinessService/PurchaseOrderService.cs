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
		private readonly IPurchaseOrderRepository _purchaseRepository;

		public PurchaseOrderService(IUnitOfWork unitOfWork, IPurchaseOrderRepository purchaseRepository)
		{
			_purchaseRepository = purchaseRepository;
			_purchaseRepository.UnitOfWork = unitOfWork;
		}

		public async Task<IEnumerable<PurchaseOrderViewModel>> GetPurchaseList()
		{
			var purchaseList = await _purchaseRepository.GetPurchaseOrderList();
			return Mapper.Map<List<PurchaseOrderViewModel>>(purchaseList);
		}
	}
}