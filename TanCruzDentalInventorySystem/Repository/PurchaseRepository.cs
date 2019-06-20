
using System.Collections.Generic;
using System.Threading.Tasks;
using TanCruzDentalInventorySystem.Models;
using TanCruzDentalInventorySystem.Repository.DataServiceInterface;

namespace TanCruzDentalInventorySystem.Repository
{
	public class PurchaseRepository : IPurchaseRepository
	{
		public IUnitOfWork UnitOfWork { get; set; }

		public async Task<IEnumerable<Purchase>> GetPurchaseList()
		{
			return await Task.Run(() =>
			{
				List<Purchase> result = new List<Purchase>();
				IEnumerable<Purchase> output = result;
				return output;
			});
		}
	}
}