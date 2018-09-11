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
    public class FindReceptacleController: Controller
    {
        int page_size = int.Parse(ConfigurationManager.AppSettings["PAGE_SIZE"]);
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


        //Phần show ra dữ liệu của bảng edi_consigment_resdit_event
        [HttpGet]
        public ActionResult ListReceptacleReport(int? page, string fromdate, string todate, string receptacle_id)
        {
            int currentPageIndex = page.HasValue ? page.Value : 1;
            ViewBag.currentPageIndex = currentPageIndex;
            ViewBag.PageSize = page_size;
            ReceptacleIDRepository receptacleRepository = new ReceptacleIDRepository();
            ReturnRECEPTACLE returnreceptacle = new ReturnRECEPTACLE();
            returnreceptacle = receptacleRepository.RECEPTACLE_Detail(currentPageIndex, page_size, fromdate, todate, receptacle_id);
            ViewBag.total = returnreceptacle.Total;
            ViewBag.total_page = (returnreceptacle.Total + page_size - 1) / page_size;
            return View(returnreceptacle);
            
        }

        
    }
}