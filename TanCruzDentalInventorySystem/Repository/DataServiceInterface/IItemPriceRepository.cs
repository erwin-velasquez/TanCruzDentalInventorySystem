using System.Threading.Tasks;
using TanCruzDentalInventorySystem.Models;

namespace TanCruzDentalInventorySystem.Repository.DataServiceInterface
{
	interface IItemPriceRepository
	{
		IUnitOfWork UnitOfWork { get; set; }

		Task<int> SaveItemPrice(ItemPrice itemPrice);
		Task<string> CreateItemPrice(string userId);
	}
}
