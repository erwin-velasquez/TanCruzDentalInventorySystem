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
	public class BusinessPartnerService : IBusinessPartnerService
	{
		private readonly IBusinessPartnerRepository _businessPartnerRepository;

		public BusinessPartnerService(IUnitOfWork unitOfWork,
			IBusinessPartnerRepository businessPartnerRepository)
		{
			_businessPartnerRepository = businessPartnerRepository;
			_businessPartnerRepository.UnitOfWork = unitOfWork;
		}

		public async Task<IEnumerable<BusinessPartnerViewModel>> GetBusinessPartnerList()
		{
			var businessPartnerList = await _businessPartnerRepository.GetBusinessPartnerList();
			return Mapper.Map<List<BusinessPartnerViewModel>>(businessPartnerList);
		}

		public async Task<BusinessPartnerViewModel> GetBusinessPartner(string businessPartnerId)
		{
			var businessPartner = await _businessPartnerRepository.GetBusinessPartner(businessPartnerId);
			return Mapper.Map<BusinessPartnerViewModel>(businessPartner);
		}

		public async Task<string> CreateBusinessPartner(string userId)
		{
			string businessPartnerId = await _businessPartnerRepository.CreateBusinessPartner(userId);

			return businessPartnerId;
		}

		public async Task<BusinessPartnerFormViewModel> GetBusinessPartnerForm(string businessPartnerId)
		{
			var businessPartnerForm = new BusinessPartnerFormViewModel()
			{
				BusinessPartner = Mapper.Map<BusinessPartnerViewModel>(await _businessPartnerRepository.GetBusinessPartner(businessPartnerId))
			};
			return businessPartnerForm;
		}

		public async Task<int> SaveBusinessPartner(BusinessPartnerViewModel businessPartnerViewModel)
		{
			var businessPartner = Mapper.Map<BusinessPartner>(businessPartnerViewModel);

			_businessPartnerRepository.UnitOfWork.Begin();
			try
			{
				var rowsAffected = await _businessPartnerRepository.SaveBusinessPartner(businessPartner);

				if (rowsAffected > 0 && businessPartner.BusinessPartnerDetails != null)
				{
					foreach (var businessPartnerDetail in businessPartner.BusinessPartnerDetails)
					{
						if (businessPartnerDetail.BusinessPartnerDetailId == null)
						{
							// NEW BusinessPartnerDetail
							businessPartnerDetail.BusinessPartnerDetailId = await _businessPartnerRepository.CreateBusinessPartnerDetail(businessPartnerDetail.UserId);
							var newBusinessPartnerDetail = await _businessPartnerRepository.GetBusinessPartnerDetail(businessPartnerDetail.BusinessPartnerDetailId);
							businessPartnerDetail.VersionTimeStamp = newBusinessPartnerDetail.VersionTimeStamp;
						}

						await _businessPartnerRepository.SaveBusinessPartnerDetail(businessPartnerDetail);
					}
				}

				_businessPartnerRepository.UnitOfWork.Commit();

				return rowsAffected;
			}
			catch (Exception ex)
			{
				_businessPartnerRepository.UnitOfWork.Rollback();
				throw ex;
			}
		}
	}
}