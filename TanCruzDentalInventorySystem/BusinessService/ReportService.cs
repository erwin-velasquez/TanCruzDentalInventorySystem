using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TanCruzDentalInventorySystem.BusinessService.BusinessServiceInterface;
using TanCruzDentalInventorySystem.Repository.DataServiceInterface;

namespace TanCruzDentalInventorySystem.BusinessService
{
    public class ReportService : IReportService
    {

        private readonly IReportRepository _reportRepository;

        public ReportService(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public DataSet GetItemsReport()
        {
            return _reportRepository.GetItemsReport();
        }

        public DataSet GetSalesOrderReport() {
            return _reportRepository.GetSalesOrderReport();
        }

        public DataSet GetPurchaseOrderReport() {
            return _reportRepository.GetPurchaseOrderReport();
        }
    }
}