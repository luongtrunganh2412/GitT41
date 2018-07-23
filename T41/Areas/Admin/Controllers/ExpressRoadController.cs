using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T41.Areas.Admin.Data;
using T41.Areas.Admin.Model.DataModel;

namespace T41.Areas.Admin.Controllers
{
    public class ExpressRoadController : Controller
    {
        // GET: Admin/Collect
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ExpressRoadDetailReport()
        {
            return View();
        }

        //Controller gọi đến chi tiết của bảng CHI TIET
        [HttpGet]
        public ActionResult ListDetailedExpressRoadReport(int zone)
        {
            ExpressRoadRepository expressroadRepository = new ExpressRoadRepository();
            ReturnExpressRoad returnexpressroad = new ReturnExpressRoad();
            returnexpressroad = expressroadRepository.EXPRESS_ROAD_DETAIL(zone);
            return View(returnexpressroad);
        }
    }
}