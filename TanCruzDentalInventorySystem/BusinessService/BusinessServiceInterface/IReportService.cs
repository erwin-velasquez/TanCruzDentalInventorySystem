using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TanCruzDentalInventorySystem.BusinessService.BusinessServiceInterface
{
    public interface IReportService
    {
        DataSet GetItemsReport();
    }
}
