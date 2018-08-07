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
    public class AirwayTransportComeController : Controller
    {
        Convertion common = new Convertion();
        // GET: Admin/AirwayTransport
        public ActionResult Index()
        {
            return View();
        }
        //Phần Controller gọi đến Bảng Load_DATA1
        public ActionResult AirwayTransportComeDetailReport()
        {
            return View();
        }
        public ActionResult ListAirwayTransportCome()
        {
            return View();
        }

        //Phần Controller gọi đến Bảng TOTAL_DATA
        public ActionResult TotalDataReport(String date)
        {
            AirwayTransportComeRepository AirwayTransportComeRepository = new AirwayTransportComeRepository();
            ReturnAirwayTransportCome ReturnAirwayTransportCome = new ReturnAirwayTransportCome();
            ReturnAirwayTransportCome = AirwayTransportComeRepository.TOTAL_DATA(common.DateToInt(date));
            return View(ReturnAirwayTransportCome.ListAirwayTransportComeReport);
        }

        //Phần Controller gọi đến Bảng Load_DATA1
        public ActionResult ListDetailedAirwayTransportCome1(String date, int way)
        {
            AirwayTransportComeRepository AirwayTransportComeRepository = new AirwayTransportComeRepository();
            ReturnAirwayTransportCome ReturnAirwayTransportCome = new ReturnAirwayTransportCome();
            ReturnAirwayTransportCome = AirwayTransportComeRepository.LOAD_DATA1(common.DateToInt(date),way );
            return View(ReturnAirwayTransportCome.ListAirwayTransportComeReport);
        }

        //Phần Controller gọi đến Bảng Load_DATA2
        public ActionResult ListDetailedAirwayTransportCome2(String date, int way)
        {
            AirwayTransportComeRepository AirwayTransportComeRepository = new AirwayTransportComeRepository();
            ReturnAirwayTransportCome ReturnAirwayTransportCome = new ReturnAirwayTransportCome();
            ReturnAirwayTransportCome = AirwayTransportComeRepository.LOAD_DATA2(common.DateToInt(date), way);
            return View(ReturnAirwayTransportCome.ListAirwayTransportComeReport);
        }

        //Phần Controller gọi đến Bảng Load_DATA3
        public ActionResult ListDetailedAirwayTransportCome3(String date, int way)
        {
            AirwayTransportComeRepository AirwayTransportComeRepository = new AirwayTransportComeRepository();
            ReturnAirwayTransportCome ReturnAirwayTransportCome = new ReturnAirwayTransportCome();
            ReturnAirwayTransportCome = AirwayTransportComeRepository.LOAD_DATA3(common.DateToInt(date), way);
            return View(ReturnAirwayTransportCome.ListAirwayTransportComeReport);
        }

        //Phần Controller gọi đến Bảng Load_DATA4
        public ActionResult ListDetailedAirwayTransportCome4(String date, int way)
        {
            AirwayTransportComeRepository AirwayTransportComeRepository = new AirwayTransportComeRepository();
            ReturnAirwayTransportCome ReturnAirwayTransportCome = new ReturnAirwayTransportCome();
            ReturnAirwayTransportCome = AirwayTransportComeRepository.LOAD_DATA4(common.DateToInt(date), way);
            return View(ReturnAirwayTransportCome.ListAirwayTransportComeReport);
        }

        //Phần Controller gọi đến Bảng Load_DATA5
        public ActionResult ListDetailedAirwayTransportCome5(String date, int way)
        {
            AirwayTransportComeRepository AirwayTransportComeRepository = new AirwayTransportComeRepository();
            ReturnAirwayTransportCome ReturnAirwayTransportCome = new ReturnAirwayTransportCome();
            ReturnAirwayTransportCome = AirwayTransportComeRepository.LOAD_DATA5(common.DateToInt(date), way);
            return View(ReturnAirwayTransportCome.ListAirwayTransportComeReport);
        }

        //Phần Controller gọi đến Bảng Load_DATA6
        public ActionResult ListDetailedAirwayTransportCome6(String date, int way)
        {
            AirwayTransportComeRepository AirwayTransportComeRepository = new AirwayTransportComeRepository();
            ReturnAirwayTransportCome ReturnAirwayTransportCome = new ReturnAirwayTransportCome();
            ReturnAirwayTransportCome = AirwayTransportComeRepository.LOAD_DATA6(common.DateToInt(date), way);
            return View(ReturnAirwayTransportCome.ListAirwayTransportComeReport);
        }

        //Phần Controller gọi đến Bảng Load_DATA7
        public ActionResult ListDetailedAirwayTransportCome7(String date, int way)
        {
            AirwayTransportComeRepository AirwayTransportComeRepository = new AirwayTransportComeRepository();
            ReturnAirwayTransportCome ReturnAirwayTransportCome = new ReturnAirwayTransportCome();
            ReturnAirwayTransportCome = AirwayTransportComeRepository.LOAD_DATA7(common.DateToInt(date), way);
            return View(ReturnAirwayTransportCome.ListAirwayTransportComeReport);
        }

        //Phần Controller gọi đến Bảng Load_DATA8
        public ActionResult ListDetailedAirwayTransportCome8(String date, int way)
        {
            AirwayTransportComeRepository AirwayTransportComeRepository = new AirwayTransportComeRepository();
            ReturnAirwayTransportCome ReturnAirwayTransportCome = new ReturnAirwayTransportCome();
            ReturnAirwayTransportCome = AirwayTransportComeRepository.LOAD_DATA8(common.DateToInt(date), way);
            return View(ReturnAirwayTransportCome.ListAirwayTransportComeReport);
        }

        //Phần Controller gọi đến Bảng Load_DATA9
        public ActionResult ListDetailedAirwayTransportCome9(String date, int way)
        {
            AirwayTransportComeRepository AirwayTransportComeRepository = new AirwayTransportComeRepository();
            ReturnAirwayTransportCome ReturnAirwayTransportCome = new ReturnAirwayTransportCome();
            ReturnAirwayTransportCome = AirwayTransportComeRepository.LOAD_DATA9(common.DateToInt(date), way);
            return View(ReturnAirwayTransportCome.ListAirwayTransportComeReport);
        }

        //Phần Controller gọi đến Bảng Load_DATA10
        public ActionResult ListDetailedAirwayTransportCome10(String date, int way)
        {
            AirwayTransportComeRepository AirwayTransportComeRepository = new AirwayTransportComeRepository();
            ReturnAirwayTransportCome ReturnAirwayTransportCome = new ReturnAirwayTransportCome();
            ReturnAirwayTransportCome = AirwayTransportComeRepository.LOAD_DATA10(common.DateToInt(date), way);
            return View(ReturnAirwayTransportCome.ListAirwayTransportComeReport);
        }

        //Phần Controller gọi đến Bảng Load_DATA11
        public ActionResult ListDetailedAirwayTransportCome11(String date, int way)
        {
            AirwayTransportComeRepository AirwayTransportComeRepository = new AirwayTransportComeRepository();
            ReturnAirwayTransportCome ReturnAirwayTransportCome = new ReturnAirwayTransportCome();
            ReturnAirwayTransportCome = AirwayTransportComeRepository.LOAD_DATA11(common.DateToInt(date), way);
            return View(ReturnAirwayTransportCome.ListAirwayTransportComeReport);
        }

        //Phần Controller gọi đến Bảng Load_DATA12
        public ActionResult ListDetailedAirwayTransportCome12(String date, int way)
        {
            AirwayTransportComeRepository AirwayTransportComeRepository = new AirwayTransportComeRepository();
            ReturnAirwayTransportCome ReturnAirwayTransportCome = new ReturnAirwayTransportCome();
            ReturnAirwayTransportCome = AirwayTransportComeRepository.LOAD_DATA12(common.DateToInt(date), way);
            return View(ReturnAirwayTransportCome.ListAirwayTransportComeReport);
        }

        //Phần Controller gọi đến Bảng Load_DATA13
        public ActionResult ListDetailedAirwayTransportCome13(String date, int way)
        {
            AirwayTransportComeRepository AirwayTransportComeRepository = new AirwayTransportComeRepository();
            ReturnAirwayTransportCome ReturnAirwayTransportCome = new ReturnAirwayTransportCome();
            ReturnAirwayTransportCome = AirwayTransportComeRepository.LOAD_DATA13(common.DateToInt(date), way);
            return View(ReturnAirwayTransportCome.ListAirwayTransportComeReport);
        }

        //Phần Controller gọi đến Bảng Load_DATA14
        public ActionResult ListDetailedAirwayTransportCome14(String date, int way)
        {
            AirwayTransportComeRepository AirwayTransportComeRepository = new AirwayTransportComeRepository();
            ReturnAirwayTransportCome ReturnAirwayTransportCome = new ReturnAirwayTransportCome();
            ReturnAirwayTransportCome = AirwayTransportComeRepository.LOAD_DATA14(common.DateToInt(date), way);
            return View(ReturnAirwayTransportCome.ListAirwayTransportComeReport);
        }

        //Phần Controller gọi đến Bảng Load_DATA15
        public ActionResult ListDetailedAirwayTransportCome15(String date, int way)
        {
            AirwayTransportComeRepository AirwayTransportComeRepository = new AirwayTransportComeRepository();
            ReturnAirwayTransportCome ReturnAirwayTransportCome = new ReturnAirwayTransportCome();
            ReturnAirwayTransportCome = AirwayTransportComeRepository.LOAD_DATA15(common.DateToInt(date), way);
            return View(ReturnAirwayTransportCome.ListAirwayTransportComeReport);
        }

        //Phần Controller gọi đến Bảng Load_DATA16
        public ActionResult ListDetailedAirwayTransportCome16(String date, int way)
        {
            AirwayTransportComeRepository AirwayTransportComeRepository = new AirwayTransportComeRepository();
            ReturnAirwayTransportCome ReturnAirwayTransportCome = new ReturnAirwayTransportCome();
            ReturnAirwayTransportCome = AirwayTransportComeRepository.LOAD_DATA16(common.DateToInt(date), way);
            return View(ReturnAirwayTransportCome.ListAirwayTransportComeReport);
        }

        //Phần Controller gọi đến Bảng Load_DATA17
        public ActionResult ListDetailedAirwayTransportCome17(String date, int way)
        {
            AirwayTransportComeRepository AirwayTransportComeRepository = new AirwayTransportComeRepository();
            ReturnAirwayTransportCome ReturnAirwayTransportCome = new ReturnAirwayTransportCome();
            ReturnAirwayTransportCome = AirwayTransportComeRepository.LOAD_DATA17(common.DateToInt(date), way);
            return View(ReturnAirwayTransportCome.ListAirwayTransportComeReport);
        }

        //Phần Controller gọi đến Bảng Load_DATA18
        public ActionResult ListDetailedAirwayTransportCome18(String date, int way)
        {
            AirwayTransportComeRepository AirwayTransportComeRepository = new AirwayTransportComeRepository();
            ReturnAirwayTransportCome ReturnAirwayTransportCome = new ReturnAirwayTransportCome();
            ReturnAirwayTransportCome = AirwayTransportComeRepository.LOAD_DATA18(common.DateToInt(date), way);
            return View(ReturnAirwayTransportCome.ListAirwayTransportComeReport);
        }

        //Phần Controller gọi đến Bảng Load_DATA19
        public ActionResult ListDetailedAirwayTransportCome19(String date, int way)
        {
            AirwayTransportComeRepository AirwayTransportComeRepository = new AirwayTransportComeRepository();
            ReturnAirwayTransportCome ReturnAirwayTransportCome = new ReturnAirwayTransportCome();
            ReturnAirwayTransportCome = AirwayTransportComeRepository.LOAD_DATA19(common.DateToInt(date), way);
            return View(ReturnAirwayTransportCome.ListAirwayTransportComeReport);
        }

        //Phần Controller gọi đến Bảng Load_DATA20
        public ActionResult ListDetailedAirwayTransportCome20(String date, int way)
        {
            AirwayTransportComeRepository AirwayTransportComeRepository = new AirwayTransportComeRepository();
            ReturnAirwayTransportCome ReturnAirwayTransportCome = new ReturnAirwayTransportCome();
            ReturnAirwayTransportCome = AirwayTransportComeRepository.LOAD_DATA20(common.DateToInt(date), way);
            return View(ReturnAirwayTransportCome.ListAirwayTransportComeReport);
        }

        
    }
}