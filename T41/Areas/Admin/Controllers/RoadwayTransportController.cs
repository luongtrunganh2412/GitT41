using System;
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
    public class RoadwayTransportController : Controller
    {
        Convertion common = new Convertion();
        // GET: Admin/AirwayTransport
        public ActionResult Index()
        {
            return View();
        }
        //Phần Controller gọi đến Bảng Load_DATA1
        public ActionResult RoadwayTransportDetailReport()
        {
            return View();
        }
        public ActionResult ListRoadwayTransport()
        {
            return View();
        }

        //Controller lấy dữ liệu bưu cục phát
        public JsonResult GetAllMailRouteCode()
        {
            RoadwayTransportRepository roadwaytransportRepository = new RoadwayTransportRepository();
            return Json(roadwaytransportRepository.GetAllMailRouteCode(), JsonRequestBehavior.AllowGet);
        }
        
        //Phần Controller gọi đến Bảng Load_DATA1
        public ActionResult LOAD_DATA1(string mailroutecode, string tungay, string denngay, int vung, string cap, int loaipt)
        {
            RoadwayTransportRepository roadwaytransportRepository = new RoadwayTransportRepository();
            ReturnRoadwayTransport returnroadwaytransport = new ReturnRoadwayTransport();
            returnroadwaytransport = roadwaytransportRepository.LOAD_DATA1(mailroutecode, common.DateToInt(tungay), common.DateToInt(denngay), vung, cap, loaipt);
            return View(returnroadwaytransport.ListRoadwayTransportReport);
        }
    }
}