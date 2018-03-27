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

        public JsonResult DMKip()
        {
            TimeKeepingRepository timekeepingRepository = new TimeKeepingRepository();
            return Json(timekeepingRepository.GetAllDMKip(), JsonRequestBehavior.AllowGet);
            //  return Json(apiRepository.ListPostCode(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListDetailedTimekeepingKipReport(string ngay, int donvi, int ankip)
        {
            TimeKeepingRepository timekeepingRepository = new TimeKeepingRepository();
            ReturnTimekeeping returntimekeeping = new ReturnTimekeeping();
            returntimekeeping = timekeepingRepository.TIMEKEEPING_KIP_DETAIL(ngay, donvi, ankip);
            return View(returntimekeeping.ListTimekeepingKipReport);
        }

        public ActionResult SumTimekeepingKipReport(string ngay, int donvi, int to)
        {
            TimeKeepingRepository timekeepingRepository = new TimeKeepingRepository();
            ReturnTimekeeping returntimekeeping = new ReturnTimekeeping();
            returntimekeeping = timekeepingRepository.SUM_TIMEKEEPING_KIP_DETAIL(ngay, donvi, to);
            return View(returntimekeeping.ListSumTimekeepingKipReport);
        }

        public ActionResult ListDetailedTimekeepingTitleReport(string ngay, int donvi, int to)
        {
            TimeKeepingRepository timekeepingRepository = new TimeKeepingRepository();
            ReturnTimekeeping returntimekeeping = new ReturnTimekeeping();
            returntimekeeping = timekeepingRepository.TIMEKEEPING_TITLE_DETAIL(ngay, donvi, to);
            return View(returntimekeeping.ListTimekeepingTitleReport);
        }

        public ActionResult ListDetailedTimekeepingReport(string ngay, int donvi, int to)
        {
            TimeKeepingRepository timekeepingRepository = new TimeKeepingRepository();
            ReturnTimekeeping returntimekeeping = new ReturnTimekeeping();
            returntimekeeping = timekeepingRepository.TIMEKEEPING_DETAIL(ngay, donvi, to);
            return View(returntimekeeping.ListTimekeepingReport);
        }

    }
}