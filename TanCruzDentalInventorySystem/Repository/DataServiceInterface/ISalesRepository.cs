using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TanCruzDentalInventorySystem.Models;

namespace TanCruzDentalInventorySystem.Repository.DataServiceInterface
{
    public interface ISalesRepository
    {
        IUnitOfWork UnitOfWork { get; set; }
        IEnumerable<Sales> GetSalesList();
    }
}
