using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Model.DataModel
{
    //Parameter truyền vào DB để gọi dữ liệu của Báo cáo tổng hợp dữ liệu truyền nhận EMS Center
    public class PARAMETER_VNPOST
    {
        public int fromprovince { get; set; }
        public int toprovince { get; set; }
        public int fromposcode { get; set; }
        public int toposcode { get; set; }
        public string fromdate { get; set; }
        public string todate { get; set; }
        public int typeComunication { get; set; }
    }
    
    //Parameter truyền vào DB để gọi dữ liệu của Chi tiết tổng hợp dữ liệu truyền nhận EMS Center
    public class PARAMETER_CTTS_VNPOST
    {
        public int fromposcode { get; set; }
        public int toposcode { get; set; }
        public int date { get; set; }
        public int typeComunication { get; set; }
    }

    //Parameter truyền vào DB để gọi dữ liệu của số túi trong chuyến thư
    public class PARAMETER_ST_CTTS_VNPOST
    {
        public int fromposcode { get; set; }
        public int toposcode { get; set; }
        public string date { get; set; }
        public int mailtrip { get; set; }
        public int typeComunication { get; set; }
    }


    //Parameter truyền vào DB để gọi dữ liệu của Phần In Bản kê e2 theo chuyến thư túi số, mã cổ túi
    public class PARAMETER_E2_VNPOST
    {
        public int fromposcode { get; set; }
        public int toposcode { get; set; }
        public int date { get; set; }
        public int mailtrip { get; set; }
        public int postbag { get; set; }
        public int typeComunication { get; set; }
    }
    
    //Lấy mã tỉnh đóng , tỉnh nhận
    public class GETPROVINCE_VNPOST
    {
        public string PROVINCECODE { get; set; }

        public string PROVINCENAME { get; set; }

    }

    //Lấy mã bưu cục đóng , bưu cục nhận, GETCRPOSCODE: GETCLOSERECEIVEPOSCODE
    public class GETCRPOSCODE_VNPOST
    {
        public string POSCODE { get; set; }

        public string POSNAME { get; set; }

    }


    //Dữ liệu lấy ra của Báo cáo tổng hợp dữ liệu truyền nhận EMS Center
    public class TransferManagementDetail_VNPOST
    {

        public int FromPosCode { get; set; }
        public String FromPosName { get; set; }
        public int ToPosCode { get; set; }
        public String ToPosName { get; set; }
        public String PickUpDate { get; set; }
        public int TotalMailTrip { get; set; }
        public int TotalPostBag { get; set; }
        public int TotalItem { get; set; }

    }

    //Dữ liệu lấy ra của Chi tiết tổng hợp dữ liệu truyền nhận EMS Center
    public class TransferManagement_CTTS_Detail_VNPOST
    {

        public int FromPosCode { get; set; }
        public String FromPosName { get; set; }
        public int ToPosCode { get; set; }
        public String ToPosName { get; set; }
        public String PickUpDate { get; set; }
        public int TotalMailTrip { get; set; }
        public int TotalPostBag { get; set; }
        public int TotalItem { get; set; }

    }

    //Dữ liệu lấy ra của số túi theo tổng số chuyển thư
    public class TransferManagement_SOTUI_Detail_VNPOST
    {

        public int FromPosCode { get; set; }
        public String FromPosName { get; set; }
        public int ToPosCode { get; set; }
        public String ToPosName { get; set; }
        public String PickUpDate { get; set; }
        public int ChThu { get; set; }
        public int TuiSo { get; set; }
        public int CountMaE1 { get; set; }

    }

    //Dữ liệu lấy ra của chi tiết chuyến thư túi số phần in bản kê E2 theo chuyến thư túi số
    public class TransferManagement_CTTS_E2_Detail_VNPOST
    {

        public String FromPos { get; set; }
        public String ToPos { get; set; }
        public int MailTrip { get; set; }
        public int PostBag { get; set; }
        public String PickUpDate { get; set; }
        public String PickUpTime { get; set; }
        public Decimal TotalWeight { get; set; }

    }


    //Dữ liệu lấy ra của bảng chi tiết in bản kê E2 theo chuyến thư túi số
    public class TransferManagement_E2_Detail_VNPOST
    {

        public String MaE1 { get; set; }
        public int MaBCTra { get; set; }
        public String KhoiLuong { get; set; }
        
        public String CuocCS { get; set; }
        public String Cuoc_DV { get; set; }

        public String Tong_Cuoc { get; set; }
        public String TrangThai { get; set; }
        
    }

    //Dữ liệu lấy ra của chi tiết chuyến thư túi số phần in bản kê E2 theo chuyến thư túi số
    public class TransferManagement_CTTS_MCT_E2_Detail_VNPOST
    {

        public String FromPos { get; set; }
        public String ToPos { get; set; }
        public int MailTrip { get; set; }
        public int PostBag { get; set; }
        public String PickUpDate { get; set; }
        public String PickUpTime { get; set; }
        public Decimal TotalWeight { get; set; }

    }


    //Dữ liệu lấy ra của bảng chi tiết in bản kê E2 theo chuyến thư túi số
    public class TransferManagement_MCT_E2_Detail_VNPOST
    {

        public String MaE1 { get; set; }
        public int MaBCTra { get; set; }
        public String KhoiLuong { get; set; }
        
        public String CuocCS { get; set; }
        public String Cuoc_DV { get; set; }

        public String Tong_Cuoc { get; set; }
        public String TrangThai { get; set; }

    }

    //Dữ liệu trả về sau khi gọi dữ liệu dưới DB
    public class ReturnTransferManagement_VNPOST
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

        public TransferManagementDetail_VNPOST TransferManagement_VNPOSTReport { get; set; }
        public List<TransferManagementDetail_VNPOST> ListTransferManagement_VNPOSTReport;

        public TransferManagement_CTTS_Detail_VNPOST TransferManagement_CTTS_VNPOST_Report { get; set; }
        public List<TransferManagement_CTTS_Detail_VNPOST> ListTransferManagement_CTTS_VNPOST_Report;

        public TransferManagement_SOTUI_Detail_VNPOST TransferManagement_SOTUI_VNPOST_Report { get; set; }
        public List<TransferManagement_SOTUI_Detail_VNPOST> ListTransferManagement_SOTUI_VNPOST_Report;

        public TransferManagement_E2_Detail_VNPOST TransferManagement_E2_VNPOST_Report { get; set; }
        public List<TransferManagement_E2_Detail_VNPOST> ListTransferManagement_E2_VNPOST_Report;

        public TransferManagement_CTTS_E2_Detail_VNPOST TransferManagement_CTTS_E2_VNPOST_Report { get; set; }
        public List<TransferManagement_CTTS_E2_Detail_VNPOST> ListTransferManagement_CTTS_E2_VNPOST_Report;

        public TransferManagement_MCT_E2_Detail_VNPOST TransferManagement_MCT_E2_VNPOST_Report { get; set; }
        public List<TransferManagement_MCT_E2_Detail_VNPOST> ListTransferManagement_MCT_E2_VNPOST_Report;

        public TransferManagement_CTTS_MCT_E2_Detail_VNPOST TransferManagement_CTTS_MCT_E2_VNPOST_Report { get; set; }
        public List<TransferManagement_CTTS_MCT_E2_Detail_VNPOST> ListTransferManagement_CTTS_MCT_E2_VNPOST_Report;

        public MetaData MetaData { get; set; }


    }

    
}