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

    public class QualityDeliveryController : Controller
    {
        Convertion common = new Convertion();
        // GET: Admin/QualityDelivery
        
        //Controller lấy dữ liệu bưu cục phát
        public JsonResult PosCode(int zone)
        {
            QualityDeliveryRepository qualitydeliveryRepository = new QualityDeliveryRepository();
            return Json(qualitydeliveryRepository.GetAllPOSCODE(zone), JsonRequestBehavior.AllowGet);
            //  return Json(apiRepository.ListPostCode(), JsonRequestBehavior.AllowGet);
        }


        //Controller lấy dữ liệu tuyến phát
        public JsonResult RouteCode(int endpostcode)
        {
            QualityDeliveryRepository qualitydeliveryRepository = new QualityDeliveryRepository();
            return Json(qualitydeliveryRepository.GetAllROUTECODE(endpostcode), JsonRequestBehavior.AllowGet);
            //  return Json(apiRepository.ListPostCode(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            return View();
        }


        //Controller gọi đến phần view Tổng hợp sản lượng đi phát
        public ActionResult QualityDeliveryDetailReport(Post_DTO data)
        {
            return View();

        }

        //Controller ví dụ của a tùng lấy dữ liệu theo kiểu json để đẩy sang phần modal
        [HttpPost]
        public ActionResult QualityDeliveryDetailReport_List(Post_DTO data)
        {
            var list = new List<Test_DTO>() {
                new Test_DTO() {Id= 1 ,Name ="test1" },
                new Test_DTO() {Id= 2 ,Name ="test2" },
                new Test_DTO() {Id= 3 ,Name ="test3" },
                new Test_DTO() {Id= 4 ,Name ="test4" },
            };
            return Json(list, JsonRequestBehavior.AllowGet);
            
        }

        [HttpPost]
        //Controller gọi đến chi tiết theo từng mã bưu cục của sản lượng phát thành công trong 6H
        public ActionResult QualityDeliveryDetailReport_Success6H(int endpostcode, int routecode, string startdate, string enddate, int service, int type)
        {
            QualityDeliveryRepository qualitydeliveryRepository = new QualityDeliveryRepository();
            ReturnQuality returnquality = new ReturnQuality();
            returnquality = qualitydeliveryRepository.Quality_Delivery_Success6H_Detail(endpostcode, routecode, common.DateToInt(startdate), common.DateToInt(enddate), service, type);

            //return Json(returnquality.ListQualityDeliverySuccess6HReport, JsonRequestBehavior.AllowGet);
            return Json(returnquality, JsonRequestBehavior.AllowGet);
            
        }
        
        //Controller gọi đến chi tiết của bảng tổng hợp sản lượng đi phát
        public ActionResult ListDetailedQualityDeliveryReport(int zone, int endpostcode, int routecode, string startdate, string enddate, int service)
        {
            
            //ViewBag.zone = zone;
            ViewBag.endpostcode = endpostcode;
            ViewBag.routecode = routecode;
            ViewBag.service = service;
            ViewBag.startdate = startdate;
            ViewBag.enddate = enddate;
            
            QualityDeliveryRepository qualitydeliveryRepository = new QualityDeliveryRepository();
            ReturnQuality returnquality = new ReturnQuality();
            returnquality = qualitydeliveryRepository.QUALITY_DELIVERY_DETAIL(zone , endpostcode , routecode, common.DateToInt(startdate), common.DateToInt(enddate), service);
            //return View(returnquality.ListQualityDeliveryReport);
            return View(returnquality);

        }
        public class Test_DTO
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        public class Post_DTO
        {
            public int service { get; set; }
            public string startdate { get; set; }
            public string enddate { get; set; }
        }
    }
}