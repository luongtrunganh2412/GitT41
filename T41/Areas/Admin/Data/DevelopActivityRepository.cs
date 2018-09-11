using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using T41.Areas.Admin.Common;
using System.Data;
using T41.Areas.Admin.Model.DataModel;

namespace T41.Areas.Admin.Data
{
    public class DevelopActivityRepository
    {

        // Phần lấy dữ liệu từ bảng KPI_SummingPassByMailRoute
        #region BDHN_DI_HCM          
        public ReturnBDHN_DI_HCM BDHN_DI_HCM(int workcenter, string AcceptDate, int arriveprovince, int arrivepartner, ref long ARRIVEQUANTITY_LK, ref decimal ARRIVEWEIGHT_KG_LK, ref long LEAVEQUANTITY_LK, ref decimal LEAVEWEIGHT_KG_LK, ref decimal DAPUNGKL, ref decimal DAPUNGLUYKE)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnBDHN_DI_HCM _ReturnBDHN_DI_HCM = new ReturnBDHN_DI_HCM();
            List<BDHN_DI_HCM> listBDHN_DI_HCM_Detail = null;
            BDHN_DI_HCM oBDHN_DI_HCM_Detail = null; 
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = Helper.OraDCComOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "DEVELOP_ACTIVITY.BDHN_DI_HCM";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new OracleParameter("p_workcenter", OracleDbType.Int32)).Value = workcenter;
                    cmd.Parameters.Add(new OracleParameter("p_AcceptDate", OracleDbType.Int32)).Value = common.DateToInt(AcceptDate);
                    cmd.Parameters.Add(new OracleParameter("p_arriveprovince", OracleDbType.Int32)).Value = arriveprovince;
                    cmd.Parameters.Add(new OracleParameter("p_arrivepartner", OracleDbType.Int32)).Value = arrivepartner;
                    cmd.Parameters.Add("p_ListStage", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    OracleDataReader dr = Helper.ExecuteDataReader(cmd, Helper.OraDCComOracleConnection);
                    if (dr.HasRows)
                    {
                        listBDHN_DI_HCM_Detail = new List<BDHN_DI_HCM>();
                        while (dr.Read())
                        {
                            oBDHN_DI_HCM_Detail = new BDHN_DI_HCM();
                            oBDHN_DI_HCM_Detail.TGDEN = dr["TGDEN"].ToString();
                            oBDHN_DI_HCM_Detail.ARRIVEIDVNPOST = dr["ARRIVEIDVNPOST"].ToString();
                            oBDHN_DI_HCM_Detail.ARRIVEMAILROUTENAME = dr["ARRIVEMAILROUTENAME"].ToString();
                            oBDHN_DI_HCM_Detail.ARRIVEFROMPOSCODE = dr["ARRIVEFROMPOSCODE"].ToString();
                            oBDHN_DI_HCM_Detail.ARRIVEFROMPOSNAME = dr["ARRIVEFROMPOSNAME"].ToString();
                            oBDHN_DI_HCM_Detail.ARRIVEQUANTITY = dr["ARRIVEQUANTITY"].ToString();
                            oBDHN_DI_HCM_Detail.ARRIVEWEIGHT_KG = dr["ARRIVEWEIGHT_KG"].ToString();

                            //Phần Cộng SL Đến Lũy Kế Chiều Đến
                            ARRIVEQUANTITY_LK += Convert.ToInt32(oBDHN_DI_HCM_Detail.ARRIVEQUANTITY);
                            oBDHN_DI_HCM_Detail.ARRIVEQUANTITY_LK = ARRIVEQUANTITY_LK;


                            //Phần Cộng KL Đến Lũy Kế Chiều Đến
                            ARRIVEWEIGHT_KG_LK += Convert.ToDecimal(oBDHN_DI_HCM_Detail.ARRIVEWEIGHT_KG);
                            oBDHN_DI_HCM_Detail.ARRIVEWEIGHT_KG_LK = ARRIVEWEIGHT_KG_LK;

                            
                            oBDHN_DI_HCM_Detail.TGDI = dr["TGDI"].ToString();
                            oBDHN_DI_HCM_Detail.LEAVEIDVNPOST = dr["LEAVEIDVNPOST"].ToString();
                            oBDHN_DI_HCM_Detail.LEAVEMAILROUTENAME = dr["LEAVEMAILROUTENAME"].ToString();
                            oBDHN_DI_HCM_Detail.LEAVETOPOSCODE = dr["LEAVETOPOSCODE"].ToString();
                            oBDHN_DI_HCM_Detail.LEAVETOPOSNAME = dr["LEAVETOPOSNAME"].ToString();
                            oBDHN_DI_HCM_Detail.LEAVEQUANTITY = dr["LEAVEQUANTITY"].ToString();
                            oBDHN_DI_HCM_Detail.LEAVEWEIGHT_KG = dr["LEAVEWEIGHT_KG"].ToString();

                            //Phần Cộng SL Đến Lũy Kế Chiều Đi
                            LEAVEQUANTITY_LK += Convert.ToInt32(oBDHN_DI_HCM_Detail.LEAVEQUANTITY);
                            oBDHN_DI_HCM_Detail.LEAVEQUANTITY_LK = LEAVEQUANTITY_LK;

                            //Phần Cộng KL Đến Lũy Kế Chiều Đi
                            LEAVEWEIGHT_KG_LK += Convert.ToDecimal(oBDHN_DI_HCM_Detail.LEAVEWEIGHT_KG);
                            oBDHN_DI_HCM_Detail.LEAVEWEIGHT_KG_LK = LEAVEWEIGHT_KG_LK;

                            //Phần Tính SL Tồn Lũy Kế Chiều Đến
                            oBDHN_DI_HCM_Detail.ARRIVEQUANTITY_TON_LK = ARRIVEQUANTITY_LK - LEAVEQUANTITY_LK;
                            //Phần Tính KL Tồn Lũy Kế Chiều Đến
                            oBDHN_DI_HCM_Detail.ARRIVEWEIGHT_KG_TON_LK = ARRIVEWEIGHT_KG_LK - LEAVEWEIGHT_KG_LK;

                            //Phần Tính Tỷ Lệ Đáp Ứng Theo Chuyến (SL)
                            oBDHN_DI_HCM_Detail.DAPUNGSL = (Convert.ToInt32(oBDHN_DI_HCM_Detail.LEAVEQUANTITY) > 0) ? ( (Convert.ToDecimal(oBDHN_DI_HCM_Detail.LEAVEQUANTITY) / Convert.ToDecimal(ARRIVEQUANTITY_LK)) *100).ToString().Substring(0, 5) + "%" : "";


                            //Phần Tính Tỷ Lệ Đáp Ứng Theo Chuyến (KL)
                            if (Convert.ToDecimal(oBDHN_DI_HCM_Detail.LEAVEWEIGHT_KG) > 0)
                            {
                                DAPUNGKL = (Convert.ToDecimal(oBDHN_DI_HCM_Detail.LEAVEWEIGHT_KG) / oBDHN_DI_HCM_Detail.ARRIVEWEIGHT_KG_LK) * 100;
                                oBDHN_DI_HCM_Detail.DAPUNGKL = (DAPUNGKL.ToString()).Substring(0, 5) + "%";
                            }
                            else {
                                oBDHN_DI_HCM_Detail.DAPUNGKL = "";
                            }

                            //Phần Tính Thời Gian Chiều Đi và Đến
                            if (oBDHN_DI_HCM_Detail.TGDEN != "")
                            {
                                oBDHN_DI_HCM_Detail.CHECK_TG = dr["TGDEN"].ToString();
                            }
                            else {
                                oBDHN_DI_HCM_Detail.CHECK_TG = dr["TGDI"].ToString();
                            }

                            //Phần Tính Tỷ Lệ Đáp Ứng Lũy Kế Theo KLG
                            //Kiểm tra xem LEAVETOPOSCOD có hay không ?
                            if (oBDHN_DI_HCM_Detail.LEAVETOPOSCODE == "")
                            {
                                oBDHN_DI_HCM_Detail.DAPUNGLUYKE = "";
                            }
                            else {

                                if (oBDHN_DI_HCM_Detail.LEAVEWEIGHT_KG_LK > 0) {
                                    DAPUNGLUYKE = (Convert.ToDecimal(oBDHN_DI_HCM_Detail.LEAVEWEIGHT_KG_LK) / Convert.ToDecimal(oBDHN_DI_HCM_Detail.ARRIVEWEIGHT_KG_LK)) * 100;
                                    oBDHN_DI_HCM_Detail.DAPUNGLUYKE = (DAPUNGLUYKE.ToString()).Substring(0, 5) + "%"; ;
                                }

                            }

                            listBDHN_DI_HCM_Detail.Add(oBDHN_DI_HCM_Detail);
                            

                        }
                        _ReturnBDHN_DI_HCM.Code = "00";
                        _ReturnBDHN_DI_HCM.Message = "Lấy dữ liệu thành công.";
                        _ReturnBDHN_DI_HCM.ListBDHN_DI_HCMReport = listBDHN_DI_HCM_Detail;
                    }
                    else
                    {
                        _ReturnBDHN_DI_HCM.Code = "01";
                        _ReturnBDHN_DI_HCM.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _ReturnBDHN_DI_HCM.Code = "99";
                _ReturnBDHN_DI_HCM.Message = "Lỗi xử lý dữ liệu";
                //_returnQuality.Total = 0;
                //_returnQuality.ListBDHN_DI_HCMReport = null;
            }
            return _ReturnBDHN_DI_HCM;
        }



        #endregion


        

        
    }



}

