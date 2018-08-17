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

        //Phần controller xử lý để lấy dữ liệu bảng nguoi_dung_sale theo ID
        [HttpGet]
        public JsonResult ListDevelopActivity_GETID(int id_nguoi_dung)
        {
            SaleUserManagementRepository saleusermanagementRepository = new SaleUserManagementRepository();
            ReturnSaleUserManagement returnsaleusermanagement = new ReturnSaleUserManagement();
            returnsaleusermanagement = saleusermanagementRepository.SALE_USER_MANAGEMENT_BYID_DETAIL(id_nguoi_dung);
            return Json(returnsaleusermanagement, JsonRequestBehavior.AllowGet);

        }

    }
}