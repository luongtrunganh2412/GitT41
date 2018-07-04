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
    public class AirwayTransportComeManagementController : Controller
    {
        Convertion common = new Convertion();
        // GET: Admin/AirwayTransport
        // GET: Admin/AirwayTransportManagement
        public ActionResult Index()
        {
            return View();
        }
        //Phần Controller gọi đến Bảng Load_DATA1
        public ActionResult AirwayTransportComeManagementDetailReport()
        {
            return View();
        }
        public ActionResult ListAirwayTransportComeManagement()
        {
            return View();
        }

        //Phần controller xử lý để thêm dữ liệu dưới database
        [HttpGet]
        public ActionResult CreateAirwaytransportComeManagementReport(string NGAY, int CHIEU, string TAICUNG_TH, string TAIMEM_TH, string GIOGIAO_TT, string GIOBAY_TT, string SOHIEUCHUYENBAY, string GIONHAN_TT, int ID_VNP)
        {
            AirwaytransportComeManagementRepository airwaytransportcomemanagementRepository = new AirwaytransportComeManagementRepository();
            ReturnAirwaytransportComeManagement returnairwaytransportcomemanagement = new ReturnAirwaytransportComeManagement();
            returnairwaytransportcomemanagement = airwaytransportcomemanagementRepository.InsertAirwaytransportComeManagement(common.DateToInt(NGAY), CHIEU, TAICUNG_TH, TAIMEM_TH, GIOGIAO_TT, GIOBAY_TT, SOHIEUCHUYENBAY, GIONHAN_TT, ID_VNP);
            return View(returnairwaytransportcomemanagement);

        }
    }
}