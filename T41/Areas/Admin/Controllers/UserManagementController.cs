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
    public class UserManagementController : Controller
    {
        int page_size = int.Parse(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        // GET: Admin/UserManagement
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserManagementDetailReport()
        {
            return View();
        }

        public ActionResult ID_USER_MANAGEMENT_DETAIL()
        {
            return View();
        }

        //Controller lấy dữ liệu tỉnh đóng, tỉnh nhận
        public JsonResult ProvinceCode()
        {
            UserManagementRepository usermanagementRepository = new UserManagementRepository();
            return Json(usermanagementRepository.GETPROVINCE(), JsonRequestBehavior.AllowGet);
        }

        //Controller lấy dữ liệu quận
        public JsonResult DistrictCode(int provincecode)
        {
            UserManagementRepository usermanagementRepository = new UserManagementRepository();
            return Json(usermanagementRepository.GETDISTRICT(provincecode), JsonRequestBehavior.AllowGet);
        }

        //Phần controller xử lý để thêm dữ liệu dưới database
        [HttpGet]
        public ActionResult CreateUserReport(PARAMETER_BUSINESS para)
        {
            UserManagementRepository usermanagementRepository = new UserManagementRepository();
            ReturnUserManagement returnusermanagement = new ReturnUserManagement();
            returnusermanagement = usermanagementRepository.CreatBusinessProfile(para);
            return View(returnusermanagement);
        }

        //Phần controller xử lý để sửa dữ liệu dưới database
        [HttpGet]
        public ActionResult EditUserReport(PARAMETER_BUSINESS business)
        {
            UserManagementRepository usermanagementRepository = new UserManagementRepository();
            ReturnUserManagement returnusermanagement = new ReturnUserManagement();
            returnusermanagement = usermanagementRepository.EditBusinessProfile(business);
            return View(returnusermanagement);
        }

        //Phần controller xử lý để xóa dữ liệu người dùng theo ID
        public ActionResult DeleteUserReport(int delete_id)
        {
            UserManagementRepository usermanagementRepository = new UserManagementRepository();
            ReturnUserManagement returnusermanagement = new ReturnUserManagement();
            returnusermanagement = usermanagementRepository.DeleteBusinessProfile(delete_id);
            return View(returnusermanagement);
        }
        public ActionResult CreateUser()
        {
            return View();
        }


        public ActionResult EditUser()
        {
            return View();
        }

        public ActionResult DeleteUser()
        {
            return View();
        }
        [HttpGet]
        //Phần show ra dữ liệu của bảng người dùng
        public ActionResult ListDetailedUserManagementReport(int? page, int user_id, string user_customer_code, int user_contact_phone_work)
        {
            int currentPageIndex = page.HasValue ? page.Value : 1;
            ViewBag.currentPageIndex = currentPageIndex;
            ViewBag.PageSize = page_size;
            UserManagementRepository usermanagementRepository = new UserManagementRepository();
            ReturnUserManagement returnusermanagement = new ReturnUserManagement();
            returnusermanagement = usermanagementRepository.USER_MANAGEMENT_DETAIL(page_size, currentPageIndex,user_id , user_customer_code, user_contact_phone_work);
            ViewBag.total = returnusermanagement.Total;
            ViewBag.total_page = (returnusermanagement.Total + page_size - 1) / page_size;
            return View(returnusermanagement);
            
        }

        //Phần controller show ra dữ liệu theo ID người dùng
        public ActionResult ListDetailed_ID_UserManagementReport(int edit_id)
        {
            UserManagementRepository usermanagementRepository = new UserManagementRepository();
            ReturnUserManagement returnusermanagement = new ReturnUserManagement();
            returnusermanagement = usermanagementRepository.ID_USER_MANAGEMENT_DETAIL(edit_id);
            return View(returnusermanagement);
        }

    }
}