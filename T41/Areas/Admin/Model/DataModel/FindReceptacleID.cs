using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Model.DataModel
{
    
    //Phần lấy dữ iệu của bảng edi_consigment_resdit_event
    public class RECEPTACLE_Detail
    {
        public int ID { get; set; }
        public String NGAY { get; set; }
        public String FLIGHTNUMBER { get; set; }
        public String MO_TA { get; set; }
        public String VI_TRI { get; set; }
        public String CN38 { get; set; }
        

    }

    public class ReturnRECEPTACLE
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

        public RECEPTACLE_Detail ReceptacleReport { get; set; }
        public List<RECEPTACLE_Detail> ListReceptacleReport;
        
        public MetaData MetaData { get; set; }


    }
}