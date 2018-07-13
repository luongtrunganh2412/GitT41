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

    public class TransferManagementVNPOSTController : Controller
    {
        Convertion common = new Convertion();
        
        public ActionResult Index()
        {
            return View();
        }
        //Controller gọi đến phần view Tổng hợp sản lượng đi phát
        public ActionResult TransferManagementVNPOSTDetailReport()
        {
            return View();

        }

        public ActionResult E2_TransferManagementVNPOSTDetailReport()
        {
            return View();

        }
        public ActionResult MCT_E2_TransferManagementVNPOSTDetailReport()
        {
            return View();

        }

        public ActionResult SOTUITRONGCHTHUVNPOST()
        {
            return View();

        }
        //Controller lấy dữ liệu tỉnh đóng, tỉnh nhận
        public JsonResult ProvinceCode()
        {
            TransferManagementVNPOSTRepository transfermanagementvnpostRepository = new TransferManagementVNPOSTRepository();
            return Json(transfermanagementvnpostRepository.GETPROVINCE(), JsonRequestBehavior.AllowGet);
        }

        //Controller lấy dữ liệu bưu cục đóng, bưu cục nhận
        public JsonResult PosCode(int province)
        {
            TransferManagementVNPOSTRepository transfermanagementvnpostRepository = new TransferManagementVNPOSTRepository();
            return Json(transfermanagementvnpostRepository.GetCRPOSCODE(province), JsonRequestBehavior.AllowGet);
            //  return Json(apiRepository.ListPostCode(), JsonRequestBehavior.AllowGet);
        }

        //Controller gọi đến chi tiết của bảng tổng hợp sản lượng đi phát
        public ActionResult ListDetailedTransferManagementVNPOSTReport(int fromprovince, int toprovince, int fromposcode, int toposcode, string fromdate, string todate, int typecomunication)
        {
            ViewBag.typecomunication = typecomunication;

            TransferManagementVNPOSTRepository transfermanagementvnpostRepository = new TransferManagementVNPOSTRepository();
            ReturnTransferManagement_VNPOST returntransfermanagementvnpost = new ReturnTransferManagement_VNPOST();
            returntransfermanagementvnpost = transfermanagementvnpostRepository.TRANSFER_MANAGEMENT_VNPOST_DETAIL(fromprovince, toprovince, fromposcode, toposcode, fromdate, todate,  typecomunication);
            //return View(returntransfermanagement.ListTransferManagementReport);
            return View(returntransfermanagementvnpost);

        }
        //Controller gọi đến chi tiết theo từng chuyến thư hoặc túi số của bảng chi tiết
        public ActionResult ListTransferManagement_CTTS_VNPOST_Report(int fromposcode, int toposcode, string date, int type, int typecomunication)
        {
            TransferManagementVNPOSTRepository transfermanagementvnpostRepository = new TransferManagementVNPOSTRepository();
            ReturnTransferManagement_VNPOST returntransfermanagementvnpost = new ReturnTransferManagement_VNPOST();
            returntransfermanagementvnpost = transfermanagementvnpostRepository.TRANSFER_MANAGEMENT_CT_TS_VNPOST_DETAIL(fromposcode, toposcode, common.DateToInt(date), type, typecomunication);
            return Json(returntransfermanagementvnpost, JsonRequestBehavior.AllowGet);
            
        }

        //Controller gọi đến chi tiết số túi theo từng chuyến thư của bảng chi tiết
        public ActionResult ListTransferManagement_SOTUI_VNPOST_Report(int fromposcode, int toposcode, string date, int mailtrip, int typecomunication)
        {
            ViewBag.typecomunication = typecomunication;

            TransferManagementVNPOSTRepository transfermanagementvnpostRepository = new TransferManagementVNPOSTRepository();
            ReturnTransferManagement_VNPOST returntransfermanagementvnpost = new ReturnTransferManagement_VNPOST();
            returntransfermanagementvnpost = transfermanagementvnpostRepository.TRANSFER_MANAGEMENT_SOTUI_VNPOST_DETAIL(fromposcode, toposcode, common.DateToInt(date), mailtrip, typecomunication);
            return View(returntransfermanagementvnpost);

        }

        //Controller gọi đến chi tiết của bảng in bản kê E2 theo chuyến thư túi số
        public ActionResult ListDetailedTransferManagement_E2_VNPOST_Report(int fromposcode, int toposcode, string date, int mailtrip, int postbag, int typecomunication)
        {
            TransferManagementVNPOSTRepository transfermanagementvnpostRepository = new TransferManagementVNPOSTRepository();
            ReturnTransferManagement_VNPOST returntransfermanagementvnpost = new ReturnTransferManagement_VNPOST();
            returntransfermanagementvnpost = transfermanagementvnpostRepository.TRANSFER_MANAGEMENT_E2_VNPOST_DETAIL(fromposcode, toposcode, common.DateToInt(date), mailtrip, postbag, typecomunication);
            return View(returntransfermanagementvnpost);

        }

        //Controller gọi đến chuyến thư túi số trong phần in bản kê E2 theo chuyến thư túi số
        public ActionResult ListDetailedTransferManagement_CTTS_E2_VNPOST_Report(int fromposcode, int toposcode, string date, int mailtrip, int postbag, int typecomunication)
        {

            TransferManagementVNPOSTRepository transfermanagementvnpostRepository = new TransferManagementVNPOSTRepository();
            ReturnTransferManagement_VNPOST returntransfermanagementvnpost = new ReturnTransferManagement_VNPOST();
            returntransfermanagementvnpost = transfermanagementvnpostRepository.TRANSFER_MANAGEMENT_CTTS_E2_VNPOST_DETAIL(fromposcode, toposcode, common.DateToInt(date), mailtrip, postbag, typecomunication);
            
            return View(returntransfermanagementvnpost);
            
        }

        //Controller gọi đến chi tiết của bảng in bản kê E2 theo mã cổ túi
        public ActionResult ListDetailedTransferManagement_MCT_E2_VNPOST_Report(int fromposcode, int toposcode, int date, int mailtrip, int postbag, int typecomunication)
        {

            TransferManagementVNPOSTRepository transfermanagementvnpostRepository = new TransferManagementVNPOSTRepository();
            ReturnTransferManagement_VNPOST returntransfermanagementvnpost = new ReturnTransferManagement_VNPOST();
            returntransfermanagementvnpost = transfermanagementvnpostRepository.TRANSFER_MANAGEMENT_MCT_E2_VNPOST_DETAIL(fromposcode, toposcode, date, mailtrip, postbag, typecomunication);
            return View(returntransfermanagementvnpost);

        }

        //Controller gọi đến chuyến thư túi số trong phần in bản kê E2 theo mã cổ túi
        public ActionResult ListDetailedTransferManagement_CTTS_MCT_E2_VNPOST_Report(int fromposcode, int toposcode, int date, int mailtrip, int postbag,int typecomunication)
        {

            TransferManagementVNPOSTRepository transfermanagementvnpostRepository = new TransferManagementVNPOSTRepository();
            ReturnTransferManagement_VNPOST returntransfermanagementvnpost = new ReturnTransferManagement_VNPOST();
            returntransfermanagementvnpost = transfermanagementvnpostRepository.TRANSFER_MANAGEMENT_CTTS_MCT_E2_VNPOST_DETAIL(fromposcode, toposcode, date, mailtrip, postbag, typecomunication);

            return View(returntransfermanagementvnpost);

        }
    }
}