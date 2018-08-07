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
    public class AirwayTransportController : Controller
    {
        Convertion common = new Convertion();
        // GET: Admin/AirwayTransport
        public ActionResult Index()
        {
            return View();
        }
        //Phần Controller gọi đến Bảng Load_DATA1
        public ActionResult AirwayTransportDetailReport()
        {
            return View();
        }
        public ActionResult ListAirwayTransport()
        {
            return View();
        }

        //Phần Controller gọi đến Bảng TOTAL_DATA
        public ActionResult TotalDataReport(String date)
        {
            AirwayTransportRepository airwaytransportRepository = new AirwayTransportRepository();
            ReturnAirwayTransport returnairwaytransport = new ReturnAirwayTransport();
            returnairwaytransport = airwaytransportRepository.TOTAL_DATA(common.DateToInt(date));
            return View(returnairwaytransport.ListAirwayTransportReport);
        }

        //Phần Controller gọi đến Bảng Load_DATA1
        public ActionResult ListDetailedAirwayTransport1(String date, int way)
        {
            
            AirwayTransportRepository airwaytransportRepository = new AirwayTransportRepository();
            ReturnAirwayTransport returnairwaytransport = new ReturnAirwayTransport();
            returnairwaytransport = airwaytransportRepository.LOAD_DATA1(common.DateToInt(date), way);
            
            return View(returnairwaytransport.ListAirwayTransportReport);
        }

        //Phần Controller gọi đến Bảng Load_DATA2
        public ActionResult ListDetailedAirwayTransport2(String date, int way)
        {
            AirwayTransportRepository airwaytransportRepository = new AirwayTransportRepository();
            ReturnAirwayTransport returnairwaytransport = new ReturnAirwayTransport();
            returnairwaytransport = airwaytransportRepository.LOAD_DATA2(common.DateToInt(date), way);
            return View(returnairwaytransport.ListAirwayTransportReport);
        }

        //Phần Controller gọi đến Bảng Load_DATA3
        public ActionResult ListDetailedAirwayTransport3(String date, int way)
        {
            AirwayTransportRepository airwaytransportRepository = new AirwayTransportRepository();
            ReturnAirwayTransport returnairwaytransport = new ReturnAirwayTransport();
            returnairwaytransport = airwaytransportRepository.LOAD_DATA3(common.DateToInt(date), way);
            return View(returnairwaytransport.ListAirwayTransportReport);
        }

        //Phần Controller gọi đến Bảng Load_DATA4
        public ActionResult ListDetailedAirwayTransport4(String date, int way)
        {
            AirwayTransportRepository airwaytransportRepository = new AirwayTransportRepository();
            ReturnAirwayTransport returnairwaytransport = new ReturnAirwayTransport();
            returnairwaytransport = airwaytransportRepository.LOAD_DATA4(common.DateToInt(date), way);
            return View(returnairwaytransport.ListAirwayTransportReport);
        }

        //Phần Controller gọi đến Bảng Load_DATA5
        public ActionResult ListDetailedAirwayTransport5(String date, int way)
        {
            AirwayTransportRepository airwaytransportRepository = new AirwayTransportRepository();
            ReturnAirwayTransport returnairwaytransport = new ReturnAirwayTransport();
            returnairwaytransport = airwaytransportRepository.LOAD_DATA5(common.DateToInt(date), way);
            return View(returnairwaytransport.ListAirwayTransportReport);
        }

        //Phần Controller gọi đến Bảng Load_DATA6
        public ActionResult ListDetailedAirwayTransport6(String date, int way)
        {
            AirwayTransportRepository airwaytransportRepository = new AirwayTransportRepository();
            ReturnAirwayTransport returnairwaytransport = new ReturnAirwayTransport();
            returnairwaytransport = airwaytransportRepository.LOAD_DATA6(common.DateToInt(date), way);
            return View(returnairwaytransport.ListAirwayTransportReport);
        }

        //Phần Controller gọi đến Bảng Load_DATA7
        public ActionResult ListDetailedAirwayTransport7(String date, int way)
        {
            AirwayTransportRepository airwaytransportRepository = new AirwayTransportRepository();
            ReturnAirwayTransport returnairwaytransport = new ReturnAirwayTransport();
            returnairwaytransport = airwaytransportRepository.LOAD_DATA7(common.DateToInt(date), way);
            return View(returnairwaytransport.ListAirwayTransportReport);
        }

        //Phần Controller gọi đến Bảng Load_DATA8
        public ActionResult ListDetailedAirwayTransport8(String date, int way)
        {
            AirwayTransportRepository airwaytransportRepository = new AirwayTransportRepository();
            ReturnAirwayTransport returnairwaytransport = new ReturnAirwayTransport();
            returnairwaytransport = airwaytransportRepository.LOAD_DATA8(common.DateToInt(date), way);
            return View(returnairwaytransport.ListAirwayTransportReport);
        }

        //Phần Controller gọi đến Bảng Load_DATA9
        public ActionResult ListDetailedAirwayTransport9(String date, int way)
        {
            AirwayTransportRepository airwaytransportRepository = new AirwayTransportRepository();
            ReturnAirwayTransport returnairwaytransport = new ReturnAirwayTransport();
            returnairwaytransport = airwaytransportRepository.LOAD_DATA9(common.DateToInt(date), way);
            return View(returnairwaytransport.ListAirwayTransportReport);
        }

        //Phần Controller gọi đến Bảng Load_DATA10
        public ActionResult ListDetailedAirwayTransport10(String date, int way)
        {
            AirwayTransportRepository airwaytransportRepository = new AirwayTransportRepository();
            ReturnAirwayTransport returnairwaytransport = new ReturnAirwayTransport();
            returnairwaytransport = airwaytransportRepository.LOAD_DATA10(common.DateToInt(date), way);
            return View(returnairwaytransport.ListAirwayTransportReport);
        }

        //Phần Controller gọi đến Bảng Load_DATA11
        public ActionResult ListDetailedAirwayTransport11(String date, int way)
        {
            AirwayTransportRepository airwaytransportRepository = new AirwayTransportRepository();
            ReturnAirwayTransport returnairwaytransport = new ReturnAirwayTransport();
            returnairwaytransport = airwaytransportRepository.LOAD_DATA11(common.DateToInt(date), way);
            return View(returnairwaytransport.ListAirwayTransportReport);
        }

        //Phần Controller gọi đến Bảng Load_DATA12
        public ActionResult ListDetailedAirwayTransport12(String date, int way)
        {
            AirwayTransportRepository airwaytransportRepository = new AirwayTransportRepository();
            ReturnAirwayTransport returnairwaytransport = new ReturnAirwayTransport();
            returnairwaytransport = airwaytransportRepository.LOAD_DATA12(common.DateToInt(date), way);
            return View(returnairwaytransport.ListAirwayTransportReport);
        }

        //Phần Controller gọi đến Bảng Load_DATA13
        public ActionResult ListDetailedAirwayTransport13(String date, int way)
        {
            AirwayTransportRepository airwaytransportRepository = new AirwayTransportRepository();
            ReturnAirwayTransport returnairwaytransport = new ReturnAirwayTransport();
            returnairwaytransport = airwaytransportRepository.LOAD_DATA13(common.DateToInt(date), way);
            return View(returnairwaytransport.ListAirwayTransportReport);
        }

        //Phần Controller gọi đến Bảng Load_DATA14
        public ActionResult ListDetailedAirwayTransport14(String date, int way)
        {
            AirwayTransportRepository airwaytransportRepository = new AirwayTransportRepository();
            ReturnAirwayTransport returnairwaytransport = new ReturnAirwayTransport();
            returnairwaytransport = airwaytransportRepository.LOAD_DATA14(common.DateToInt(date), way);
            return View(returnairwaytransport.ListAirwayTransportReport);
        }

        //Phần Controller gọi đến Bảng Load_DATA15
        public ActionResult ListDetailedAirwayTransport15(String date, int way)
        {
            AirwayTransportRepository airwaytransportRepository = new AirwayTransportRepository();
            ReturnAirwayTransport returnairwaytransport = new ReturnAirwayTransport();
            returnairwaytransport = airwaytransportRepository.LOAD_DATA15(common.DateToInt(date), way);
            return View(returnairwaytransport.ListAirwayTransportReport);
        }

        //Phần Controller gọi đến Bảng Load_DATA16
        public ActionResult ListDetailedAirwayTransport16(String date, int way)
        {
            AirwayTransportRepository airwaytransportRepository = new AirwayTransportRepository();
            ReturnAirwayTransport returnairwaytransport = new ReturnAirwayTransport();
            returnairwaytransport = airwaytransportRepository.LOAD_DATA16(common.DateToInt(date), way);
            return View(returnairwaytransport.ListAirwayTransportReport);
        }

        //Phần Controller gọi đến Bảng Load_DATA17
        public ActionResult ListDetailedAirwayTransport17(String date, int way)
        {
            AirwayTransportRepository airwaytransportRepository = new AirwayTransportRepository();
            ReturnAirwayTransport returnairwaytransport = new ReturnAirwayTransport();
            returnairwaytransport = airwaytransportRepository.LOAD_DATA17(common.DateToInt(date), way);
            return View(returnairwaytransport.ListAirwayTransportReport);
        }

        //Phần Controller gọi đến Bảng Load_DATA18
        public ActionResult ListDetailedAirwayTransport18(String date, int way)
        {
            AirwayTransportRepository airwaytransportRepository = new AirwayTransportRepository();
            ReturnAirwayTransport returnairwaytransport = new ReturnAirwayTransport();
            returnairwaytransport = airwaytransportRepository.LOAD_DATA18(common.DateToInt(date), way);
            return View(returnairwaytransport.ListAirwayTransportReport);
        }

        //Phần Controller gọi đến Bảng Load_DATA19
        public ActionResult ListDetailedAirwayTransport19(String date, int way)
        {
            AirwayTransportRepository airwaytransportRepository = new AirwayTransportRepository();
            ReturnAirwayTransport returnairwaytransport = new ReturnAirwayTransport();
            returnairwaytransport = airwaytransportRepository.LOAD_DATA19(common.DateToInt(date), way);
            return View(returnairwaytransport.ListAirwayTransportReport);
        }

        //Phần Controller gọi đến Bảng Load_DATA20
        public ActionResult ListDetailedAirwayTransport20(String date, int way)
        {
            AirwayTransportRepository airwaytransportRepository = new AirwayTransportRepository();
            ReturnAirwayTransport returnairwaytransport = new ReturnAirwayTransport();
            returnairwaytransport = airwaytransportRepository.LOAD_DATA20(common.DateToInt(date), way);
            return View(returnairwaytransport.ListAirwayTransportReport);
        }

        //Phần Controller gọi đến Bảng Load_DATA21
        public ActionResult ListDetailedAirwayTransport21(String date, int way)
        {
            AirwayTransportRepository airwaytransportRepository = new AirwayTransportRepository();
            ReturnAirwayTransport returnairwaytransport = new ReturnAirwayTransport();
            returnairwaytransport = airwaytransportRepository.LOAD_DATA21(common.DateToInt(date), way);
            return View(returnairwaytransport.ListAirwayTransportReport);
        }

        //Phần Controller gọi đến Bảng Load_DATA22
        public ActionResult ListDetailedAirwayTransport22(String date, int way)
        {
            AirwayTransportRepository airwaytransportRepository = new AirwayTransportRepository();
            ReturnAirwayTransport returnairwaytransport = new ReturnAirwayTransport();
            returnairwaytransport = airwaytransportRepository.LOAD_DATA22(common.DateToInt(date), way);
            return View(returnairwaytransport.ListAirwayTransportReport);
        }

        //Phần Controller gọi đến Bảng Load_DATA23
        public ActionResult ListDetailedAirwayTransport23(String date, int way)
        {
            AirwayTransportRepository airwaytransportRepository = new AirwayTransportRepository();
            ReturnAirwayTransport returnairwaytransport = new ReturnAirwayTransport();
            returnairwaytransport = airwaytransportRepository.LOAD_DATA23(common.DateToInt(date), way);
            return View(returnairwaytransport.ListAirwayTransportReport);
        }

        //Phần Controller gọi đến Bảng Load_DATA24
        public ActionResult ListDetailedAirwayTransport24(String date, int way)
        {
            AirwayTransportRepository airwaytransportRepository = new AirwayTransportRepository();
            ReturnAirwayTransport returnairwaytransport = new ReturnAirwayTransport();
            returnairwaytransport = airwaytransportRepository.LOAD_DATA24(common.DateToInt(date), way);
            return View(returnairwaytransport.ListAirwayTransportReport);
        }

        //Phần Controller gọi đến Bảng Load_DATA25
        public ActionResult ListDetailedAirwayTransport25(String date, int way)
        {
            AirwayTransportRepository airwaytransportRepository = new AirwayTransportRepository();
            ReturnAirwayTransport returnairwaytransport = new ReturnAirwayTransport();
            returnairwaytransport = airwaytransportRepository.LOAD_DATA25(common.DateToInt(date), way);
            return View(returnairwaytransport.ListAirwayTransportReport);
        }

        //Phần Controller gọi đến Bảng Load_DATA26
        public ActionResult ListDetailedAirwayTransport26(String date, int way)
        {
            AirwayTransportRepository airwaytransportRepository = new AirwayTransportRepository();
            ReturnAirwayTransport returnairwaytransport = new ReturnAirwayTransport();
            returnairwaytransport = airwaytransportRepository.LOAD_DATA26(common.DateToInt(date), way);
            return View(returnairwaytransport.ListAirwayTransportReport);
        }

        //Phần Controller gọi đến Bảng Load_DATA27
        public ActionResult ListDetailedAirwayTransport27(String date, int way)
        {
            AirwayTransportRepository airwaytransportRepository = new AirwayTransportRepository();
            ReturnAirwayTransport returnairwaytransport = new ReturnAirwayTransport();
            returnairwaytransport = airwaytransportRepository.LOAD_DATA27(common.DateToInt(date), way);
            return View(returnairwaytransport.ListAirwayTransportReport);
        }

        //Phần Controller gọi đến Bảng Load_DATA28
        public ActionResult ListDetailedAirwayTransport28(String date, int way)
        {
            AirwayTransportRepository airwaytransportRepository = new AirwayTransportRepository();
            ReturnAirwayTransport returnairwaytransport = new ReturnAirwayTransport();
            returnairwaytransport = airwaytransportRepository.LOAD_DATA28(common.DateToInt(date), way);
            return View(returnairwaytransport.ListAirwayTransportReport);
        }

        //Phần Controller gọi đến Bảng Load_DATA29
        public ActionResult ListDetailedAirwayTransport29(String date, int way)
        {
            AirwayTransportRepository airwaytransportRepository = new AirwayTransportRepository();
            ReturnAirwayTransport returnairwaytransport = new ReturnAirwayTransport();
            returnairwaytransport = airwaytransportRepository.LOAD_DATA29(common.DateToInt(date), way);
            return View(returnairwaytransport.ListAirwayTransportReport);
        }
    }
}