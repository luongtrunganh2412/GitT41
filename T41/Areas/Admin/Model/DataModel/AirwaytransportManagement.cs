using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Model.DataModel
{
    //Parameter truyền vào DB để tạo mới dữ liệu bảng AirwaytransportManagement
    public class PARAMETER_AirwaytransportManagement
    {
        public int NGAY { get; set; }
        public int CHIEU { get; set; }
        public string TAICUNG_TH { get; set; }
        public string TAIMEM_TH { get; set; }
        public string GIOGIAO_TT { get; set; }
        public string GIOBAY_TT { get; set; }
        public string SOHIEUCHUYENBAY { get; set; }
        public string GIONHAN_TT { get; set; }
        public int ID_VNP { get; set; }
        

    }


    public class ReturnAirwaytransportManagement
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
        
        public MetaData MetaData { get; set; }


    }
}