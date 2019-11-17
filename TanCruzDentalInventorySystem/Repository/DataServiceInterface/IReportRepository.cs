using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TanCruzDentalInventorySystem.Repository.DataServiceInterface
{
    public interface IReportRepository
    {
        DataSet GetItemsReport();
        DataSet GetSalesOrderReport();
        DataSet GetPurchaseOrderReport();

    }
}
