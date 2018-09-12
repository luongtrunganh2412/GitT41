using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using T41.Areas.Admin.Common;
using T41.Areas.Admin.Data;
using T41.Areas.Admin.Models.DataModel;
using T41.Areas.Admin.Model.DataModel;

namespace T41.Areas.Admin.Controllers
{
    public class TrackAndTraceController : Controller
    {
       
        Convertion common = new Convertion();
        
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult ListTrackAndTrace(string emscode)
        {
            TrackAndTraceRepository trackandtraceRepository = new TrackAndTraceRepository();
            ReturnTrackAndTrace returntrackandtrace = new ReturnTrackAndTrace();
            returntrackandtrace = trackandtraceRepository.ListTrackAndTrace(emscode);

            return Json(returntrackandtrace, JsonRequestBehavior.AllowGet);
            //return View(returntrackandtrace);
        }

        
    }
}