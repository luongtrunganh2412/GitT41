using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T41.Areas.Admin.Model.CanvasModel;
using Newtonsoft.Json;

namespace T41.Areas.Admin.Controllers
{
    public class ShipmentManagementController : Controller
    {
        // GET: Admin/ShipmentManagement
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ShipmentManagementDetail()
        {
            List<DataPoint> dataPoints1 = new List<DataPoint>();
            List<DataPoint> dataPoints2 = new List<DataPoint>();
            List<DataPoint> dataPoints3 = new List<DataPoint>();
            List<DataPoint> dataPoints4 = new List<DataPoint>();

            dataPoints1.Add(new DataPoint("SL Bưu Gửi Đến", 72));
            dataPoints1.Add(new DataPoint("SL Phát Thành Công", 67));
            dataPoints1.Add(new DataPoint("SL Phát Chưa Có Thông Tin", 55));
            dataPoints1.Add(new DataPoint("SL PTC Đúng Quy Định", 42));
            dataPoints1.Add(new DataPoint("SL PTC Không Đúng Quy Định", 40));
            dataPoints1.Add(new DataPoint("Tỉ Lệ TC Đạt Đúng Quy Định", 35));
            dataPoints1.Add(new DataPoint("Tỉ Lệ TC Không Đúng Quy Định", 35));
            dataPoints1.Add(new DataPoint("SL PTC Không Xác Định", 35));

            dataPoints2.Add(new DataPoint("SL Bưu Gửi Đến", 10));
            dataPoints2.Add(new DataPoint("SL Phát Thành Công", 12));
            dataPoints2.Add(new DataPoint("SL Phát Chưa Có Thông Tin", 54));
            dataPoints2.Add(new DataPoint("SL PTC Đúng Quy Định", 14));
            dataPoints2.Add(new DataPoint("SL PTC Không Đúng Quy Định", 18));
            dataPoints2.Add(new DataPoint("Tỉ Lệ TC Đạt Đúng Quy Định", 20));
            dataPoints2.Add(new DataPoint("Tỉ Lệ TC Không Đúng Quy Định", 36));
            dataPoints2.Add(new DataPoint("SL PTC Không Xác Định", 40));

            dataPoints3.Add(new DataPoint("SL Bưu Gửi Đến", 14));
            dataPoints3.Add(new DataPoint("SL Phát Thành Công", 25));
            dataPoints3.Add(new DataPoint("SL Phát Chưa Có Thông Tin", 75));
            dataPoints3.Add(new DataPoint("SL PTC Đúng Quy Định", 41));
            dataPoints3.Add(new DataPoint("SL PTC Không Đúng Quy Định", 50));
            dataPoints3.Add(new DataPoint("Tỉ Lệ TC Đạt Đúng Quy Định", 57));
            dataPoints3.Add(new DataPoint("Tỉ Lệ TC Không Đúng Quy Định", 30));
            dataPoints3.Add(new DataPoint("SL PTC Không Xác Định", 78));

            dataPoints4.Add(new DataPoint("SL Bưu Gửi Đến", 12));
            dataPoints4.Add(new DataPoint("SL Phát Thành Công", 45));
            dataPoints4.Add(new DataPoint("SL Phát Chưa Có Thông Tin", 80));
            dataPoints4.Add(new DataPoint("SL PTC Đúng Quy Định", 90));
            dataPoints4.Add(new DataPoint("SL PTC Không Đúng Quy Định", 16));
            dataPoints4.Add(new DataPoint("Tỉ Lệ TC Đạt Đúng Quy Định", 45));
            dataPoints4.Add(new DataPoint("Tỉ Lệ TC Không Đúng Quy Định", 12));
            dataPoints4.Add(new DataPoint("SL PTC Không Xác Định", 41));

            ViewBag.DataPoints1 = JsonConvert.SerializeObject(dataPoints1);
            ViewBag.DataPoints2 = JsonConvert.SerializeObject(dataPoints2);
            ViewBag.DataPoints3 = JsonConvert.SerializeObject(dataPoints3);
            ViewBag.DataPoints4 = JsonConvert.SerializeObject(dataPoints4);
            return View();
        }

        
    }
}