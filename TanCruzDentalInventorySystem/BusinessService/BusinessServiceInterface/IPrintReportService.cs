using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace TanCruzDentalInventorySystem.BusinessService.BusinessServiceInterface
{
    public interface IPrintReportService
    {
        void PrintReport(string ReportPath, DataSet ReportDataSet);
    }
}
