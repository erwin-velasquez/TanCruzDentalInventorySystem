using TanCruzDentalInventorySystem.Repository.DataServiceInterface;

namespace TanCruzDentalInventorySystem.Repository
{
	public class ItemPriceRepository : IItemPriceRepository
	{
		public IUnitOfWork UnitOfWork { get; set; }

	}
}