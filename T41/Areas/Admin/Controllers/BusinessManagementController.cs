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

        [HttpGet]
        //Phần show ra dữ liệu của bảng NGUOI_DUNG_SALE
        public ActionResult ListDetailedBusinessManagementReport(int? page)
        {
            int currentPageIndex = page.HasValue ? page.Value : 1;
            ViewBag.currentPageIndex = currentPageIndex;
            ViewBag.PageSize = page_size;
            BusinessManagementRepository businessmanagementRepository = new BusinessManagementRepository();
            ReturnBusinessManagement returnbusinessmanagement = new ReturnBusinessManagement();
            returnbusinessmanagement = businessmanagementRepository.BUSINESS_MANAGEMENT_Detail(page_size, currentPageIndex);
            ViewBag.total = returnbusinessmanagement.Total;
            ViewBag.total_page = (returnbusinessmanagement.Total + page_size - 1) / page_size;
            return View(returnbusinessmanagement);

        }


    }
}