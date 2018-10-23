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

        // Phần lấy dữ liệu từ bảng KPI_SummingPassByMailRoute LIÊN TỈNH  
        #region LIÊN TỈNH            
        public ReturnBDHN_DI_HCM BDHN_DI_HCM(int workcenter, string AcceptDate, int arriveprovince, int arrivepartner, ref long ARRIVEQUANTITY_LK, ref decimal ARRIVEWEIGHT_KG_LK, ref long LEAVEQUANTITY_LK, ref decimal LEAVEWEIGHT_KG_LK, ref decimal DAPUNGKL, ref decimal DAPUNGLUYKE)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnBDHN_DI_HCM _ReturnBDHN_DI_HCM = new ReturnBDHN_DI_HCM();
            List<BDHN_DI_HCM> listBDHN_DI_HCM_Detail = null;
            BDHN_DI_HCM oBDHN_DI_HCM_Detail = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = Helper.OraDCComOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "DEVELOP_ACTIVITY.KHAI_THAC_LIEN_TINH";
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
                            oBDHN_DI_HCM_Detail.STT = a++;
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
                            //Chuyển Số liệu âm > DƯƠNG
                            oBDHN_DI_HCM_Detail.ARRIVEQUANTITY_TON_LK = System.Math.Abs(ARRIVEQUANTITY_LK - LEAVEQUANTITY_LK);

                            //Phần Tính KL Tồn Lũy Kế Chiều Đến
                            //Chuyển Số liệu âm > DƯƠNG
                            oBDHN_DI_HCM_Detail.ARRIVEWEIGHT_KG_TON_LK = System.Math.Abs(ARRIVEWEIGHT_KG_LK - LEAVEWEIGHT_KG_LK);

                            //Phần Tính Tỷ Lệ Đáp Ứng Theo Chuyến (SL)

                            //Công Thức Cũ
                            //oBDHN_DI_HCM_Detail.DAPUNGSL = (Convert.ToInt32(oBDHN_DI_HCM_Detail.LEAVEQUANTITY) > 0) ? ((Convert.ToDecimal(oBDHN_DI_HCM_Detail.LEAVEQUANTITY) / Convert.ToDecimal(ARRIVEQUANTITY_LK)) * 100).ToString().Substring(0, 3) + "%" : "";

                            //Công Thức Mới
                            oBDHN_DI_HCM_Detail.DAPUNGSL = (Convert.ToDecimal(oBDHN_DI_HCM_Detail.LEAVEQUANTITY) > 0) ? ((Convert.ToDecimal(oBDHN_DI_HCM_Detail.LEAVEQUANTITY) / (oBDHN_DI_HCM_Detail.ARRIVEQUANTITY_TON_LK + Convert.ToDecimal(oBDHN_DI_HCM_Detail.LEAVEQUANTITY))) * 100).ToString().Substring(0, 3) + "%" : "";


                            //Phần Tính Tỷ Lệ Đáp Ứng Theo Chuyến (KL)
                            if (Convert.ToDecimal(oBDHN_DI_HCM_Detail.LEAVEWEIGHT_KG) > 0)
                            {
                                //Công Thức Cũ
                                //DAPUNGKL = (Convert.ToDecimal(oBDHN_DI_HCM_Detail.LEAVEWEIGHT_KG) / oBDHN_DI_HCM_Detail.ARRIVEWEIGHT_KG_LK) * 100;
                                //oBDHN_DI_HCM_Detail.DAPUNGKL = (DAPUNGKL.ToString()).Substring(0, 3) + "%";

                                //Công Thức Mới
                                DAPUNGKL = (Convert.ToDecimal(oBDHN_DI_HCM_Detail.LEAVEWEIGHT_KG) / (oBDHN_DI_HCM_Detail.ARRIVEWEIGHT_KG_TON_LK + Convert.ToDecimal(oBDHN_DI_HCM_Detail.LEAVEWEIGHT_KG))) * 100;
                                oBDHN_DI_HCM_Detail.DAPUNGKL = (DAPUNGKL.ToString()).Substring(0, 3) + "%";
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
                                    oBDHN_DI_HCM_Detail.DAPUNGLUYKE = (DAPUNGLUYKE.ToString()).Substring(0, 3) + "%"; ;
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
                LogAPI.LogToFile(LogFileType.EXCEPTION, "DevelopActivityRepository# Region BDHN_DI_HCM" + ex.Message);
                //_returnQuality.Total = 0;
                //_returnQuality.ListBDHN_DI_HCMReport = null;
            }
            return _ReturnBDHN_DI_HCM;
        }



        #endregion

        // Phần lấy dữ liệu từ bảng KPI_SummingPassByMailRoute NỘI TỈNH
        #region NỘI TỈNH          
        public ReturnNOI_TINH NOI_TINH(int workcenter, string AcceptDate, int arriveprovince, int arrivepartner, int leaveprovince, int transitbag)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnNOI_TINH _ReturnNOI_TINH = new ReturnNOI_TINH();
            List<NOI_TINH> listNOI_TINH_Detail = null;
            NOI_TINH oNOI_TINH_Detail = null;
            int a = 1;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {

                    OracleCommand myCommand = new OracleCommand("DEVELOP_ACTIVITY.KHAI_THAC_NOI_TINH", Helper.OraDCComOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("p_workcenter", OracleDbType.Int32).Value = workcenter;
                    myCommand.Parameters.Add("p_AcceptDate", OracleDbType.Int32).Value = common.DateToInt(AcceptDate);
                    myCommand.Parameters.Add("p_arriveprovince", OracleDbType.Int32).Value = arriveprovince;
                    myCommand.Parameters.Add("p_arrivepartner", OracleDbType.Int32).Value = arrivepartner;
                    myCommand.Parameters.Add("p_leaveprovince", OracleDbType.Int32).Value = leaveprovince;
                    myCommand.Parameters.Add("p_transitbag", OracleDbType.Int32).Value = transitbag;
                    myCommand.Parameters.Add(new OracleParameter("p_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);
                    //myCommand.ExecuteNonQuery();
                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listNOI_TINH_Detail = new List<NOI_TINH>();
                        while (dr.Read())
                        {
                            oNOI_TINH_Detail = new NOI_TINH();
                            oNOI_TINH_Detail.STT = a++;
                            oNOI_TINH_Detail.TGDEN = dr["TGDEN"].ToString();
                            oNOI_TINH_Detail.ARRIVEMAILROUTE = dr["ARRIVEMAILROUTE"].ToString();
                            oNOI_TINH_Detail.ARRIVEIDVNPOSTNAME = dr["ARRIVEIDVNPOSTNAME"].ToString();
                            oNOI_TINH_Detail.ARRIVEIDVNPOST = dr["ARRIVEIDVNPOST"].ToString();
                            oNOI_TINH_Detail.ARRIVEMAILROUTENAME = dr["ARRIVEMAILROUTENAME"].ToString();
                            oNOI_TINH_Detail.ARRIVEDONVI = dr["ARRIVEDONVI"].ToString();
                            oNOI_TINH_Detail.ARRIVEFROMPOSCODE = dr["ARRIVEFROMPOSCODE"].ToString();
                            oNOI_TINH_Detail.ARRIVEFROMPOSNAME = dr["ARRIVEFROMPOSNAME"].ToString();
                            oNOI_TINH_Detail.ARRIVEQUANTITY = dr["ARRIVEQUANTITY"].ToString();
                            oNOI_TINH_Detail.ARRIVEWEIGHT_KG = dr["ARRIVEWEIGHT_KG"].ToString();
                            oNOI_TINH_Detail.ARRIVEQUANTITY_ACCUM = dr["ARRIVEQUANTITY_ACCUM"].ToString();
                            oNOI_TINH_Detail.DEN_KLG_LUYKE = dr["DEN_KLG_LUYKE"].ToString();
                            oNOI_TINH_Detail.ARRIVEQUANTITY_STOCKACCUM = dr["ARRIVEQUANTITY_STOCKACCUM"].ToString();
                            oNOI_TINH_Detail.DEN_KLG_TON_LUYKE = dr["DEN_KLG_TON_LUYKE"].ToString();
                            oNOI_TINH_Detail.TGQUETTUIDI = dr["TGQUETTUIDI"].ToString();
                            oNOI_TINH_Detail.LEAVEMAILROUTE = dr["LEAVEMAILROUTE"].ToString();
                            oNOI_TINH_Detail.LEAVEIDVNPOSTNAME = dr["LEAVEIDVNPOSTNAME"].ToString();
                            oNOI_TINH_Detail.LEAVEIDVNPOST = dr["LEAVEIDVNPOST"].ToString();
                            oNOI_TINH_Detail.LEAVEMAILROUTENAME = dr["LEAVEMAILROUTENAME"].ToString();
                            oNOI_TINH_Detail.LEAVEDONVI = dr["LEAVEDONVI"].ToString();
                            oNOI_TINH_Detail.LEAVETOPOSCODE = dr["LEAVETOPOSCODE"].ToString();
                            oNOI_TINH_Detail.LEAVETOPOSNAME = dr["LEAVETOPOSNAME"].ToString();
                            oNOI_TINH_Detail.LEAVEQUANTITY = dr["LEAVEQUANTITY"].ToString();
                            oNOI_TINH_Detail.LEAVEWEIGHT_KG = dr["LEAVEWEIGHT_KG"].ToString();
                            oNOI_TINH_Detail.LEAVEQUANTITY_ACCUM = dr["LEAVEQUANTITY_ACCUM"].ToString();
                            oNOI_TINH_Detail.DI_KLG_LUYKE = dr["DI_KLG_LUYKE"].ToString();
                            oNOI_TINH_Detail.TYLEDAPUNGCHUYEN_SL = Convert.ToDecimal(dr["TYLEDAPUNGCHUYEN_SL"].ToString()) *100;
                            oNOI_TINH_Detail.TYLEDAPUNGCHUYEN_KLG = Convert.ToDecimal(dr["TYLEDAPUNGCHUYEN_KLG"].ToString()) *100;
                            oNOI_TINH_Detail.TYLEDAPUNGLUYKE_SLG = Convert.ToDecimal(dr["TYLEDAPUNGLUYKE_SLG"].ToString()) * 100;
                            oNOI_TINH_Detail.TYLEDAPUNGLUYKE_KLG = Convert.ToDecimal(dr["TYLEDAPUNGLUYKE_KLG"].ToString()) * 100;
                            oNOI_TINH_Detail.MAXDATE = dr["MAXDATE"].ToString();

                            listNOI_TINH_Detail.Add(oNOI_TINH_Detail);


                        }
                        _ReturnNOI_TINH.Code = "00";
                        _ReturnNOI_TINH.Message = "Lấy dữ liệu thành công.";
                        _ReturnNOI_TINH.ListNOI_TINHReport = listNOI_TINH_Detail;
                    }
                    else
                    {
                        _ReturnNOI_TINH.Code = "01";
                        _ReturnNOI_TINH.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _ReturnNOI_TINH.Code = "99";
                _ReturnNOI_TINH.Message = "Lỗi xử lý dữ liệu";
                LogAPI.LogToFile(LogFileType.EXCEPTION, "DevelopActivityRepository# Region ReturnNOI_TINH" + ex.Message);
                //_ReturnNOI_TINH.Total = 0;
                //_ReturnNOI_TINH.ListBDHN_DI_HCMReport = null;
            }
            return _ReturnNOI_TINH;
        }



        #endregion

        //Phần lấy dữ liệu CHI TIẾT LIÊN TỈNH
        #region CHI TIẾT LIÊN TỈNH
        public ReturnCHI_TIET_LIEN_TINH CHI_TIET_LIEN_TINH(int workcenter, string AcceptDate, int arriveprovince, int arrivepartner)
        {
            DataTable da = new DataTable();
            Convertion common = new Convertion();
            ReturnCHI_TIET_LIEN_TINH _returnChiTietLienTinh = new ReturnCHI_TIET_LIEN_TINH();

            List<CHI_TIET_LIEN_TINH> listChiTietLienTinh = null;
            CHI_TIET_LIEN_TINH oChiTietLienTinh = null;
            String StoreProcedureName = "";
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    //Chiều HN ĐI HCM 100916
                    
                    if (workcenter == 100916)
                    {
                        //BỘ LỌC EMS-HN ĐI HCM
                        if (arriveprovince == 100000 && arrivepartner == 2)
                        {
                            StoreProcedureName = "DEVELOP_ACTIVITY.DETAIL_EMSHN_DI_HCM";
                        }

                        //BỘ LỌC BĐ-HN ĐI HCM
                        if (arriveprovince == 100000 && arrivepartner == 1)
                        {
                            StoreProcedureName = "DEVELOP_ACTIVITY.DETAIL_BDHN_DI_HCM";
                        }

                        //BỘ LỌC Miền Bắc Trừ HN ĐI HCM
                        if (arriveprovince == 0 && arrivepartner == 99)
                        {
                            StoreProcedureName = "DEVELOP_ACTIVITY.DETAIL_MIENBAC_TRU_HN_DI_HCM";
                        }
                    }


                    //Chiều HCM ĐI HN 100916
                    
                    if (workcenter == 700916)
                    {
                        //BỘ LỌC EMS-HCM ĐI HN
                        if (arriveprovince == 700000 && arrivepartner == 2)
                        {
                            StoreProcedureName = "DEVELOP_ACTIVITY.DETAIL_EMSHCM_DI_HN";
                        }

                        //BỘ LỌC BĐ-HCM ĐI HN
                        if (arriveprovince == 700000 && arrivepartner == 1)
                        {
                            StoreProcedureName = "DEVELOP_ACTIVITY.DETAIL_BDHCM_DI_HN";
                        }

                        //BỘ LỌC Miền Bắc Trừ HCM ĐI HN
                        if (arriveprovince == 0 && arrivepartner == 99)
                        {
                            StoreProcedureName = "DEVELOP_ACTIVITY.DETAIL_MIENNAM_TRU_HCM_DI_HN";
                        }
                    }

                    //OracleCommand myCommand = new OracleCommand("DEVELOP_ACTIVITY.DETAIL_BDHN_DI_HCM", Helper.OraDCComOracleConnection);
                    OracleCommand myCommand = new OracleCommand(StoreProcedureName, Helper.OraDCComOracleConnection);
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    myCommand.Parameters.Add("p_workcenter", OracleDbType.Int32).Value = workcenter;
                    myCommand.Parameters.Add("p_AcceptDate", OracleDbType.Int32).Value = common.DateToInt(AcceptDate);
                    myCommand.Parameters.Add(new OracleParameter("p_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);
                    listChiTietLienTinh = ConvertListToDataTable.DataTableToList<CHI_TIET_LIEN_TINH>(da);
                    if (listChiTietLienTinh != null && listChiTietLienTinh.Count != 0)
                    {
                        _returnChiTietLienTinh.Code = "00";
                        _returnChiTietLienTinh.Message = "Lấy dữ liệu thành công.";
                        _returnChiTietLienTinh.ListCHI_TIET_LIEN_TINHReport = listChiTietLienTinh;
                    }

                    else
                    {
                        _returnChiTietLienTinh.Code = "01";
                        _returnChiTietLienTinh.Message = "Không có dữ liệu";
                        _returnChiTietLienTinh.ListCHI_TIET_LIEN_TINHReport = null;
                    }



                    //DataTableReader dr = da.CreateDataReader();

                    //if (dr.HasRows)
                    //{
                    //    listChiTietLienTinh = new List<CHI_TIET_LIEN_TINH>();
                    //    while (dr.Read())
                    //    {
                    //        oChiTietLienTinh = new CHI_TIET_LIEN_TINH();
                    //        oChiTietLienTinh.PROVINCECODE = dr["PROVINCECODE"].ToString();
                    //        oChiTietLienTinh.PROVINCENAME = dr["PROVINCENAME"].ToString();
                    //        listChiTietLienTinh.Add(oChiTietLienTinh);

                    //    }

                    //    _returnChiTietLienTinh.Code = "00";
                    //    _returnChiTietLienTinh.Message = "Lấy dữ liệu thành công.";
                    //    _returnChiTietLienTinh.ListCHI_TIET_LIEN_TINHReport = listChiTietLienTinh;
                    //}
                    //else
                    //{
                    //    _returnChiTietLienTinh.Code = "01";
                    //    _returnChiTietLienTinh.Message = "Không có dữ liệu";

                    //}


                }
            }
            catch (Exception ex)
            {
                _returnChiTietLienTinh.Code = "99";
                _returnChiTietLienTinh.Message = "Lỗi xử lý dữ liệu";
                LogAPI.LogToFile(LogFileType.EXCEPTION, "DevelopActivityRepository# Region ReturnCHI_TIET_LIEN_TINH" + ex.Message);
                //_returnChiTietLienTinh.Total = 0;
                //_returnChiTietLienTinh.ListCHI_TIET_LIEN_TINHReport = null;
            }
            return _returnChiTietLienTinh;
        }
        #endregion
    }



}

