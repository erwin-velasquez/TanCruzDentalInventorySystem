using System.Collections.Generic;
using System.Threading.Tasks;
using TanCruzDentalInventorySystem.Models;

namespace TanCruzDentalInventorySystem.Repository.DataServiceInterface
{
	public interface IBusinessPartnerRepository
	{
		IUnitOfWork UnitOfWork { get; set; }
		Task<IEnumerable<BusinessPartner>> GetBusinessPartnerList();
		Task<BusinessPartner> GetBusinessPartner(string businessPartnerId);
		Task<string> CreateBusinessPartner(string userId);
		Task<int> SaveBusinessPartner(BusinessPartner businessPartner);
		Task<string> CreateBusinessPartnerDetail(string userId);
		Task<BusinessPartnerDetail> GetBusinessPartnerDetail(string businessPartnerDetailId);
		Task<int> SaveBusinessPartnerDetail(BusinessPartnerDetail businessPartnerDetail);
	}
}