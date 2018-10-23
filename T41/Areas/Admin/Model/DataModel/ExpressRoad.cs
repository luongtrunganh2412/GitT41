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
        public int ID { get; set; }
        public String CONTACT_NAME { get; set; }
        public String CONTACT_ADDRESS { get; set; }
        public String GENERAL_EMAIL { get; set; }
        public String CONTACT_PHONE_WORK { get; set; }
        public int CONTACT_PROVINCE { get; set; }
        public int CONTACT_DISTRICT { get; set; }
        public String CUSTOMER_CODE { get; set; }
        public String UNIT_CODE { get; set; }
        public String API_KEY { get; set; }
        public String CUSTOMER_ID { get; set; }


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