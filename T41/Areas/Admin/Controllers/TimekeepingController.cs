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
        //Controller gọi đến view của báo cáo chấm công theo kíp
        public ActionResult TimekeepingDetailReport()
        {
            return View();
        }
        
        //Controller gọi dữ liệu kíp từ bảng dm_kip , hiện tại đang fix cứng dữ liệu để lấy theo từng kíp 1
        public JsonResult DMKip()
        {
            TimeKeepingRepository timekeepingRepository = new TimeKeepingRepository();
            return Json(timekeepingRepository.GetAllDMKip(), JsonRequestBehavior.AllowGet);
            //  return Json(apiRepository.ListPostCode(), JsonRequestBehavior.AllowGet);
        }

        //Controller gọi view chi tiết phần bảng tổng hợp theo kíp của báo cáo chấm công theo kíp
        //Hiện tại đang lấy dữ liệu phần tổng từ repository
        public ActionResult ListDetailedTimekeepingKipReport(string ngay, int donvi, int ankip, int kip)
        {
            long sumSO_NGUOI = 0;
            long sumDen_9h = 0;
            long sumDen_10h = 0;
            long sumDen_11h = 0;
            long sumDen_12h = 0;
            long sumDen_13h = 0;
            long sumDen_14h = 0;
            long sumDen_15h = 0;
            long sumDen_16h = 0;
            long sumDen_17h = 0;
            long sumDen_18h = 0;
            long sumDen_19h = 0;
            long sumDen_20h = 0;
            long sumDen_21h = 0;
            long sumDen_22h = 0;
            long sumDen_23h = 0;
            long sumDen_24h = 0;
            long sumDen_1h = 0;
            long sumDen_2h = 0;
            long sumDen_3h = 0;
            long sumDen_4h = 0;
            long sumDen_5h = 0;
            long sumDen_6h = 0;
            long sumDen_7h = 0;
            long sumDen_8h = 0;
            TimeKeepingRepository timekeepingRepository = new TimeKeepingRepository();
            ReturnTimekeeping returntimekeeping = new ReturnTimekeeping();
            returntimekeeping = timekeepingRepository.TIMEKEEPING_KIP_DETAIL(ngay, donvi, ankip, kip, ref sumSO_NGUOI, ref   sumDen_9h, ref   sumDen_10h, ref   sumDen_11h, ref   sumDen_12h, ref   sumDen_13h, ref   sumDen_14h, ref   sumDen_15h, ref   sumDen_16h, ref   sumDen_17h, ref   sumDen_18h, ref   sumDen_19h, ref   sumDen_20h, ref   sumDen_21h, ref   sumDen_22h, ref   sumDen_23h, ref   sumDen_24h, ref   sumDen_1h, ref   sumDen_2h, ref   sumDen_3h, ref   sumDen_4h, ref   sumDen_5h, ref   sumDen_6h, ref   sumDen_7h, ref   sumDen_8h);
            ViewBag.SumSO_NGUOI = sumSO_NGUOI;
            ViewBag.SumDen_9h = sumDen_9h;
            ViewBag.SumDen_10h = sumDen_10h;
            ViewBag.SumDen_11h = sumDen_11h;
            ViewBag.SumDen_12h = sumDen_12h;
            ViewBag.SumDen_13h = sumDen_13h;
            ViewBag.SumDen_14h = sumDen_14h;
            ViewBag.SumDen_15h = sumDen_15h;
            ViewBag.SumDen_16h = sumDen_16h;
            ViewBag.SumDen_17h = sumDen_17h;
            ViewBag.SumDen_18h = sumDen_18h;
            ViewBag.SumDen_19h = sumDen_19h;
            ViewBag.SumDen_20h = sumDen_20h;
            ViewBag.SumDen_21h = sumDen_21h;
            ViewBag.SumDen_22h = sumDen_22h;
            ViewBag.SumDen_23h = sumDen_23h;
            ViewBag.SumDen_24h = sumDen_24h;
            ViewBag.SumDen_1h = sumDen_1h;
            ViewBag.SumDen_2h = sumDen_2h;
            ViewBag.SumDen_3h = sumDen_3h;
            ViewBag.SumDen_4h = sumDen_4h;
            ViewBag.SumDen_5h = sumDen_5h;
            ViewBag.SumDen_6h = sumDen_6h;
            ViewBag.SumDen_7h = sumDen_7h;
            ViewBag.SumDen_8h = sumDen_8h;

            return View(returntimekeeping.ListTimekeepingKipReport);
        }
        
        //Controller gọi view tổng phần bảng tổng hợp theo kíp của báo cáo chấm công theo kíp
        public ActionResult SumTimekeepingKipReport(string ngay, int donvi, int to)
        {
            TimeKeepingRepository timekeepingRepository = new TimeKeepingRepository();
            ReturnTimekeeping returntimekeeping = new ReturnTimekeeping();
            returntimekeeping = timekeepingRepository.SUM_TIMEKEEPING_KIP_DETAIL(ngay, donvi, to);
            return View(returntimekeeping.ListSumTimekeepingKipReport);
            //return View(returntimekeeping.ListSumTimekeepingKipReport);
        }
        
        //Controller gọi view chi tiết phần bảng tổng hợp sản lượng khối lượng theo kíp của báo cáo chấm công theo kíp
        public ActionResult SumSLKLTimekeepingKipReport(string ngay, int donvi)
        {
            TimeKeepingRepository timekeepingRepository = new TimeKeepingRepository();
            ReturnTimekeeping returntimekeeping = new ReturnTimekeeping();
            returntimekeeping = timekeepingRepository.SUM_SLKL_TIMEKEEPING_KIP_DETAIL(ngay, donvi);
            return View(returntimekeeping.ListSumSLKLTimekeepingKipReport);
        }
        
        //Controller gọi view chi tiết phần bảng tổng hợp theo chuwcs danh của báo cáo chấm công theo kíp
        public ActionResult ListDetailedTimekeepingTitleReport(string ngay, int donvi, int kip)
        {
            TimeKeepingRepository timekeepingRepository = new TimeKeepingRepository();
            ReturnTimekeeping returntimekeeping = new ReturnTimekeeping();
            returntimekeeping = timekeepingRepository.TIMEKEEPING_TITLE_DETAIL(ngay, donvi, kip);
            return View(returntimekeeping.ListTimekeepingTitleReport);
        }
        
        //Controller gọi view tổng phần bảng tổng hợp theo chức danh của báo cáo chấm công theo kíp
        public ActionResult SumTimekeepingTitleReport(string ngay, int donvi, int to)
        {
            TimeKeepingRepository timekeepingRepository = new TimeKeepingRepository();
            ReturnTimekeeping returntimekeeping = new ReturnTimekeeping();
            returntimekeeping = timekeepingRepository.SUM_TIMEKEEPING_TITLE_DETAIL(ngay, donvi, to);
            return View(returntimekeeping.ListSumTimekeepingTitleReport);
        }
        
        //Controller gọi view tổng sản lượng khối lượng phần bảng tổng hợp theo chức danh của báo cáo chấm công theo kíp
        public ActionResult SumSLKLTimekeepingTitleReport(string ngay, int donvi)
        {
            TimeKeepingRepository timekeepingRepository = new TimeKeepingRepository();
            ReturnTimekeeping returntimekeeping = new ReturnTimekeeping();
            returntimekeeping = timekeepingRepository.SUM_SLKL_TIMEKEEPING_TITLE_DETAIL(ngay, donvi);
            return View(returntimekeeping.ListSumSLKLTimekeepingTitleReport);
        }
        
        //Controller gọi view chi tiết phần bảng chi tiết của báo cáo chấm công theo kíp
        public ActionResult ListDetailedTimekeepingReport(string ngay, int donvi, int to, int kip)
        {
            
            TimeKeepingRepository timekeepingRepository = new TimeKeepingRepository();
            ReturnTimekeeping returntimekeeping = new ReturnTimekeeping();
            returntimekeeping = timekeepingRepository.TIMEKEEPING_DETAIL(ngay, donvi, to, kip);
           

            return View(returntimekeeping.ListTimekeepingReport);
        }
        
        //Controller gọi view tổng phần bảng chi tiết của báo cáo chấm công theo kíp
        public ActionResult SumTimekeepingReport(string ngay, int donvi)
        {
            TimeKeepingRepository timekeepingRepository = new TimeKeepingRepository();
            ReturnTimekeeping returntimekeeping = new ReturnTimekeeping();
            returntimekeeping = timekeepingRepository.SUM_TIMEKEEPING_DETAIL(ngay, donvi);
            return View(returntimekeeping.ListSumTimekeepingReport);
        }

    }
}