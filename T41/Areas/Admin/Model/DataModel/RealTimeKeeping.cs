using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Model.DataModel
{
    public class RealTimeKeeping
    {
        public int MAKIP { get; set; }

        public string TENKIP { get; set; }

        public int CA { get; set; }

        public int DONVI { get; set; }

        public String TENDONVI { get; set; }

        public int TUGIO { get; set; }

        public int DENGIO { get; set; }
    }
    public class RealTimekeepingKipDetail
    {
        public String Ca { get; set; }
        public String TenKip { get; set; }
        public String ThoiGian { get; set; }
        public int TongGioLam { get; set; }
        public int SoNguoi { get; set; }
        public int Gio8Den9h { get; set; }
        public int Gio9Den10h { get; set; }
        public int Gio10Den11h { get; set; }
        public int Gio11Den12h { get; set; }
        public int Gio12Den13h { get; set; }
        public int Gio13Den14h { get; set; }
        public int Gio14Den15h { get; set; }
        public int Gio15Den16h { get; set; }
        public int Gio16Den17h { get; set; }
        public int Gio17Den18h { get; set; }
        public int Gio18Den19h { get; set; }
        public int Gio19Den20h { get; set; }
        public int Gio20Den21h { get; set; }
        public int Gio21Den22h { get; set; }
        public int Gio22Den23h { get; set; }
        public int Gio23Den24h { get; set; }
        public int Gio24Den1h { get; set; }
        public int Gio1Den2h { get; set; }
        public int Gio2Den3h { get; set; }
        public int Gio3Den4h { get; set; }
        public int Gio4Den5h { get; set; }
        public int Gio5Den6h { get; set; }
        public int Gio6Den7h { get; set; }
        public int Gio7Den8h { get; set; }
        


    }

    public class RealSumTimekeepingKipDetail
    {
        public int SumSoNguoi { get; set; }
        public int SumDen9h { get; set; }
        public int SumDen10h { get; set; }
        public int SumDen11h { get; set; }
        public int SumDen12h { get; set; }
        public int SumDen13h { get; set; }
        public int SumDen14h { get; set; }
        public int SumDen15h { get; set; }
        public int SumDen16h { get; set; }
        public int SumDen17h { get; set; }
        public int SumDen18h { get; set; }
        public int SumDen19h { get; set; }
        public int SumDen20h { get; set; }
        public int SumDen21h { get; set; }
        public int SumDen22h { get; set; }
        public int SumDen23h { get; set; }
        public int SumDen24h { get; set; }
        public int SumDen1h { get; set; }
        public int SumDen2h { get; set; }
        public int SumDen3h { get; set; }
        public int SumDen4h { get; set; }
        public int SumDen5h { get; set; }
        public int SumDen6h { get; set; }
        public int SumDen7h { get; set; }
        public int SumDen8h { get; set; }




    }

    public class RealSumSLKLTimekeepingKipDetail
    {
        public Decimal SLDen9h { get; set; }
        public Decimal KLDen9h { get; set; }
        public Decimal SLDen10h { get; set; }
        public Decimal KLDen10h { get; set; }
        public Decimal SLDen11h { get; set; }
        public Decimal KLDen11h { get; set; }
        public Decimal SLDen12h { get; set; }
        public Decimal KLDen12h { get; set; }
        public Decimal SLDen13h { get; set; }
        public Decimal KLDen13h { get; set; }
        public Decimal SLDen14h { get; set; }
        public Decimal KLDen14h { get; set; }
        public Decimal SLDen15h { get; set; }
        public Decimal KLDen15h { get; set; }
        public Decimal SLDen16h { get; set; }
        public Decimal KLDen16h { get; set; }
        public Decimal SLDen17h { get; set; }
        public Decimal KLDen17h { get; set; }
        public Decimal SLDen18h { get; set; }
        public Decimal KLDen18h { get; set; }
        public Decimal SLDen19h { get; set; }
        public Decimal KLDen19h { get; set; }
        public Decimal SLDen20h { get; set; }
        public Decimal KLDen20h { get; set; }
        public Decimal SLDen21h { get; set; }
        public Decimal KLDen21h { get; set; }
        public Decimal SLDen22h { get; set; }
        public Decimal KLDen22h { get; set; }
        public Decimal SLDen23h { get; set; }
        public Decimal KLDen23h { get; set; }
        public Decimal SLDen24h { get; set; }
        public Decimal KLDen24h { get; set; }
        public Decimal SLDen1h { get; set; }
        public Decimal KLDen1h { get; set; }
        public Decimal SLDen2h { get; set; }
        public Decimal KLDen2h { get; set; }
        public Decimal SLDen3h { get; set; }
        public Decimal KLDen3h { get; set; }
        public Decimal SLDen4h { get; set; }
        public Decimal KLDen4h { get; set; }
        public Decimal SLDen5h { get; set; }
        public Decimal KLDen5h { get; set; }
        public Decimal SLDen6h { get; set; }
        public Decimal KLDen6h { get; set; }
        public Decimal SLDen7h { get; set; }
        public Decimal KLDen7h { get; set; }
        public Decimal SLDen8h { get; set; }
        public Decimal KLDen8h { get; set; }



    }

    public class RealTimekeepingTitleDetail
    {
        public String TenChucDanh { get; set; }
        public String Loai { get; set; }
        public int SoNguoi { get; set; }
        public int Gio8Den9h { get; set; }
        public int Gio9Den10h { get; set; }
        public int Gio10Den11h { get; set; }
        public int Gio11Den12h { get; set; }
        public int Gio12Den13h { get; set; }
        public int Gio13Den14h { get; set; }
        public int Gio14Den15h { get; set; }
        public int Gio15Den16h { get; set; }
        public int Gio16Den17h { get; set; }
        public int Gio17Den18h { get; set; }
        public int Gio18Den19h { get; set; }
        public int Gio19Den20h { get; set; }
        public int Gio20Den21h { get; set; }
        public int Gio21Den22h { get; set; }
        public int Gio22Den23h { get; set; }
        public int Gio23Den24h { get; set; }
        public int Gio24Den1h { get; set; }
        public int Gio1Den2h { get; set; }
        public int Gio2Den3h { get; set; }
        public int Gio3Den4h { get; set; }
        public int Gio4Den5h { get; set; }
        public int Gio5Den6h { get; set; }
        public int Gio6Den7h { get; set; }
        public int Gio7Den8h { get; set; }



    }
    public class RealSumTimekeepingTitleDetail
    {
        public int SumSoNguoi { get; set; }
        public int SumDen9h { get; set; }
        public int SumDen10h { get; set; }
        public int SumDen11h { get; set; }
        public int SumDen12h { get; set; }
        public int SumDen13h { get; set; }
        public int SumDen14h { get; set; }
        public int SumDen15h { get; set; }
        public int SumDen16h { get; set; }
        public int SumDen17h { get; set; }
        public int SumDen18h { get; set; }
        public int SumDen19h { get; set; }
        public int SumDen20h { get; set; }
        public int SumDen21h { get; set; }
        public int SumDen22h { get; set; }
        public int SumDen23h { get; set; }
        public int SumDen24h { get; set; }
        public int SumDen1h { get; set; }
        public int SumDen2h { get; set; }
        public int SumDen3h { get; set; }
        public int SumDen4h { get; set; }
        public int SumDen5h { get; set; }
        public int SumDen6h { get; set; }
        public int SumDen7h { get; set; }
        public int SumDen8h { get; set; }




    }

    public class RealSumSLKLTimekeepingTitleDetail
    {
        public Decimal SLDen9h { get; set; }
        public Decimal KLDen9h { get; set; }
        public Decimal SLDen10h { get; set; }
        public Decimal KLDen10h { get; set; }
        public Decimal SLDen11h { get; set; }
        public Decimal KLDen11h { get; set; }
        public Decimal SLDen12h { get; set; }
        public Decimal KLDen12h { get; set; }
        public Decimal SLDen13h { get; set; }
        public Decimal KLDen13h { get; set; }
        public Decimal SLDen14h { get; set; }
        public Decimal KLDen14h { get; set; }
        public Decimal SLDen15h { get; set; }
        public Decimal KLDen15h { get; set; }
        public Decimal SLDen16h { get; set; }
        public Decimal KLDen16h { get; set; }
        public Decimal SLDen17h { get; set; }
        public Decimal KLDen17h { get; set; }
        public Decimal SLDen18h { get; set; }
        public Decimal KLDen18h { get; set; }
        public Decimal SLDen19h { get; set; }
        public Decimal KLDen19h { get; set; }
        public Decimal SLDen20h { get; set; }
        public Decimal KLDen20h { get; set; }
        public Decimal SLDen21h { get; set; }
        public Decimal KLDen21h { get; set; }
        public Decimal SLDen22h { get; set; }
        public Decimal KLDen22h { get; set; }
        public Decimal SLDen23h { get; set; }
        public Decimal KLDen23h { get; set; }
        public Decimal SLDen24h { get; set; }
        public Decimal KLDen24h { get; set; }
        public Decimal SLDen1h { get; set; }
        public Decimal KLDen1h { get; set; }
        public Decimal SLDen2h { get; set; }
        public Decimal KLDen2h { get; set; }
        public Decimal SLDen3h { get; set; }
        public Decimal KLDen3h { get; set; }
        public Decimal SLDen4h { get; set; }
        public Decimal KLDen4h { get; set; }
        public Decimal SLDen5h { get; set; }
        public Decimal KLDen5h { get; set; }
        public Decimal SLDen6h { get; set; }
        public Decimal KLDen6h { get; set; }
        public Decimal SLDen7h { get; set; }
        public Decimal KLDen7h { get; set; }
        public Decimal SLDen8h { get; set; }
        public Decimal KLDen8h { get; set; }



    }
    public class RealTimekeepingDetail
    {
        public String Ma { get; set; }
        public String Ten { get; set; }
        public String ChucDanh { get; set; }
        public String Loai { get; set; }
        public String Ca { get; set; }
        public String ThoiGian { get; set; }
        public int TongGioLam { get; set; }
        public String Kip1 { get; set; }
        public String Kip2 { get; set; }
        public String Kip3 { get; set; }
        public String Kip4 { get; set; }
        public String Kip5 { get; set; }
        public String Kip6 { get; set; }
        public String Kip7 { get; set; }
        public String Kip8 { get; set; }
        public String Kip9 { get; set; }
        public String Kip10 { get; set; }
        public String Kip11 { get; set; }
        public String Kip12 { get; set; }
        public String Kip13 { get; set; }
        public String Kip14 { get; set; }
        public String Kip15 { get; set; }
        public String Kip16 { get; set; }
        public String Kip17 { get; set; }
        public String Kip18 { get; set; }
        public String Kip19 { get; set; }
        public String Kip20 { get; set; }

        public String Gio8Den9h { get; set; }
        public String Gio9Den10h { get; set; }
        public String Gio10Den11h { get; set; }
        public String Gio11Den12h { get; set; }
        public String Gio12Den13h { get; set; }
        public String Gio13Den14h { get; set; }
        public String Gio14Den15h { get; set; }
        public String Gio15Den16h { get; set; }
        public String Gio16Den17h { get; set; }
        public String Gio17Den18h { get; set; }
        public String Gio18Den19h { get; set; }
        public String Gio19Den20h { get; set; }
        public String Gio20Den21h { get; set; }
        public String Gio21Den22h { get; set; }
        public String Gio22Den23h { get; set; }
        public String Gio23Den24h { get; set; }
        public String Gio24Den1h { get; set; }
        public String Gio1Den2h { get; set; }
        public String Gio2Den3h { get; set; }
        public String Gio3Den4h { get; set; }
        public String Gio4Den5h { get; set; }
        public String Gio5Den6h { get; set; }
        public String Gio6Den7h { get; set; }
        public String Gio7Den8h { get; set; }
        public String TGVAO { get; set; }
        public String TGRA { get; set; }



    }
    public class RealSumTimekeepingDetail
    {
        public int Kip1 { get; set; }
        public int Kip2 { get; set; }
        public int Kip3 { get; set; }
        public int Kip4 { get; set; }
        public int Kip5 { get; set; }
        public int Kip6 { get; set; }
        public int Kip7 { get; set; }
        public int Kip8 { get; set; }
        public int Kip9 { get; set; }
        public int Kip10 { get; set; }
        public int Kip11 { get; set; }
        public int Kip12 { get; set; }
        public int Kip13 { get; set; }
        public int Kip14 { get; set; }
        public int Kip15 { get; set; }
        public int Kip16 { get; set; }
        public int Kip17 { get; set; }
        public int Kip18 { get; set; }
        public int Kip19 { get; set; }
        public int Kip20 { get; set; }
        public int Den9h { get; set; }
        public int Den10h { get; set; }
        public int Den11h { get; set; }
        public int Den12h { get; set; }
        public int Den13h { get; set; }
        public int Den14h { get; set; }
        public int Den15h { get; set; }
        public int Den16h { get; set; }
        public int Den17h { get; set; }
        public int Den18h { get; set; }
        public int Den19h { get; set; }
        public int Den20h { get; set; }
        public int Den21h { get; set; }
        public int Den22h { get; set; }
        public int Den23h { get; set; }
        public int Den24h { get; set; }
        public int Den1h { get; set; }
        public int Den2h { get; set; }
        public int Den3h { get; set; }
        public int Den4h { get; set; }
        public int Den5h { get; set; }
        public int Den6h { get; set; }
        public int Den7h { get; set; }
        public int Den8h { get; set; }

    }
    public class ReturnRealTimekeeping
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

        public RealTimekeepingKipDetail RealTimekeepingKipReport { get; set; }
        public List<RealTimekeepingKipDetail> RealListTimekeepingKipReport;

        public RealSumTimekeepingKipDetail RealSumTimekeepingKipReport { get; set; }
        public List<RealSumTimekeepingKipDetail> RealListSumTimekeepingKipReport;

        public RealSumSLKLTimekeepingKipDetail RealSumSLKLTimekeepingKipReport { get; set; }
        public List<RealSumSLKLTimekeepingKipDetail> RealListSumSLKLTimekeepingKipReport;

        public RealTimekeepingTitleDetail RealTimekeepingTitleReport { get; set; }
        public List<RealTimekeepingTitleDetail> RealListTimekeepingTitleReport;

        public RealSumTimekeepingTitleDetail RealSumTimekeepingTitleReport { get; set; }
        public List<RealSumTimekeepingTitleDetail> RealListSumTimekeepingTitleReport;

        public RealSumSLKLTimekeepingTitleDetail RealSumSLKLTimekeepingTitleReport { get; set; }
        public List<RealSumSLKLTimekeepingTitleDetail> RealListSumSLKLTimekeepingTitleReport;

        public RealTimekeepingDetail RealTimekeepingReport { get; set; }
        public List<RealTimekeepingDetail> RealListTimekeepingReport;

        public RealSumTimekeepingDetail RealSumTimekeepingReport { get; set; }
        public List<RealSumTimekeepingDetail> RealListSumTimekeepingReport;



        public MetaData MetaData { get; set; }


    }


    
    public class ReturnRealSummaryTimekeeping
    {
        public string Code { get; set; }
        public string Message { get; set; }

       
        public MetaData MetaData { get; set; }
    }
}