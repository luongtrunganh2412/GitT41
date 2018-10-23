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


    //Phần lấy dữ iệu của bảng KPI_SummingPassByMailRoute LIÊN TỈNH
    #region LIEN_TINH
    public class BDHN_DI_HCM
    {
        public int STT { get; set; }
        public String TGDEN { get; set; }
        public String ARRIVEIDVNPOST { get; set; }
        public String ARRIVEMAILROUTENAME { get; set; }
        public String ARRIVEFROMPOSCODE { get; set; }
        public String ARRIVEFROMPOSNAME { get; set; }
        public String ARRIVEQUANTITY { get; set; }
        public String ARRIVEWEIGHT_KG { get; set; }

        public long ARRIVEQUANTITY_LK { get; set; }
        public decimal ARRIVEWEIGHT_KG_LK { get; set; }

        public long ARRIVEQUANTITY_TON_LK { get; set; }
        public decimal ARRIVEWEIGHT_KG_TON_LK { get; set; }

        public String TGDI { get; set; }
        public String LEAVEIDVNPOST { get; set; }
        public String LEAVEMAILROUTENAME { get; set; }
        public String LEAVETOPOSCODE { get; set; }
        public String LEAVETOPOSNAME { get; set; }
        public String LEAVEQUANTITY { get; set; }
        public String LEAVEWEIGHT_KG { get; set; }

        public long LEAVEQUANTITY_LK { get; set; }
        public decimal LEAVEWEIGHT_KG_LK { get; set; }
        public string DAPUNGSL { get; set; }
        public string DAPUNGKL { get; set; }

        public string DAPUNGLUYKE { get; set; }
        public string CHECK_TG { get; set; }

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
    #endregion


    //Phần lấy dữ iệu của bảng KPI_SummingPassByMailRoute NỘI TỈNH
    #region NOI_TINH
    public class NOI_TINH
    {
        public int STT { get; set; }
        public String TGDEN { get; set; }

        public String ARRIVEMAILROUTE { get; set; }
        public String ARRIVEIDVNPOSTNAME { get; set; }
        public String ARRIVEIDVNPOST { get; set; }
        public String ARRIVEMAILROUTENAME { get; set; }
        public String ARRIVEDONVI { get; set; }
        public String ARRIVEFROMPOSCODE { get; set; }
        public String ARRIVEFROMPOSNAME { get; set; }
        public String ARRIVEQUANTITY { get; set; }
        public String ARRIVEWEIGHT_KG { get; set; }
        public String ARRIVEQUANTITY_ACCUM { get; set; }
        public String DEN_KLG_LUYKE { get; set; }
        public String ARRIVEQUANTITY_STOCKACCUM { get; set; }
        public String DEN_KLG_TON_LUYKE { get; set; }
        public String TGQUETTUIDI { get; set; }
        public String LEAVEMAILROUTE { get; set; }
        public String LEAVEIDVNPOSTNAME { get; set; }
        public String LEAVEIDVNPOST { get; set; }
        public String LEAVEMAILROUTENAME { get; set; }
        public String LEAVEDONVI { get; set; }
        public String LEAVETOPOSCODE { get; set; }
        public String LEAVETOPOSNAME { get; set; }
        public String LEAVEQUANTITY { get; set; }
        public String LEAVEWEIGHT_KG { get; set; }
        public String LEAVEQUANTITY_ACCUM { get; set; }
        public String DI_KLG_LUYKE { get; set; }
        public Decimal TYLEDAPUNGCHUYEN_SL { get; set; }
        public Decimal TYLEDAPUNGCHUYEN_KLG { get; set; }
        public Decimal TYLEDAPUNGLUYKE_SLG { get; set; }
        public Decimal TYLEDAPUNGLUYKE_KLG { get; set; }
        public String MAXDATE { get; set; }


    }

    public class ReturnNOI_TINH
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

        public NOI_TINH NOI_TINHReport { get; set; }
        public List<NOI_TINH> ListNOI_TINHReport;

        public MetaData MetaData { get; set; }


    }
    #endregion

    //Phần lấy dữ liệu CHI TIẾT LIÊN TỈNH
    public class CHI_TIET_LIEN_TINH
    {
        public int STT { get; set; }
        //CHIEU DEN
        public String HUONG_DEN { get; set; }
        public String ITEMCODE_DEN { get; set; }
        public String WEIGHT_DEN { get; set; }
        public String FROMPOSCODE_DEN { get; set; }
        public String TOPOSCODE_DEN { get; set; }
        public String MAILTRIPNUMBER_DEN { get; set; }
        public String MAILTRIPDATE_DEN { get; set; }
        public String POSTBAGINDEX_DEN { get; set; }

        public String POSTBAGCODE_DEN { get; set; }
        public String TO_CHAR_ACCEPTTIME_DEN { get; set; }
        public String TO_CHAR_BC37CODE_DEN { get; set; }

        public String TO_CHAR_BC37CREATETIME_DEN { get; set; }
        public String IDVNPOST_DEN { get; set; }
        public String CONVERT_3_IDVNPOST_DEN { get; set; }


        //CHIEU DI
        public String HUONG_DI { get; set; }
        public String ITEMCODE_DI { get; set; }
        //public String WEIGHT_DI { get; set; }
        public String FROMPOSCODE_DI { get; set; }
        public String TOPOSCODE_DI { get; set; }
        public String MAILTRIPNUMBER_DI { get; set; }
        public String MAILTRIPDATE_DI { get; set; }
        public String POSTBAGINDEX_DI { get; set; }

        public String POSTBAGCODE_DI { get; set; }
        public String TO_CHAR_ACCEPTTIME_DI { get; set; }
        public String TO_CHAR_BC37CODE_DI { get; set; }

        public String TO_CHAR_BC37CREATETIME_DI { get; set; }
        public String IDVNPOST_DI { get; set; }
        public String CONVERT_3_IDVNPOST_DI { get; set; }
    }

    public class ReturnCHI_TIET_LIEN_TINH
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

        public CHI_TIET_LIEN_TINH CHI_TIET_LIEN_TINHReport { get; set; }
        public List<CHI_TIET_LIEN_TINH> ListCHI_TIET_LIEN_TINHReport;

        
        
    }
}