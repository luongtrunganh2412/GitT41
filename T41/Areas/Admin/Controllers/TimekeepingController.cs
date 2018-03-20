using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T41.Areas.Admin.Data;

namespace T41.Areas.Admin.Controllers
{
    public class TimekeepingController : Controller
    {
        // GET: Admin/Timekeeping
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult DMKip(int donvi)
        {
            TimeKeepingRepository timekeepingRepository = new TimeKeepingRepository();
            return Json(timekeepingRepository.GetAllDMKip(donvi), JsonRequestBehavior.AllowGet);
            //  return Json(apiRepository.ListPostCode(), JsonRequestBehavior.AllowGet);
        }
    }
}