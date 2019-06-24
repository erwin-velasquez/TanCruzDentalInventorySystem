using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TanCruzDentalInventorySystem.BusinessService.BusinessServiceInterface;


namespace TanCruzDentalInventorySystem.Controllers
{
    public class SalesController : Controller
    {
        private ISalesService _salesService;

        // GET: Sales
        public ActionResult SalesHome()
        {
            return View();
        }

        public ActionResult SalesDocument()
        {
            return View();
        }

        public ActionResult GetHomeData()
        {
            var items = _salesService.GetSalesList();
            return Json(new { data = items }, JsonRequestBehavior.AllowGet);

        }
    }
}