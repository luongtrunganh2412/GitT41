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
        //Phần chi tiết của bảng theo
        #region TIMEKEEPING_KIP_DETAIL          
        public ReturnTimekeeping TIMEKEEPING_KIP_DETAIL(string ngay, int donvi, int ankip)              
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
        #region TIMEKEEPING_TITLE_DETAIL

        public ReturnTimekeeping TIMEKEEPING_TITLE_DETAIL(string ngay, int donvi, int to)
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
                    cmd.CommandText = Helper.SchemaName + "EMS_CHAMCONG.TongHopTheoChucDanhTK";
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
        #region TIMEKEEPING_DETAIL

        public ReturnTimekeeping TIMEKEEPING_DETAIL(string ngay, int donvi, int to)
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
                    cmd.CommandText = Helper.SchemaName + "EMS_CHAMCONG.ChiTietPhanCongTK";
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
    }

}

