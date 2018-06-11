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
    public class RealTimekeepingController : Controller
    {
        // GET: Admin/Timekeeping
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RealTimekeepingDetailReport()
        {
            return View();
        }

        public JsonResult DMKip()
        {
            RealTimeKeepingRepository realtimeKeepingRepository = new RealTimeKeepingRepository();
            return Json(realtimeKeepingRepository.GetAllDMKip(), JsonRequestBehavior.AllowGet);
            //  return Json(apiRepository.ListPostCode(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult RealListDetailedTimekeepingKipReport(string ngay, int donvi, int ankip, int kip)
        {
            RealTimeKeepingRepository realtimeKeepingRepository = new RealTimeKeepingRepository();
            ReturnRealTimekeeping returnrealtimekeeping = new ReturnRealTimekeeping();
            returnrealtimekeeping = realtimeKeepingRepository.REAL_TIMEKEEPING_KIP_DETAIL(ngay, donvi, ankip, kip);
            return View(returnrealtimekeeping.RealListTimekeepingKipReport);
        }

        public ActionResult RealSumTimekeepingKipReport(string ngay, int donvi, int to)
        {
            RealTimeKeepingRepository realtimeKeepingRepository = new RealTimeKeepingRepository();
            ReturnRealTimekeeping returnrealtimekeeping = new ReturnRealTimekeeping();
            returnrealtimekeeping = realtimeKeepingRepository.REAL_SUM_TIMEKEEPING_KIP_DETAIL(ngay, donvi, to);
            return View(returnrealtimekeeping.RealListSumTimekeepingKipReport);
            //return View(returntimekeeping.ListSumTimekeepingKipReport);
        }

        public ActionResult RealSumSLKLTimekeepingKipReport(string ngay, int donvi)
        {
            RealTimeKeepingRepository realtimeKeepingRepository = new RealTimeKeepingRepository();
            ReturnRealTimekeeping returnrealtimekeeping = new ReturnRealTimekeeping();
            returnrealtimekeeping = realtimeKeepingRepository.REAL_SUM_SLKL_TIMEKEEPING_KIP_DETAIL(ngay, donvi);
            return View(returnrealtimekeeping.RealListSumSLKLTimekeepingKipReport);
        }

        public ActionResult RealListDetailedTimekeepingTitleReport(string ngay, int donvi, int kip)
        {
            RealTimeKeepingRepository realtimeKeepingRepository = new RealTimeKeepingRepository();
            ReturnRealTimekeeping returnrealtimekeeping = new ReturnRealTimekeeping();
            returnrealtimekeeping = realtimeKeepingRepository.REAL_TIMEKEEPING_TITLE_DETAIL(ngay, donvi, kip);
            return View(returnrealtimekeeping.RealListTimekeepingTitleReport);
        }
        public ActionResult RealSumTimekeepingTitleReport(string ngay, int donvi, int to)
        {
            RealTimeKeepingRepository realtimeKeepingRepository = new RealTimeKeepingRepository();
            ReturnRealTimekeeping returnrealtimekeeping = new ReturnRealTimekeeping();
            returnrealtimekeeping = realtimeKeepingRepository.REAL_SUM_TIMEKEEPING_TITLE_DETAIL(ngay, donvi, to);
            return View(returnrealtimekeeping.RealListSumTimekeepingTitleReport);
        }
        public ActionResult RealSumSLKLTimekeepingTitleReport(string ngay, int donvi)
        {
            RealTimeKeepingRepository realtimeKeepingRepository = new RealTimeKeepingRepository();
            ReturnRealTimekeeping returnrealtimekeeping = new ReturnRealTimekeeping();
            returnrealtimekeeping = realtimeKeepingRepository.REAL_SUM_SLKL_TIMEKEEPING_TITLE_DETAIL(ngay, donvi);
            return View(returnrealtimekeeping.RealListSumSLKLTimekeepingTitleReport);
        }


        public ActionResult RealListDetailedTimekeepingReport(string ngay, int donvi, int to, int kip)
        {
            RealTimeKeepingRepository realtimeKeepingRepository = new RealTimeKeepingRepository();
            ReturnRealTimekeeping returnrealtimekeeping = new ReturnRealTimekeeping();
            returnrealtimekeeping = realtimeKeepingRepository.REAL_TIMEKEEPING_DETAIL(ngay, donvi, to, kip);
            return View(returnrealtimekeeping.RealListTimekeepingReport);
        }
        public ActionResult RealSumTimekeepingReport(string ngay, int donvi)
        {
            RealTimeKeepingRepository realtimeKeepingRepository = new RealTimeKeepingRepository();
            ReturnRealTimekeeping returnrealtimekeeping = new ReturnRealTimekeeping();
            returnrealtimekeeping = realtimeKeepingRepository.REAL_SUM_TIMEKEEPING_DETAIL(ngay, donvi);
            return View(returnrealtimekeeping.RealListSumTimekeepingReport);
        }

    }
}