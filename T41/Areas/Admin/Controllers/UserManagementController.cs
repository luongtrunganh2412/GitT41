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
    public class UserManagementController : Controller
    {
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

        //Phần controller xử lý để thêm dữ liệu dưới database
        public ActionResult CreateUserReport(PARAMETER_BUSINESS para)
        {
            UserManagementRepository usermanagementRepository = new UserManagementRepository();
            ReturnUserManagement returnusermanagement = new ReturnUserManagement();
            returnusermanagement = usermanagementRepository.CreatBusinessProfile(para);
            return View(returnusermanagement);
        }

        //Phần controller xử lý để sửa dữ liệu dưới database
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

        //Phần show ra dữ liệu của bảng người dùng
        public ActionResult ListDetailedUserManagementReport()
        {
            
            UserManagementRepository usermanagementRepository = new UserManagementRepository();
            ReturnUserManagement returnusermanagement = new ReturnUserManagement();
            returnusermanagement = usermanagementRepository.USER_MANAGEMENT_DETAIL();
            
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