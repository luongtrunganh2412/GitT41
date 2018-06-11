using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;
using T41.Areas.Admin.Common;
using System.Data;
using T41.Areas.Admin.Model.DataModel;
using System.Data.SqlClient;

namespace T41.Areas.Admin.Data
{
    // Phần lấy dữ liệu tuyến phát từ bảng gps_mailroute
    

    public class RoadwayTransportRepository
    {
        #region GetALLMailroutecode
        public IEnumerable<MAILROUTE> GetAllMailRouteCode()
        {
            List<MAILROUTE> listMAILROUTE = null;
            MAILROUTE oMAILROUTE = null;

            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDCOracleConnection;
                    cm.CommandText = string.Format("Select A.mailroutecode,mailroutename From gps_mailroute A " +
                                "inner join bccp_mailroutetype B on A.MailrouteTypeCode = B.MailrouteTypeCode " +
                                "inner join bccp_transporttype C on A.TransportTypeCode = C.TransportTypeCode " +
                                "inner join gps_mailroute_zone D on A.MailrouteCode = D.MailrouteCode " +
                                "where  D.Zone >0 " +
                                "order by A.mailroutecode");
                    cm.CommandType = CommandType.Text;
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        listMAILROUTE = new List<MAILROUTE>();
                        while (dr.Read())
                        {
                            oMAILROUTE = new MAILROUTE();
                            oMAILROUTE.MAILROUTECODE = dr["MAILROUTECODE"].ToString();
                            oMAILROUTE.MAILROUTENAME = dr["MAILROUTENAME"].ToString();
                            listMAILROUTE.Add(oMAILROUTE);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "GetAllMailRouteCode" + ex.Message);
                listMAILROUTE = null;
            }

            return listMAILROUTE;
        }
        #endregion
        private String GetConditionTuyen(String mailroutecode)
        {
            if (mailroutecode == "0")
            {
                return "";
            }
            else
            {
                return " And A.MailRouteCode = '" + mailroutecode + "' ";
            }
        }

        private string GetConditionCap(string cap)
        {
            if (cap == "0")
            {
                return "";
            }
            else
            {
                return " And typecode.MailRouteTypeCode = '" + cap + "' ";
            }
        }

        private string GetConditionLoaiPT(int loaipt)
        {
            if (loaipt == 0)
            {
                return "";
            }
            else
            {
                return " And transport.TransportTypeCode = " + loaipt + " ";
            }
        }

        private string GetConditionVung(int vung)
        {
            if (vung == 0)
            {
                return "";
            }
            else
            {
                return " And Zone.Zone = " + vung + " ";
            }
        }

        //Phần chi tiết của bảng LOAD_DATA1
        #region LOAD_DATA1          
        public ReturnRoadwayTransport LOAD_DATA1(string mailroutecode, int tungay, int denngay, int vung, string cap, int loaipt)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnRoadwayTransport _returnRoadwayTransport = new ReturnRoadwayTransport();


            List<RoadwayTransportDetail> listRoadwayTransportDetail = null;
            RoadwayTransportDetail oRoadwayTransportDetail = null;
            try
            {

                // Gọi vào DB để lấy dữ liệu.
                using (SqlCommand cmd = new SqlCommand())
                {
                    SqlConnection conn = new SqlConnection("Server= 192.168.68.90;Database= Ems_Enterprise;UID = sa;pwd= 12345678;");
                    cmd.Connection = conn;
                    cmd.CommandText = string.Format(
                    "Select CT.Ngay,CT.BD10,sum(CT.SLTui) as SLTui,sum(CT.KL)as KL,CT.BCDong,CT.TenBCDong,CT.BCNhan,CT.TenBCNhan,CT.IDHanhTrinh,CT.HanhTrinh,CT.Cap,CT.loaipt " +
                    "From " +
                             "(Select SUBSTRING(pos.BC37Date,7,2) + '/' + SUBSTRING(pos.BC37Date,5,2) + '/' + SUBSTRING(pos.BC37Date,1,4) as Ngay, " +
                                "'A-'+ BC37Code as BD10,Count(*) as SLTui,Round(Sum(TotalWeight),2) as KL, " +
                                "pos.StartPO as BCDong ,dbo.TimTenBC(pos.StartPO) as TenBCDong,pos.EndPO as BCNhan ,dbo.TimTenBC(pos.EndPO) as TenBCNhan, " +
                                "code.MailrouteCode as IDHanhTrinh,code.MailRouteName as HanhTrinh, " +
                                "(case when '" + cap + "' = '0' then 'Tât ca' else '" + cap + "' end) as Cap, " +
                                "transport.TransporttypeName as loaipt " +
                                "From " +
                                "(Select bc37.BC37Date,bc37.BC37Code,bc37.BC37FromPosCode as StartPO,bc37.BC37ToPosCode as EndPO, " +
                                    "bc37.MailRouteCode,bc37.MailRouteScheduleCode, " +
                                    "bag.FromPosCode,bag.ToPosCode,bag.Year, " +
                                    "bag.MailTripNumber,bag.PostBagIndex,bag.MailTripType,bag.ServiceCode, " +
                                    "bag.Weight + bag.CaseWeight/1000 as TotalWeight,bag.Quantity " +
                                "From Postbag bag " +
                                "Inner Join  " +
                                    "(Select A.Bc37Code,B.Bc37FromPosCode,B.Bc37ToPosCode,B.Bc37Date,B.Bc37Index,A.MailRouteCode,A.MailRouteScheduleCode, " +
                                        "B.FromPosCode,B.ToPosCode,B.Year,B.MailTripNumber,B.PostBagIndex,B.MailTripType,B.ServiceCode " +
                                    "From Bc37_TH A  " +
                                    "Inner Join Mailtriptransportpostbag B " +
                                    "On A.FromPosCode = B.Bc37FromPosCode And A.ToPosCode = B.Bc37ToPosCode " +
                                        "And A.Bc37Date = B.Bc37Date And A.Bc37Index = B.Bc37Index " +
                                    "Where A.Bc37Date Between " + tungay + "  and " + denngay + " " +
                                        "And B.ServiceCode = 'E' " +
                                        "And Substring(A.FromPosCode,1,2)<>Substring(A.ToPosCode,1,2) " +
                                        GetConditionTuyen(mailroutecode) +
                                        ") bc37 " +
                                "On bag.FromPosCode = bc37.FromPosCode And bag.toPosCode = bc37.ToPosCode " +
                                    "And bag.Year = bc37.Year And bag.MailTripNumber = bc37.MailTripNumber " +
                                    "And bag.ServiceCode = bc37.ServiceCode And bag.PostBagIndex = bc37.PostBagIndex) pos " +
                                "Left Join mailroute code " +
                                "On pos.MailRouteCode = code.MailRouteCode " +

                                "Left Join mailrouteschedule schedule " +
                                "On pos.MailRouteScheduleCode = schedule.MailRouteScheduleCode " +

                                "Left Join MailrouteType typecode " +
                                "On code.MailRoutetypeCode = typecode.MailRoutetypeCode " +

                                "Left Join TransportType transport " +
                                "On code.TransporttypeCode = transport.TransporttypeCode " +

                                "Inner Join mailroutezone zone " +
                                "On pos.MailRouteCode = zone.MailRouteCode " +

                                "Where pos.BC37Date Between " + tungay + "  and " + denngay + " " +
                                GetConditionCap(cap) +
                                GetConditionLoaiPT(loaipt) +
                                GetConditionVung(vung) +
                                "Group by Pos.BC37Date,code.MailRouteName,pos.StartPO,dbo.TimTenBC(pos.StartPO), " +
                                "pos.EndPO,dbo.TimTenBC(pos.EndPO),BC37Code,code.MailrouteCode,transport.TransporttypeName " +

                              "Union All " +

                              "Select SUBSTRING(pos.BC37Date,7,2) + '/' + SUBSTRING(pos.BC37Date,5,2) + '/' + SUBSTRING(pos.BC37Date,1,4) as Ngay, " +
                                "'A-'+ BC37Code as BD10,Count(*) as SLTui,Round(Sum(TotalWeight),2) as KL, " +
                                "pos.StartPO as BCDong ,dbo.TimTenBC(pos.StartPO) as TenBCDong,pos.EndPO as BCNhan ,dbo.TimTenBC(pos.EndPO) as TenBCNhan, " +
                                "code.MailrouteCode as IDHanhTrinh,code.MailRouteName as HanhTrinh, " +
                                "(case when '" + cap + "' = '0' then 'Tât ca' else '" + cap + "' end) as Cap, " +
                                "transport.TransporttypeName as loaipt " +
                                "From " +
                                "(Select bc37.BC37Date,bc37.BC37Code,bc37.BC37FromPosCode as StartPO,bc37.BC37ToPosCode as EndPO, " +
                                    "bc37.MailRouteCode,bc37.MailRouteScheduleCode, " +
                                    "bag.FromPosCode,bag.ToPosCode,bag.Year, " +
                                    "bag.MailTripNumber,bag.PostBagIndex,bag.MailTripType,bag.ServiceCode, " +
                                    "bag.Weight + bag.CaseWeight/1000 as TotalWeight,bag.Quantity " +
                                "From Postbag bag " +
                                "Inner Join  " +
                                    "(Select A.Bc37Code,B.Bc37FromPosCode,B.Bc37ToPosCode,B.Bc37Date,B.Bc37Index,A.MailRouteCode,A.MailRouteScheduleCode, " +
                                        "B.FromPosCode,B.ToPosCode,B.Year,B.MailTripNumber,B.PostBagIndex,B.MailTripType,B.ServiceCode " +
                                    "From Bc37_TH A  " +
                                    "Inner Join Mailtriptransportpostbag B " +
                                    "On A.FromPosCode = B.Bc37FromPosCode And A.ToPosCode = B.Bc37ToPosCode " +
                                        "And A.Bc37Date = B.Bc37Date And A.Bc37Index = B.Bc37Index " +
                                    "Where A.Bc37Date Between " + tungay + "  and " + denngay + " " +
                                        "And B.ServiceCode = 'E' " +
                                        "And A.TransportTypeCode <> 3 " +
                                        " And A.FromPosCode in (100916,100915,550916,550915,700916,700915,100959,550959,700959,590959) " +
                                        " And A.ToPosCode in (100916,100915,550916,550915,700916,700915,100959,550959,700959,590959) " +
                                        GetConditionTuyen(mailroutecode) +
                                        ") bc37 " +
                                "On bag.FromPosCode = bc37.FromPosCode And bag.toPosCode = bc37.ToPosCode " +
                                    "And bag.Year = bc37.Year And bag.MailTripNumber = bc37.MailTripNumber " +
                                    "And bag.ServiceCode = bc37.ServiceCode And bag.PostBagIndex = bc37.PostBagIndex) pos " +
                                "Left Join mailroute code " +
                                "On pos.MailRouteCode = code.MailRouteCode " +

                                "Left Join mailrouteschedule schedule " +
                                "On pos.MailRouteScheduleCode = schedule.MailRouteScheduleCode " +

                                "Left Join MailrouteType typecode " +
                                "On code.MailRoutetypeCode = typecode.MailRoutetypeCode " +

                                "Left Join TransportType transport " +
                                "On code.TransporttypeCode = transport.TransporttypeCode " +

                                "Inner Join mailroutezone zone " +
                                "On pos.MailRouteCode = zone.MailRouteCode " +

                                "Where pos.BC37Date Between " + tungay + "  and " + denngay + " " +
                                GetConditionCap(cap) +
                                GetConditionLoaiPT(loaipt) +
                                GetConditionVung(vung) +
                                "Group by Pos.BC37Date,code.MailRouteName,pos.StartPO,dbo.TimTenBC(pos.StartPO), " +
                                "pos.EndPO,dbo.TimTenBC(pos.EndPO),BC37Code,code.MailrouteCode,transport.TransporttypeName) CT" +
                            " Group By CT.Ngay,CT.BD10,CT.BCDong,CT.TenBCDong,CT.BCNhan,CT.TenBCNhan,CT.IDHanhTrinh,CT.HanhTrinh,CT.Cap,CT.loaipt", conn);
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        listRoadwayTransportDetail = new List<RoadwayTransportDetail>();
                        while (dr.Read())
                        {
                            oRoadwayTransportDetail = new RoadwayTransportDetail();
                            oRoadwayTransportDetail.NGAY = dr["Ngay"].ToString();
                            oRoadwayTransportDetail.BD10 = dr["BD10"].ToString();
                            oRoadwayTransportDetail.BCDONG = dr["BCDong"].ToString();
                            oRoadwayTransportDetail.TENBCDONG = dr["TenBCDong"].ToString();
                            oRoadwayTransportDetail.BCNHAN = dr["BCNhan"].ToString();
                            oRoadwayTransportDetail.TENBCNHAN = dr["TenBCNhan"].ToString();
                            oRoadwayTransportDetail.IDHANHTRINH = dr["IDHanhTrinh"].ToString();
                            oRoadwayTransportDetail.HANHTRINH = dr["HanhTrinh"].ToString();
                            oRoadwayTransportDetail.CAP = dr["Cap"].ToString();
                            oRoadwayTransportDetail.LOAIPT = dr["loaipt"].ToString();
                            listRoadwayTransportDetail.Add(oRoadwayTransportDetail);
                        }
                        _returnRoadwayTransport.Code = "00";
                        _returnRoadwayTransport.Message = "Lấy dữ liệu thành công.";
                        _returnRoadwayTransport.ListRoadwayTransportReport = listRoadwayTransportDetail;
                    }


                }
            }
            catch (Exception ex)
            {
                _returnRoadwayTransport.Code = "99";
                _returnRoadwayTransport.Message = "Lỗi xử lý dữ liệu";

            }
            return _returnRoadwayTransport;
        }



        #endregion


    }

}

