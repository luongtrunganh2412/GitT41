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
//EPPLUS library
using System.IO;
using System.Data;
using System.Drawing;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using OfficeOpenXml.Style;



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

        public ActionResult DevelopActivity_NOI_TINH_DetailReport()
        {
            return View();
        }


        public ActionResult ListDevelopActivity_NOI_TINH()
        {
            return View();
        }

        //Phần controller xử lý để lấy dữ liệu  LIÊN TỈNH
        [HttpGet]
        public JsonResult ListDevelopActivity_BDHN_DI_HCM(int workcenter, string AcceptDate, int arriveprovince, int arrivepartner )
        {
            long ARRIVEQUANTITY_LK = 0;
            decimal ARRIVEWEIGHT_KG_LK = 0;

            long LEAVEQUANTITY_LK = 0;
            decimal LEAVEWEIGHT_KG_LK = 0;
            decimal DAPUNGKL = 0;
            decimal DAPUNGLUYKE = 0;

            DevelopActivityRepository developactivityrepository = new DevelopActivityRepository();
            ReturnBDHN_DI_HCM returnBDHN_DI_HCM = new ReturnBDHN_DI_HCM();
            returnBDHN_DI_HCM = developactivityrepository.BDHN_DI_HCM( workcenter,  AcceptDate,  arriveprovince,  arrivepartner, ref ARRIVEQUANTITY_LK, ref ARRIVEWEIGHT_KG_LK, ref  LEAVEQUANTITY_LK, ref  LEAVEWEIGHT_KG_LK, ref DAPUNGKL, ref DAPUNGLUYKE);
            
            return Json(returnBDHN_DI_HCM, JsonRequestBehavior.AllowGet);
            
        }

        //Phần controller xử lý để lấy dữ liệu NỘI TỈNH
        [HttpGet]
        public JsonResult ListDevelopActivity_Action_NOI_TINH(int workcenter, string AcceptDate, int arriveprovince, int arrivepartner, int leaveprovince, int transitbag)
        {
            DevelopActivityRepository developactivityrepository = new DevelopActivityRepository();
            ReturnNOI_TINH returnNOI_TINH = new ReturnNOI_TINH();
            returnNOI_TINH = developactivityrepository.NOI_TINH( workcenter,  AcceptDate,  arriveprovince,  arrivepartner,  leaveprovince,  transitbag);

            return Json(returnNOI_TINH, JsonRequestBehavior.AllowGet);

        }

        //Phần trả về data theo list để xuất excel
        [HttpGet]
        public List<BDHN_DI_HCM> ReturnListExcel(int workcenter, string AcceptDate, int arriveprovince, int arrivepartner)
        {
            long ARRIVEQUANTITY_LK = 0;
            decimal ARRIVEWEIGHT_KG_LK = 0;

            long LEAVEQUANTITY_LK = 0;
            decimal LEAVEWEIGHT_KG_LK = 0;
            decimal DAPUNGKL = 0;
            decimal DAPUNGLUYKE = 0;

            DevelopActivityRepository developactivityrepository = new DevelopActivityRepository();
            ReturnBDHN_DI_HCM returnBDHN_DI_HCM = new ReturnBDHN_DI_HCM();
            returnBDHN_DI_HCM = developactivityrepository.BDHN_DI_HCM(workcenter, AcceptDate, arriveprovince, arrivepartner, ref ARRIVEQUANTITY_LK, ref ARRIVEWEIGHT_KG_LK, ref LEAVEQUANTITY_LK, ref LEAVEWEIGHT_KG_LK, ref DAPUNGKL, ref DAPUNGLUYKE);

            return returnBDHN_DI_HCM.ListBDHN_DI_HCMReport;
        }


        public Stream CreateExcelFile(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcel( ViewBag.workcenter, ViewBag.AcceptDate, ViewBag.arriveprovince, ViewBag.arrivepartner);
            using (var excelPackage = new ExcelPackage(stream ?? new MemoryStream()))
            {
                // Tạo author cho file Excel
                excelPackage.Workbook.Properties.Author = "Window.User";
                // Tạo title cho file Excel
                excelPackage.Workbook.Properties.Title = "Export Excel";
                // thêm tí comments vào làm màu 
                excelPackage.Workbook.Properties.Comments = "Export Excel Success !";
                // Add Sheet vào file Excel
                excelPackage.Workbook.Worksheets.Add("First Sheet");
                // Lấy Sheet bạn vừa mới tạo ra để thao tác 
                var workSheet = excelPackage.Workbook.Worksheets[1];
                // Đổ data vào Excel file
                workSheet.Cells[1, 1].LoadFromCollection(list, true, TableStyles.Dark9);
                BindingFormatForExcel(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }


        //Phần sửa excel

        private void BindingFormatForExcel(ExcelWorksheet worksheet, List<BDHN_DI_HCM> listItems)
        {
            // Set default width cho tất cả column
            worksheet.DefaultColWidth = 30;
            worksheet.DefaultRowHeight = 20;
            // Tự động xuống hàng khi text quá dài
            worksheet.Cells.Style.WrapText = true;
            // Tạo header
            worksheet.Cells[1, 1].Value = "STT";
            worksheet.Cells[1, 2].Value = "Thời gian đến";
            worksheet.Cells[1, 3].Value = "ID chuyến đến";
            worksheet.Cells[1, 4].Value = "Tên chuyến đến";
            worksheet.Cells[1, 5].Value = "Mã Đơn Vị";
            worksheet.Cells[1, 6].Value = "Tên Đơn Vị";
            worksheet.Cells[1, 7].Value = "SL đến";
            worksheet.Cells[1, 8].Value = "KL đến (kg)";
            worksheet.Cells[1, 9].Value = "SL đến lũy kế";
            worksheet.Cells[1, 10].Value = "KL đến lũy kế";
            worksheet.Cells[1, 11].Value = "SL tồn lũy kế";
            worksheet.Cells[1, 12].Value = "KLg tồn lũy kế";
            worksheet.Cells[1, 13].Value = "Thời gian";
            worksheet.Cells[1, 14].Value = "ID Tuyến đi";
            worksheet.Cells[1, 15].Value = "Tên tuyến đi";
            worksheet.Cells[1, 16].Value = "ID chuyến đi";
            worksheet.Cells[1, 17].Value = "Tên chuyến đi";
            worksheet.Cells[1, 18].Value = "SL đi";
            worksheet.Cells[1, 19].Value = "KL đi (kg)";
            worksheet.Cells[1, 20].Value = "SL đi lũy kế";
            worksheet.Cells[1, 21].Value = "KL đi lũy kế";
            worksheet.Cells[1, 22].Value = "Đáp ứng (SL)";
            worksheet.Cells[1, 23].Value = "Đáp ứng (KLg)";
            worksheet.Cells[1, 24].Value = "Tỷ lệ đáp ứng lũy kế theo KLG";

            // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
            using (var range = worksheet.Cells["A1:Z1"])
            {
                // Set PatternType
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                // Set Màu cho Background
                range.Style.Fill.BackgroundColor.SetColor(Color.Orange);
                // Canh giữa cho các text
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                // Set Font cho text  trong Range hiện tại
                range.Style.Font.SetFromFont(new Font("Arial", 11));
                // Set Border
                //range.Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
                // Set màu ch Border
                //range.Style.Border.Bottom.Color.SetColor(Color.Blue);
            }


        }

        //Hàm Export excel  , truyền parameter vào để export
        [HttpGet]
        public ActionResult Export(int workcenter, string AcceptDate, int arriveprovince, int arrivepartner)
        {
            ViewBag.workcenter = workcenter;
            ViewBag.AcceptDate = AcceptDate;
            ViewBag.arriveprovince = arriveprovince;
            ViewBag.arrivepartner = arrivepartner;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFile();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=Báo cáo hoạt động tại sàn khai thác" + "_" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx");
            // Lưu file excel của chúng ta như 1 mảng byte để trả về response
            Response.BinaryWrite(buffer.ToArray());
            // Send tất cả ouput bytes về phía clients
            Response.Flush();
            Response.End();
            // Redirect về luôn trang index >
            return RedirectToAction("Index");
        }

    }
}