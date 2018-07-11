using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T41.Areas.Admin.Common;
using T41.Areas.Admin.Data;
using T41.Areas.Admin.Models.DataModel;

namespace T41.Areas.Admin.Controllers
{

    public class TotalDataCustomerController : Controller
    {
        Convertion common = new Convertion();
        
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TotalDataCustomerDetailReport()
        {
            return View();
        }

    }
}