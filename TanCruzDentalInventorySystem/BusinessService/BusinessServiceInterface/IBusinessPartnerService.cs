using System.Collections.Generic;
using System.Threading.Tasks;
using TanCruzDentalInventorySystem.ViewModels;

namespace TanCruzDentalInventorySystem.BusinessService.BusinessServiceInterface
{
	public interface IBusinessPartnerService
	{
		Task<IEnumerable<BusinessPartnerViewModel>> GetBusinessPartnerList();
		Task<BusinessPartnerViewModel> GetBusinessPartner(string businessPartnerId);
		Task<string> CreateBusinessPartner(string userId);
		Task<BusinessPartnerFormViewModel> GetBusinessPartnerForm(string businessPartnerId);
		Task<int> SaveBusinessPartner(BusinessPartnerViewModel businessPartnerViewModel);
	}
}
