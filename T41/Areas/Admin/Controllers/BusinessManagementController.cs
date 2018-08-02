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

    public class BusinessManagementController : Controller
    {
        int page_size = int.Parse(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        Convertion common = new Convertion();
        // GET: Admin/BusinessManagement


        public ActionResult Index()
        {
            return View();
        }

        //Controller gọi đến phần view Báo Cáo Kinh Doanh
        public ActionResult BusinessManagementDetailReport()
        {
            return View();

        }
        //Controller lấy dữ liệu đơn vị
        public JsonResult UnitCode()
        {
            BusinessManagementRepository businessmanagementRepository = new BusinessManagementRepository();
            return Json(businessmanagementRepository.GETUNIT(), JsonRequestBehavior.AllowGet);
        }

        //Controller lấy dữ liệu dịch vụ
        public JsonResult ServiceCode()
        {
            BusinessManagementRepository businessmanagementRepository = new BusinessManagementRepository();
            return Json(businessmanagementRepository.GETSERVICE(), JsonRequestBehavior.AllowGet);
        }

        //Controller lấy dữ liệu bưu cục
        public JsonResult PosCode(int id_don_vi)
        {
            BusinessManagementRepository businessmanagementRepository = new BusinessManagementRepository();
            return Json(businessmanagementRepository.GET_BM_POSCODE(id_don_vi), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        //Phần show ra dữ liệu của bảng NGUOI_DUNG_SALE ĐÃ PHÂN TRANG
        public ActionResult ListDetailedBusinessManagementReport(int? page , int ma_don_vi, int ma_bc_khai_thac)
        {
            int currentPageIndex = page.HasValue ? page.Value : 1;
            ViewBag.currentPageIndex = currentPageIndex;
            ViewBag.PageSize = page_size;
            BusinessManagementRepository businessmanagementRepository = new BusinessManagementRepository();
            ReturnBusinessManagement returnbusinessmanagement = new ReturnBusinessManagement();
            returnbusinessmanagement = businessmanagementRepository.BUSINESS_MANAGEMENT_Detail(page_size, currentPageIndex, ma_don_vi, ma_bc_khai_thac);
            ViewBag.total = returnbusinessmanagement.Total;
            ViewBag.total_page = (returnbusinessmanagement.Total + page_size - 1) / page_size;
            return View(returnbusinessmanagement);

        }

        [HttpGet]
        //Phần show ra dữ liệu của bảng NGUOI_DUNG_SALE CHƯA PHÂN TRANG
        public ActionResult ListDetailedBusinessManagement2Report(int ma_don_vi, int ma_bc_khai_thac, int ngay_xac_dinh_khach_hang, string tu_ngay, string den_ngay)
        {
            BusinessManagementRepository businessmanagementRepository = new BusinessManagementRepository();
            ReturnBusinessManagement returnbusinessmanagement = new ReturnBusinessManagement();
            returnbusinessmanagement = businessmanagementRepository.BUSINESS_MANAGEMENT_2_Detail(ma_don_vi, ma_bc_khai_thac, ngay_xac_dinh_khach_hang, common.DateToInt(tu_ngay), common.DateToInt(den_ngay));
            return View(returnbusinessmanagement);

        }

        [HttpGet]
        //Phần show ra dữ liệu của tổng chân trang
        public ActionResult ListSumBusinessManagementReport(int ma_don_vi, int ma_bc_khai_thac, int ngay_xac_dinh_khach_hang, string tu_ngay, string den_ngay)
        {
            BusinessManagementRepository businessmanagementRepository = new BusinessManagementRepository();
            ReturnBusinessManagement returnbusinessmanagement = new ReturnBusinessManagement();
            returnbusinessmanagement = businessmanagementRepository.SUM_BUSINESS_MANAGEMENT_Detail(ma_don_vi, ma_bc_khai_thac, ngay_xac_dinh_khach_hang, common.DateToInt(tu_ngay), common.DateToInt(den_ngay));
            return View(returnbusinessmanagement);

        }


    }
}