using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Model.DataModel
{
    //Parameter truyền vào DB để gọi dữ liệu của Báo cáo tổng hợp dữ liệu truyền nhận EMS Center
    public class PARAMETER_AIRWAY_COME
    {
        
        public string date { get; set; }
       
        public int way { get; set; }
    }
    
    
    //Dữ liệu lấy ra của Báo cáo tổng hợp dữ liệu truyền nhận EMS Center
    public class AirwayTransportComeDetail
    {
        public int SO{get; set;}
        public int STT {get; set;}
        public String CHANGBAY { get; set; }
        public String DONVIVANCHUYEN { get; set; }
        public String DICHVUVANCHUYEN { get; set; }
        public String KHUNGGIO { get; set; }
        public String TONGTAITHEOHOPDONG { get; set; }
        public int ID { get; set; }
        public int NGAY { get; set; }
        public int MABC { get; set; }
        public int CHIEU { get; set; }
        public int HUONG { get; set; }
        public String TAICUNG_KH { get; set; }
        public String TAIMEM_KH { get; set; }
        public String MASP { get; set; }
        public String TAICUNG_TH { get; set; }
        public String TAIMEM_TH { get; set; }
        public String TAICUNG_LK { get; set; }
        public String TAIMEM_LK { get; set; }
        public String GIOGIAO_QD { get; set; }
        public String GIOBAY_QD { get; set; }
        public String GIOGIAO_TT { get; set; }
        public String GIOBAY_TT { get; set; }
        public String SOHIEUCHUYENBAY { get; set; }
        public String GIOGIAO_CL { get; set; }
        public String GIOBAY_CL { get; set; }
        public String GIODAP_QD { get; set; }
        public String GIONHAN_QD { get; set; }
        public String GIONHAN_TT { get; set; }
        public String GIONHAN_CL { get; set; }

    }

    
    //Dữ liệu trả về sau khi gọi dữ liệu dưới DB
    public class ReturnAirwayTransportCome
    {
        public string Code { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        public int Total { get; set; }

        public AirwayTransportComeDetail AirwayTransportComeReport { get; set; }
        public List<AirwayTransportComeDetail> ListAirwayTransportComeReport;

        
        public MetaData MetaData { get; set; }


    }

    
}