using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;
using T41.Areas.Admin.Common;
using System.Data;
using T41.Areas.Admin.Model.DataModel;
using System.Configuration;
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

        //Phần gọi đến procedure [INSERTBAOCAOCHITIETHANHTRINH] để tổng hợp dữ liệu
        #region TOTAL_DATA          
        public ReturnRoadwayTransport TOTAL_DATA(int fromdate, int todate)
        {
            int thang = 0;
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            thang = Convert.ToInt32(fromdate.ToString().Substring(0, 6)) - 1;

            ReturnRoadwayTransport _returnRoadwayTransport = new ReturnRoadwayTransport();
            //List<RoadwayTransportDetail_TG> listRoadwayTransportDetail_TG = null;
            //RoadwayTransportDetail_TG oRoadwayTransportDetail_TG = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (SqlCommand cmd = new SqlCommand())
                {

                    DataSet ds = new DataSet();
                    SqlConnection conn = new SqlConnection("Server= 192.168.68.90;Database= Ems_Enterprise;UID = sa;pwd= 12345678;");
                    cmd.Connection = conn;
                    cmd.CommandText = string.Format("[dbo].[INSERTBAOCAOCHITIETHANHTRINH]", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Thang", SqlDbType.Int)).Value = thang;
                    cmd.Parameters.Add(new SqlParameter("@Tungay", SqlDbType.Int)).Value = fromdate;
                    cmd.Parameters.Add(new SqlParameter("@Denngay", SqlDbType.Int)).Value = todate;

                    cmd.CommandTimeout = 2000;
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    //SqlDataAdapter da1 = new SqlDataAdapter(cmd);
                    //conn.Close();                    
                    //da1.Fill(ds);
                    _returnRoadwayTransport.Code = "00";
                    _returnRoadwayTransport.Message = "Tổng dữ liệu thành công.";
                    cmd.Connection.Close();
                    cmd.Connection.Dispose();
 
                }
            }
            catch (Exception ex)
            {
                _returnRoadwayTransport.Code = "99";
                _returnRoadwayTransport.Message = "Lỗi xử lý dữ liệu";
                LogAPI.LogToFile(LogFileType.EXCEPTION, ex.Message);
                _returnRoadwayTransport = null;
            }
            return _returnRoadwayTransport;
        }



        #endregion

        //Phần Gọi dữ liệu của bảng tổng hợp theo đường thư
        #region LOAD_DATA1          
        public ReturnRoadwayTransport LOAD_DATA1(string mailroutecode, int fromdate, int todate, int vung, string cap, int loaipt)
        {
            int thang = 0;
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnRoadwayTransport _returnRoadwayTransport = new ReturnRoadwayTransport();
            thang = Convert.ToInt32(fromdate.ToString().Substring(0, 6));
            List<RoadwayTransportDetail_TH> listRoadwayTransportDetail_TH = null;
            RoadwayTransportDetail_TH oRoadwayTransportDetail_TH = null;
            try
            {

                // Gọi vào DB để lấy dữ liệu.
                using (SqlCommand cmd = new SqlCommand())
                {
                    
                    DataSet ds = new DataSet();                    
                    SqlConnection conn = new SqlConnection("Server= 192.168.68.90;Database= Ems_Enterprise;UID = sa;pwd= 12345678;");
                    cmd.Connection = conn;
                    //cmd.CommandText = string.Format("Select Ngay,BD10,SLTui,KL,BCDong,TenBCDong,BCNhan,TenBCNhan,IDHanhTrinh,HanhTrinh,Cap,loaipt From Bao_Cao_Chi_Tiet_Hanh_Trinh where thang = " + thang + "", conn);
                    //cmd.CommandType = CommandType.Text;
                    cmd.CommandText = string.Format("[dbo].[BANGKETONGHOPBD10VANCHUYENTHEODUONGTHU]", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Thang", SqlDbType.Int)).Value = thang;
                    
                    
                    cmd.CommandTimeout = 2000;
                    cmd.Connection.Open();

                    //SqlDataAdapter da1 = new SqlDataAdapter(cmd);
                    //conn.Close();                    
                    //da1.Fill(ds);
                    
                    
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        listRoadwayTransportDetail_TH = new List<RoadwayTransportDetail_TH>();
                        while (dr.Read())
                        {
                            oRoadwayTransportDetail_TH = new RoadwayTransportDetail_TH();
                            //oRoadwayTransportDetail.NGAY = dr["NGAY"].ToString();
                            //oRoadwayTransportDetail.BD10 = dr["BD10"].ToString();
                            //oRoadwayTransportDetail.SLTUI = dr["SLTUI"].ToString();
                            //oRoadwayTransportDetail.KL = dr["KL"].ToString();
                            //oRoadwayTransportDetail.BCDONG = dr["BCDONG"].ToString();



                            oRoadwayTransportDetail_TH.ID_HT = dr["ID_HT"].ToString();
                            oRoadwayTransportDetail_TH.TEN_HT = dr["TEN_HT"].ToString();
                            oRoadwayTransportDetail_TH.SLBD10 = dr["SLBD10"].ToString();
                            oRoadwayTransportDetail_TH.SLTUI = dr["SLTUI"].ToString();
                            oRoadwayTransportDetail_TH.KL = dr["KL"].ToString();
                            listRoadwayTransportDetail_TH.Add(oRoadwayTransportDetail_TH);
                        }
                        _returnRoadwayTransport.Code = "00";
                        _returnRoadwayTransport.Message = "Lấy dữ liệu thành công.";
                        _returnRoadwayTransport.ListRoadwayTransportReport_TH = listRoadwayTransportDetail_TH;
                        cmd.Connection.Close();
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

        //Phần Gọi dữ liệu của bảng chi tiết 
        #region LOAD_DATA2          
        public ReturnRoadwayTransport LOAD_DATA2(string mailroutecode, int fromdate, int todate, int vung, string cap, int loaipt, int page_size, int page_index)
        {
            int thang = 0;
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnRoadwayTransport _returnRoadwayTransport = new ReturnRoadwayTransport();
            thang = Convert.ToInt32(fromdate.ToString().Substring(0, 6));
            List<RoadwayTransportDetail_CT> listRoadwayTransportDetail_CT = null;
            RoadwayTransportDetail_CT oRoadwayTransportDetail_CT = null;
            try
            {

                // Gọi vào DB để lấy dữ liệu.
                using (SqlCommand cmd = new SqlCommand())
                {

                    DataSet ds = new DataSet();
                    SqlConnection conn = new SqlConnection("Server= 192.168.68.90;Database= Ems_Enterprise;UID = sa;pwd= 12345678;");
                    cmd.Connection = conn;
                    cmd.Connection.Open();
                    //cmd.CommandText = string.Format("Select Ngay,BD10,SLTui,KL,BCDong,TenBCDong,BCNhan,TenBCNhan,IDHanhTrinh,HanhTrinh,Cap,loaipt From Bao_Cao_Chi_Tiet_Hanh_Trinh where thang = " + thang + "", conn);
                    //cmd.CommandType = CommandType.Text;
                    cmd.CommandText = string.Format("[dbo].[BANGKECHITIETBD10VANCHUYENTHEODUONGTHU]", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Thang", SqlDbType.Int)).Value = thang;
                    cmd.Parameters.Add(new SqlParameter("@Pageindex", SqlDbType.Int)).Value = page_index;
                    cmd.Parameters.Add(new SqlParameter("@Pagesize", SqlDbType.Int)).Value = page_size;
                    //cmd.Parameters.Add("@Total", SqlDbType.Int);
                    //cmd.Parameters["@Total"].Direction = ParameterDirection.Output;

                    cmd.Parameters.Add(new SqlParameter("@Total", SqlDbType.Int, 0)).Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery(); // MISSING
                    //int retunvalue = (int)cmd.Parameters["@Total"].Value;

                    cmd.CommandTimeout = 2000;
                    
                    
                    //SqlDataAdapter da1 = new SqlDataAdapter(cmd);
                    //conn.Close();                    
                    //da1.Fill(ds);


                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        listRoadwayTransportDetail_CT = new List<RoadwayTransportDetail_CT>();
                        while (dr.Read())
                        {
                            oRoadwayTransportDetail_CT = new RoadwayTransportDetail_CT();
                            oRoadwayTransportDetail_CT.NGAY = dr["NGAY"].ToString();
                            oRoadwayTransportDetail_CT.BD10 = dr["BD10"].ToString();
                            oRoadwayTransportDetail_CT.SLTUI = dr["SLTUI"].ToString();
                            oRoadwayTransportDetail_CT.KL = dr["KL"].ToString();
                            oRoadwayTransportDetail_CT.BCDONG = dr["BCDONG"].ToString();
                            oRoadwayTransportDetail_CT.TENBCDONG = dr["TENBCDONG"].ToString();
                            oRoadwayTransportDetail_CT.BCNHAN = dr["BCNHAN"].ToString();
                            oRoadwayTransportDetail_CT.TENBCNHAN = dr["TENBCNHAN"].ToString();
                            oRoadwayTransportDetail_CT.IDHANHTRINH = dr["IDHANHTRINH"].ToString();
                            oRoadwayTransportDetail_CT.HANHTRINH = dr["HANHTRINH"].ToString();
                            oRoadwayTransportDetail_CT.CAP = dr["CAP"].ToString();
                            oRoadwayTransportDetail_CT.LOAIPT = dr["LOAIPT"].ToString();
                            listRoadwayTransportDetail_CT.Add(oRoadwayTransportDetail_CT);
                        }
                        cmd.Connection.Close();
                        _returnRoadwayTransport.Code = "00";
                        _returnRoadwayTransport.Message = "Lấy dữ liệu thành công.";
                        _returnRoadwayTransport.ListRoadwayTransportReport_CT = listRoadwayTransportDetail_CT;
                        //_returnRoadwayTransport.Total = retunvalue;
                        _returnRoadwayTransport.Total = Convert.ToInt32(cmd.Parameters["@Total"].Value.ToString());

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

        //Phần Gọi dữ liệu của bảng tổng hợp theo thời gian
        #region LOAD_DATA3          
        public ReturnRoadwayTransport LOAD_DATA3(string mailroutecode, int fromdate, int todate, int vung, string cap, int loaipt)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnRoadwayTransport _returnRoadwayTransport = new ReturnRoadwayTransport();
            List<RoadwayTransportDetail_TG> listRoadwayTransportDetail_TG = null;
            RoadwayTransportDetail_TG oRoadwayTransportDetail_TG = null;
            try
            {

                // Gọi vào DB để lấy dữ liệu.
                using (SqlCommand cmd = new SqlCommand())
                {

                    DataSet ds = new DataSet();
                    SqlConnection conn = new SqlConnection("Server= 192.168.68.90;Database= Ems_Enterprise;UID = sa;pwd= 12345678;");
                    cmd.Connection = conn;
                    cmd.CommandText = string.Format("[dbo].[BANGKETONGHOPBD10VANCHUYENTHEOTHOIGIAN]", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Tungay", SqlDbType.Int)).Value = fromdate;
                    cmd.Parameters.Add(new SqlParameter("@Denngay", SqlDbType.Int)).Value = todate;

                    cmd.CommandTimeout = 2000;
                    cmd.Connection.Open();

                    //SqlDataAdapter da1 = new SqlDataAdapter(cmd);
                    //conn.Close();                    
                    //da1.Fill(ds);


                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        listRoadwayTransportDetail_TG = new List<RoadwayTransportDetail_TG>();
                        while (dr.Read())
                        {
                            oRoadwayTransportDetail_TG = new RoadwayTransportDetail_TG();
                            oRoadwayTransportDetail_TG.NGAY = dr["NGAY"].ToString();
                            oRoadwayTransportDetail_TG.SLBD10 = Convert.ToInt32(dr["SLBD10"].ToString());
                            oRoadwayTransportDetail_TG.SLTUI = Convert.ToInt32(dr["SLTUI"].ToString());
                            oRoadwayTransportDetail_TG.KL = Convert.ToDecimal(dr["KL"].ToString());
                            listRoadwayTransportDetail_TG.Add(oRoadwayTransportDetail_TG);
                        }
                        _returnRoadwayTransport.Code = "00";
                        _returnRoadwayTransport.Message = "Lấy dữ liệu thành công.";
                        _returnRoadwayTransport.ListRoadwayTransportReport_TG = listRoadwayTransportDetail_TG;
                        cmd.Connection.Close();
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

