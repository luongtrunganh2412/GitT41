using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T41.Areas.Admin.Common;
using T41.Areas.Admin.Data;
using T41.Areas.Admin.Model.DataModel;
using T41.Areas.Admin.Models.DataModel;


namespace T41.Areas.Admin.Controllers
{
    public class TimekeepingController : Controller
    {
        // GET: Admin/Timekeeping
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TimekeepingDetailReport()
        {
            return View();
        }
        public JsonResult DMKip(int donvi)
        {
            TimeKeepingRepository timekeepingRepository = new TimeKeepingRepository();
            return Json(timekeepingRepository.GetAllDMKip(donvi), JsonRequestBehavior.AllowGet);
            //  return Json(apiRepository.ListPostCode(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult ListDetailedTimekeepingReport(int ngay, int donvi, int ankip)
        {
            TimeKeepingRepository timekeepingRepository = new TimeKeepingRepository();
            ReturnTimekeeping returntimekeeping = new ReturnTimekeeping();
            returntimekeeping = timekeepingRepository.TIMEKEEPING_DETAIL(ngay, donvi, ankip);
            return View(returntimekeeping.ListTimekeepingReport);
        }
        //public ActionResult SummaryDetailedTimekeepingReport()
        //{

        //}
    }
}