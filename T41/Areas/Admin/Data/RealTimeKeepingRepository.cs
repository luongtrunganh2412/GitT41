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
    public class RealTimeKeepingRepository
    {
        // Phần lấy dữ liệu kíp từ bảng DM_KIP
        #region GetALLDMKip
        public IEnumerable<RealTimeKeeping> GetAllDMKip()
        {
            List<RealTimeKeeping> listRealTimeKeeping = null;
            RealTimeKeeping oRealTimeKeeping = null;
        
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
                        listRealTimeKeeping = new List<RealTimeKeeping>();
                        while (dr.Read())
                        {
                            oRealTimeKeeping = new RealTimeKeeping();
                            oRealTimeKeeping.MAKIP = int.Parse(dr["MAKIP"].ToString());
                            oRealTimeKeeping.TENKIP = dr["TENKIP"].ToString();
                            listRealTimeKeeping.Add(oRealTimeKeeping);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "GetAllDMKip" + ex.Message);
                listRealTimeKeeping = null;
            }

            return listRealTimeKeeping;
        }
        #endregion
        //Phần chi tiết của bảng tổng hợp theo kíp
        #region REAL_TIMEKEEPING_KIP_DETAIL          
        public ReturnRealTimekeeping REAL_TIMEKEEPING_KIP_DETAIL(string ngay, int donvi, int ankip, int kip)              
        {
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnRealTimekeeping _returnRealTimekeeping = new ReturnRealTimekeeping();
            
            
            List<RealTimekeepingKipDetail> listRealTimekeepingKipDetail = null;
            RealTimekeepingKipDetail oRealTimekeepingKipDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = Helper.OraDCOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "EMS_CHAMCONG.TongHopTheoKipTT";
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
                    //if (ankip == 0)
                    //{
                    //    cmd.Parameters.Add(new OracleParameter("v_AnKip", OracleDbType.Int32)).Value = "0";
                    //}
                    //else
                    //{
                    //    cmd.Parameters.Add(new OracleParameter("v_AnKip", OracleDbType.Int32)).Value = "1";
                    //}
                    cmd.Parameters.Add("v_List", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    OracleDataReader dr = Helper.ExecuteDataReader(cmd, Helper.OraDCOracleConnection);

                    if (dr.HasRows)
                    {
                        listRealTimekeepingKipDetail = new List<RealTimekeepingKipDetail>();
                        while (dr.Read())
                        {
                            oRealTimekeepingKipDetail = new RealTimekeepingKipDetail();
                            oRealTimekeepingKipDetail.Ca = dr["CA"].ToString();
                            oRealTimekeepingKipDetail.TenKip = dr["TEN_KIP"].ToString();
                            oRealTimekeepingKipDetail.ThoiGian = dr["THOI_GIAN"].ToString();
                            oRealTimekeepingKipDetail.TongGioLam = Convert.ToInt32(dr["TONG_GIO"].ToString());
                            oRealTimekeepingKipDetail.SoNguoi = Convert.ToInt32(dr["SO_NGUOI"].ToString());
                            oRealTimekeepingKipDetail.Gio8Den9h = Convert.ToInt32(dr["Den_9h"].ToString());
                            oRealTimekeepingKipDetail.Gio9Den10h = Convert.ToInt32(dr["Den_10h"].ToString());
                            oRealTimekeepingKipDetail.Gio10Den11h = Convert.ToInt32(dr["Den_11h"].ToString());
                            oRealTimekeepingKipDetail.Gio11Den12h = Convert.ToInt32(dr["Den_12h"].ToString());
                            oRealTimekeepingKipDetail.Gio12Den13h = Convert.ToInt32(dr["Den_13h"].ToString());
                            oRealTimekeepingKipDetail.Gio13Den14h = Convert.ToInt32(dr["Den_14h"].ToString());
                            oRealTimekeepingKipDetail.Gio14Den15h = Convert.ToInt32(dr["Den_15h"].ToString());
                            oRealTimekeepingKipDetail.Gio15Den16h = Convert.ToInt32(dr["Den_16h"].ToString());
                            oRealTimekeepingKipDetail.Gio16Den17h = Convert.ToInt32(dr["Den_17h"].ToString());
                            oRealTimekeepingKipDetail.Gio17Den18h = Convert.ToInt32(dr["Den_18h"].ToString());
                            oRealTimekeepingKipDetail.Gio18Den19h = Convert.ToInt32(dr["Den_19h"].ToString());
                            oRealTimekeepingKipDetail.Gio19Den20h = Convert.ToInt32(dr["Den_20h"].ToString());
                            oRealTimekeepingKipDetail.Gio20Den21h = Convert.ToInt32(dr["Den_21h"].ToString());
                            oRealTimekeepingKipDetail.Gio21Den22h = Convert.ToInt32(dr["Den_22h"].ToString());
                            oRealTimekeepingKipDetail.Gio22Den23h = Convert.ToInt32(dr["Den_23h"].ToString());
                            oRealTimekeepingKipDetail.Gio23Den24h = Convert.ToInt32(dr["Den_24h"].ToString());
                            oRealTimekeepingKipDetail.Gio24Den1h = Convert.ToInt32(dr["Den_1h"].ToString());
                            oRealTimekeepingKipDetail.Gio1Den2h = Convert.ToInt32(dr["Den_2h"].ToString());
                            oRealTimekeepingKipDetail.Gio2Den3h = Convert.ToInt32(dr["Den_3h"].ToString());
                            oRealTimekeepingKipDetail.Gio3Den4h = Convert.ToInt32(dr["Den_4h"].ToString());
                            oRealTimekeepingKipDetail.Gio4Den5h = Convert.ToInt32(dr["Den_5h"].ToString());
                            oRealTimekeepingKipDetail.Gio5Den6h = Convert.ToInt32(dr["Den_6h"].ToString());
                            oRealTimekeepingKipDetail.Gio6Den7h = Convert.ToInt32(dr["Den_7h"].ToString());
                            oRealTimekeepingKipDetail.Gio7Den8h = Convert.ToInt32(dr["Den_8h"].ToString());
                            listRealTimekeepingKipDetail.Add(oRealTimekeepingKipDetail);
                        }
                        _returnRealTimekeeping.Code = "00";
                        _returnRealTimekeeping.Message = "Lấy dữ liệu thành công.";
                        _returnRealTimekeeping.RealListTimekeepingKipReport = listRealTimekeepingKipDetail;
                    }
                }
            }
            catch(Exception ex)
            {
                _returnRealTimekeeping.Code = "99";
                _returnRealTimekeeping.Message = "Lỗi xử lý dữ liệu";
                _returnRealTimekeeping.Total = 0;
                _returnRealTimekeeping.RealListTimekeepingKipReport = null;
            }
            return _returnRealTimekeeping;
        }



        #endregion
        //Phần tổng của bảng tổng hợp theo kíp
        #region REAL_SUM_TIMEKEEPING_KIP_DETAIL 
        public ReturnRealTimekeeping REAL_SUM_TIMEKEEPING_KIP_DETAIL(string ngay, int donvi, int to)
        {
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnRealTimekeeping _returnRealTimekeeping = new ReturnRealTimekeeping();


            List<RealSumTimekeepingKipDetail> listRealSumTimekeepingKipDetail = null;
            RealSumTimekeepingKipDetail oRealSumTimekeepingKipDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = Helper.OraDCOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "EMS_CHAMCONG.TongTT";
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
                        listRealSumTimekeepingKipDetail = new List<RealSumTimekeepingKipDetail>();
                        while (dr.Read())
                        {
                            oRealSumTimekeepingKipDetail = new RealSumTimekeepingKipDetail();
                            oRealSumTimekeepingKipDetail.SumSoNguoi = Convert.ToInt32(dr["SUM(SO_NGUOI)"].ToString());
                            oRealSumTimekeepingKipDetail.SumDen9h = Convert.ToInt32(dr["SUM(Den_9h)"].ToString());
                            oRealSumTimekeepingKipDetail.SumDen10h = Convert.ToInt32(dr["SUM(Den_10h)"].ToString());
                            oRealSumTimekeepingKipDetail.SumDen11h = Convert.ToInt32(dr["SUM(Den_11h)"].ToString());
                            oRealSumTimekeepingKipDetail.SumDen12h = Convert.ToInt32(dr["SUM(Den_12h)"].ToString());
                            oRealSumTimekeepingKipDetail.SumDen13h = Convert.ToInt32(dr["SUM(Den_13h)"].ToString());
                            oRealSumTimekeepingKipDetail.SumDen14h = Convert.ToInt32(dr["SUM(Den_14h)"].ToString());
                            oRealSumTimekeepingKipDetail.SumDen15h = Convert.ToInt32(dr["SUM(Den_15h)"].ToString());
                            oRealSumTimekeepingKipDetail.SumDen16h = Convert.ToInt32(dr["SUM(Den_16h)"].ToString());
                            oRealSumTimekeepingKipDetail.SumDen17h = Convert.ToInt32(dr["SUM(Den_17h)"].ToString());
                            oRealSumTimekeepingKipDetail.SumDen18h = Convert.ToInt32(dr["SUM(Den_18h)"].ToString());
                            oRealSumTimekeepingKipDetail.SumDen19h = Convert.ToInt32(dr["SUM(Den_19h)"].ToString());
                            oRealSumTimekeepingKipDetail.SumDen20h = Convert.ToInt32(dr["SUM(Den_20h)"].ToString());
                            oRealSumTimekeepingKipDetail.SumDen21h = Convert.ToInt32(dr["SUM(Den_21h)"].ToString());
                            oRealSumTimekeepingKipDetail.SumDen22h = Convert.ToInt32(dr["SUM(Den_22h)"].ToString());
                            oRealSumTimekeepingKipDetail.SumDen23h = Convert.ToInt32(dr["SUM(Den_23h)"].ToString());
                            oRealSumTimekeepingKipDetail.SumDen24h = Convert.ToInt32(dr["SUM(Den_24h)"].ToString());
                            oRealSumTimekeepingKipDetail.SumDen1h = Convert.ToInt32(dr["SUM(Den_1h)"].ToString());
                            oRealSumTimekeepingKipDetail.SumDen2h = Convert.ToInt32(dr["SUM(Den_2h)"].ToString());
                            oRealSumTimekeepingKipDetail.SumDen3h = Convert.ToInt32(dr["SUM(Den_3h)"].ToString());
                            oRealSumTimekeepingKipDetail.SumDen4h = Convert.ToInt32(dr["SUM(Den_4h)"].ToString());
                            oRealSumTimekeepingKipDetail.SumDen5h = Convert.ToInt32(dr["SUM(Den_5h)"].ToString());
                            oRealSumTimekeepingKipDetail.SumDen6h = Convert.ToInt32(dr["SUM(Den_6h)"].ToString());
                            oRealSumTimekeepingKipDetail.SumDen7h = Convert.ToInt32(dr["SUM(Den_7h)"].ToString());
                            oRealSumTimekeepingKipDetail.SumDen8h = Convert.ToInt32(dr["SUM(Den_8h)"].ToString());
                            listRealSumTimekeepingKipDetail.Add(oRealSumTimekeepingKipDetail);
                        }
                        _returnRealTimekeeping.Code = "00";
                        _returnRealTimekeeping.Message = "Lấy dữ liệu thành công.";
                        _returnRealTimekeeping.RealListSumTimekeepingKipReport = listRealSumTimekeepingKipDetail;
                    }
                }
            }
            catch (Exception ex)
            {
                _returnRealTimekeeping.Code = "99";
                _returnRealTimekeeping.Message = "Lỗi xử lý dữ liệu";
                _returnRealTimekeeping.Total = 0;
                _returnRealTimekeeping.RealListSumTimekeepingKipReport = null;
            }
            return _returnRealTimekeeping;
        }

        #endregion
        //Phần tổng sản lượng, khối lượng của bảng tổng hợp theo kíp 
        #region REAL_SUM_SLKL_TIMEKEEPING_KIP_DETAIL
        public ReturnRealTimekeeping REAL_SUM_SLKL_TIMEKEEPING_KIP_DETAIL(string ngay, int donvi)
        {
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnRealTimekeeping _returnRealTimekeeping = new ReturnRealTimekeeping();


            List<RealSumSLKLTimekeepingKipDetail> listRealSumSLKLTimekeepingKipDetail = null;
            RealSumSLKLTimekeepingKipDetail oRealSumSLKLTimekeepingKipDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = Helper.OraDCOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "EMS_CHAMCONG.TongSLKLTT";
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
                        listRealSumSLKLTimekeepingKipDetail = new List<RealSumSLKLTimekeepingKipDetail>();
                        while (dr.Read())
                        {
                            oRealSumSLKLTimekeepingKipDetail = new RealSumSLKLTimekeepingKipDetail();
                            oRealSumSLKLTimekeepingKipDetail.SLDen9h = Convert.ToDecimal(dr["SL_DEN_9H"].ToString());
                            oRealSumSLKLTimekeepingKipDetail.KLDen9h = Convert.ToDecimal(dr["KL_DEN_9H"].ToString());
                            oRealSumSLKLTimekeepingKipDetail.SLDen10h = Convert.ToDecimal(dr["SL_DEN_10H"].ToString());
                            oRealSumSLKLTimekeepingKipDetail.KLDen10h = Convert.ToDecimal(dr["KL_DEN_10H"].ToString());
                            oRealSumSLKLTimekeepingKipDetail.SLDen11h = Convert.ToDecimal(dr["SL_DEN_11H"].ToString());
                            oRealSumSLKLTimekeepingKipDetail.KLDen11h = Convert.ToDecimal(dr["KL_DEN_11H"].ToString());
                            oRealSumSLKLTimekeepingKipDetail.SLDen12h = Convert.ToDecimal(dr["SL_DEN_12H"].ToString());
                            oRealSumSLKLTimekeepingKipDetail.KLDen12h = Convert.ToDecimal(dr["KL_DEN_12H"].ToString());
                            oRealSumSLKLTimekeepingKipDetail.SLDen13h = Convert.ToDecimal(dr["SL_DEN_13H"].ToString());
                            oRealSumSLKLTimekeepingKipDetail.KLDen13h = Convert.ToDecimal(dr["KL_DEN_13H"].ToString());
                            oRealSumSLKLTimekeepingKipDetail.SLDen14h = Convert.ToDecimal(dr["SL_DEN_14H"].ToString());
                            oRealSumSLKLTimekeepingKipDetail.KLDen14h = Convert.ToDecimal(dr["KL_DEN_14H"].ToString());
                            oRealSumSLKLTimekeepingKipDetail.SLDen15h = Convert.ToDecimal(dr["SL_DEN_15H"].ToString());
                            oRealSumSLKLTimekeepingKipDetail.KLDen15h = Convert.ToDecimal(dr["KL_DEN_15H"].ToString());
                            oRealSumSLKLTimekeepingKipDetail.SLDen16h = Convert.ToDecimal(dr["SL_DEN_16H"].ToString());
                            oRealSumSLKLTimekeepingKipDetail.KLDen16h = Convert.ToDecimal(dr["KL_DEN_16H"].ToString());
                            oRealSumSLKLTimekeepingKipDetail.SLDen17h = Convert.ToDecimal(dr["SL_DEN_17H"].ToString());
                            oRealSumSLKLTimekeepingKipDetail.KLDen17h = Convert.ToDecimal(dr["KL_DEN_17H"].ToString());
                            oRealSumSLKLTimekeepingKipDetail.SLDen18h = Convert.ToDecimal(dr["SL_DEN_18H"].ToString());
                            oRealSumSLKLTimekeepingKipDetail.KLDen18h = Convert.ToDecimal(dr["KL_DEN_18H"].ToString());
                            oRealSumSLKLTimekeepingKipDetail.SLDen19h = Convert.ToDecimal(dr["SL_DEN_19H"].ToString());
                            oRealSumSLKLTimekeepingKipDetail.KLDen19h = Convert.ToDecimal(dr["KL_DEN_19H"].ToString());
                            oRealSumSLKLTimekeepingKipDetail.SLDen20h = Convert.ToDecimal(dr["SL_DEN_20H"].ToString());
                            oRealSumSLKLTimekeepingKipDetail.KLDen20h = Convert.ToDecimal(dr["KL_DEN_20H"].ToString());
                            oRealSumSLKLTimekeepingKipDetail.SLDen21h = Convert.ToDecimal(dr["SL_DEN_21H"].ToString());
                            oRealSumSLKLTimekeepingKipDetail.KLDen21h = Convert.ToDecimal(dr["KL_DEN_21H"].ToString());
                            oRealSumSLKLTimekeepingKipDetail.SLDen22h = Convert.ToDecimal(dr["SL_DEN_22H"].ToString());
                            oRealSumSLKLTimekeepingKipDetail.KLDen22h = Convert.ToDecimal(dr["KL_DEN_22H"].ToString());
                            oRealSumSLKLTimekeepingKipDetail.SLDen23h = Convert.ToDecimal(dr["SL_DEN_23H"].ToString());
                            oRealSumSLKLTimekeepingKipDetail.KLDen23h = Convert.ToDecimal(dr["KL_DEN_23H"].ToString());
                            oRealSumSLKLTimekeepingKipDetail.SLDen24h = Convert.ToDecimal(dr["SL_DEN_24H"].ToString());
                            oRealSumSLKLTimekeepingKipDetail.KLDen24h = Convert.ToDecimal(dr["KL_DEN_24H"].ToString());
                            oRealSumSLKLTimekeepingKipDetail.SLDen1h = Convert.ToDecimal(dr["SL_DEN_1H"].ToString());
                            oRealSumSLKLTimekeepingKipDetail.KLDen1h = Convert.ToDecimal(dr["KL_DEN_1H"].ToString());
                            oRealSumSLKLTimekeepingKipDetail.SLDen2h = Convert.ToDecimal(dr["SL_DEN_2H"].ToString());
                            oRealSumSLKLTimekeepingKipDetail.KLDen2h = Convert.ToDecimal(dr["KL_DEN_2H"].ToString());
                            oRealSumSLKLTimekeepingKipDetail.SLDen3h = Convert.ToDecimal(dr["SL_DEN_3H"].ToString());
                            oRealSumSLKLTimekeepingKipDetail.KLDen3h = Convert.ToDecimal(dr["KL_DEN_3H"].ToString());
                            oRealSumSLKLTimekeepingKipDetail.SLDen4h = Convert.ToDecimal(dr["SL_DEN_4H"].ToString());
                            oRealSumSLKLTimekeepingKipDetail.KLDen4h = Convert.ToDecimal(dr["KL_DEN_4H"].ToString());
                            oRealSumSLKLTimekeepingKipDetail.SLDen5h = Convert.ToDecimal(dr["SL_DEN_5H"].ToString());
                            oRealSumSLKLTimekeepingKipDetail.KLDen5h = Convert.ToDecimal(dr["KL_DEN_5H"].ToString());
                            oRealSumSLKLTimekeepingKipDetail.SLDen6h = Convert.ToDecimal(dr["SL_DEN_6H"].ToString());
                            oRealSumSLKLTimekeepingKipDetail.KLDen6h = Convert.ToDecimal(dr["KL_DEN_6H"].ToString());
                            oRealSumSLKLTimekeepingKipDetail.SLDen7h = Convert.ToDecimal(dr["SL_DEN_7H"].ToString());
                            oRealSumSLKLTimekeepingKipDetail.KLDen7h = Convert.ToDecimal(dr["KL_DEN_7H"].ToString());
                            oRealSumSLKLTimekeepingKipDetail.SLDen8h = Convert.ToDecimal(dr["SL_DEN_8H"].ToString());
                            oRealSumSLKLTimekeepingKipDetail.KLDen8h = Convert.ToDecimal(dr["KL_DEN_8H"].ToString());
                            listRealSumSLKLTimekeepingKipDetail.Add(oRealSumSLKLTimekeepingKipDetail);
                        }
                        _returnRealTimekeeping.Code = "00";
                        _returnRealTimekeeping.Message = "Lấy dữ liệu thành công.";
                        _returnRealTimekeeping.RealListSumSLKLTimekeepingKipReport = listRealSumSLKLTimekeepingKipDetail;
                    }
                }
            }
            catch (Exception ex)
            {
                _returnRealTimekeeping.Code = "99";
                _returnRealTimekeeping.Message = "Lỗi xử lý dữ liệu";
                _returnRealTimekeeping.Total = 0;
                _returnRealTimekeeping.RealListSumSLKLTimekeepingKipReport = null;
            }
            return _returnRealTimekeeping;
        }

        #endregion
        //Phần chi tiết của bảng tổng hợp theo chức danh
        #region REAL_TIMEKEEPING_TITLE_DETAIL

        public ReturnRealTimekeeping REAL_TIMEKEEPING_TITLE_DETAIL(string ngay, int donvi, int kip)
        {
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnRealTimekeeping _returnRealTimekeeping = new ReturnRealTimekeeping();


            List<RealTimekeepingTitleDetail> listRealTimekeepingTitleDetail = null;
            RealTimekeepingTitleDetail oRealTimekeepingTitleDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = Helper.OraDCOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "EMS_TIMEKEEPING.TongHopTheoChucDanhTT";
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
                        listRealTimekeepingTitleDetail = new List<RealTimekeepingTitleDetail>();
                        while (dr.Read())
                        {
                            oRealTimekeepingTitleDetail = new RealTimekeepingTitleDetail();
                            oRealTimekeepingTitleDetail.TenChucDanh = dr["TEN_CHUCDANH"].ToString();
                            oRealTimekeepingTitleDetail.Loai = dr["LOAI"].ToString();
                            oRealTimekeepingTitleDetail.SoNguoi = Convert.ToInt32(dr["SO_NGUOI"].ToString());
                            oRealTimekeepingTitleDetail.Gio8Den9h = Convert.ToInt32(dr["Den_9h"].ToString());
                            oRealTimekeepingTitleDetail.Gio9Den10h = Convert.ToInt32(dr["Den_10h"].ToString());
                            oRealTimekeepingTitleDetail.Gio10Den11h = Convert.ToInt32(dr["Den_11h"].ToString());
                            oRealTimekeepingTitleDetail.Gio11Den12h = Convert.ToInt32(dr["Den_12h"].ToString());
                            oRealTimekeepingTitleDetail.Gio12Den13h = Convert.ToInt32(dr["Den_13h"].ToString());
                            oRealTimekeepingTitleDetail.Gio13Den14h = Convert.ToInt32(dr["Den_14h"].ToString());
                            oRealTimekeepingTitleDetail.Gio14Den15h = Convert.ToInt32(dr["Den_15h"].ToString());
                            oRealTimekeepingTitleDetail.Gio15Den16h = Convert.ToInt32(dr["Den_16h"].ToString());
                            oRealTimekeepingTitleDetail.Gio16Den17h = Convert.ToInt32(dr["Den_17h"].ToString());
                            oRealTimekeepingTitleDetail.Gio17Den18h = Convert.ToInt32(dr["Den_18h"].ToString());
                            oRealTimekeepingTitleDetail.Gio18Den19h = Convert.ToInt32(dr["Den_19h"].ToString());
                            oRealTimekeepingTitleDetail.Gio19Den20h = Convert.ToInt32(dr["Den_20h"].ToString());
                            oRealTimekeepingTitleDetail.Gio20Den21h = Convert.ToInt32(dr["Den_21h"].ToString());
                            oRealTimekeepingTitleDetail.Gio21Den22h = Convert.ToInt32(dr["Den_22h"].ToString());
                            oRealTimekeepingTitleDetail.Gio22Den23h = Convert.ToInt32(dr["Den_23h"].ToString());
                            oRealTimekeepingTitleDetail.Gio23Den24h = Convert.ToInt32(dr["Den_24h"].ToString());
                            oRealTimekeepingTitleDetail.Gio24Den1h = Convert.ToInt32(dr["Den_1h"].ToString());
                            oRealTimekeepingTitleDetail.Gio1Den2h = Convert.ToInt32(dr["Den_2h"].ToString());
                            oRealTimekeepingTitleDetail.Gio2Den3h = Convert.ToInt32(dr["Den_3h"].ToString());
                            oRealTimekeepingTitleDetail.Gio3Den4h = Convert.ToInt32(dr["Den_4h"].ToString());
                            oRealTimekeepingTitleDetail.Gio4Den5h = Convert.ToInt32(dr["Den_5h"].ToString());
                            oRealTimekeepingTitleDetail.Gio5Den6h = Convert.ToInt32(dr["Den_6h"].ToString());
                            oRealTimekeepingTitleDetail.Gio6Den7h = Convert.ToInt32(dr["Den_7h"].ToString());
                            oRealTimekeepingTitleDetail.Gio7Den8h = Convert.ToInt32(dr["Den_8h"].ToString());
                            listRealTimekeepingTitleDetail.Add(oRealTimekeepingTitleDetail);
                        }
                        _returnRealTimekeeping.Code = "00";
                        _returnRealTimekeeping.Message = "Lấy dữ liệu thành công.";
                        _returnRealTimekeeping.RealListTimekeepingTitleReport = listRealTimekeepingTitleDetail;
                    }
                }
            }
            catch (Exception ex)
            {
                _returnRealTimekeeping.Code = "99";
                _returnRealTimekeeping.Message = "Lỗi xử lý dữ liệu";
                _returnRealTimekeeping.Total = 0;
                _returnRealTimekeeping.RealListTimekeepingTitleReport = null;
            }
            return _returnRealTimekeeping;
        }

        #endregion
        //Phần tổng của bảng tổng hợp theo chức danh
        #region REAL_SUM_TIMEKEEPING_TITLE_DETAIL
        public ReturnRealTimekeeping REAL_SUM_TIMEKEEPING_TITLE_DETAIL(string ngay, int donvi, int to)
        {
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnRealTimekeeping _returnRealTimekeeping = new ReturnRealTimekeeping();


            List<RealSumTimekeepingTitleDetail> listRealSumTimekeepingTitleDetail = null;
            RealSumTimekeepingTitleDetail oRealSumTimekeepingTitleDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = Helper.OraDCOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "EMS_CHAMCONG.TongTT";
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
                        listRealSumTimekeepingTitleDetail = new List<RealSumTimekeepingTitleDetail>();
                        while (dr.Read())
                        {
                            oRealSumTimekeepingTitleDetail = new RealSumTimekeepingTitleDetail();
                            oRealSumTimekeepingTitleDetail.SumSoNguoi = Convert.ToInt32(dr["SUM(SO_NGUOI)"].ToString());
                            oRealSumTimekeepingTitleDetail.SumDen9h = Convert.ToInt32(dr["SUM(Den_9h)"].ToString());
                            oRealSumTimekeepingTitleDetail.SumDen10h = Convert.ToInt32(dr["SUM(Den_10h)"].ToString());
                            oRealSumTimekeepingTitleDetail.SumDen11h = Convert.ToInt32(dr["SUM(Den_11h)"].ToString());
                            oRealSumTimekeepingTitleDetail.SumDen12h = Convert.ToInt32(dr["SUM(Den_12h)"].ToString());
                            oRealSumTimekeepingTitleDetail.SumDen13h = Convert.ToInt32(dr["SUM(Den_13h)"].ToString());
                            oRealSumTimekeepingTitleDetail.SumDen14h = Convert.ToInt32(dr["SUM(Den_14h)"].ToString());
                            oRealSumTimekeepingTitleDetail.SumDen15h = Convert.ToInt32(dr["SUM(Den_15h)"].ToString());
                            oRealSumTimekeepingTitleDetail.SumDen16h = Convert.ToInt32(dr["SUM(Den_16h)"].ToString());
                            oRealSumTimekeepingTitleDetail.SumDen17h = Convert.ToInt32(dr["SUM(Den_17h)"].ToString());
                            oRealSumTimekeepingTitleDetail.SumDen18h = Convert.ToInt32(dr["SUM(Den_18h)"].ToString());
                            oRealSumTimekeepingTitleDetail.SumDen19h = Convert.ToInt32(dr["SUM(Den_19h)"].ToString());
                            oRealSumTimekeepingTitleDetail.SumDen20h = Convert.ToInt32(dr["SUM(Den_20h)"].ToString());
                            oRealSumTimekeepingTitleDetail.SumDen21h = Convert.ToInt32(dr["SUM(Den_21h)"].ToString());
                            oRealSumTimekeepingTitleDetail.SumDen22h = Convert.ToInt32(dr["SUM(Den_22h)"].ToString());
                            oRealSumTimekeepingTitleDetail.SumDen23h = Convert.ToInt32(dr["SUM(Den_23h)"].ToString());
                            oRealSumTimekeepingTitleDetail.SumDen24h = Convert.ToInt32(dr["SUM(Den_24h)"].ToString());
                            oRealSumTimekeepingTitleDetail.SumDen1h = Convert.ToInt32(dr["SUM(Den_1h)"].ToString());
                            oRealSumTimekeepingTitleDetail.SumDen2h = Convert.ToInt32(dr["SUM(Den_2h)"].ToString());
                            oRealSumTimekeepingTitleDetail.SumDen3h = Convert.ToInt32(dr["SUM(Den_3h)"].ToString());
                            oRealSumTimekeepingTitleDetail.SumDen4h = Convert.ToInt32(dr["SUM(Den_4h)"].ToString());
                            oRealSumTimekeepingTitleDetail.SumDen5h = Convert.ToInt32(dr["SUM(Den_5h)"].ToString());
                            oRealSumTimekeepingTitleDetail.SumDen6h = Convert.ToInt32(dr["SUM(Den_6h)"].ToString());
                            oRealSumTimekeepingTitleDetail.SumDen7h = Convert.ToInt32(dr["SUM(Den_7h)"].ToString());
                            oRealSumTimekeepingTitleDetail.SumDen8h = Convert.ToInt32(dr["SUM(Den_8h)"].ToString());
                            listRealSumTimekeepingTitleDetail.Add(oRealSumTimekeepingTitleDetail);
                        }
                        _returnRealTimekeeping.Code = "00";
                        _returnRealTimekeeping.Message = "Lấy dữ liệu thành công.";
                        _returnRealTimekeeping.RealListSumTimekeepingTitleReport = listRealSumTimekeepingTitleDetail;
                    }
                }
            }
            catch (Exception ex)
            {
                _returnRealTimekeeping.Code = "99";
                _returnRealTimekeeping.Message = "Lỗi xử lý dữ liệu";
                _returnRealTimekeeping.Total = 0;
                _returnRealTimekeeping.RealListSumTimekeepingTitleReport = null;
            }
            return _returnRealTimekeeping;
        }

        #endregion
        //Phần tổng sản lượng, khối lượng của bảng tổng hợp theo chức danh
        #region REAL_SUM_SLKL_TIMEKEEPING_TITLE_DETAIL
        public ReturnRealTimekeeping REAL_SUM_SLKL_TIMEKEEPING_TITLE_DETAIL(string ngay, int donvi)
        {
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnRealTimekeeping _returnRealTimekeeping = new ReturnRealTimekeeping();


            List<RealSumSLKLTimekeepingTitleDetail> listRealSumSLKLTimekeepingTitleDetail = null;
            RealSumSLKLTimekeepingTitleDetail oRealSumSLKLTimekeepingTitleDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = Helper.OraDCOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "EMS_CHAMCONG.TongSLKLTT";
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
                        listRealSumSLKLTimekeepingTitleDetail = new List<RealSumSLKLTimekeepingTitleDetail>();
                        while (dr.Read())
                        {
                            oRealSumSLKLTimekeepingTitleDetail = new RealSumSLKLTimekeepingTitleDetail();
                            oRealSumSLKLTimekeepingTitleDetail.SLDen9h = Convert.ToDecimal(dr["SL_DEN_9H"].ToString());
                            oRealSumSLKLTimekeepingTitleDetail.KLDen9h = Convert.ToDecimal(dr["KL_DEN_9H"].ToString());
                            oRealSumSLKLTimekeepingTitleDetail.SLDen10h = Convert.ToDecimal(dr["SL_DEN_10H"].ToString());
                            oRealSumSLKLTimekeepingTitleDetail.KLDen10h = Convert.ToDecimal(dr["KL_DEN_10H"].ToString());
                            oRealSumSLKLTimekeepingTitleDetail.SLDen11h = Convert.ToDecimal(dr["SL_DEN_11H"].ToString());
                            oRealSumSLKLTimekeepingTitleDetail.KLDen11h = Convert.ToDecimal(dr["KL_DEN_11H"].ToString());
                            oRealSumSLKLTimekeepingTitleDetail.SLDen12h = Convert.ToDecimal(dr["SL_DEN_12H"].ToString());
                            oRealSumSLKLTimekeepingTitleDetail.KLDen12h = Convert.ToDecimal(dr["KL_DEN_12H"].ToString());
                            oRealSumSLKLTimekeepingTitleDetail.SLDen13h = Convert.ToDecimal(dr["SL_DEN_13H"].ToString());
                            oRealSumSLKLTimekeepingTitleDetail.KLDen13h = Convert.ToDecimal(dr["KL_DEN_13H"].ToString());
                            oRealSumSLKLTimekeepingTitleDetail.SLDen14h = Convert.ToDecimal(dr["SL_DEN_14H"].ToString());
                            oRealSumSLKLTimekeepingTitleDetail.KLDen14h = Convert.ToDecimal(dr["KL_DEN_14H"].ToString());
                            oRealSumSLKLTimekeepingTitleDetail.SLDen15h = Convert.ToDecimal(dr["SL_DEN_15H"].ToString());
                            oRealSumSLKLTimekeepingTitleDetail.KLDen15h = Convert.ToDecimal(dr["KL_DEN_15H"].ToString());
                            oRealSumSLKLTimekeepingTitleDetail.SLDen16h = Convert.ToDecimal(dr["SL_DEN_16H"].ToString());
                            oRealSumSLKLTimekeepingTitleDetail.KLDen16h = Convert.ToDecimal(dr["KL_DEN_16H"].ToString());
                            oRealSumSLKLTimekeepingTitleDetail.SLDen17h = Convert.ToDecimal(dr["SL_DEN_17H"].ToString());
                            oRealSumSLKLTimekeepingTitleDetail.KLDen17h = Convert.ToDecimal(dr["KL_DEN_17H"].ToString());
                            oRealSumSLKLTimekeepingTitleDetail.SLDen18h = Convert.ToDecimal(dr["SL_DEN_18H"].ToString());
                            oRealSumSLKLTimekeepingTitleDetail.KLDen18h = Convert.ToDecimal(dr["KL_DEN_18H"].ToString());
                            oRealSumSLKLTimekeepingTitleDetail.SLDen19h = Convert.ToDecimal(dr["SL_DEN_19H"].ToString());
                            oRealSumSLKLTimekeepingTitleDetail.KLDen19h = Convert.ToDecimal(dr["KL_DEN_19H"].ToString());
                            oRealSumSLKLTimekeepingTitleDetail.SLDen20h = Convert.ToDecimal(dr["SL_DEN_20H"].ToString());
                            oRealSumSLKLTimekeepingTitleDetail.KLDen20h = Convert.ToDecimal(dr["KL_DEN_20H"].ToString());
                            oRealSumSLKLTimekeepingTitleDetail.SLDen21h = Convert.ToDecimal(dr["SL_DEN_21H"].ToString());
                            oRealSumSLKLTimekeepingTitleDetail.KLDen21h = Convert.ToDecimal(dr["KL_DEN_21H"].ToString());
                            oRealSumSLKLTimekeepingTitleDetail.SLDen22h = Convert.ToDecimal(dr["SL_DEN_22H"].ToString());
                            oRealSumSLKLTimekeepingTitleDetail.KLDen22h = Convert.ToDecimal(dr["KL_DEN_22H"].ToString());
                            oRealSumSLKLTimekeepingTitleDetail.SLDen23h = Convert.ToDecimal(dr["SL_DEN_23H"].ToString());
                            oRealSumSLKLTimekeepingTitleDetail.KLDen23h = Convert.ToDecimal(dr["KL_DEN_23H"].ToString());
                            oRealSumSLKLTimekeepingTitleDetail.SLDen24h = Convert.ToDecimal(dr["SL_DEN_24H"].ToString());
                            oRealSumSLKLTimekeepingTitleDetail.KLDen24h = Convert.ToDecimal(dr["KL_DEN_24H"].ToString());
                            oRealSumSLKLTimekeepingTitleDetail.SLDen1h = Convert.ToDecimal(dr["SL_DEN_1H"].ToString());
                            oRealSumSLKLTimekeepingTitleDetail.KLDen1h = Convert.ToDecimal(dr["KL_DEN_1H"].ToString());
                            oRealSumSLKLTimekeepingTitleDetail.SLDen2h = Convert.ToDecimal(dr["SL_DEN_2H"].ToString());
                            oRealSumSLKLTimekeepingTitleDetail.KLDen2h = Convert.ToDecimal(dr["KL_DEN_2H"].ToString());
                            oRealSumSLKLTimekeepingTitleDetail.SLDen3h = Convert.ToDecimal(dr["SL_DEN_3H"].ToString());
                            oRealSumSLKLTimekeepingTitleDetail.KLDen3h = Convert.ToDecimal(dr["KL_DEN_3H"].ToString());
                            oRealSumSLKLTimekeepingTitleDetail.SLDen4h = Convert.ToDecimal(dr["SL_DEN_4H"].ToString());
                            oRealSumSLKLTimekeepingTitleDetail.KLDen4h = Convert.ToDecimal(dr["KL_DEN_4H"].ToString());
                            oRealSumSLKLTimekeepingTitleDetail.SLDen5h = Convert.ToDecimal(dr["SL_DEN_5H"].ToString());
                            oRealSumSLKLTimekeepingTitleDetail.KLDen5h = Convert.ToDecimal(dr["KL_DEN_5H"].ToString());
                            oRealSumSLKLTimekeepingTitleDetail.SLDen6h = Convert.ToDecimal(dr["SL_DEN_6H"].ToString());
                            oRealSumSLKLTimekeepingTitleDetail.KLDen6h = Convert.ToDecimal(dr["KL_DEN_6H"].ToString());
                            oRealSumSLKLTimekeepingTitleDetail.SLDen7h = Convert.ToDecimal(dr["SL_DEN_7H"].ToString());
                            oRealSumSLKLTimekeepingTitleDetail.KLDen7h = Convert.ToDecimal(dr["KL_DEN_7H"].ToString());
                            oRealSumSLKLTimekeepingTitleDetail.SLDen8h = Convert.ToDecimal(dr["SL_DEN_8H"].ToString());
                            oRealSumSLKLTimekeepingTitleDetail.KLDen8h = Convert.ToDecimal(dr["KL_DEN_8H"].ToString());
                            listRealSumSLKLTimekeepingTitleDetail.Add(oRealSumSLKLTimekeepingTitleDetail);
                        }
                        _returnRealTimekeeping.Code = "00";
                        _returnRealTimekeeping.Message = "Lấy dữ liệu thành công.";
                        _returnRealTimekeeping.RealListSumSLKLTimekeepingTitleReport = listRealSumSLKLTimekeepingTitleDetail;
                    }
                }
            }
            catch (Exception ex)
            {
                _returnRealTimekeeping.Code = "99";
                _returnRealTimekeeping.Message = "Lỗi xử lý dữ liệu";
                _returnRealTimekeeping.Total = 0;
                _returnRealTimekeeping.RealListSumSLKLTimekeepingTitleReport = null;
            }
            return _returnRealTimekeeping;
        }

        #endregion
        //Phần chi tiết của bảng tổng hợp theo chi tiết
        #region REAL_TIMEKEEPING_DETAIL

        public ReturnRealTimekeeping REAL_TIMEKEEPING_DETAIL(string ngay, int donvi, int to, int kip)
        {
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnRealTimekeeping _returnRealTimekeeping = new ReturnRealTimekeeping();


            List<RealTimekeepingDetail> listRealTimekeepingDetail = null;
            RealTimekeepingDetail oRealTimekeepingDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = Helper.OraDCOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "EMS_TIMEKEEPING.ChiTietPhanCongTT";
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
                        listRealTimekeepingDetail = new List<RealTimekeepingDetail>();
                        while (dr.Read())
                        {
                            oRealTimekeepingDetail = new RealTimekeepingDetail();
                            oRealTimekeepingDetail.Ma = dr["MA"].ToString();
                            oRealTimekeepingDetail.Ten = dr["TEN"].ToString();
                            oRealTimekeepingDetail.ChucDanh = dr["CHUC_DANH"].ToString();
                            oRealTimekeepingDetail.Loai = dr["LOAI"].ToString();
                            oRealTimekeepingDetail.Ca = dr["CA"].ToString();
                            oRealTimekeepingDetail.ThoiGian = dr["TG"].ToString();
                            oRealTimekeepingDetail.TongGioLam = Convert.ToInt32(dr["TONG_GIO"].ToString());
                            oRealTimekeepingDetail.Kip1 = dr["KIP_1"].ToString();
                            oRealTimekeepingDetail.Kip2 = dr["KIP_2"].ToString();
                            oRealTimekeepingDetail.Kip3 = dr["KIP_3"].ToString();
                            oRealTimekeepingDetail.Kip4 =  dr["KIP_4"].ToString();
                            oRealTimekeepingDetail.Kip5 =  dr["KIP_5"].ToString();
                            oRealTimekeepingDetail.Kip6 =  dr["KIP_6"].ToString();
                            oRealTimekeepingDetail.Kip7 =  dr["KIP_7"].ToString();
                            oRealTimekeepingDetail.Kip8 =  dr["KIP_8"].ToString();
                            oRealTimekeepingDetail.Kip9 =  dr["KIP_9"].ToString();
                            oRealTimekeepingDetail.Kip10 =  dr["KIP_10"].ToString();
                            oRealTimekeepingDetail.Kip11 =  dr["KIP_11"].ToString();
                            oRealTimekeepingDetail.Kip12 =  dr["KIP_12"].ToString();
                            oRealTimekeepingDetail.Kip13 =  dr["KIP_13"].ToString();
                            oRealTimekeepingDetail.Kip14 =  dr["KIP_14"].ToString();
                            oRealTimekeepingDetail.Kip15 =  dr["KIP_15"].ToString();
                            oRealTimekeepingDetail.Kip16 =  dr["KIP_16"].ToString();
                            oRealTimekeepingDetail.Kip17 =  dr["KIP_17"].ToString();
                            oRealTimekeepingDetail.Kip18 =  dr["KIP_18"].ToString();
                            oRealTimekeepingDetail.Kip19 =  dr["KIP_19"].ToString();
                            oRealTimekeepingDetail.Kip20 =  dr["KIP_20"].ToString();
                            oRealTimekeepingDetail.Gio8Den9h =  dr["Den_9h"].ToString();
                            oRealTimekeepingDetail.Gio9Den10h =  dr["Den_10h"].ToString();
                            oRealTimekeepingDetail.Gio10Den11h =  dr["Den_11h"].ToString();
                            oRealTimekeepingDetail.Gio11Den12h =  dr["Den_12h"].ToString();
                            oRealTimekeepingDetail.Gio12Den13h =  dr["Den_13h"].ToString();
                            oRealTimekeepingDetail.Gio13Den14h =  dr["Den_14h"].ToString();
                            oRealTimekeepingDetail.Gio14Den15h =  dr["Den_15h"].ToString();
                            oRealTimekeepingDetail.Gio15Den16h =  dr["Den_16h"].ToString();
                            oRealTimekeepingDetail.Gio16Den17h =  dr["Den_17h"].ToString();
                            oRealTimekeepingDetail.Gio17Den18h =  dr["Den_18h"].ToString();
                            oRealTimekeepingDetail.Gio18Den19h =  dr["Den_19h"].ToString();
                            oRealTimekeepingDetail.Gio19Den20h =  dr["Den_20h"].ToString();
                            oRealTimekeepingDetail.Gio20Den21h =  dr["Den_21h"].ToString();
                            oRealTimekeepingDetail.Gio21Den22h =  dr["Den_22h"].ToString();
                            oRealTimekeepingDetail.Gio22Den23h =  dr["Den_23h"].ToString();
                            oRealTimekeepingDetail.Gio23Den24h =  dr["Den_24h"].ToString();
                            oRealTimekeepingDetail.Gio24Den1h =  dr["Den_1h"].ToString();
                            oRealTimekeepingDetail.Gio1Den2h =  dr["Den_2h"].ToString();
                            oRealTimekeepingDetail.Gio2Den3h =  dr["Den_3h"].ToString();
                            oRealTimekeepingDetail.Gio3Den4h =  dr["Den_4h"].ToString();
                            oRealTimekeepingDetail.Gio4Den5h =  dr["Den_5h"].ToString();
                            oRealTimekeepingDetail.Gio5Den6h =  dr["Den_6h"].ToString();
                            oRealTimekeepingDetail.Gio6Den7h =  dr["Den_7h"].ToString();
                            oRealTimekeepingDetail.Gio7Den8h =  dr["Den_8h"].ToString();
                            oRealTimekeepingDetail.TGVAO = dr["TGVAO"].ToString();
                            oRealTimekeepingDetail.TGRA = dr["TGRA"].ToString();
                            //oRealTimekeepingDetail.TGVAO = Convert.ToDateTime(dr["TGVAO"].ToString());
                            //oRealTimekeepingDetail.TGRA = Convert.ToDateTime(dr["TGRA"].ToString());
                            listRealTimekeepingDetail.Add(oRealTimekeepingDetail);
                        }
                        _returnRealTimekeeping.Code = "00";
                        _returnRealTimekeeping.Message = "Lấy dữ liệu thành công.";
                        _returnRealTimekeeping.RealListTimekeepingReport = listRealTimekeepingDetail;
                    }
                }
            }
            catch (Exception ex)
            {
                _returnRealTimekeeping.Code = "99";
                _returnRealTimekeeping.Message = "Lỗi xử lý dữ liệu";
                _returnRealTimekeeping.Total = 0;
                _returnRealTimekeeping.RealListTimekeepingReport = null;
            }
            return _returnRealTimekeeping;
        }

        #endregion
        //Phần tổng của bảng tổng hợp theo chi tiết
        #region REAL_SUM_TIMEKEEPING_DETAIL
        public ReturnRealTimekeeping REAL_SUM_TIMEKEEPING_DETAIL(string ngay, int donvi)
        {
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnRealTimekeeping _returnRealTimekeeping = new ReturnRealTimekeeping();


            List<RealSumTimekeepingDetail> listRealSumTimekeepingDetail = null;
            RealSumTimekeepingDetail oRealSumTimekeepingDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = Helper.OraDCOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "EMS_CHAMCONG.TongChiTietTT";
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
                        listRealSumTimekeepingDetail = new List<RealSumTimekeepingDetail>();
                        while (dr.Read())
                        {
                            oRealSumTimekeepingDetail = new RealSumTimekeepingDetail();       
                            oRealSumTimekeepingDetail.Kip1 = Convert.ToInt32(dr["KIP_1"].ToString());
                            oRealSumTimekeepingDetail.Kip2 = Convert.ToInt32(dr["KIP_2"].ToString());
                            oRealSumTimekeepingDetail.Kip3 = Convert.ToInt32(dr["KIP_3"].ToString());
                            oRealSumTimekeepingDetail.Kip4 = Convert.ToInt32(dr["KIP_4"].ToString());
                            oRealSumTimekeepingDetail.Kip5 = Convert.ToInt32(dr["KIP_5"].ToString());
                            oRealSumTimekeepingDetail.Kip6 = Convert.ToInt32(dr["KIP_6"].ToString());
                            oRealSumTimekeepingDetail.Kip7 = Convert.ToInt32(dr["KIP_7"].ToString());
                            oRealSumTimekeepingDetail.Kip8 = Convert.ToInt32(dr["KIP_8"].ToString());
                            oRealSumTimekeepingDetail.Kip9 = Convert.ToInt32(dr["KIP_9"].ToString());
                            oRealSumTimekeepingDetail.Kip10 = Convert.ToInt32(dr["KIP_10"].ToString());
                            oRealSumTimekeepingDetail.Kip11 = Convert.ToInt32(dr["KIP_11"].ToString());
                            oRealSumTimekeepingDetail.Kip12 = Convert.ToInt32(dr["KIP_12"].ToString());
                            oRealSumTimekeepingDetail.Kip13 = Convert.ToInt32(dr["KIP_13"].ToString());
                            oRealSumTimekeepingDetail.Kip14 = Convert.ToInt32(dr["KIP_14"].ToString());
                            oRealSumTimekeepingDetail.Kip15 = Convert.ToInt32(dr["KIP_15"].ToString());
                            oRealSumTimekeepingDetail.Kip16 = Convert.ToInt32(dr["KIP_16"].ToString());
                            oRealSumTimekeepingDetail.Kip17 = Convert.ToInt32(dr["KIP_17"].ToString());
                            oRealSumTimekeepingDetail.Kip18 = Convert.ToInt32(dr["KIP_18"].ToString());
                            oRealSumTimekeepingDetail.Kip19 = Convert.ToInt32(dr["KIP_19"].ToString());
                            oRealSumTimekeepingDetail.Kip20 = Convert.ToInt32(dr["KIP_20"].ToString());
                            oRealSumTimekeepingDetail.Den9h = Convert.ToInt32(dr["Den_9h"].ToString());
                            oRealSumTimekeepingDetail.Den10h = Convert.ToInt32(dr["Den_10h"].ToString());
                            oRealSumTimekeepingDetail.Den11h = Convert.ToInt32(dr["Den_11h"].ToString());
                            oRealSumTimekeepingDetail.Den12h = Convert.ToInt32(dr["Den_12h"].ToString());
                            oRealSumTimekeepingDetail.Den13h = Convert.ToInt32(dr["Den_13h"].ToString());
                            oRealSumTimekeepingDetail.Den14h = Convert.ToInt32(dr["Den_14h"].ToString());
                            oRealSumTimekeepingDetail.Den15h = Convert.ToInt32(dr["Den_15h"].ToString());
                            oRealSumTimekeepingDetail.Den16h = Convert.ToInt32(dr["Den_16h"].ToString());
                            oRealSumTimekeepingDetail.Den17h = Convert.ToInt32(dr["Den_17h"].ToString());
                            oRealSumTimekeepingDetail.Den18h = Convert.ToInt32(dr["Den_18h"].ToString());
                            oRealSumTimekeepingDetail.Den19h = Convert.ToInt32(dr["Den_19h"].ToString());
                            oRealSumTimekeepingDetail.Den20h = Convert.ToInt32(dr["Den_20h"].ToString());
                            oRealSumTimekeepingDetail.Den21h = Convert.ToInt32(dr["Den_21h"].ToString());
                            oRealSumTimekeepingDetail.Den22h = Convert.ToInt32(dr["Den_22h"].ToString());
                            oRealSumTimekeepingDetail.Den23h = Convert.ToInt32(dr["Den_23h"].ToString());
                            oRealSumTimekeepingDetail.Den24h = Convert.ToInt32(dr["Den_24h"].ToString());
                            oRealSumTimekeepingDetail.Den1h = Convert.ToInt32(dr["Den_1h"].ToString());
                            oRealSumTimekeepingDetail.Den2h = Convert.ToInt32(dr["Den_2h"].ToString());
                            oRealSumTimekeepingDetail.Den3h = Convert.ToInt32(dr["Den_3h"].ToString());
                            oRealSumTimekeepingDetail.Den4h = Convert.ToInt32(dr["Den_4h"].ToString());
                            oRealSumTimekeepingDetail.Den5h = Convert.ToInt32(dr["Den_5h"].ToString());
                            oRealSumTimekeepingDetail.Den6h = Convert.ToInt32(dr["Den_6h"].ToString());
                            oRealSumTimekeepingDetail.Den7h = Convert.ToInt32(dr["Den_7h"].ToString());
                            oRealSumTimekeepingDetail.Den8h = Convert.ToInt32(dr["Den_8h"].ToString());
                            listRealSumTimekeepingDetail.Add(oRealSumTimekeepingDetail);
                        }
                        _returnRealTimekeeping.Code = "00";
                        _returnRealTimekeeping.Message = "Lấy dữ liệu thành công.";
                        _returnRealTimekeeping.RealListSumTimekeepingReport = listRealSumTimekeepingDetail;
                    }
                }
            }
            catch (Exception ex)
            {
                _returnRealTimekeeping.Code = "99";
                _returnRealTimekeeping.Message = "Lỗi xử lý dữ liệu";
                _returnRealTimekeeping.Total = 0;
                _returnRealTimekeeping.RealListSumTimekeepingReport = null;
            }
            return _returnRealTimekeeping;
        }

        #endregion

    }

}

