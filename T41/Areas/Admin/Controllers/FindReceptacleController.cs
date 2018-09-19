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


using System.Data;
using System.Drawing;
using System.IO;

using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;


namespace T41.Areas.Admin.Controllers
{
    public class FindReceptacleController: Controller
    {
        //int page_size = int.Parse(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        Convertion common = new Convertion();
        // GET: Admin/UserManagement
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListReceptacleReport()
        {
            return View();
        }
        
        [HttpGet]
        public ActionResult ListReceptacleReport(string fromdate, string todate, string receptacle_id)
        {

            ReceptacleIDRepository receptacleRepository = new ReceptacleIDRepository();
            ReturnRECEPTACLE returnreceptacle = new ReturnRECEPTACLE();
            returnreceptacle = receptacleRepository.RECEPTACLE_Detail(fromdate, todate, receptacle_id);
            return View(returnreceptacle);
        }

        //Phần trả về data theo list để xuất excel
        [HttpGet]
        public List<RECEPTACLE_Detail> ReturnListExcel(string fromdate, string todate, string receptacle_id)
        {
            ReceptacleIDRepository receptacleRepository = new ReceptacleIDRepository();
            ReturnRECEPTACLE returnreceptacle = new ReturnRECEPTACLE();
            returnreceptacle = receptacleRepository.RECEPTACLE_Detail(fromdate, todate, receptacle_id);
            ViewBag.fromdate = fromdate;
            ViewBag.todate = todate;
            ViewBag.receptacle_id = receptacle_id;
            ViewBag.ListReceptacleDetail = returnreceptacle.ListReceptacleReport;
            return returnreceptacle.ListReceptacleReport;
        }

        //Source : https://toidicodedao.com/2015/11/24/series-c-hay-ho-epplus-thu-vien-excel-ba-dao-phan-1/
        public Stream CreateExcelFile(Stream stream = null)
        {
            //var list = CreateTestItems();
            var list = ReturnListExcel(ViewBag.fromdate,ViewBag.todate, ViewBag.receptacle_id );
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
                //BindingFormatForExcel(workSheet, list);
                excelPackage.Save();
                return excelPackage.Stream;
            }
        }


        //Phần sửa excel

        //private void BindingFormatForExcel(ExcelWorksheet worksheet, List<RECEPTACLE_Detail> listItems)
        //{
        //    // Set default width cho tất cả column
        //    worksheet.DefaultColWidth = 10;
        //    // Tự động xuống hàng khi text quá dài
        //    worksheet.Cells.Style.WrapText = true;
        //    // Tạo header
        //    worksheet.Cells[1, 1].Value = "ID";
        //    worksheet.Cells[1, 2].Value = "Full Name";
        //    worksheet.Cells[1, 3].Value = "Address";
        //    worksheet.Cells[1, 4].Value = "Money";

        //    // Lấy range vào tạo format cho range đó ở đây là từ A1 tới D1
        //    using (var range = worksheet.Cells["A1:D1"])
        //    {
        //        // Set PatternType
        //        range.Style.Fill.PatternType = ExcelFillStyle.DarkGray;
        //        // Set Màu cho Background
        //        range.Style.Fill.BackgroundColor.SetColor(Color.Aqua);
        //        // Canh giữa cho các text
        //        range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //        // Set Font cho text  trong Range hiện tại
        //        range.Style.Font.SetFromFont(new Font("Arial", 10));
        //        // Set Border
        //        range.Style.Border.Bottom.Style = ExcelBorderStyle.Thick;
        //        // Set màu ch Border
        //        range.Style.Border.Bottom.Color.SetColor(Color.Blue);
        //    }


        //}

        //Hàm Export excel
        [HttpGet]
        public ActionResult Export(string fromdate, string todate, string receptacle_id)
        {
            ViewBag.fromdate = fromdate;
            ViewBag.todate = todate;
            ViewBag.receptacle_id = receptacle_id;
            // Gọi lại hàm để tạo file excel
            var stream = CreateExcelFile();
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=ExportExcel.xlsx");
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