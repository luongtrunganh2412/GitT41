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
    public class AirwayTransportManagementController : Controller
    {
        Convertion common = new Convertion();
        // GET: Admin/AirwayTransportManagement
        public ActionResult Index()
        {
            return View();
        }
        //Phần Controller gọi đến Bảng Load_DATA1
        public ActionResult AirwayTransportManagementDetailReport()
        {
            return View();
        }
        public ActionResult ListAirwayTransportManagement()
        {
            return View();
        }

        //Phần controller xử lý để thêm dữ liệu dưới database
        [HttpGet]
        public ActionResult CreateAirwaytransportManagementReport(string NGAY, int CHIEU, string TAICUNG_TH, string TAIMEM_TH, string GIOGIAO_TT, string GIOBAY_TT, string SOHIEUCHUYENBAY, string GIONHAN_TT, int ID_VNP)
        {
            AirwaytransportManagementRepository airwaytransportmanagementRepository = new AirwaytransportManagementRepository();
            ReturnAirwaytransportManagement returnairwaytransportmanagement = new ReturnAirwaytransportManagement();
            returnairwaytransportmanagement = airwaytransportmanagementRepository.InsertAirwaytransportManagement(common.DateToInt(NGAY), CHIEU, TAICUNG_TH, TAIMEM_TH, GIOGIAO_TT, GIOBAY_TT, SOHIEUCHUYENBAY, GIONHAN_TT, ID_VNP);
            return View(returnairwaytransportmanagement);
            
        }
    }
}