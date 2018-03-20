using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace T41.Areas.Admin.Controllers
{
    public class TimekeepingController : Controller
    {
        // GET: Admin/Timekeeping
        public ActionResult Index()
        {
            return View();
        }
    }
}