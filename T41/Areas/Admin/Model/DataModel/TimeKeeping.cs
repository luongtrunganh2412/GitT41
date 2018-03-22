using System;
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
    public class TimekeepingDetail
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