using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Model.DataModel
{
    //Phần lấy dữ liệu đơn vị
    public class GETUNIT
    {

        public String UNIT_CODE { get; set; }
        public String UNIT_NAME { get; set; }

    }


    //Phần lấy dữ liệu dịch vụ
    public class GETSERVICE
    {

        public String SERVICE_CODE { get; set; }
        public String SERVICE_NAME { get; set; }

    }

    //Phần lấy dữ liệu bưu cục
    public class GET_BM_POSCODE
    {

        public String POS_CODE { get; set; }
        public String POS_NAME { get; set; }

    }

    //Phần lấy dữ liệu của bảng business_profile
    public class BUSINESS_MANAGEMENT_Detail
    {
        public String NGAY_HD { get; set; }
        public String MA_KH { get; set; }
        public String THOI_GIAN_HD { get; set; }
        public String TEN_KHACH_HANG { get; set; }
        public String Ma_NV_Sale { get; set; }
        public String MA_BC_KHAI_THAC { get; set; }
        public String TONG_SO { get; set; }
        public String TONG_KHOI_LUONG_QD { get; set; }
        public String TONG_TIEN_COD { get; set; }
        public String TONG_CUOC_CHINH { get; set; }
        public String TONG_CUOC_DV { get; set; }
        public String TONG_CUOC_COD { get; set; }
        public String VAT { get; set; }
        public String TONG_CUOC_E1 { get; set; }
        public String CHIET_KHAU { get; set; }
        public String TRICH_THUONG { get; set; }
        public String TONG_NO { get; set; }


    }

    //Phần lấy dữ liệu tổng chân trang
    public class SUM_BUSINESS_MANAGEMENT_Detail
    {
        public String sumThoiGianHD { get; set; }
        public String sumTongSo { get; set; }
        public String sumTongKLQD { get; set; }
        public String sumTongTienCOD { get; set; }
        public String sumTongCC { get; set; }
        public String sumTongCuocDV { get; set; }
        public String sumTongCuocCOD { get; set; }
        public String sumVat { get; set; }
        public String sumTongCuocE1 { get; set; }
        public String sumChietKhau { get; set; }
        public String sumTongNo { get; set; }
        

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

        public SUM_BUSINESS_MANAGEMENT_Detail Sum_BusinessManagement_Report { get; set; }
        public List<SUM_BUSINESS_MANAGEMENT_Detail> ListSumBusinessManagement_Report;
        public MetaData MetaData { get; set; }


    }
}