using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Model.DataModel
{
    //Parameter truyền vào DB để gọi dữ liệu của Báo cáo tổng hợp dữ liệu truyền nhận EMS Center
    public class PARAMETER_ExpressRoad
    {
        public int zone { get; set; }
        
    }

   


    //Dữ liệu lấy ra của Báo cáo tổng hợp dữ liệu truyền nhận EMS Center
    public class ExpressRoadDetail
    {
        public String EVENT { get; set; }
        public String POSTTIMEVIEW { get; set; }
        public String MAILROUTEARRIVE { get; set; }
        public String MAILROUTEARRIVENAME { get; set; }
        public String ARRIVETIME { get; set; }
        public String MAILROUTE_TYPE { get; set; }
        public String MAILROUTE_CLASSIFY { get; set; }
        public String TRANSPORT_TYPE { get; set; }
        public String MAILLEAVE { get; set; }
        public String MAILROUTENAME { get; set; }
        public String LEAVE { get; set; }
        public String SERVICE { get; set; }
        public String MAILROUTELEAVE { get; set; }
        public String MAILROUTELEAVENAME { get; set; }
        public String LEAVETIME { get; set; }
        public String MAILROUTE_TYPE1 { get; set; }
        public String MAILROUTE_CLASSIFY1 { get; set; }
        public String TRANSPORT_TYPE1 { get; set; }
        

    }

    

    //Dữ liệu trả về sau khi gọi dữ liệu dưới DB
    public class ReturnExpressRoad
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

        public ExpressRoadDetail ExpressRoadReport { get; set; }
        public List<ExpressRoadDetail> ListExpressRoadReport;

        

        public MetaData MetaData { get; set; }


    }

    
}