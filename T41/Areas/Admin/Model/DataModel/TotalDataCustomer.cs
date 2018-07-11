using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Model.DataModel
{
    //Parameter truyền vào DB để gọi dữ liệu của Báo cáo tổng hợp dữ liệu truyền nhận EMS Center
    public class PARAMETER_TOTALDATA
    {
        public int fromprovince { get; set; }
        public int toprovince { get; set; }
        public int fromposcode { get; set; }
        public int toposcode { get; set; }
        public string fromdate { get; set; }
        public string todate { get; set; }
        public int typeComunication { get; set; }
    }

    public class GETPOSCODE_TOTALDATA
    {
        public string POSTCODE { get; set; }

        public string POSNAME { get; set; }

    }


    //Dữ liệu lấy ra của Báo cáo tổng hợp dữ liệu truyền nhận EMS Center
    public class TotalDataCustomerDetail
    {

        public String CUSTOMERNAME { get; set; }
        public String CUSTOMERCODE { get; set; }
        public String PROVINCENAME { get; set; }
        public String TOTALITEM { get; set; }
        public String TOTALI { get; set; }
        public String TotalItem { get; set; }
        public String RATEI { get; set; }
        public String TOTALH { get; set; }
        public String RATEH { get; set; }
        public String TOTALT { get; set; }
        public String RATET { get; set; }
        public String TOTALP { get; set; }
        public String RATEP { get; set; }
        public String TOTALL { get; set; }
        public String RATEL { get; set; }
        public String TOTALJ { get; set; }
        public String RATEJ { get; set; }
        public String TOTALKXD { get; set; }
        public String RATEKXD { get; set; }


    }

    

    //Dữ liệu trả về sau khi gọi dữ liệu dưới DB
    public class ReturnTotalDataCustomer
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

        public TotalDataCustomerDetail TotalDataCustomerReport { get; set; }
        public List<TotalDataCustomerDetail> ListTotalDataCustomerReport;

        

        public MetaData MetaData { get; set; }


    }

    
}