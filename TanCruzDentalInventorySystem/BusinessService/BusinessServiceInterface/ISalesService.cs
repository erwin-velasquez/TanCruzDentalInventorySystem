using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TanCruzDentalInventorySystem.ViewModel;

namespace TanCruzDentalInventorySystem.BusinessService.BusinessServiceInterface
{
    public interface ISalesService
    {
        IEnumerable<SalesViewModel> GetSalesList();
    }
}
