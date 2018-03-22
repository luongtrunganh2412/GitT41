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

        #region GetALLDMKip
        public IEnumerable<TimeKeeping> GetAllDMKip(int don_vi)
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
                            oTimeKeeping.DONVI= int.Parse(dr["DONVI"].ToString());
                            listTimeKeeping.Add(oTimeKeeping);
                        }
                        //while (dr.Read())
                        //{
                        //    oTimeKeeping = new TimeKeeping();
                        //    oTimeKeeping.MAKIP = int.Parse(dr["MAKIP"].ToString());
                        //    oTimeKeeping.TENKIP = dr["TENKIP"].ToString();
                        //    listTimeKeeping.Add(oTimeKeeping);
                        //}
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
        #region TIMEKEEPING_DETAIL
        public ReturnTimekeeping TIMEKEEPING_DETAIL(int ngay, int donvi, int ankip)
        {
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnTimekeeping _ReturnTimekeeping = new ReturnTimekeeping();
            switch (donvi)
            {
                case 0:
                    _metadata.donvi = "Tất Cả"; break;
                case 1:
                    _metadata.donvi = "TTKTTN-HN";break;
                case 2:
                    _metadata.donvi = "TTKTTN-DN"; break;
                case 3:
                    _metadata.donvi = "TTKTTN-HCM"; break;
            }
            
            List<TimekeepingDetail> listTimekeepingDetail = null;
            TimekeepingDetail oTimekeepingDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = Helper.OraDCOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "EMS_CHAMCONG.TongHopTheoKipTK";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new OracleParameter("v_Ngay", OracleDbType.Int32)).Value = ngay;
                    cmd.Parameters.Add(new OracleParameter("v_DonVi", OracleDbType.NVarchar2)).Value = donvi;
                    cmd.Parameters.Add(new OracleParameter("v_AnKip", OracleDbType.Int32)).Value = ankip;
                    cmd.Parameters.Add("v_List", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    OracleDataReader dr = Helper.ExecuteDataReader(cmd, Helper.OraDCOracleConnection);
                    listTimekeepingDetail = new List<TimekeepingDetail>();
                    while (dr.Read())
                    {
                        oTimekeepingDetail = new TimekeepingDetail();
                        oTimekeepingDetail.Ca = dr["CA"].ToString();
                        oTimekeepingDetail.TenKip = dr["TEN_KIP"].ToString();
                        oTimekeepingDetail.ThoiGian = dr["THOI_GIAN"].ToString();
                        oTimekeepingDetail.TongGioLam = Convert.ToInt32(dr["TONG_GIO"].ToString());
                        oTimekeepingDetail.SoNguoi = Convert.ToInt32(dr["SO_NGUOI"].ToString());
                        oTimekeepingDetail.Gio8Den9h = Convert.ToInt32(dr["Den_9h"].ToString());
                        oTimekeepingDetail.Gio9Den10h = Convert.ToInt32(dr["Den_10h"].ToString());
                        oTimekeepingDetail.Gio10Den11h = Convert.ToInt32(dr["Den_11h"].ToString());
                        oTimekeepingDetail.Gio11Den12h = Convert.ToInt32(dr["Den_12h"].ToString());
                        oTimekeepingDetail.Gio12Den13h = Convert.ToInt32(dr["Den_13h"].ToString());
                        oTimekeepingDetail.Gio13Den14h = Convert.ToInt32(dr["Den_14h"].ToString());
                        oTimekeepingDetail.Gio14Den15h = Convert.ToInt32(dr["Den_15h"].ToString());
                        oTimekeepingDetail.Gio15Den16h = Convert.ToInt32(dr["Den_16h"].ToString());
                        oTimekeepingDetail.Gio16Den17h = Convert.ToInt32(dr["Den_17h"].ToString());
                        oTimekeepingDetail.Gio17Den18h = Convert.ToInt32(dr["Den_18h"].ToString());
                        oTimekeepingDetail.Gio18Den19h = Convert.ToInt32(dr["Den_19h"].ToString());
                        oTimekeepingDetail.Gio19Den20h = Convert.ToInt32(dr["Den_20h"].ToString());
                        oTimekeepingDetail.Gio20Den21h = Convert.ToInt32(dr["Den_21h"].ToString());
                        oTimekeepingDetail.Gio21Den22h = Convert.ToInt32(dr["Den_22h"].ToString());
                        oTimekeepingDetail.Gio22Den23h = Convert.ToInt32(dr["Den_23h"].ToString());
                        oTimekeepingDetail.Gio23Den24h = Convert.ToInt32(dr["Den_24h"].ToString());
                        oTimekeepingDetail.Gio24Den1h = Convert.ToInt32(dr["Den_1h"].ToString());
                        oTimekeepingDetail.Gio1Den2h = Convert.ToInt32(dr["Den_2h"].ToString());
                        oTimekeepingDetail.Gio2Den3h = Convert.ToInt32(dr["Den_3h"].ToString());
                        oTimekeepingDetail.Gio3Den4h = Convert.ToInt32(dr["Den_4h"].ToString());
                        oTimekeepingDetail.Gio4Den5h = Convert.ToInt32(dr["Den_5h"].ToString());
                        oTimekeepingDetail.Gio5Den6h = Convert.ToInt32(dr["Den_6h"].ToString());
                        oTimekeepingDetail.Gio6Den7h = Convert.ToInt32(dr["Den_7h"].ToString());
                        oTimekeepingDetail.Gio7Den8h = Convert.ToInt32(dr["Den_8h"].ToString());

                        listTimekeepingDetail.Add(oTimekeepingDetail);
                    }
                    _ReturnTimekeeping.Code = "00";
                    _ReturnTimekeeping.Message = "Lấy dữ liệu thành công.";
                    _ReturnTimekeeping.Total = Convert.ToInt32(cmd.Parameters["P_TOTAL"].Value.ToString());
                    _ReturnTimekeeping.ListTimekeepingReport = listTimekeepingDetail;
                }
            }
            catch
            {
                _ReturnTimekeeping.Code = "99";
                _ReturnTimekeeping.Message = "Lỗi xử lý dữ liệu";
                _ReturnTimekeeping.Total = 0;
                _ReturnTimekeeping.ListTimekeepingReport = null;
            }
            return _ReturnTimekeeping;
        }
        #endregion
        #region COME_TIMEKEEPING_SUMMARY 
        //public ReturnSummaryTimekeeping COME_TIMEKEEPING_SUMMARY(int ngay,int donvi,)
        //{
        //    MetaData _metadata = new MetaData();
        //    Convertion common = new Convertion();
        //    ReturnSummaryTimekeeping _ReturnSummaryTimekeeping = new ReturnSummaryTimekeeping();
        //    try
        //    {

        //    }
        //    catch
        //    {
        //        _ReturnSummaryTimekeeping.Code = "99";
        //        _ReturnSummaryTimekeeping.Message = "Lỗi xử lý dữ liệu";

        //    }
        //    return _ReturnSummaryTimekeeping;
        //}
        #endregion
    }

}