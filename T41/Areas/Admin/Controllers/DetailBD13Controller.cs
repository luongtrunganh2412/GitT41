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
    public class DetailBD13Controller : Controller
    {
        int page_size = int.Parse(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        Convertion common = new Convertion();
        // GET: Admin/UserManagement
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BD13DetailReport()
        {
            return View();
        }

        //Phần show ra dữ liệu của Bưu Cục Phát
        public JsonResult ListPostCode()
        {
            DetailBD13Repository bd13Repository = new DetailBD13Repository();
            return Json(bd13Repository.GetAllDeliveryPostCode(), JsonRequestBehavior.AllowGet);
            
        }

        //Phần show ra dữ liệu của Tuyến Phát theo bưu cục phát
        public JsonResult ListDeliveryRouteByPostCode(int delivery_post_code)
        {
            DetailBD13Repository bd13Repository = new DetailBD13Repository();
            return Json(bd13Repository.GetDeliveryRouteCodeByDeliveryCode(delivery_post_code), JsonRequestBehavior.AllowGet);
            
        }

        //Phần show ra dữ liệu của bảng người dùng
        public ActionResult ListDetailedBD13Report(int? page, int mabc_kt, int mabc, string ngay, int cakt, int chthu, int tuiso)
        {
            int currentPageIndex = page.HasValue ? page.Value : 1;
            ViewBag.currentPageIndex = currentPageIndex;
            ViewBag.PageSize = page_size;
            DetailBD13Repository bd13Repository = new DetailBD13Repository();
            ReturnBD13 returnbd13 = new ReturnBD13();
            returnbd13 = bd13Repository.BD13_DI_DETAIL(currentPageIndex, page_size,  mabc_kt, mabc, common.DateToInt(ngay), cakt, chthu, tuiso);
            ViewBag.total = returnbd13.Total;
            ViewBag.total_page = (returnbd13.Total + page_size - 1) / page_size;
            return View(returnbd13);

        }

        
    }
}