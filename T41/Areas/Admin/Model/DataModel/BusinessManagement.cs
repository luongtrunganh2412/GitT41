using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Model.DataModel
{

    
    //Phần lấy dữ liệu của bảng business_profile
    public class BUSINESS_MANAGEMENT_Detail
    {
        public String NGAY_HD { get; set; }
        public String MA_KH { get; set; }
        public String TEN_KHACH_HANG { get; set; }
        public String MA_BC_KHAI_THAC { get; set; }
        public String TONG_SO { get; set; }
        public String TONG_KHOI_LUONG_QD { get; set; }
        public String TONG_CUOC_CHINH { get; set; }
        public String TONG_CUOC_DV { get; set; }
        public String TONG_CUOC_COD { get; set; }
        public String VAT { get; set; }
        public String TONG_CUOC_E1 { get; set; }
        public String CHIET_KHAU { get; set; }
        public String TRICH_THUONG { get; set; }
        public String TONG_NO { get; set; }


    }

    public class ReturnBusinessManagement
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

        public BUSINESS_MANAGEMENT_Detail BusinessManagement_Report { get; set; }
        public List<BUSINESS_MANAGEMENT_Detail> ListBusinessManagement_Report;

        
        public MetaData MetaData { get; set; }


    }
}