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
    public class SaleUserManagementController : Controller
    {
        int page_size = int.Parse(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        // GET: Admin/SaleUserManagement
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SaleUserManagementDetailReport()
        {
            return View();
        }

        [HttpGet]
        //Phần show ra dữ liệu của bảng NGUOI_DUNG_SALE
        public ActionResult ListDetailedSaleUserManagementReport(int? page, int id_nguoi_dung, int id_don_vi, int dien_thoai, string email)
        {
            int currentPageIndex = page.HasValue ? page.Value : 1;
            ViewBag.currentPageIndex = currentPageIndex;
            ViewBag.PageSize = page_size;
            SaleUserManagementRepository saleusermanagementRepository = new SaleUserManagementRepository();
            ReturnSaleUserManagement returnsaleusermanagement = new ReturnSaleUserManagement();
            returnsaleusermanagement = saleusermanagementRepository.SALE_USER_MANAGEMENT_DETAIL(page_size, currentPageIndex,id_nguoi_dung,id_don_vi,dien_thoai,email);
            ViewBag.total = returnsaleusermanagement.Total;
            ViewBag.total_page = (returnsaleusermanagement.Total + page_size - 1) / page_size;
            return View(returnsaleusermanagement);

        }



    }
}