using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;
using T41.Areas.Admin.Common;
using System.Data;
using T41.Areas.Admin.Model.DataModel;

namespace T41.Areas.Admin.Data
{
    public class TimeKeepingRepository
    {
        // Phần lấy dữ liệu kíp từ bảng DM_KIP
        #region GetALLDMKip
        public IEnumerable<TimeKeeping> GetAllDMKip()
        {
            List<TimeKeeping> listTimeKeeping = null;
            TimeKeeping oTimeKeeping = null;
        
            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDCOracleConnection;
                    cm.CommandText = string.Format("SELECT * FROM DM_KIP WHERE DONVI =1");
                    //cm.CommandText = string.Format("SELECT * FROM DM_KIP WHERE DONVI = " + don_vi);
                    cm.CommandType = CommandType.Text;
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        listTimeKeeping = new List<TimeKeeping>();
                        while (dr.Read())
                        {
                            oTimeKeeping = new TimeKeeping();
                            oTimeKeeping.MAKIP = int.Parse(dr["MAKIP"].ToString());
                            oTimeKeeping.TENKIP = dr["TENKIP"].ToString();
                            listTimeKeeping.Add(oTimeKeeping);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "GetAllDMKip" + ex.Message);
                listTimeKeeping = null;
            }

            return listTimeKeeping;
        }
        #endregion
        //Phần chi tiết của bảng tổng hợp theo kíp
        #region TIMEKEEPING_KIP_DETAIL          
        public ReturnTimekeeping TIMEKEEPING_KIP_DETAIL(string ngay, int donvi, int ankip, int kip, ref long sumSO_NGUOI, ref long sumDen_9h, ref long sumDen_10h, ref long sumDen_11h, ref long sumDen_12h, ref long sumDen_13h, ref long sumDen_14h, ref long sumDen_15h, ref long sumDen_16h, ref long sumDen_17h, ref long sumDen_18h, ref long sumDen_19h, ref long sumDen_20h, ref long sumDen_21h, ref long sumDen_22h, ref long sumDen_23h, ref long sumDen_24h, ref long sumDen_1h, ref long sumDen_2h, ref long sumDen_3h, ref long sumDen_4h, ref long sumDen_5h, ref long sumDen_6h, ref long sumDen_7h, ref long sumDen_8h)              
        {
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnTimekeeping _returnTimekeeping = new ReturnTimekeeping();
            
            
            List<TimekeepingKipDetail> listTimekeepingKipDetail = null;
            TimekeepingKipDetail oTimekeepingKipDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = Helper.OraDCOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "EMS_CHAMCONG.TongHopTheoKipTK";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new OracleParameter("v_Ngay", OracleDbType.Int32)).Value = common.DateToInt(ngay);
                    switch (donvi)
                    {
                      case 1:
                          cmd.Parameters.Add(new OracleParameter("v_DonVi", OracleDbType.NVarchar2)).Value = "TTKTTN-HN";
                            break;
                      case 2:
                         cmd.Parameters.Add(new OracleParameter("v_DonVi", OracleDbType.NVarchar2)).Value = "TTKTTN-DN";
                            break;
                      case 3:
                          cmd.Parameters.Add(new OracleParameter("v_DonVi", OracleDbType.NVarchar2)).Value = "TTKTTN-HCM";
                            break;
                     }

                    cmd.Parameters.Add(new OracleParameter("v_AnKip", OracleDbType.Int32)).Value = ankip;
                    cmd.Parameters.Add(new OracleParameter("v_Kip", OracleDbType.Int32)).Value = kip;
                    cmd.Parameters.Add("v_List", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    OracleDataReader dr = Helper.ExecuteDataReader(cmd, Helper.OraDCOracleConnection);

                    if (dr.HasRows)
                    {
                        listTimekeepingKipDetail = new List<TimekeepingKipDetail>();
                        while (dr.Read())
                        {
                            oTimekeepingKipDetail = new TimekeepingKipDetail();
                            oTimekeepingKipDetail.Ca = dr["CA"].ToString();
                            oTimekeepingKipDetail.TenKip = dr["TEN_KIP"].ToString();
                            oTimekeepingKipDetail.ThoiGian = dr["THOI_GIAN"].ToString();
                            oTimekeepingKipDetail.TongGioLam = Convert.ToInt32(dr["TONG_GIO"].ToString());
                            oTimekeepingKipDetail.SoNguoi = Convert.ToInt32(dr["SO_NGUOI"].ToString());
                            oTimekeepingKipDetail.Gio8Den9h = Convert.ToInt32(dr["Den_9h"].ToString());
                            oTimekeepingKipDetail.Gio9Den10h = Convert.ToInt32(dr["Den_10h"].ToString());
                            oTimekeepingKipDetail.Gio10Den11h = Convert.ToInt32(dr["Den_11h"].ToString());
                            oTimekeepingKipDetail.Gio11Den12h = Convert.ToInt32(dr["Den_12h"].ToString());
                            oTimekeepingKipDetail.Gio12Den13h = Convert.ToInt32(dr["Den_13h"].ToString());
                            oTimekeepingKipDetail.Gio13Den14h = Convert.ToInt32(dr["Den_14h"].ToString());
                            oTimekeepingKipDetail.Gio14Den15h = Convert.ToInt32(dr["Den_15h"].ToString());
                            oTimekeepingKipDetail.Gio15Den16h = Convert.ToInt32(dr["Den_16h"].ToString());
                            oTimekeepingKipDetail.Gio16Den17h = Convert.ToInt32(dr["Den_17h"].ToString());
                            oTimekeepingKipDetail.Gio17Den18h = Convert.ToInt32(dr["Den_18h"].ToString());
                            oTimekeepingKipDetail.Gio18Den19h = Convert.ToInt32(dr["Den_19h"].ToString());
                            oTimekeepingKipDetail.Gio19Den20h = Convert.ToInt32(dr["Den_20h"].ToString());
                            oTimekeepingKipDetail.Gio20Den21h = Convert.ToInt32(dr["Den_21h"].ToString());
                            oTimekeepingKipDetail.Gio21Den22h = Convert.ToInt32(dr["Den_22h"].ToString());
                            oTimekeepingKipDetail.Gio22Den23h = Convert.ToInt32(dr["Den_23h"].ToString());
                            oTimekeepingKipDetail.Gio23Den24h = Convert.ToInt32(dr["Den_24h"].ToString());
                            oTimekeepingKipDetail.Gio24Den1h = Convert.ToInt32(dr["Den_1h"].ToString());
                            oTimekeepingKipDetail.Gio1Den2h = Convert.ToInt32(dr["Den_2h"].ToString());
                            oTimekeepingKipDetail.Gio2Den3h = Convert.ToInt32(dr["Den_3h"].ToString());
                            oTimekeepingKipDetail.Gio3Den4h = Convert.ToInt32(dr["Den_4h"].ToString());
                            oTimekeepingKipDetail.Gio4Den5h = Convert.ToInt32(dr["Den_5h"].ToString());
                            oTimekeepingKipDetail.Gio5Den6h = Convert.ToInt32(dr["Den_6h"].ToString());
                            oTimekeepingKipDetail.Gio6Den7h = Convert.ToInt32(dr["Den_7h"].ToString());
                            oTimekeepingKipDetail.Gio7Den8h = Convert.ToInt32(dr["Den_8h"].ToString());
                            listTimekeepingKipDetail.Add(oTimekeepingKipDetail);
                            sumSO_NGUOI += oTimekeepingKipDetail.SoNguoi;
                            sumDen_9h += oTimekeepingKipDetail.Gio8Den9h;
                            sumDen_10h += oTimekeepingKipDetail.Gio9Den10h;
                            sumDen_11h += oTimekeepingKipDetail.Gio10Den11h;
                            sumDen_12h += oTimekeepingKipDetail.Gio11Den12h;
                            sumDen_13h += oTimekeepingKipDetail.Gio12Den13h;
                            sumDen_14h += oTimekeepingKipDetail.Gio13Den14h;
                            sumDen_15h += oTimekeepingKipDetail.Gio14Den15h;
                            sumDen_16h += oTimekeepingKipDetail.Gio15Den16h;
                            sumDen_17h += oTimekeepingKipDetail.Gio16Den17h;
                            sumDen_18h += oTimekeepingKipDetail.Gio17Den18h;
                            sumDen_19h += oTimekeepingKipDetail.Gio18Den19h;
                            sumDen_20h += oTimekeepingKipDetail.Gio19Den20h;
                            sumDen_21h += oTimekeepingKipDetail.Gio20Den21h;
                            sumDen_22h += oTimekeepingKipDetail.Gio21Den22h;
                            sumDen_23h += oTimekeepingKipDetail.Gio22Den23h;
                            sumDen_24h += oTimekeepingKipDetail.Gio23Den24h;
                            sumDen_1h += oTimekeepingKipDetail.Gio24Den1h;
                            sumDen_2h += oTimekeepingKipDetail.Gio1Den2h;
                            sumDen_3h += oTimekeepingKipDetail.Gio2Den3h;
                            sumDen_4h += oTimekeepingKipDetail.Gio3Den4h;
                            sumDen_5h += oTimekeepingKipDetail.Gio4Den5h;
                            sumDen_6h += oTimekeepingKipDetail.Gio5Den6h;
                            sumDen_7h += oTimekeepingKipDetail.Gio6Den7h;
                            sumDen_8h += oTimekeepingKipDetail.Gio7Den8h;
                            //Tính tổng của từng trường 1 

                        }
                        _returnTimekeeping.Code = "00";
                        _returnTimekeeping.Message = "Lấy dữ liệu thành công.";
                        _returnTimekeeping.ListTimekeepingKipReport = listTimekeepingKipDetail;
                    }
                }
            }
            catch(Exception ex)
            {
                _returnTimekeeping.Code = "99";
                _returnTimekeeping.Message = "Lỗi xử lý dữ liệu";
                _returnTimekeeping.Total = 0;
                _returnTimekeeping.ListTimekeepingKipReport = null;
            }
            return _returnTimekeeping;
        }



        #endregion
        //Phần tổng của bảng tổng hợp theo kíp
        #region SUM_TIMEKEEPING_KIP_DETAIL 
        public ReturnTimekeeping SUM_TIMEKEEPING_KIP_DETAIL(string ngay, int donvi, int to)
        {
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnTimekeeping _returnTimekeeping = new ReturnTimekeeping();


            List<SumTimekeepingKipDetail> listSumTimekeepingKipDetail = null;
            SumTimekeepingKipDetail oSumTimekeepingKipDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = Helper.OraDCOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "EMS_CHAMCONG.TongTK";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new OracleParameter("v_Ngay", OracleDbType.Int32)).Value = common.DateToInt(ngay);
                    switch (donvi)
                    {
                        case 1:
                            cmd.Parameters.Add(new OracleParameter("v_DonVi", OracleDbType.NVarchar2)).Value = "TTKTTN-HN";
                            break;
                        case 2:
                            cmd.Parameters.Add(new OracleParameter("v_DonVi", OracleDbType.NVarchar2)).Value = "TTKTTN-DN";
                            break;
                        case 3:
                            cmd.Parameters.Add(new OracleParameter("v_DonVi", OracleDbType.NVarchar2)).Value = "TTKTTN-HCM";
                            break;
                    }

                    cmd.Parameters.Add(new OracleParameter("v_To", OracleDbType.Int32)).Value = to;
                    cmd.Parameters.Add("v_List", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    OracleDataReader dr = Helper.ExecuteDataReader(cmd, Helper.OraDCOracleConnection);

                    if (dr.HasRows)
                    {
                        listSumTimekeepingKipDetail = new List<SumTimekeepingKipDetail>();
                        while (dr.Read())
                        {
                            oSumTimekeepingKipDetail = new SumTimekeepingKipDetail();
                            oSumTimekeepingKipDetail.SumSoNguoi = Convert.ToInt32(dr["SUM(SO_NGUOI)"].ToString());
                            oSumTimekeepingKipDetail.SumDen9h = Convert.ToInt32(dr["SUM(Den_9h)"].ToString());
                            oSumTimekeepingKipDetail.SumDen10h = Convert.ToInt32(dr["SUM(Den_10h)"].ToString());
                            oSumTimekeepingKipDetail.SumDen11h = Convert.ToInt32(dr["SUM(Den_11h)"].ToString());
                            oSumTimekeepingKipDetail.SumDen12h = Convert.ToInt32(dr["SUM(Den_12h)"].ToString());
                            oSumTimekeepingKipDetail.SumDen13h = Convert.ToInt32(dr["SUM(Den_13h)"].ToString());
                            oSumTimekeepingKipDetail.SumDen14h = Convert.ToInt32(dr["SUM(Den_14h)"].ToString());
                            oSumTimekeepingKipDetail.SumDen15h = Convert.ToInt32(dr["SUM(Den_15h)"].ToString());
                            oSumTimekeepingKipDetail.SumDen16h = Convert.ToInt32(dr["SUM(Den_16h)"].ToString());
                            oSumTimekeepingKipDetail.SumDen17h = Convert.ToInt32(dr["SUM(Den_17h)"].ToString());
                            oSumTimekeepingKipDetail.SumDen18h = Convert.ToInt32(dr["SUM(Den_18h)"].ToString());
                            oSumTimekeepingKipDetail.SumDen19h = Convert.ToInt32(dr["SUM(Den_19h)"].ToString());
                            oSumTimekeepingKipDetail.SumDen20h = Convert.ToInt32(dr["SUM(Den_20h)"].ToString());
                            oSumTimekeepingKipDetail.SumDen21h = Convert.ToInt32(dr["SUM(Den_21h)"].ToString());
                            oSumTimekeepingKipDetail.SumDen22h = Convert.ToInt32(dr["SUM(Den_22h)"].ToString());
                            oSumTimekeepingKipDetail.SumDen23h = Convert.ToInt32(dr["SUM(Den_23h)"].ToString());
                            oSumTimekeepingKipDetail.SumDen24h = Convert.ToInt32(dr["SUM(Den_24h)"].ToString());
                            oSumTimekeepingKipDetail.SumDen1h = Convert.ToInt32(dr["SUM(Den_1h)"].ToString());
                            oSumTimekeepingKipDetail.SumDen2h = Convert.ToInt32(dr["SUM(Den_2h)"].ToString());
                            oSumTimekeepingKipDetail.SumDen3h = Convert.ToInt32(dr["SUM(Den_3h)"].ToString());
                            oSumTimekeepingKipDetail.SumDen4h = Convert.ToInt32(dr["SUM(Den_4h)"].ToString());
                            oSumTimekeepingKipDetail.SumDen5h = Convert.ToInt32(dr["SUM(Den_5h)"].ToString());
                            oSumTimekeepingKipDetail.SumDen6h = Convert.ToInt32(dr["SUM(Den_6h)"].ToString());
                            oSumTimekeepingKipDetail.SumDen7h = Convert.ToInt32(dr["SUM(Den_7h)"].ToString());
                            oSumTimekeepingKipDetail.SumDen8h = Convert.ToInt32(dr["SUM(Den_8h)"].ToString());
                            listSumTimekeepingKipDetail.Add(oSumTimekeepingKipDetail);
                        }
                        _returnTimekeeping.Code = "00";
                        _returnTimekeeping.Message = "Lấy dữ liệu thành công.";
                        _returnTimekeeping.ListSumTimekeepingKipReport = listSumTimekeepingKipDetail;
                    }
                }
            }
            catch (Exception ex)
            {
                _returnTimekeeping.Code = "99";
                _returnTimekeeping.Message = "Lỗi xử lý dữ liệu";
                _returnTimekeeping.Total = 0;
                _returnTimekeeping.ListSumTimekeepingKipReport = null;
            }
            return _returnTimekeeping;
        }

        #endregion
        //Phần tổng sản lượng, khối lượng của bảng tổng hợp theo kíp 
        #region SUM_SLKL_TIMEKEEPING_KIP_DETAIL
        public ReturnTimekeeping SUM_SLKL_TIMEKEEPING_KIP_DETAIL(string ngay, int donvi)
        {
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnTimekeeping _returnTimekeeping = new ReturnTimekeeping();


            List<SumSLKLTimekeepingKipDetail> listSumSLKLTimekeepingKipDetail = null;
            SumSLKLTimekeepingKipDetail oSumSLKLTimekeepingKipDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = Helper.OraDCOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "EMS_CHAMCONG.TongSLKLTK";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new OracleParameter("v_Ngay", OracleDbType.Int32)).Value = common.DateToInt(ngay);
                    //cmd.Parameters.Add(new OracleParameter("v_Mabc", OracleDbType.Int32)).Value = mabc;
                    switch (donvi)
                    {
                        case 1:
                            cmd.Parameters.Add(new OracleParameter("v_DonVi", OracleDbType.NVarchar2)).Value = "TTKTTN-HN";
                            break;
                        case 2:
                            cmd.Parameters.Add(new OracleParameter("v_DonVi", OracleDbType.NVarchar2)).Value = "TTKTTN-DN";
                            break;
                        case 3:
                            cmd.Parameters.Add(new OracleParameter("v_DonVi", OracleDbType.NVarchar2)).Value = "TTKTTN-HCM";
                            break;
                    }    
                    cmd.Parameters.Add("v_List", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    OracleDataReader dr = Helper.ExecuteDataReader(cmd, Helper.OraDCOracleConnection);

                    if (dr.HasRows)
                    {
                        listSumSLKLTimekeepingKipDetail = new List<SumSLKLTimekeepingKipDetail>();
                        while (dr.Read())
                        {
                            oSumSLKLTimekeepingKipDetail = new SumSLKLTimekeepingKipDetail();
                            oSumSLKLTimekeepingKipDetail.SLDen9h = Convert.ToDecimal(dr["SL_DEN_9H"].ToString());
                            oSumSLKLTimekeepingKipDetail.KLDen9h = Convert.ToDecimal(dr["KL_DEN_9H"].ToString());
                            oSumSLKLTimekeepingKipDetail.SLDen10h = Convert.ToDecimal(dr["SL_DEN_10H"].ToString());
                            oSumSLKLTimekeepingKipDetail.KLDen10h = Convert.ToDecimal(dr["KL_DEN_10H"].ToString());
                            oSumSLKLTimekeepingKipDetail.SLDen11h = Convert.ToDecimal(dr["SL_DEN_11H"].ToString());
                            oSumSLKLTimekeepingKipDetail.KLDen11h = Convert.ToDecimal(dr["KL_DEN_11H"].ToString());
                            oSumSLKLTimekeepingKipDetail.SLDen12h = Convert.ToDecimal(dr["SL_DEN_12H"].ToString());
                            oSumSLKLTimekeepingKipDetail.KLDen12h = Convert.ToDecimal(dr["KL_DEN_12H"].ToString());
                            oSumSLKLTimekeepingKipDetail.SLDen13h = Convert.ToDecimal(dr["SL_DEN_13H"].ToString());
                            oSumSLKLTimekeepingKipDetail.KLDen13h = Convert.ToDecimal(dr["KL_DEN_13H"].ToString());
                            oSumSLKLTimekeepingKipDetail.SLDen14h = Convert.ToDecimal(dr["SL_DEN_14H"].ToString());
                            oSumSLKLTimekeepingKipDetail.KLDen14h = Convert.ToDecimal(dr["KL_DEN_14H"].ToString());
                            oSumSLKLTimekeepingKipDetail.SLDen15h = Convert.ToDecimal(dr["SL_DEN_15H"].ToString());
                            oSumSLKLTimekeepingKipDetail.KLDen15h = Convert.ToDecimal(dr["KL_DEN_15H"].ToString());
                            oSumSLKLTimekeepingKipDetail.SLDen16h = Convert.ToDecimal(dr["SL_DEN_16H"].ToString());
                            oSumSLKLTimekeepingKipDetail.KLDen16h = Convert.ToDecimal(dr["KL_DEN_16H"].ToString());
                            oSumSLKLTimekeepingKipDetail.SLDen17h = Convert.ToDecimal(dr["SL_DEN_17H"].ToString());
                            oSumSLKLTimekeepingKipDetail.KLDen17h = Convert.ToDecimal(dr["KL_DEN_17H"].ToString());
                            oSumSLKLTimekeepingKipDetail.SLDen18h = Convert.ToDecimal(dr["SL_DEN_18H"].ToString());
                            oSumSLKLTimekeepingKipDetail.KLDen18h = Convert.ToDecimal(dr["KL_DEN_18H"].ToString());
                            oSumSLKLTimekeepingKipDetail.SLDen19h = Convert.ToDecimal(dr["SL_DEN_19H"].ToString());
                            oSumSLKLTimekeepingKipDetail.KLDen19h = Convert.ToDecimal(dr["KL_DEN_19H"].ToString());
                            oSumSLKLTimekeepingKipDetail.SLDen20h = Convert.ToDecimal(dr["SL_DEN_20H"].ToString());
                            oSumSLKLTimekeepingKipDetail.KLDen20h = Convert.ToDecimal(dr["KL_DEN_20H"].ToString());
                            oSumSLKLTimekeepingKipDetail.SLDen21h = Convert.ToDecimal(dr["SL_DEN_21H"].ToString());
                            oSumSLKLTimekeepingKipDetail.KLDen21h = Convert.ToDecimal(dr["KL_DEN_21H"].ToString());
                            oSumSLKLTimekeepingKipDetail.SLDen22h = Convert.ToDecimal(dr["SL_DEN_22H"].ToString());
                            oSumSLKLTimekeepingKipDetail.KLDen22h = Convert.ToDecimal(dr["KL_DEN_22H"].ToString());
                            oSumSLKLTimekeepingKipDetail.SLDen23h = Convert.ToDecimal(dr["SL_DEN_23H"].ToString());
                            oSumSLKLTimekeepingKipDetail.KLDen23h = Convert.ToDecimal(dr["KL_DEN_23H"].ToString());
                            oSumSLKLTimekeepingKipDetail.SLDen24h = Convert.ToDecimal(dr["SL_DEN_24H"].ToString());
                            oSumSLKLTimekeepingKipDetail.KLDen24h = Convert.ToDecimal(dr["KL_DEN_24H"].ToString());
                            oSumSLKLTimekeepingKipDetail.SLDen1h = Convert.ToDecimal(dr["SL_DEN_1H"].ToString());
                            oSumSLKLTimekeepingKipDetail.KLDen1h = Convert.ToDecimal(dr["KL_DEN_1H"].ToString());
                            oSumSLKLTimekeepingKipDetail.SLDen2h = Convert.ToDecimal(dr["SL_DEN_2H"].ToString());
                            oSumSLKLTimekeepingKipDetail.KLDen2h = Convert.ToDecimal(dr["KL_DEN_2H"].ToString());
                            oSumSLKLTimekeepingKipDetail.SLDen3h = Convert.ToDecimal(dr["SL_DEN_3H"].ToString());
                            oSumSLKLTimekeepingKipDetail.KLDen3h = Convert.ToDecimal(dr["KL_DEN_3H"].ToString());
                            oSumSLKLTimekeepingKipDetail.SLDen4h = Convert.ToDecimal(dr["SL_DEN_4H"].ToString());
                            oSumSLKLTimekeepingKipDetail.KLDen4h = Convert.ToDecimal(dr["KL_DEN_4H"].ToString());
                            oSumSLKLTimekeepingKipDetail.SLDen5h = Convert.ToDecimal(dr["SL_DEN_5H"].ToString());
                            oSumSLKLTimekeepingKipDetail.KLDen5h = Convert.ToDecimal(dr["KL_DEN_5H"].ToString());
                            oSumSLKLTimekeepingKipDetail.SLDen6h = Convert.ToDecimal(dr["SL_DEN_6H"].ToString());
                            oSumSLKLTimekeepingKipDetail.KLDen6h = Convert.ToDecimal(dr["KL_DEN_6H"].ToString());
                            oSumSLKLTimekeepingKipDetail.SLDen7h = Convert.ToDecimal(dr["SL_DEN_7H"].ToString());
                            oSumSLKLTimekeepingKipDetail.KLDen7h = Convert.ToDecimal(dr["KL_DEN_7H"].ToString());
                            oSumSLKLTimekeepingKipDetail.SLDen8h = Convert.ToDecimal(dr["SL_DEN_8H"].ToString());
                            oSumSLKLTimekeepingKipDetail.KLDen8h = Convert.ToDecimal(dr["KL_DEN_8H"].ToString());
                            listSumSLKLTimekeepingKipDetail.Add(oSumSLKLTimekeepingKipDetail);
                        }
                        _returnTimekeeping.Code = "00";
                        _returnTimekeeping.Message = "Lấy dữ liệu thành công.";
                        _returnTimekeeping.ListSumSLKLTimekeepingKipReport = listSumSLKLTimekeepingKipDetail;
                    }
                }
            }
            catch (Exception ex)
            {
                _returnTimekeeping.Code = "99";
                _returnTimekeeping.Message = "Lỗi xử lý dữ liệu";
                _returnTimekeeping.Total = 0;
                _returnTimekeeping.ListSumSLKLTimekeepingKipReport = null;
            }
            return _returnTimekeeping;
        }

        #endregion
        //Phần chi tiết của bảng tổng hợp theo chức danh
        #region TIMEKEEPING_TITLE_DETAIL

        public ReturnTimekeeping TIMEKEEPING_TITLE_DETAIL(string ngay, int donvi, int kip)
        {
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnTimekeeping _returnTimekeeping = new ReturnTimekeeping();


            List<TimekeepingTitleDetail> listTimekeepingTitleDetail = null;
            TimekeepingTitleDetail oTimekeepingTitleDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = Helper.OraDCOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "EMS_TIMEKEEPING.TongHopTheoChucDanhTK";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new OracleParameter("v_Ngay", OracleDbType.Int32)).Value = common.DateToInt(ngay);
                    switch (donvi)
                    {
                        case 1:
                            cmd.Parameters.Add(new OracleParameter("v_DonVi", OracleDbType.NVarchar2)).Value = "TTKTTN-HN";
                            break;
                        case 2:
                            cmd.Parameters.Add(new OracleParameter("v_DonVi", OracleDbType.NVarchar2)).Value = "TTKTTN-DN";
                            break;
                        case 3:
                            cmd.Parameters.Add(new OracleParameter("v_DonVi", OracleDbType.NVarchar2)).Value = "TTKTTN-HCM";
                            break;
                    }

                    cmd.Parameters.Add(new OracleParameter("v_Kip", OracleDbType.Int32)).Value = kip;
                    cmd.Parameters.Add("v_List", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    OracleDataReader dr = Helper.ExecuteDataReader(cmd, Helper.OraDCOracleConnection);

                    if (dr.HasRows)
                    {
                        listTimekeepingTitleDetail = new List<TimekeepingTitleDetail>();
                        while (dr.Read())
                        {
                            oTimekeepingTitleDetail = new TimekeepingTitleDetail();
                            oTimekeepingTitleDetail.TenChucDanh = dr["TEN_CHUCDANH"].ToString();
                            oTimekeepingTitleDetail.Loai = dr["LOAI"].ToString();
                            oTimekeepingTitleDetail.SoNguoi = Convert.ToInt32(dr["SO_NGUOI"].ToString());
                            oTimekeepingTitleDetail.Gio8Den9h = Convert.ToInt32(dr["Den_9h"].ToString());
                            oTimekeepingTitleDetail.Gio9Den10h = Convert.ToInt32(dr["Den_10h"].ToString());
                            oTimekeepingTitleDetail.Gio10Den11h = Convert.ToInt32(dr["Den_11h"].ToString());
                            oTimekeepingTitleDetail.Gio11Den12h = Convert.ToInt32(dr["Den_12h"].ToString());
                            oTimekeepingTitleDetail.Gio12Den13h = Convert.ToInt32(dr["Den_13h"].ToString());
                            oTimekeepingTitleDetail.Gio13Den14h = Convert.ToInt32(dr["Den_14h"].ToString());
                            oTimekeepingTitleDetail.Gio14Den15h = Convert.ToInt32(dr["Den_15h"].ToString());
                            oTimekeepingTitleDetail.Gio15Den16h = Convert.ToInt32(dr["Den_16h"].ToString());
                            oTimekeepingTitleDetail.Gio16Den17h = Convert.ToInt32(dr["Den_17h"].ToString());
                            oTimekeepingTitleDetail.Gio17Den18h = Convert.ToInt32(dr["Den_18h"].ToString());
                            oTimekeepingTitleDetail.Gio18Den19h = Convert.ToInt32(dr["Den_19h"].ToString());
                            oTimekeepingTitleDetail.Gio19Den20h = Convert.ToInt32(dr["Den_20h"].ToString());
                            oTimekeepingTitleDetail.Gio20Den21h = Convert.ToInt32(dr["Den_21h"].ToString());
                            oTimekeepingTitleDetail.Gio21Den22h = Convert.ToInt32(dr["Den_22h"].ToString());
                            oTimekeepingTitleDetail.Gio22Den23h = Convert.ToInt32(dr["Den_23h"].ToString());
                            oTimekeepingTitleDetail.Gio23Den24h = Convert.ToInt32(dr["Den_24h"].ToString());
                            oTimekeepingTitleDetail.Gio24Den1h = Convert.ToInt32(dr["Den_1h"].ToString());
                            oTimekeepingTitleDetail.Gio1Den2h = Convert.ToInt32(dr["Den_2h"].ToString());
                            oTimekeepingTitleDetail.Gio2Den3h = Convert.ToInt32(dr["Den_3h"].ToString());
                            oTimekeepingTitleDetail.Gio3Den4h = Convert.ToInt32(dr["Den_4h"].ToString());
                            oTimekeepingTitleDetail.Gio4Den5h = Convert.ToInt32(dr["Den_5h"].ToString());
                            oTimekeepingTitleDetail.Gio5Den6h = Convert.ToInt32(dr["Den_6h"].ToString());
                            oTimekeepingTitleDetail.Gio6Den7h = Convert.ToInt32(dr["Den_7h"].ToString());
                            oTimekeepingTitleDetail.Gio7Den8h = Convert.ToInt32(dr["Den_8h"].ToString());
                            listTimekeepingTitleDetail.Add(oTimekeepingTitleDetail);
                        }
                        _returnTimekeeping.Code = "00";
                        _returnTimekeeping.Message = "Lấy dữ liệu thành công.";
                        _returnTimekeeping.ListTimekeepingTitleReport = listTimekeepingTitleDetail;
                    }
                }
            }
            catch (Exception ex)
            {
                _returnTimekeeping.Code = "99";
                _returnTimekeeping.Message = "Lỗi xử lý dữ liệu";
                _returnTimekeeping.Total = 0;
                _returnTimekeeping.ListTimekeepingTitleReport = null;
            }
            return _returnTimekeeping;
        }

        #endregion
        //Phần tổng của bảng tổng hợp theo chức danh
        #region SUM_TIMEKEEPING_TITLE_DETAIL
        public ReturnTimekeeping SUM_TIMEKEEPING_TITLE_DETAIL(string ngay, int donvi, int to)
        {
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnTimekeeping _returnTimekeeping = new ReturnTimekeeping();


            List<SumTimekeepingTitleDetail> listSumTimekeepingTitleDetail = null;
            SumTimekeepingTitleDetail oSumTimekeepingTitleDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = Helper.OraDCOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "EMS_CHAMCONG.TongTK";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new OracleParameter("v_Ngay", OracleDbType.Int32)).Value = common.DateToInt(ngay);
                    switch (donvi)
                    {
                        case 1:
                            cmd.Parameters.Add(new OracleParameter("v_DonVi", OracleDbType.NVarchar2)).Value = "TTKTTN-HN";
                            break;
                        case 2:
                            cmd.Parameters.Add(new OracleParameter("v_DonVi", OracleDbType.NVarchar2)).Value = "TTKTTN-DN";
                            break;
                        case 3:
                            cmd.Parameters.Add(new OracleParameter("v_DonVi", OracleDbType.NVarchar2)).Value = "TTKTTN-HCM";
                            break;
                    }

                    cmd.Parameters.Add(new OracleParameter("v_To", OracleDbType.Int32)).Value = to;
                    cmd.Parameters.Add("v_List", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    OracleDataReader dr = Helper.ExecuteDataReader(cmd, Helper.OraDCOracleConnection);

                    if (dr.HasRows)
                    {
                        listSumTimekeepingTitleDetail = new List<SumTimekeepingTitleDetail>();
                        while (dr.Read())
                        {
                            oSumTimekeepingTitleDetail = new SumTimekeepingTitleDetail();
                            oSumTimekeepingTitleDetail.SumSoNguoi = Convert.ToInt32(dr["SUM(SO_NGUOI)"].ToString());
                            oSumTimekeepingTitleDetail.SumDen9h = Convert.ToInt32(dr["SUM(Den_9h)"].ToString());
                            oSumTimekeepingTitleDetail.SumDen10h = Convert.ToInt32(dr["SUM(Den_10h)"].ToString());
                            oSumTimekeepingTitleDetail.SumDen11h = Convert.ToInt32(dr["SUM(Den_11h)"].ToString());
                            oSumTimekeepingTitleDetail.SumDen12h = Convert.ToInt32(dr["SUM(Den_12h)"].ToString());
                            oSumTimekeepingTitleDetail.SumDen13h = Convert.ToInt32(dr["SUM(Den_13h)"].ToString());
                            oSumTimekeepingTitleDetail.SumDen14h = Convert.ToInt32(dr["SUM(Den_14h)"].ToString());
                            oSumTimekeepingTitleDetail.SumDen15h = Convert.ToInt32(dr["SUM(Den_15h)"].ToString());
                            oSumTimekeepingTitleDetail.SumDen16h = Convert.ToInt32(dr["SUM(Den_16h)"].ToString());
                            oSumTimekeepingTitleDetail.SumDen17h = Convert.ToInt32(dr["SUM(Den_17h)"].ToString());
                            oSumTimekeepingTitleDetail.SumDen18h = Convert.ToInt32(dr["SUM(Den_18h)"].ToString());
                            oSumTimekeepingTitleDetail.SumDen19h = Convert.ToInt32(dr["SUM(Den_19h)"].ToString());
                            oSumTimekeepingTitleDetail.SumDen20h = Convert.ToInt32(dr["SUM(Den_20h)"].ToString());
                            oSumTimekeepingTitleDetail.SumDen21h = Convert.ToInt32(dr["SUM(Den_21h)"].ToString());
                            oSumTimekeepingTitleDetail.SumDen22h = Convert.ToInt32(dr["SUM(Den_22h)"].ToString());
                            oSumTimekeepingTitleDetail.SumDen23h = Convert.ToInt32(dr["SUM(Den_23h)"].ToString());
                            oSumTimekeepingTitleDetail.SumDen24h = Convert.ToInt32(dr["SUM(Den_24h)"].ToString());
                            oSumTimekeepingTitleDetail.SumDen1h = Convert.ToInt32(dr["SUM(Den_1h)"].ToString());
                            oSumTimekeepingTitleDetail.SumDen2h = Convert.ToInt32(dr["SUM(Den_2h)"].ToString());
                            oSumTimekeepingTitleDetail.SumDen3h = Convert.ToInt32(dr["SUM(Den_3h)"].ToString());
                            oSumTimekeepingTitleDetail.SumDen4h = Convert.ToInt32(dr["SUM(Den_4h)"].ToString());
                            oSumTimekeepingTitleDetail.SumDen5h = Convert.ToInt32(dr["SUM(Den_5h)"].ToString());
                            oSumTimekeepingTitleDetail.SumDen6h = Convert.ToInt32(dr["SUM(Den_6h)"].ToString());
                            oSumTimekeepingTitleDetail.SumDen7h = Convert.ToInt32(dr["SUM(Den_7h)"].ToString());
                            oSumTimekeepingTitleDetail.SumDen8h = Convert.ToInt32(dr["SUM(Den_8h)"].ToString());
                            listSumTimekeepingTitleDetail.Add(oSumTimekeepingTitleDetail);
                        }
                        _returnTimekeeping.Code = "00";
                        _returnTimekeeping.Message = "Lấy dữ liệu thành công.";
                        _returnTimekeeping.ListSumTimekeepingTitleReport = listSumTimekeepingTitleDetail;
                    }
                }
            }
            catch (Exception ex)
            {
                _returnTimekeeping.Code = "99";
                _returnTimekeeping.Message = "Lỗi xử lý dữ liệu";
                _returnTimekeeping.Total = 0;
                _returnTimekeeping.ListSumTimekeepingTitleReport = null;
            }
            return _returnTimekeeping;
        }

        #endregion
        //Phần tổng sản lượng, khối lượng của bảng tổng hợp theo chức danh
        #region SUM_SLKL_TIMEKEEPING_TITLE_DETAIL
        public ReturnTimekeeping SUM_SLKL_TIMEKEEPING_TITLE_DETAIL(string ngay, int donvi)
        {
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnTimekeeping _returnTimekeeping = new ReturnTimekeeping();


            List<SumSLKLTimekeepingTitleDetail> listSumSLKLTimekeepingTitleDetail = null;
            SumSLKLTimekeepingTitleDetail oSumSLKLTimekeepingTitleDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = Helper.OraDCOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "EMS_CHAMCONG.TongSLKLTK";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new OracleParameter("v_Ngay", OracleDbType.Int32)).Value = common.DateToInt(ngay);
                    //cmd.Parameters.Add(new OracleParameter("v_Mabc", OracleDbType.Int32)).Value = mabc;
                    switch (donvi)
                    {
                        case 1:
                            cmd.Parameters.Add(new OracleParameter("v_DonVi", OracleDbType.NVarchar2)).Value = "TTKTTN-HN";
                            break;
                        case 2:
                            cmd.Parameters.Add(new OracleParameter("v_DonVi", OracleDbType.NVarchar2)).Value = "TTKTTN-DN";
                            break;
                        case 3:
                            cmd.Parameters.Add(new OracleParameter("v_DonVi", OracleDbType.NVarchar2)).Value = "TTKTTN-HCM";
                            break;
                    }
                    cmd.Parameters.Add("v_List", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    OracleDataReader dr = Helper.ExecuteDataReader(cmd, Helper.OraDCOracleConnection);

                    if (dr.HasRows)
                    {
                        listSumSLKLTimekeepingTitleDetail = new List<SumSLKLTimekeepingTitleDetail>();
                        while (dr.Read())
                        {
                            oSumSLKLTimekeepingTitleDetail = new SumSLKLTimekeepingTitleDetail();
                            oSumSLKLTimekeepingTitleDetail.SLDen9h = Convert.ToDecimal(dr["SL_DEN_9H"].ToString());
                            oSumSLKLTimekeepingTitleDetail.KLDen9h = Convert.ToDecimal(dr["KL_DEN_9H"].ToString());
                            oSumSLKLTimekeepingTitleDetail.SLDen10h = Convert.ToDecimal(dr["SL_DEN_10H"].ToString());
                            oSumSLKLTimekeepingTitleDetail.KLDen10h = Convert.ToDecimal(dr["KL_DEN_10H"].ToString());
                            oSumSLKLTimekeepingTitleDetail.SLDen11h = Convert.ToDecimal(dr["SL_DEN_11H"].ToString());
                            oSumSLKLTimekeepingTitleDetail.KLDen11h = Convert.ToDecimal(dr["KL_DEN_11H"].ToString());
                            oSumSLKLTimekeepingTitleDetail.SLDen12h = Convert.ToDecimal(dr["SL_DEN_12H"].ToString());
                            oSumSLKLTimekeepingTitleDetail.KLDen12h = Convert.ToDecimal(dr["KL_DEN_12H"].ToString());
                            oSumSLKLTimekeepingTitleDetail.SLDen13h = Convert.ToDecimal(dr["SL_DEN_13H"].ToString());
                            oSumSLKLTimekeepingTitleDetail.KLDen13h = Convert.ToDecimal(dr["KL_DEN_13H"].ToString());
                            oSumSLKLTimekeepingTitleDetail.SLDen14h = Convert.ToDecimal(dr["SL_DEN_14H"].ToString());
                            oSumSLKLTimekeepingTitleDetail.KLDen14h = Convert.ToDecimal(dr["KL_DEN_14H"].ToString());
                            oSumSLKLTimekeepingTitleDetail.SLDen15h = Convert.ToDecimal(dr["SL_DEN_15H"].ToString());
                            oSumSLKLTimekeepingTitleDetail.KLDen15h = Convert.ToDecimal(dr["KL_DEN_15H"].ToString());
                            oSumSLKLTimekeepingTitleDetail.SLDen16h = Convert.ToDecimal(dr["SL_DEN_16H"].ToString());
                            oSumSLKLTimekeepingTitleDetail.KLDen16h = Convert.ToDecimal(dr["KL_DEN_16H"].ToString());
                            oSumSLKLTimekeepingTitleDetail.SLDen17h = Convert.ToDecimal(dr["SL_DEN_17H"].ToString());
                            oSumSLKLTimekeepingTitleDetail.KLDen17h = Convert.ToDecimal(dr["KL_DEN_17H"].ToString());
                            oSumSLKLTimekeepingTitleDetail.SLDen18h = Convert.ToDecimal(dr["SL_DEN_18H"].ToString());
                            oSumSLKLTimekeepingTitleDetail.KLDen18h = Convert.ToDecimal(dr["KL_DEN_18H"].ToString());
                            oSumSLKLTimekeepingTitleDetail.SLDen19h = Convert.ToDecimal(dr["SL_DEN_19H"].ToString());
                            oSumSLKLTimekeepingTitleDetail.KLDen19h = Convert.ToDecimal(dr["KL_DEN_19H"].ToString());
                            oSumSLKLTimekeepingTitleDetail.SLDen20h = Convert.ToDecimal(dr["SL_DEN_20H"].ToString());
                            oSumSLKLTimekeepingTitleDetail.KLDen20h = Convert.ToDecimal(dr["KL_DEN_20H"].ToString());
                            oSumSLKLTimekeepingTitleDetail.SLDen21h = Convert.ToDecimal(dr["SL_DEN_21H"].ToString());
                            oSumSLKLTimekeepingTitleDetail.KLDen21h = Convert.ToDecimal(dr["KL_DEN_21H"].ToString());
                            oSumSLKLTimekeepingTitleDetail.SLDen22h = Convert.ToDecimal(dr["SL_DEN_22H"].ToString());
                            oSumSLKLTimekeepingTitleDetail.KLDen22h = Convert.ToDecimal(dr["KL_DEN_22H"].ToString());
                            oSumSLKLTimekeepingTitleDetail.SLDen23h = Convert.ToDecimal(dr["SL_DEN_23H"].ToString());
                            oSumSLKLTimekeepingTitleDetail.KLDen23h = Convert.ToDecimal(dr["KL_DEN_23H"].ToString());
                            oSumSLKLTimekeepingTitleDetail.SLDen24h = Convert.ToDecimal(dr["SL_DEN_24H"].ToString());
                            oSumSLKLTimekeepingTitleDetail.KLDen24h = Convert.ToDecimal(dr["KL_DEN_24H"].ToString());
                            oSumSLKLTimekeepingTitleDetail.SLDen1h = Convert.ToDecimal(dr["SL_DEN_1H"].ToString());
                            oSumSLKLTimekeepingTitleDetail.KLDen1h = Convert.ToDecimal(dr["KL_DEN_1H"].ToString());
                            oSumSLKLTimekeepingTitleDetail.SLDen2h = Convert.ToDecimal(dr["SL_DEN_2H"].ToString());
                            oSumSLKLTimekeepingTitleDetail.KLDen2h = Convert.ToDecimal(dr["KL_DEN_2H"].ToString());
                            oSumSLKLTimekeepingTitleDetail.SLDen3h = Convert.ToDecimal(dr["SL_DEN_3H"].ToString());
                            oSumSLKLTimekeepingTitleDetail.KLDen3h = Convert.ToDecimal(dr["KL_DEN_3H"].ToString());
                            oSumSLKLTimekeepingTitleDetail.SLDen4h = Convert.ToDecimal(dr["SL_DEN_4H"].ToString());
                            oSumSLKLTimekeepingTitleDetail.KLDen4h = Convert.ToDecimal(dr["KL_DEN_4H"].ToString());
                            oSumSLKLTimekeepingTitleDetail.SLDen5h = Convert.ToDecimal(dr["SL_DEN_5H"].ToString());
                            oSumSLKLTimekeepingTitleDetail.KLDen5h = Convert.ToDecimal(dr["KL_DEN_5H"].ToString());
                            oSumSLKLTimekeepingTitleDetail.SLDen6h = Convert.ToDecimal(dr["SL_DEN_6H"].ToString());
                            oSumSLKLTimekeepingTitleDetail.KLDen6h = Convert.ToDecimal(dr["KL_DEN_6H"].ToString());
                            oSumSLKLTimekeepingTitleDetail.SLDen7h = Convert.ToDecimal(dr["SL_DEN_7H"].ToString());
                            oSumSLKLTimekeepingTitleDetail.KLDen7h = Convert.ToDecimal(dr["KL_DEN_7H"].ToString());
                            oSumSLKLTimekeepingTitleDetail.SLDen8h = Convert.ToDecimal(dr["SL_DEN_8H"].ToString());
                            oSumSLKLTimekeepingTitleDetail.KLDen8h = Convert.ToDecimal(dr["KL_DEN_8H"].ToString());
                            listSumSLKLTimekeepingTitleDetail.Add(oSumSLKLTimekeepingTitleDetail);
                        }
                        _returnTimekeeping.Code = "00";
                        _returnTimekeeping.Message = "Lấy dữ liệu thành công.";
                        _returnTimekeeping.ListSumSLKLTimekeepingTitleReport = listSumSLKLTimekeepingTitleDetail;
                    }
                }
            }
            catch (Exception ex)
            {
                _returnTimekeeping.Code = "99";
                _returnTimekeeping.Message = "Lỗi xử lý dữ liệu";
                _returnTimekeeping.Total = 0;
                _returnTimekeeping.ListSumSLKLTimekeepingTitleReport = null;
            }
            return _returnTimekeeping;
        }

        #endregion
        //Phần chi tiết của bảng tổng hợp theo chi tiết
        #region TIMEKEEPING_DETAIL

        public ReturnTimekeeping TIMEKEEPING_DETAIL(string ngay, int donvi, int to, int kip)
        {
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnTimekeeping _returnTimekeeping = new ReturnTimekeeping();


            List<TimekeepingDetail> listTimekeepingDetail = null;
            TimekeepingDetail oTimekeepingDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = Helper.OraDCOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "EMS_TIMEKEEPING.ChiTietPhanCongTK";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new OracleParameter("v_Ngay", OracleDbType.Int32)).Value = common.DateToInt(ngay);
                    switch (donvi)
                    {
                        case 1:
                            cmd.Parameters.Add(new OracleParameter("v_DonVi", OracleDbType.NVarchar2)).Value = "TTKTTN-HN";
                            break;
                        case 2:
                            cmd.Parameters.Add(new OracleParameter("v_DonVi", OracleDbType.NVarchar2)).Value = "TTKTTN-DN";
                            break;
                        case 3:
                            cmd.Parameters.Add(new OracleParameter("v_DonVi", OracleDbType.NVarchar2)).Value = "TTKTTN-HCM";
                            break;
                    }

                    cmd.Parameters.Add(new OracleParameter("v_To", OracleDbType.Int32)).Value = to;
                    cmd.Parameters.Add(new OracleParameter("v_Kip", OracleDbType.Int32)).Value = kip;
                    cmd.Parameters.Add("v_List", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    OracleDataReader dr = Helper.ExecuteDataReader(cmd, Helper.OraDCOracleConnection);

                    if (dr.HasRows)
                    {
                        listTimekeepingDetail = new List<TimekeepingDetail>();
                        while (dr.Read())
                        {
                            oTimekeepingDetail = new TimekeepingDetail();
                            oTimekeepingDetail.Ma = dr["MA"].ToString();
                            oTimekeepingDetail.Ten = dr["TEN"].ToString();
                            oTimekeepingDetail.ChucDanh = dr["CHUC_DANH"].ToString();
                            oTimekeepingDetail.Loai = dr["LOAI"].ToString();
                            oTimekeepingDetail.Ca = dr["CA"].ToString();
                            oTimekeepingDetail.ThoiGian = dr["TG"].ToString();
                            oTimekeepingDetail.TongGioLam = Convert.ToInt32(dr["TONG_GIO"].ToString());
                            oTimekeepingDetail.Kip1 = dr["KIP_1"].ToString();
                            oTimekeepingDetail.Kip2 = dr["KIP_2"].ToString();
                            oTimekeepingDetail.Kip3 = dr["KIP_3"].ToString();
                            oTimekeepingDetail.Kip4 =  dr["KIP_4"].ToString();
                            oTimekeepingDetail.Kip5 =  dr["KIP_5"].ToString();
                            oTimekeepingDetail.Kip6 =  dr["KIP_6"].ToString();
                            oTimekeepingDetail.Kip7 =  dr["KIP_7"].ToString();
                            oTimekeepingDetail.Kip8 =  dr["KIP_8"].ToString();
                            oTimekeepingDetail.Kip9 =  dr["KIP_9"].ToString();
                            oTimekeepingDetail.Kip10 =  dr["KIP_10"].ToString();
                            oTimekeepingDetail.Kip11 =  dr["KIP_11"].ToString();
                            oTimekeepingDetail.Kip12 =  dr["KIP_12"].ToString();
                            oTimekeepingDetail.Kip13 =  dr["KIP_13"].ToString();
                            oTimekeepingDetail.Kip14 =  dr["KIP_14"].ToString();
                            oTimekeepingDetail.Kip15 =  dr["KIP_15"].ToString();
                            oTimekeepingDetail.Kip16 =  dr["KIP_16"].ToString();
                            oTimekeepingDetail.Kip17 =  dr["KIP_17"].ToString();
                            oTimekeepingDetail.Kip18 =  dr["KIP_18"].ToString();
                            oTimekeepingDetail.Kip19 =  dr["KIP_19"].ToString();
                            oTimekeepingDetail.Kip20 =  dr["KIP_20"].ToString();
                            oTimekeepingDetail.Gio8Den9h =  dr["Den_9h"].ToString();
                            oTimekeepingDetail.Gio9Den10h =  dr["Den_10h"].ToString();
                            oTimekeepingDetail.Gio10Den11h =  dr["Den_11h"].ToString();
                            oTimekeepingDetail.Gio11Den12h =  dr["Den_12h"].ToString();
                            oTimekeepingDetail.Gio12Den13h =  dr["Den_13h"].ToString();
                            oTimekeepingDetail.Gio13Den14h =  dr["Den_14h"].ToString();
                            oTimekeepingDetail.Gio14Den15h =  dr["Den_15h"].ToString();
                            oTimekeepingDetail.Gio15Den16h =  dr["Den_16h"].ToString();
                            oTimekeepingDetail.Gio16Den17h =  dr["Den_17h"].ToString();
                            oTimekeepingDetail.Gio17Den18h =  dr["Den_18h"].ToString();
                            oTimekeepingDetail.Gio18Den19h =  dr["Den_19h"].ToString();
                            oTimekeepingDetail.Gio19Den20h =  dr["Den_20h"].ToString();
                            oTimekeepingDetail.Gio20Den21h =  dr["Den_21h"].ToString();
                            oTimekeepingDetail.Gio21Den22h =  dr["Den_22h"].ToString();
                            oTimekeepingDetail.Gio22Den23h =  dr["Den_23h"].ToString();
                            oTimekeepingDetail.Gio23Den24h =  dr["Den_24h"].ToString();
                            oTimekeepingDetail.Gio24Den1h =  dr["Den_1h"].ToString();
                            oTimekeepingDetail.Gio1Den2h =  dr["Den_2h"].ToString();
                            oTimekeepingDetail.Gio2Den3h =  dr["Den_3h"].ToString();
                            oTimekeepingDetail.Gio3Den4h =  dr["Den_4h"].ToString();
                            oTimekeepingDetail.Gio4Den5h =  dr["Den_5h"].ToString();
                            oTimekeepingDetail.Gio5Den6h =  dr["Den_6h"].ToString();
                            oTimekeepingDetail.Gio6Den7h =  dr["Den_7h"].ToString();
                            oTimekeepingDetail.Gio7Den8h =  dr["Den_8h"].ToString();
                            listTimekeepingDetail.Add(oTimekeepingDetail);
                            

                        }
                        _returnTimekeeping.Code = "00";
                        _returnTimekeeping.Message = "Lấy dữ liệu thành công.";
                        _returnTimekeeping.ListTimekeepingReport = listTimekeepingDetail;
                    }
                }
            }
            catch (Exception ex)
            {
                _returnTimekeeping.Code = "99";
                _returnTimekeeping.Message = "Lỗi xử lý dữ liệu";
                _returnTimekeeping.Total = 0;
                _returnTimekeeping.ListTimekeepingReport = null;
            }
            return _returnTimekeeping;
        }

        #endregion
        //Phần tổng của bảng tổng hợp theo chi tiết
        #region SUM_TIMEKEEPING_DETAIL
        public ReturnTimekeeping SUM_TIMEKEEPING_DETAIL(string ngay, int donvi)
        {
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnTimekeeping _returnTimekeeping = new ReturnTimekeeping();


            List<SumTimekeepingDetail> listSumTimekeepingDetail = null;
            SumTimekeepingDetail oSumTimekeepingDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = Helper.OraDCOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "EMS_CHAMCONG.TongChiTietTK";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new OracleParameter("v_Ngay", OracleDbType.Int32)).Value = common.DateToInt(ngay);
                    switch (donvi)
                    {
                        case 1:
                            cmd.Parameters.Add(new OracleParameter("v_DonVi", OracleDbType.NVarchar2)).Value = "TTKTTN-HN";
                            break;
                        case 2:
                            cmd.Parameters.Add(new OracleParameter("v_DonVi", OracleDbType.NVarchar2)).Value = "TTKTTN-DN";
                            break;
                        case 3:
                            cmd.Parameters.Add(new OracleParameter("v_DonVi", OracleDbType.NVarchar2)).Value = "TTKTTN-HCM";
                            break;
                    }
                    
                    cmd.Parameters.Add("v_List", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    OracleDataReader dr = Helper.ExecuteDataReader(cmd, Helper.OraDCOracleConnection);

                    if (dr.HasRows)
                    {
                        listSumTimekeepingDetail = new List<SumTimekeepingDetail>();
                        while (dr.Read())
                        {
                            oSumTimekeepingDetail = new SumTimekeepingDetail();       
                            oSumTimekeepingDetail.Kip1 = Convert.ToInt32(dr["KIP_1"].ToString());
                            oSumTimekeepingDetail.Kip2 = Convert.ToInt32(dr["KIP_2"].ToString());
                            oSumTimekeepingDetail.Kip3 = Convert.ToInt32(dr["KIP_3"].ToString());
                            oSumTimekeepingDetail.Kip4 = Convert.ToInt32(dr["KIP_4"].ToString());
                            oSumTimekeepingDetail.Kip5 = Convert.ToInt32(dr["KIP_5"].ToString());
                            oSumTimekeepingDetail.Kip6 = Convert.ToInt32(dr["KIP_6"].ToString());
                            oSumTimekeepingDetail.Kip7 = Convert.ToInt32(dr["KIP_7"].ToString());
                            oSumTimekeepingDetail.Kip8 = Convert.ToInt32(dr["KIP_8"].ToString());
                            oSumTimekeepingDetail.Kip9 = Convert.ToInt32(dr["KIP_9"].ToString());
                            oSumTimekeepingDetail.Kip10 = Convert.ToInt32(dr["KIP_10"].ToString());
                            oSumTimekeepingDetail.Kip11 = Convert.ToInt32(dr["KIP_11"].ToString());
                            oSumTimekeepingDetail.Kip12 = Convert.ToInt32(dr["KIP_12"].ToString());
                            oSumTimekeepingDetail.Kip13 = Convert.ToInt32(dr["KIP_13"].ToString());
                            oSumTimekeepingDetail.Kip14 = Convert.ToInt32(dr["KIP_14"].ToString());
                            oSumTimekeepingDetail.Kip15 = Convert.ToInt32(dr["KIP_15"].ToString());
                            oSumTimekeepingDetail.Kip16 = Convert.ToInt32(dr["KIP_16"].ToString());
                            oSumTimekeepingDetail.Kip17 = Convert.ToInt32(dr["KIP_17"].ToString());
                            oSumTimekeepingDetail.Kip18 = Convert.ToInt32(dr["KIP_18"].ToString());
                            oSumTimekeepingDetail.Kip19 = Convert.ToInt32(dr["KIP_19"].ToString());
                            oSumTimekeepingDetail.Kip20 = Convert.ToInt32(dr["KIP_20"].ToString());
                            oSumTimekeepingDetail.Den9h = Convert.ToInt32(dr["Den_9h"].ToString());
                            oSumTimekeepingDetail.Den10h = Convert.ToInt32(dr["Den_10h"].ToString());
                            oSumTimekeepingDetail.Den11h = Convert.ToInt32(dr["Den_11h"].ToString());
                            oSumTimekeepingDetail.Den12h = Convert.ToInt32(dr["Den_12h"].ToString());
                            oSumTimekeepingDetail.Den13h = Convert.ToInt32(dr["Den_13h"].ToString());
                            oSumTimekeepingDetail.Den14h = Convert.ToInt32(dr["Den_14h"].ToString());
                            oSumTimekeepingDetail.Den15h = Convert.ToInt32(dr["Den_15h"].ToString());
                            oSumTimekeepingDetail.Den16h = Convert.ToInt32(dr["Den_16h"].ToString());
                            oSumTimekeepingDetail.Den17h = Convert.ToInt32(dr["Den_17h"].ToString());
                            oSumTimekeepingDetail.Den18h = Convert.ToInt32(dr["Den_18h"].ToString());
                            oSumTimekeepingDetail.Den19h = Convert.ToInt32(dr["Den_19h"].ToString());
                            oSumTimekeepingDetail.Den20h = Convert.ToInt32(dr["Den_20h"].ToString());
                            oSumTimekeepingDetail.Den21h = Convert.ToInt32(dr["Den_21h"].ToString());
                            oSumTimekeepingDetail.Den22h = Convert.ToInt32(dr["Den_22h"].ToString());
                            oSumTimekeepingDetail.Den23h = Convert.ToInt32(dr["Den_23h"].ToString());
                            oSumTimekeepingDetail.Den24h = Convert.ToInt32(dr["Den_24h"].ToString());
                            oSumTimekeepingDetail.Den1h = Convert.ToInt32(dr["Den_1h"].ToString());
                            oSumTimekeepingDetail.Den2h = Convert.ToInt32(dr["Den_2h"].ToString());
                            oSumTimekeepingDetail.Den3h = Convert.ToInt32(dr["Den_3h"].ToString());
                            oSumTimekeepingDetail.Den4h = Convert.ToInt32(dr["Den_4h"].ToString());
                            oSumTimekeepingDetail.Den5h = Convert.ToInt32(dr["Den_5h"].ToString());
                            oSumTimekeepingDetail.Den6h = Convert.ToInt32(dr["Den_6h"].ToString());
                            oSumTimekeepingDetail.Den7h = Convert.ToInt32(dr["Den_7h"].ToString());
                            oSumTimekeepingDetail.Den8h = Convert.ToInt32(dr["Den_8h"].ToString());
                            listSumTimekeepingDetail.Add(oSumTimekeepingDetail);
                        }
                        _returnTimekeeping.Code = "00";
                        _returnTimekeeping.Message = "Lấy dữ liệu thành công.";
                        _returnTimekeeping.ListSumTimekeepingReport = listSumTimekeepingDetail;
                    }
                }
            }
            catch (Exception ex)
            {
                _returnTimekeeping.Code = "99";
                _returnTimekeeping.Message = "Lỗi xử lý dữ liệu";
                _returnTimekeeping.Total = 0;
                _returnTimekeeping.ListSumTimekeepingReport = null;
            }
            return _returnTimekeeping;
        }

        #endregion

    }

}

