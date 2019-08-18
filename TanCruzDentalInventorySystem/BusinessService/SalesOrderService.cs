using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TanCruzDentalInventorySystem.BusinessService.BusinessServiceInterface;
using TanCruzDentalInventorySystem.Models;
using TanCruzDentalInventorySystem.Repository.DataServiceInterface;
using TanCruzDentalInventorySystem.ViewModels;

namespace TanCruzDentalInventorySystem.BusinessService
{
	public class SalesOrderService : ISalesOrderService
	{
		private readonly ISalesOrderRepository _salesOrderRepository;
		private readonly ICurrencyRepository _currencyRepository;
		private readonly IBusinessPartnerRepository _businessPartnerRepository;

		public SalesOrderService(IUnitOfWork unitOfWork,
			ISalesOrderRepository salesOrderRepository,
			ICurrencyRepository currencyRepository,
			IBusinessPartnerRepository businessPartnerRepository)
		{
			_salesOrderRepository = salesOrderRepository;
			_salesOrderRepository.UnitOfWork = unitOfWork;

			_currencyRepository = currencyRepository;
			_currencyRepository.UnitOfWork = unitOfWork;

			_businessPartnerRepository = businessPartnerRepository;
			_businessPartnerRepository.UnitOfWork = unitOfWork;
		}

		public async Task<SalesOrderViewModel> GetSalesOrder(string salesOrderId)
		{
			var salesOrder = await _salesOrderRepository.GetSalesOrder(salesOrderId);
			return Mapper.Map<SalesOrderViewModel>(salesOrder);
		}

		public async Task<IEnumerable<SalesOrderViewModel>> GetSalesOrderList()
		{
			var salesOrderList = await _salesOrderRepository.GetSalesOrderList();
			return Mapper.Map<List<SalesOrderViewModel>>(salesOrderList);
		}

		public async Task<SalesOrderFormViewModel> GetSalesOrderForm(string salesOrderId)
		{
			var salesOrderForm = new SalesOrderFormViewModel()
			{
				SalesOrder = Mapper.Map<SalesOrderViewModel>(await _salesOrderRepository.GetSalesOrder(salesOrderId)),
				Currencies = Mapper.Map<IEnumerable<CurrencyViewModel>>(await _currencyRepository.GetCurrencyList()),
				BusinessPartners = Mapper.Map<IEnumerable<BusinessPartnerViewModel>>(await _businessPartnerRepository.GetBusinessPartnerList())
			};
			return salesOrderForm;
		}

		public async Task<SalesOrderFormViewModel> CreateSalesOrderForm(string userId)
		{
			string salesOrderId = await _salesOrderRepository.CreateSalesOrder(userId);

			var salesOrderForm = new SalesOrderFormViewModel()
			{
				SalesOrder = Mapper.Map<SalesOrderViewModel>(await _salesOrderRepository.GetSalesOrder(salesOrderId)),
				Currencies = Mapper.Map<IEnumerable<CurrencyViewModel>>(await _currencyRepository.GetCurrencyList()),
				BusinessPartners = Mapper.Map<IEnumerable<BusinessPartnerViewModel>>(await _businessPartnerRepository.GetBusinessPartnerList())
			};
			return salesOrderForm;
		}

		public async Task<int> SaveSalesOrder(SalesOrderViewModel salesOrderViewModel)
		{
			var salesOrder = Mapper.Map<SalesOrder>(salesOrderViewModel);


			_salesOrderRepository.UnitOfWork.Begin();
			try
			{
				var rowsAffected = await _salesOrderRepository.SaveSalesOrder(salesOrder);

				if (rowsAffected > 0 && salesOrder.SalesOrderDetails != null)
				{
					foreach (var salesOrderDetail in salesOrder.SalesOrderDetails)
					{
						if (salesOrderDetail.SalesOrderDetailId == null)
						{
							// NEW SalesOrderDetail
							salesOrderDetail.SalesOrderDetailId = await _salesOrderRepository.CreateSalesOrderDetail(salesOrderDetail.UserId);
							var newSalesOrderDetail = await _salesOrderRepository.GetSalesOrderDetail(salesOrderDetail.SalesOrderDetailId);
							salesOrderDetail.VersionTimeStamp = newSalesOrderDetail.VersionTimeStamp;
						}

						await _salesOrderRepository.SaveSalesOrderDetail(salesOrderDetail);
					}
				}

				_salesOrderRepository.UnitOfWork.Commit();

				return rowsAffected;
			}
			catch (Exception ex)
			{
				_salesOrderRepository.UnitOfWork.Rollback();
				throw ex;
			}
		}
	}
}