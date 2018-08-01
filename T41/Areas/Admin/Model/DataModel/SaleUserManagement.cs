using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Model.DataModel
{
    //Parameter truyền vào DB để tạo mới dữ liệu bảng nguoi_dung_sale
    public class PARAMETER_NGUOI_DUNG_SALE
    {
        public int ID_NGUOI_DUNG { get; set; }
        public int ID_DON_VI { get; set; }
        public string HO_TEN { get; set; }
        public string CHUC_VU { get; set; }
        public string DIEN_THOAI { get; set; }
        public string EMAIL { get; set; }
        public int ID_NHAN_VIEN { get; set; }
        
    }

    //Phần lấy dữ liệu của bảng business_profile
    public class SALE_USER_MANAGEMENT_Detail
    {
        public int ID_NGUOI_DUNG { get; set; }
        public int ID_DON_VI { get; set; }
        public String HO_TEN { get; set; }
        public String CHUC_VU { get; set; }
        public String DIEN_THOAI { get; set; }
        public String EMAIL { get; set; }
        
    }

    public class ReturnSaleUserManagement
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

        public SALE_USER_MANAGEMENT_Detail SaleUserManagement_Report { get; set; }
        public List<SALE_USER_MANAGEMENT_Detail> ListSaleUserManagement_Report;

        
        public MetaData MetaData { get; set; }


    }
}