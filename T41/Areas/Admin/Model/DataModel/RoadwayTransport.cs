using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Model.DataModel
{
    //Parameter truyền vào DB để gọi dữ liệu của Báo cáo tổng hợp dữ liệu truyền nhận EMS Center
    public class PARAMETER_ROADWAY
    {
        
        public string date { get; set; }
       
        public int way { get; set; }
        public string mailroutecode { get; set; }
        public int tungay { get; set; }
        public int denngay { get; set; }
        public int vung { get; set; }
        public string cap { get; set; }
        public int loaipt { get; set; }
    }
    //Phần lấy ra mã tuyến phát dưới DB
    public class MAILROUTE
    {
        public string MAILROUTECODE { get; set; }
        public string MAILROUTENAME { get; set; }
    }

    //Dữ liệu lấy ra của Báo cáo chi tiết giao nhận vận chuyển theo đường thư
    public class RoadwayTransportDetail_CT
    {


        public String NGAY { get; set; }
        public String BD10 { get; set; }
        public String SLTUI { get; set; }
        public String KL { get; set; }
        public String BCDONG { get; set; }


        public String TENBCDONG { get; set; }
        public String BCNHAN { get; set; }
        public String TENBCNHAN { get; set; }
        public String IDHANHTRINH { get; set; }
        public String HANHTRINH { get; set; }
        public String CAP { get; set; }
        public String LOAIPT { get; set; }



    }

    //Dữ liệu lấy ra của Báo cáo tổng hợp giao nhận vận chuyển theo đường thư
    public class RoadwayTransportDetail_TH
    {
        public String ID_HT { get; set; }
        public String TEN_HT { get; set; }
        public String SLBD10 { get; set; }
        public String SLTUI { get; set; }
        public String KL { get; set; }
    }

    //Dữ liệu lấy ra của Báo cáo tổng hợp giao nhận vận chuyển theo thời gian
    public class RoadwayTransportDetail_TG
    {
        public String NGAY { get; set; }
        public int SLBD10 { get; set; }
        public int SLTUI { get; set; }
        public Decimal KL { get; set; }
        //public String SLBD10 { get; set; }
        //public String SLTUI { get; set; }
        //public String KL { get; set; }
    }

    //Dữ liệu trả về sau khi gọi dữ liệu dưới DB
    public class ReturnRoadwayTransport
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

        
        public RoadwayTransportDetail_TH RoadwayTransportReport_TH { get; set; }
        public List<RoadwayTransportDetail_TH> ListRoadwayTransportReport_TH;

        public RoadwayTransportDetail_CT RoadwayTransportReport_CT { get; set; }
        public List<RoadwayTransportDetail_CT> ListRoadwayTransportReport_CT;

        public RoadwayTransportDetail_TG RoadwayTransportReport_TG { get; set; }
        public List<RoadwayTransportDetail_TG> ListRoadwayTransportReport_TG;

        public MetaData MetaData { get; set; }


    }

    
}