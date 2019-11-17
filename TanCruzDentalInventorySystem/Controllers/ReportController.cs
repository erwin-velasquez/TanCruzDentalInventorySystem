using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using TanCruzDentalInventorySystem.App_Data;
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
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(900);
            reportViewer.Height = Unit.Percentage(900);
            var ds = _reportService.GetItemsReport();
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\RDL\ItemsReport.rdlc";
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("ItemListDataset", ds.Tables[0]));


            ViewBag.ReportViewer = reportViewer;

            return View();
        }
    }
}