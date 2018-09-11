using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T41.Areas.Admin.Model.DataModel
{
    
    //Phần lấy dữ iệu của bảng edi_consigment_resdit_event
    public class RECEPTACLE_Detail
    {
        public String CONSIGMENTID { get; set; }
        public String EVTCODE { get; set; }
        public String EVTDATE { get; set; }
        public String EVTTIME { get; set; }
        public String EVTLOCATIONS { get; set; }
        public String FLIGHTNUMBER { get; set; }
        public String DEPARTURELOC { get; set; }
        public String ARRIVALLOC { get; set; }
        public String DEPARTUREDATE { get; set; }
        public String DEPARTURETIME { get; set; }
        public String ARRIVALDATE { get; set; }
        public String ARRIVALTIME { get; set; }
        public String EQUIPMENTID { get; set; }
        public String CONTAINERTYPE { get; set; }
        public String RECEPTACLEID { get; set; }
        public String MESSAGEID { get; set; }
        public String SENDERMAILBOX { get; set; }


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