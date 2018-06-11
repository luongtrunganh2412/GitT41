using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace T41.Areas.Admin.Controllers
{
    public class CollectController : Controller
    {
        // GET: Admin/Collect
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CollectDetailReport()
        {
            return View();
        }
    }
}