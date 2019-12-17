using Microsoft.Reporting.WebForms;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using TanCruzDentalInventorySystem.BusinessService.BusinessServiceInterface;

namespace TanCruzDentalInventorySystem.Controllers
{
    public class ReportController : Controller
    {

        IReportService _reportService;
        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }
        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult ItemReport()
        {
            ReportViewer reportViewer = new ReportViewer()
            {
                ProcessingMode = ProcessingMode.Local,
                SizeToReportContent = true,
                Width = Unit.Percentage(900),
                Height = Unit.Percentage(900)
            };
            var ds = _reportService.GetItemsReport();
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\RDL\ItemsReport.rdlc";
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ItemListDataSet", ds.Tables["ItemsDataTable"]));
            ViewBag.ReportViewer = reportViewer;
            return View();
        }

        public ActionResult SalesOrderReport()
        {
            ReportViewer reportViewer = new ReportViewer()
            {
                ProcessingMode = ProcessingMode.Local,
                SizeToReportContent = true,
                Width = Unit.Percentage(900),
                Height = Unit.Percentage(900)
            };
            var ds = _reportService.GetSalesOrderReport();
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\RDL\SalesOrderReport.rdlc";
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("SalesOrderDataSet", ds.Tables["SalesOrderDataTable"]));
            ViewBag.ReportViewer = reportViewer;
            return View();
        }


        public ActionResult PurchaseOrderReport()
        {
            ReportViewer reportViewer = new ReportViewer()
            {
                ProcessingMode = ProcessingMode.Local,
                SizeToReportContent = true,
                Width = Unit.Percentage(900),
                Height = Unit.Percentage(900)
            };
            var ds = _reportService.GetPurchaseOrderReport();
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\RDL\PurchaseOrderReport.rdlc";
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("PurchaseOrderDataSet", ds.Tables["PurchaseOrderDataTable"]));
            ViewBag.ReportViewer = reportViewer;
            return View();
        }

		public ActionResult ReportModal()
		{
			ReportViewer reportViewer = new ReportViewer()
			{
				ProcessingMode = ProcessingMode.Local,
				SizeToReportContent = true,
				Width = Unit.Percentage(900),
				Height = Unit.Percentage(900)
			};
			var ds = _reportService.GetItemsReport();
			reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\RDL\ItemsReport.rdlc";
			reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ItemListDataSet", ds.Tables["ItemsDataTable"]));
			ViewBag.ReportViewer = reportViewer;
			return View();
		}
	}
}