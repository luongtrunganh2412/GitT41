using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Model.DataModel
{
    
    //Parameter truyền vào DB để xem chi tiết BD13 đi
    public class PARAMETER_DEVELOP_ACTIVITY
    {
        public int workcenter { get; set; }
        public string AcceptDate { get; set; }
        public int arriveprovince { get; set; }
        public int arrivepartner { get; set; }
        
        
    }


    //Phần lấy dữ iệu của bảng KPI_SummingPassByMailRoute
    public class BDHN_DI_HCM
    {
        public String TGDEN { get; set; }
        public String  ARRIVEIDVNPOST { get; set; }
        public String  ARRIVEMAILROUTENAME { get; set; }
        public String  ARRIVEFROMPOSCODE { get; set; }
        public String  ARRIVEFROMPOSNAME { get; set; }
        public String  ARRIVEQUANTITY { get; set; }
        public String  ARRIVEWEIGHT_KG { get; set; }

        public long ARRIVEQUANTITY_LK { get; set; }
        public decimal ARRIVEWEIGHT_KG_LK { get; set; }

        public long ARRIVEQUANTITY_TON_LK { get; set; }
        public decimal ARRIVEWEIGHT_KG_TON_LK { get; set; }

        public String  TGDI { get; set; }
        public String  LEAVEIDVNPOST { get; set; }
        public String  LEAVEMAILROUTENAME { get; set; }
        public String  LEAVETOPOSCODE { get; set; }
        public String  LEAVETOPOSNAME { get; set; }
        public String  LEAVEQUANTITY { get; set; }
        public String  LEAVEWEIGHT_KG { get; set; }

        public long LEAVEQUANTITY_LK { get; set; }
        public decimal LEAVEWEIGHT_KG_LK { get; set; }
        public string DAPUNGSL { get; set; }
        public string DAPUNGKL { get; set; }

    }

    public class ReturnBDHN_DI_HCM
    {
        public string Code { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }

        public string Value { get; set; }
        /// <summary>
        /// Value
        /// </summary>
        public int Total { get; set; }

        public BDHN_DI_HCM BDHN_DI_HCMReport { get; set; }
        public List<BDHN_DI_HCM> ListBDHN_DI_HCMReport;
        
        public MetaData MetaData { get; set; }


    }
}