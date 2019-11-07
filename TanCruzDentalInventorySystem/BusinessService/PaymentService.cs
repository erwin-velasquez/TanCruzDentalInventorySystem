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
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IBusinessPartnerRepository _businessPartnerRepository;

        public PaymentService(IUnitOfWork unitOfWork,
            IPaymentRepository paymentRepository,
            ICurrencyRepository currencyRepository,
            IBusinessPartnerRepository businessPartnerRepository)
        {
            _paymentRepository = paymentRepository;
            _paymentRepository.UnitOfWork = unitOfWork;

            _currencyRepository = currencyRepository;
            _currencyRepository.UnitOfWork = unitOfWork;

            _businessPartnerRepository = businessPartnerRepository;
            _businessPartnerRepository.UnitOfWork = unitOfWork;
        }

        public async Task<SalesOrderPaymentFormViewModel> CreateSalesOrderPaymentForm(string userId, string salesOrderId)
        {
            string salesOrderPaymentId = await _paymentRepository.CreateSalesOrderPayment(userId, salesOrderId);

            var salesOrderPaymentForm = new SalesOrderPaymentFormViewModel()
            {
                SalesOrderPayment = Mapper.Map<SalesOrderPaymentViewModel>(await _paymentRepository.GetSalesOrderPayment(salesOrderPaymentId)),
                Currencies = Mapper.Map<IEnumerable<CurrencyViewModel>>(await _currencyRepository.GetCurrencyList()),
                BusinessPartners = Mapper.Map<IEnumerable<BusinessPartnerViewModel>>(await _businessPartnerRepository.GetBusinessPartnerList())
            };

            return salesOrderPaymentForm;
        }
        public async Task<SalesOrderViewModel> GetSalesOrderPayment(string salesOrderPaymentId)
        {
            var salesOrder = await _paymentRepository.GetSalesOrderPayment(salesOrderPaymentId);
            return Mapper.Map<SalesOrderViewModel>(salesOrder);
        }

        public async Task<int> SaveSalesOrderPayment(SalesOrderPaymentViewModel salesOrderPaymentViewModel)
        {
            var salesOrderPayment = Mapper.Map<SalesOrderPayment>(salesOrderPaymentViewModel);

            _paymentRepository.UnitOfWork.Begin();

            try
            {
                var rowsAffected = await _paymentRepository.SaveSalesOrderPayment(salesOrderPayment);

                if (rowsAffected > 0 && salesOrderPayment.SalesOrderPaymentDetails != null)
                {
                    foreach (var salesOrderPaymentDetail in salesOrderPayment.SalesOrderPaymentDetails)
                    {
                        if (salesOrderPaymentDetail.SalesOrderPaymentDetailId == null)
                        {
                            // NEW SalesOrderDetail
                            salesOrderPaymentDetail.SalesOrderPaymentDetailId = await _paymentRepository.CreateSalesOrderPaymentDetail(salesOrderPaymentDetail.UserId);
                            var newSalesOrderDetail = await _paymentRepository.GetSalesOrderPayment(salesOrderPaymentDetail.SalesOrderPaymentDetailId);
                            salesOrderPaymentDetail.VersionTimeStamp = newSalesOrderDetail.VersionTimeStamp;
                        }

                        await _paymentRepository.SaveSalesOrderPaymentDetail(salesOrderPaymentDetail);
                    }
                }

                _paymentRepository.UnitOfWork.Commit();

                return rowsAffected;
            }
            catch (Exception ex)
            {
                _paymentRepository.UnitOfWork.Rollback();
                throw ex;
            }
        }
    }
}