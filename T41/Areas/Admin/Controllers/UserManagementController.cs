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

        //Phần controller xử lý để thêm dữ liệu vào bảng business_profile_oa dưới database
        [HttpGet]
        public ActionResult CreateUserReport(PARAMETER_BUSINESS_OA para)
        {
            UserManagementRepository usermanagementRepository = new UserManagementRepository();
            ReturnUserManagement returnusermanagement = new ReturnUserManagement();
            returnusermanagement = usermanagementRepository.CreatBusinessProfile_OA(para);
            return View(returnusermanagement);
        }

        //Phần controller xử lý để thêm dữ liệu vào bảng business_profile dưới database
        [HttpGet]
        public ActionResult CreateChannelReport(PARAMETER_BUSINESS para)
        {
            UserManagementRepository usermanagementRepository = new UserManagementRepository();
            ReturnUserManagement returnusermanagement = new ReturnUserManagement();
            returnusermanagement = usermanagementRepository.CreatBusinessProfile(para);
            return View(returnusermanagement);
        }

        //Phần controller xử lý để sửa dữ liệu bảng BUSINESS_PROFILE_OA dưới database
        [HttpGet]
        public ActionResult EditUserReport(PARAMETER_BUSINESS_OA business)
        {
            UserManagementRepository usermanagementRepository = new UserManagementRepository();
            ReturnUserManagement returnusermanagement = new ReturnUserManagement();
            returnusermanagement = usermanagementRepository.EditBusinessProfile_OA(business);
            return View(returnusermanagement);
        }

        //Phần controller xử lý để sửa dữ liệu bảng BUSINESS_PROFILE_OA dưới database
        [HttpGet]
        public ActionResult EditChannelReport(PARAMETER_BUSINESS business)
        {
            UserManagementRepository usermanagementRepository = new UserManagementRepository();
            ReturnUserManagement returnusermanagement = new ReturnUserManagement();
            returnusermanagement = usermanagementRepository.EditBusinessProfile(business);
            return View(returnusermanagement);
        }

        //Phần controller xử lý để sửa dữ liệu bảng BUSINESS_PROFILE dưới database
        [HttpGet]
        public ActionResult GET_DATA_BUSINESS_PROFILE(int get_data_id)
        {
            UserManagementRepository usermanagementRepository = new UserManagementRepository();
            ReturnUserManagement returnusermanagement = new ReturnUserManagement();
            returnusermanagement = usermanagementRepository.GET_DATA_BUSINESS_PROFILE(get_data_id);
            return Json(returnusermanagement.ListUserManagement_BP_Report, JsonRequestBehavior.AllowGet);
            
            //return Json(returnquality, JsonRequestBehavior.AllowGet);

        }

        //Phần controller xử lý để xóa dữ liệu người dùng theo ID trong bảng BUSINESS_PROFILE_OA
        public ActionResult DeleteUserReport(int delete_id)
        {
            UserManagementRepository usermanagementRepository = new UserManagementRepository();
            ReturnUserManagement returnusermanagement = new ReturnUserManagement();
            returnusermanagement = usermanagementRepository.DeleteBusinessProfile_OA(delete_id);
            return View(returnusermanagement);
        }

        //Phần controller xử lý để xóa dữ liệu người dùng theo ID trong bảng BUSINESS_PROFILE
        public ActionResult DeleteChannelReport(int delete_id)
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

        public ActionResult CreateChannel()
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
        //Phần show ra dữ liệu của bảng BUSINESS_PROFILE_OA
        public ActionResult ListDetailedUserManagementReport(int? page, int user_id, string user_customer_code, int user_contact_phone_work, string user_general_email)
        {
            int currentPageIndex = page.HasValue ? page.Value : 1;
            ViewBag.currentPageIndex = currentPageIndex;
            ViewBag.PageSize = page_size;
            UserManagementRepository usermanagementRepository = new UserManagementRepository();
            ReturnUserManagement returnusermanagement = new ReturnUserManagement();
            returnusermanagement = usermanagementRepository.USER_MANAGEMENT_DETAIL(page_size, currentPageIndex,user_id , user_customer_code, user_contact_phone_work, user_general_email);
            ViewBag.total = returnusermanagement.Total;
            ViewBag.total_page = (returnusermanagement.Total + page_size - 1) / page_size;
            return View(returnusermanagement);
            
        }

        //Phần controller show ra dữ liệu theo ID người dùng bảng BUSINESS_PROFILE_OA
        public ActionResult EDIT_ID_BP_OA_UserManagementReport(int edit_id)
        {
            UserManagementRepository usermanagementRepository = new UserManagementRepository();
            ReturnUserManagement returnusermanagement = new ReturnUserManagement();
            returnusermanagement = usermanagementRepository.ID_BUSINESS_PROFILE_OA_DETAIL(edit_id);
            return View(returnusermanagement);
        }

        //Phần controller show ra dữ liệu theo ID người dùng bảng BUSINESS_PROFILE
        public ActionResult ListDetailed_ID_BP_UserManagementReport(int customer_id)
        {
            UserManagementRepository usermanagementRepository = new UserManagementRepository();
            ReturnUserManagement returnusermanagement = new ReturnUserManagement();
            returnusermanagement = usermanagementRepository.ID_BUSINESS_PROFILE_DETAIL(customer_id);
            return View(returnusermanagement);
        }

    }
}