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

        public ActionResult CreateSaleUser()
        {
            return View();
        }

        public ActionResult ID_SALE_USER_MANAGEMENT_DETAIL()
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
        
        //Phần controller xử lý để thêm dữ liệu vào bảng nguoi_dung_sale dưới database
        [HttpGet]
        public ActionResult CreatNguoiDungSaleProfile(PARAMETER_NGUOI_DUNG_SALE para)
        {
            SaleUserManagementRepository saleusermanagementRepository = new SaleUserManagementRepository();
            ReturnSaleUserManagement returnsaleusermanagement = new ReturnSaleUserManagement();
            returnsaleusermanagement = saleusermanagementRepository.CreatNguoiDungSaleProfile(para);
            return RedirectToAction("SaleUserManagementDetailReport");
            //return View(returnsaleusermanagement);
        }

        //Phần controller xử lý để lấy dữ liệu bảng nguoi_dung_sale theo ID
        [HttpGet]
        public JsonResult GetNguoiDungSaleProfile(int id_nguoi_dung)
        {
            SaleUserManagementRepository saleusermanagementRepository = new SaleUserManagementRepository();
            ReturnSaleUserManagement returnsaleusermanagement = new ReturnSaleUserManagement();
            returnsaleusermanagement = saleusermanagementRepository.SALE_USER_MANAGEMENT_BYID_DETAIL(id_nguoi_dung);
            return Json(returnsaleusermanagement.ListSaleUserManagement_Report, JsonRequestBehavior.AllowGet);
            
        }

        //Phần controller xử lý để sửa dữ liệu vào bảng nguoi_dung_sale dưới database
        [HttpGet]
        public ActionResult EditNguoiDungSaleProfile(PARAMETER_NGUOI_DUNG_SALE para)
        {
            SaleUserManagementRepository saleusermanagementRepository = new SaleUserManagementRepository();
            ReturnSaleUserManagement returnsaleusermanagement = new ReturnSaleUserManagement();
            returnsaleusermanagement = saleusermanagementRepository.EditNguoiDungSaleProfile(para);
            return RedirectToAction("SaleUserManagementDetailReport");
            
        }

        //Phần controller xử lý để xóa dữ liệu vào bảng nguoi_dung_sale dưới database
        [HttpGet]
        public ActionResult DeleteNguoiDungSaleProfile(int delete_id)
        {
            SaleUserManagementRepository saleusermanagementRepository = new SaleUserManagementRepository();
            ReturnSaleUserManagement returnsaleusermanagement = new ReturnSaleUserManagement();
            returnsaleusermanagement = saleusermanagementRepository.DeleteNguoiDungSaleProfile(delete_id);
            return RedirectToAction("SaleUserManagementDetailReport");

        }

    }
}