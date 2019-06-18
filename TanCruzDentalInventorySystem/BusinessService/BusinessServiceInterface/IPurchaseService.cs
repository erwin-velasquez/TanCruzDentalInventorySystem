using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TanCruzDentalInventorySystem.ViewModel;

namespace TanCruzDentalInventorySystem.BusinessService.BusinessServiceInterface
{
    public interface IPurchaseService
    {
        Task<IEnumerable<PurchaseViewModel>> GetPurchaseList();
    }
}
