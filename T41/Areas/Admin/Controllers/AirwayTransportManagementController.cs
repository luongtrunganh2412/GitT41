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
    public class AirwayTransportManagementController : Controller
    {
        // GET: Admin/AirwayTransportManagement
        public ActionResult Index()
        {
            return View();
        }
        //Phần Controller gọi đến Bảng Load_DATA1
        public ActionResult AirwayTransportManagementDetailReport()
        {
            return View();
        }
        public ActionResult ListAirwayTransportManagement()
        {
            return View();
        }
    }
}