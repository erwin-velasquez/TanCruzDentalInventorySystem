using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TanCruzDentalInventorySystem.Controllers
{
    public class SalesController : Controller
    {
        // GET: Sales
        public ActionResult SalesHome()
        {
            return View();
        }

        public ActionResult SalesDocument()
        {
            return View();
        }
    }
}