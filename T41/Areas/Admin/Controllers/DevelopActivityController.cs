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
    public class DevelopActivityController : Controller
    {
        //int page_size = int.Parse(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        Convertion common = new Convertion();
        // GET: Admin/AirwayTransport
        public ActionResult Index()
        {
            return View();
        }
        //Phần Controller gọi đến Bảng Load_DATA1
        public ActionResult DevelopActivityDetailReport()
        {
            return View();
        }


        public ActionResult ListDevelopActivity()
        {
            return View();
        }

        //Phần controller xử lý để lấy dữ liệu bảng KPI_SummingPassByMailRoute 
        [HttpGet]
        public JsonResult ListDevelopActivity_BDHN_DI_HCM(int workcenter, string AcceptDate, int arriveprovince, int arrivepartner )
        {
            long ARRIVEQUANTITY_LK = 0;
            decimal ARRIVEWEIGHT_KG_LK = 0;

            long LEAVEQUANTITY_LK = 0;
            decimal LEAVEWEIGHT_KG_LK = 0;
            decimal DAPUNGKL = 0;

            DevelopActivityRepository developactivityrepository = new DevelopActivityRepository();
            ReturnBDHN_DI_HCM returnBDHN_DI_HCM = new ReturnBDHN_DI_HCM();
            returnBDHN_DI_HCM = developactivityrepository.BDHN_DI_HCM( workcenter,  AcceptDate,  arriveprovince,  arrivepartner, ref ARRIVEQUANTITY_LK, ref ARRIVEWEIGHT_KG_LK, ref  LEAVEQUANTITY_LK, ref  LEAVEWEIGHT_KG_LK, ref DAPUNGKL);
            
            return Json(returnBDHN_DI_HCM, JsonRequestBehavior.AllowGet);
            
        }

    }
}