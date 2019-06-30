using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using TanCruzDentalInventorySystem.BusinessService.BusinessServiceInterface;
using TanCruzDentalInventorySystem.Models;
using TanCruzDentalInventorySystem.Repository.DataServiceInterface;
using TanCruzDentalInventorySystem.ViewModels;

namespace TanCruzDentalInventorySystem.BusinessService
{
	public class PurchaseOrderService : IPurchaseOrderService
	{
		private readonly IPurchaseOrderRepository _purchaseOrderRepository;
		private readonly ICurrencyRepository _currencyRepository;
		private readonly IBusinessPartnerRepository _businessPartnerRepository;

		public PurchaseOrderService(IUnitOfWork unitOfWork,
			IPurchaseOrderRepository purchaseOrderRepository,
			ICurrencyRepository currencyRepository,
			IBusinessPartnerRepository businessPartnerRepository)
		{
			_purchaseOrderRepository = purchaseOrderRepository;
			_purchaseOrderRepository.UnitOfWork = unitOfWork;

			_currencyRepository = currencyRepository;
			_currencyRepository.UnitOfWork = unitOfWork;

			_businessPartnerRepository = businessPartnerRepository;
			_businessPartnerRepository.UnitOfWork = unitOfWork;
		}

		public async Task<PurchaseOrderViewModel> GetPurchaseOrder(string purchaseOrderId)
		{
			var purchaseOrder = await _purchaseOrderRepository.GetPurchaseOrder(purchaseOrderId);
			return Mapper.Map<PurchaseOrderViewModel>(purchaseOrder);
		}

		public async Task<IEnumerable<PurchaseOrderViewModel>> GetPurchaseOrderList()
		{
			var purchaseOrderList = await _purchaseOrderRepository.GetPurchaseOrderList();
			return Mapper.Map<List<PurchaseOrderViewModel>>(purchaseOrderList);
		}

		public async Task<PurchaseOrderFormViewModel> GetPurchaseOrderForm(string purchaseOrderId)
		{
			var purchaseOrderForm = new PurchaseOrderFormViewModel()
			{
				PurchaseOrder = Mapper.Map<PurchaseOrderViewModel>(await _purchaseOrderRepository.GetPurchaseOrder(purchaseOrderId)),
				Currencies = Mapper.Map<IEnumerable<CurrencyViewModel>>(await _currencyRepository.GetCurrencyList()),
				BusinessPartners = Mapper.Map<IEnumerable<BusinessPartnerViewModel>>(await _businessPartnerRepository.GetBusinessPartnerList())
			};
			return purchaseOrderForm;
		}

		public async Task<PurchaseOrderFormViewModel> CreatePurchaseOrderForm(string userId)
		{
			string purchaseOrderId = await _purchaseOrderRepository.CreatePurchaseOrderAsync(userId);

			var purchaseOrderForm = new PurchaseOrderFormViewModel()
			{
				PurchaseOrder = Mapper.Map<PurchaseOrderViewModel>(await _purchaseOrderRepository.GetPurchaseOrder(purchaseOrderId)),
				Currencies = Mapper.Map<IEnumerable<CurrencyViewModel>>(await _currencyRepository.GetCurrencyList()),
				BusinessPartners = Mapper.Map<IEnumerable<BusinessPartnerViewModel>>(await _businessPartnerRepository.GetBusinessPartnerList())
			};
			return purchaseOrderForm;
		}

		public async Task<int> SavePurchaseOrder(PurchaseOrderViewModel purchaseOrderViewModel)
		{
			var purchaseOrder = Mapper.Map<PurchaseOrder>(purchaseOrderViewModel);
			return await _purchaseOrderRepository.SavePurchaseOrder(purchaseOrder);
		}
	}
}