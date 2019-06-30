using System.Collections.Generic;
using System.Threading.Tasks;
using TanCruzDentalInventorySystem.Models;

namespace TanCruzDentalInventorySystem.Repository.DataServiceInterface
{
	public interface IBusinessPartnerRepository
	{
		IUnitOfWork UnitOfWork { get; set; }
		Task<IEnumerable<BusinessPartner>> GetBusinessPartnerList();
	}
}