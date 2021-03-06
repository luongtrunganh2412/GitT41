﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Model.DataModel
{
    public class TimeKeeping
    {
        public int MAKIP { get; set; }

        public string TENKIP { get; set; }

        public int CA { get; set; }

        public int DONVI { get; set; }

        public String TENDONVI { get; set; }

        public int TUGIO { get; set; }

        public int DENGIO { get; set; }
    }
    public class TimekeepingKipDetail
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

    public class SumTimekeepingKipDetail
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

    public class TimekeepingTitleDetail
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

    public class TimekeepingDetail
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



    }
    public class ReturnTimekeeping
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

        public TimekeepingKipDetail TimekeepingKipReport { get; set; }
        public List<TimekeepingKipDetail> ListTimekeepingKipReport;

        public SumTimekeepingKipDetail SumTimekeepingKipReport { get; set; }
        public List<SumTimekeepingKipDetail> ListSumTimekeepingKipReport;

        public TimekeepingTitleDetail TimekeepingTitleReport { get; set; }
        public List<TimekeepingTitleDetail> ListTimekeepingTitleReport;

        public TimekeepingDetail TimekeepingReport { get; set; }
        public List<TimekeepingDetail> ListTimekeepingReport;



        public MetaData MetaData { get; set; }


    }


    public class MetaData
    {
        public string from_to_date { get; set; }
        public string donvi { get; set; }
        public string hoatdong { get; set; }
        public string buucuc { get; set; }
        public string diadiem { get; set; }
        public string ca { get; set; }
    }
    public class ReturnSummaryTimekeeping
    {
        public string Code { get; set; }
        public string Message { get; set; }

       
        public MetaData MetaData { get; set; }
    }
}