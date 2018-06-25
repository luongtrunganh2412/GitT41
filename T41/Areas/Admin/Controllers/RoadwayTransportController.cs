using System;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T41.Areas.Admin.Common;
using T41.Areas.Admin.Data;
using T41.Areas.Admin.Model.DataModel;
using T41.Areas.Admin.Models.DataModel;

namespace T41.Areas.Admin.Controllers
{
    public class RoadwayTransportController : Controller
    {
        int page_size = int.Parse(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        Convertion common = new Convertion();
        // GET: Admin/AirwayTransport
        public ActionResult Index()
        {
            return View();
        }
        //Phần Controller gọi đến Bảng Load_DATA1
        public ActionResult RoadwayTransportDetailReport_CT()
        {
            return View();
        }
        
        //Phần Controller gọi đến Bảng Load_DATA2
        public ActionResult RoadwayTransportDetailReport_TH()
        {
            return View();
        }
        
        //Phần Controller gọi đến Bảng Load_DATA2
        public ActionResult RoadwayTransportDetailReport_TG()
        {
            return View();
        }

        public ActionResult ListRoadwayTransport_CT()
        {
            return View();
        }

        public ActionResult ListRoadwayTransport_TH()
        {
            return View();
        }

        public ActionResult ListRoadwayTransport_TG()
        {
            return View();
        }

        //Controller lấy dữ liệu bưu cục phát
        public JsonResult GetAllMailRouteCode()
        {
            RoadwayTransportRepository roadwaytransportRepository = new RoadwayTransportRepository();
            return Json(roadwaytransportRepository.GetAllMailRouteCode(), JsonRequestBehavior.AllowGet);
        }

        //Phần Controller gọi đến Repository Total_Data
        public ActionResult TotalDataReport( string fromdate, string todate)
        {
            RoadwayTransportRepository roadwaytransportRepository = new RoadwayTransportRepository();
            ReturnRoadwayTransport returnroadwaytransport = new ReturnRoadwayTransport();
            returnroadwaytransport = roadwaytransportRepository.TOTAL_DATA(common.DateToInt(fromdate), common.DateToInt(todate));
            return View(returnroadwaytransport);
        }

        //Phần Controller gọi đến Bảng Tổng Hợp Load_DATA1
        public ActionResult ListDetailedRoadwayTransport_TH(string mailroutecode, string fromdate, string todate, int vung, string cap, int loaipt)
        {
            RoadwayTransportRepository roadwaytransportRepository = new RoadwayTransportRepository();
            ReturnRoadwayTransport returnroadwaytransport = new ReturnRoadwayTransport();
            returnroadwaytransport = roadwaytransportRepository.LOAD_DATA1(mailroutecode, common.DateToInt(fromdate), common.DateToInt(todate), vung, cap, loaipt);
            return View(returnroadwaytransport);
        }

        //Phần Controller gọi đến Bảng Chi Tiết Load_DATA2
        public ActionResult ListDetailedRoadwayTransport_CT(string mailroutecode, string fromdate, string todate, int vung, string cap, int loaipt, int? page)
        {
            RoadwayTransportRepository roadwaytransportRepository = new RoadwayTransportRepository();
            ReturnRoadwayTransport returnroadwaytransport = new ReturnRoadwayTransport();

            int currentPageIndex = page.HasValue ? page.Value : 1;
            ViewBag.currentPageIndex = currentPageIndex;
            ViewBag.PageSize = page_size;

            returnroadwaytransport = roadwaytransportRepository.LOAD_DATA2(mailroutecode, common.DateToInt(fromdate), common.DateToInt(todate), vung, cap, loaipt, page_size, currentPageIndex);
            ViewBag.total = returnroadwaytransport.Total;
            ViewBag.total_page = (returnroadwaytransport.Total + page_size - 1) / page_size;

            return View(returnroadwaytransport);
        }

        //Phần Controller gọi đến Bảng Tổng Hợp Load_DATA3
        public ActionResult ListDetailedRoadwayTransport_TG(string mailroutecode, string fromdate, string todate, int vung, string cap, int loaipt)
        {
            RoadwayTransportRepository roadwaytransportRepository = new RoadwayTransportRepository();
            ReturnRoadwayTransport returnroadwaytransport = new ReturnRoadwayTransport();
            returnroadwaytransport = roadwaytransportRepository.LOAD_DATA3(mailroutecode, common.DateToInt(fromdate), common.DateToInt(todate), vung, cap, loaipt);
            return View(returnroadwaytransport);
        }

    }
}