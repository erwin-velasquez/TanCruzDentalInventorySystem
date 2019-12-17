using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TanCruzDentalInventorySystem.BusinessService.BusinessServiceInterface;
using TanCruzDentalInventorySystem.Repository.DataServiceInterface;
using System.Web.Http;

namespace TanCruzDentalInventorySystem.BusinessService
{
    public class ReportService : IReportService
    {
        IPrintReportService _printReportService = new PrintReportService();

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

        public DataSet GetSalesOrderReceipt(string SalesOrderId)
        {
            return _reportRepository.GetSalesOrderReceipt(SalesOrderId);
        }

        public string PrintSalesOrderReceipt(string SalesOrderId, string ReportPath)
        {
            var dataSet = _reportRepository.GetSalesOrderReceipt(SalesOrderId);

            _printReportService.PrintReport(ReportPath, dataSet);

            return "Success";
        }
    }
}