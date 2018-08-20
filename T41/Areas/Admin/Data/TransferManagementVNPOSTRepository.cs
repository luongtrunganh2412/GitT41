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
    public class TransferManagementVNPOSTRepository
    {
        #region GETPROVINCE
        //Lấy mã bưu cục phát dưới DB Procedure transfer_management_ems.GetProvince_Ems
        public string GETPROVINCE()
        {
            string LISTPROVINCE = "<option value=\"0\">Tất Cả</option>";

            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDCOracleConnection;
                    cm.CommandText = Helper.SchemaName + "transfer_management_ems.GetProvince_Ems";
                    cm.CommandType = CommandType.StoredProcedure;

                    cm.Parameters.Add("v_liststage", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            LISTPROVINCE += "<option value='" + dr["PROVINCECODE"].ToString() + "'>" + dr["PROVINCECODE"].ToString() + '-' + dr["PROVINCENAME"].ToString() + "</option>";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "GETPROVINCE" + ex.Message);
                
            }

            return LISTPROVINCE;
        }
        #endregion

        #region GetAllPOSCODE
        //Lấy mã bưu cục phát dưới DB Procedure GetPosCode_Vnpost 
        public string GetCRPOSCODE(int province)
        {
            string LISTPOSCODE = "<option value=\"0\">Tất Cả</option>";
            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDCOracleConnection;
                    cm.CommandText = Helper.SchemaName + "transfer_management_ems.GetPosCode_Vnpost";
                    cm.CommandType = CommandType.StoredProcedure;

                    cm.Parameters.Add(new OracleParameter("v_Province", OracleDbType.Int32)).Value = province;
                    cm.Parameters.Add("v_liststage", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            LISTPOSCODE += "<option value='" + dr["POSCODE"].ToString() + "'>" + dr["POSCODE"].ToString() + '-' + dr["POSNAME"].ToString() + "</option>";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "GETCRPOSCODE" + ex.Message);
                
            }

            return LISTPOSCODE;
        }
        #endregion

        //Phần chi tiết của bảng tổng hợp 
        #region TRANSFER_MANAGEMENT_VNPOST_DETAIL          
        public ReturnTransferManagement_VNPOST TRANSFER_MANAGEMENT_VNPOST_DETAIL(int fromprovince, int toprovince, int fromposcode, int toposcode, string fromdate, string todate, int typecomunication)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnTransferManagement_VNPOST _ReturnTransferManagement_VNPOST = new ReturnTransferManagement_VNPOST();
            List<TransferManagementDetail_VNPOST> listTransferManagementDetail_VNPOST = null;
            TransferManagementDetail_VNPOST oTransferManagementDetail_VNPOST = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {

                    OracleCommand myCommand = new OracleCommand("transfer_management_ems.Detail_Mailtrip_VNPOST", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_typecomunication", OracleDbType.Int32).Value = typecomunication;
                    myCommand.Parameters.Add("v_FromProvince", OracleDbType.Int32).Value = fromprovince;
                    myCommand.Parameters.Add("v_ToProvince", OracleDbType.Int32).Value = toprovince;
                    myCommand.Parameters.Add("v_FromPosCode", OracleDbType.Int32).Value = fromposcode;
                    myCommand.Parameters.Add("v_ToPosCode", OracleDbType.Int32).Value = toposcode;
                    myCommand.Parameters.Add("v_FromDate", OracleDbType.Int32).Value = common.DateToInt(fromdate);
                    myCommand.Parameters.Add("v_ToDate", OracleDbType.Int32).Value = common.DateToInt(todate);
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);
                    DataTableReader dr = da.CreateDataReader();

                    

                    if (dr.HasRows)
                    {
                        listTransferManagementDetail_VNPOST = new List<TransferManagementDetail_VNPOST>();
                        while (dr.Read())
                        {
                            oTransferManagementDetail_VNPOST = new TransferManagementDetail_VNPOST();
                            oTransferManagementDetail_VNPOST.FromPosCode = Convert.ToInt32(dr["FROMPOSCODE"].ToString());
                            oTransferManagementDetail_VNPOST.FromPosName = dr["FROMPOSNAME"].ToString();
                            oTransferManagementDetail_VNPOST.ToPosCode = Convert.ToInt32(dr["TOPOSCODE"].ToString());
                            oTransferManagementDetail_VNPOST.ToPosName = dr["TOPOSNAME"].ToString();
                            oTransferManagementDetail_VNPOST.PickUpDate = dr["PICKUPDATE"].ToString();
                            oTransferManagementDetail_VNPOST.TotalMailTrip = Convert.ToInt32(dr["TOTALMAILTRIP"].ToString());
                            oTransferManagementDetail_VNPOST.TotalPostBag = Convert.ToInt32(dr["TOTALPOSTBAG"].ToString());
                            oTransferManagementDetail_VNPOST.TotalItem = Convert.ToInt32(dr["TOTALITEM"].ToString());
                            listTransferManagementDetail_VNPOST.Add(oTransferManagementDetail_VNPOST);

                        }
                        _ReturnTransferManagement_VNPOST.Code = "00";
                        _ReturnTransferManagement_VNPOST.Message = "Lấy dữ liệu thành công.";
                        _ReturnTransferManagement_VNPOST.ListTransferManagement_VNPOSTReport = listTransferManagementDetail_VNPOST;
                    }
                    else
                    {
                        _ReturnTransferManagement_VNPOST.Code = "01";
                        _ReturnTransferManagement_VNPOST.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _ReturnTransferManagement_VNPOST.Code = "99";
                _ReturnTransferManagement_VNPOST.Message = "Lỗi xử lý dữ liệu";
                //_returnQuality.Total = 0;
                //_returnQuality.ListQualityDeliveryReport = null;
            }
            return _ReturnTransferManagement_VNPOST;
        }



        #endregion

        //Phần chi tiết của bảng chi tiết theo số chuyến thư hoặc số túi 
        #region TRANSFER_MANAGEMENT_CT_TS_VNPOST_DETAIL          
        public ReturnTransferManagement_VNPOST TRANSFER_MANAGEMENT_CT_TS_VNPOST_DETAIL(int fromposcode, int toposcode, int date, int type, int typecomunication)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnTransferManagement_VNPOST _ReturnTransferManagement_VNPOST= new ReturnTransferManagement_VNPOST();
            
            
            List<TransferManagement_CTTS_Detail_VNPOST> listTransferManagement_CTTS_Detail_VNPOST = null;
            TransferManagement_CTTS_Detail_VNPOST oTransferManagement_CTTS_Detail_VNPOST = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = Helper.OraDCOracleConnection;

                    //string ngay = date;
                    cmd.CommandText = Helper.SchemaName + "transfer_management_ems.Detail_PostBag_vnpost";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new OracleParameter("v_typecomunication", OracleDbType.Int32)).Value = typecomunication;
                    cmd.Parameters.Add(new OracleParameter("v_FromPosCode", OracleDbType.Int32)).Value = fromposcode;
                    cmd.Parameters.Add(new OracleParameter("v_ToPosCode", OracleDbType.Int32)).Value = toposcode;
                    cmd.Parameters.Add(new OracleParameter("v_Date", OracleDbType.Int32)).Value = date;
                    cmd.Parameters.Add(new OracleParameter("v_type", OracleDbType.Int32)).Value = type;
                    cmd.Parameters.Add("v_ListStage", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    OracleDataReader dr = Helper.ExecuteDataReader(cmd, Helper.OraDCOracleConnection);
                    
                    if (dr.HasRows)
                    {
                        listTransferManagement_CTTS_Detail_VNPOST = new List<TransferManagement_CTTS_Detail_VNPOST>();
                        while (dr.Read())
                        {
                            oTransferManagement_CTTS_Detail_VNPOST = new TransferManagement_CTTS_Detail_VNPOST();
                            oTransferManagement_CTTS_Detail_VNPOST.FromPosCode = Convert.ToInt32(dr["FROMPOSCODE"].ToString());
                            oTransferManagement_CTTS_Detail_VNPOST.FromPosName = dr["FROMPOSNAME"].ToString();
                            oTransferManagement_CTTS_Detail_VNPOST.ToPosCode = Convert.ToInt32(dr["TOPOSCODE"].ToString());
                            oTransferManagement_CTTS_Detail_VNPOST.ToPosName = dr["TOPOSNAME"].ToString();
                            oTransferManagement_CTTS_Detail_VNPOST.PickUpDate = dr["PICKUPDATE"].ToString();
                            oTransferManagement_CTTS_Detail_VNPOST.TotalMailTrip = Convert.ToInt32(dr["CHTHU"].ToString());
                            oTransferManagement_CTTS_Detail_VNPOST.TotalPostBag = Convert.ToInt32(dr["TUISO"].ToString());
                            oTransferManagement_CTTS_Detail_VNPOST.TotalItem = Convert.ToInt32(dr["COUNT(MAE1)"].ToString());
                            listTransferManagement_CTTS_Detail_VNPOST.Add(oTransferManagement_CTTS_Detail_VNPOST);
                        }
                        _ReturnTransferManagement_VNPOST.Code = "00";
                        _ReturnTransferManagement_VNPOST.Message = "Lấy dữ liệu thành công.";
                        _ReturnTransferManagement_VNPOST.ListTransferManagement_CTTS_VNPOST_Report = listTransferManagement_CTTS_Detail_VNPOST;
                    }
                    else
                    {
                        _ReturnTransferManagement_VNPOST.Code = "01";
                        _ReturnTransferManagement_VNPOST.Message = "Không có dữ liệu";
                        _ReturnTransferManagement_VNPOST.Total = 0;
                        _ReturnTransferManagement_VNPOST.ListTransferManagement_CTTS_VNPOST_Report = null;
                    }
                    
                }
            }
            catch (Exception ex)
            {
                _ReturnTransferManagement_VNPOST.Code = "99";
                _ReturnTransferManagement_VNPOST.Message = "Lỗi xử lý dữ liệu";
                _ReturnTransferManagement_VNPOST.Total = 0;
                _ReturnTransferManagement_VNPOST.ListTransferManagement_CTTS_VNPOST_Report = null;
            }
            return _ReturnTransferManagement_VNPOST;
        }



        #endregion

        //Phần chi tiết của số túi theo số chuyến thư 
        #region TRANSFER_MANAGEMENT_SOTUI_VNPOST_DETAIL          
        public ReturnTransferManagement_VNPOST TRANSFER_MANAGEMENT_SOTUI_VNPOST_DETAIL(int fromposcode, int toposcode, int date, int mailtrip, int typecomunication)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnTransferManagement_VNPOST _ReturnTransferManagement_VNPOST = new ReturnTransferManagement_VNPOST();


            List<TransferManagement_SOTUI_Detail_VNPOST> listTransferManagement_SOTUI_Detail_VNPOST = null;
            TransferManagement_SOTUI_Detail_VNPOST oTransferManagement_SOTUI_Detail_VNPOST = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = Helper.OraDCOracleConnection;

                    //string ngay = date;
                    cmd.CommandText = Helper.SchemaName + "transfer_management_ems.Detail_PostBag_Mailtrip_vnpost";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new OracleParameter("v_typecomunication", OracleDbType.Int32)).Value = typecomunication;
                    cmd.Parameters.Add(new OracleParameter("v_FromPosCode", OracleDbType.Int32)).Value = fromposcode;
                    cmd.Parameters.Add(new OracleParameter("v_ToPosCode", OracleDbType.Int32)).Value = toposcode;
                    cmd.Parameters.Add(new OracleParameter("v_Date", OracleDbType.Int32)).Value = date;
                    cmd.Parameters.Add(new OracleParameter("v_mailtrip", OracleDbType.Int32)).Value = mailtrip;
                    cmd.Parameters.Add("v_ListStage", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    OracleDataReader dr = Helper.ExecuteDataReader(cmd, Helper.OraDCOracleConnection);

                    if (dr.HasRows)
                    {
                        listTransferManagement_SOTUI_Detail_VNPOST = new List<TransferManagement_SOTUI_Detail_VNPOST>();
                        while (dr.Read())
                        {
                            oTransferManagement_SOTUI_Detail_VNPOST = new TransferManagement_SOTUI_Detail_VNPOST();
                            oTransferManagement_SOTUI_Detail_VNPOST.FromPosCode = Convert.ToInt32(dr["FROMPOSCODE"].ToString());
                            oTransferManagement_SOTUI_Detail_VNPOST.FromPosName = dr["FROMPOSNAME"].ToString();
                            oTransferManagement_SOTUI_Detail_VNPOST.ToPosCode = Convert.ToInt32(dr["TOPOSCODE"].ToString());
                            oTransferManagement_SOTUI_Detail_VNPOST.ToPosName = dr["TOPOSNAME"].ToString();
                            oTransferManagement_SOTUI_Detail_VNPOST.PickUpDate = dr["PICKUPDATE"].ToString();
                            oTransferManagement_SOTUI_Detail_VNPOST.ChThu = Convert.ToInt32(dr["CHTHU"].ToString());
                            oTransferManagement_SOTUI_Detail_VNPOST.TuiSo = Convert.ToInt32(dr["TUISO"].ToString());
                            oTransferManagement_SOTUI_Detail_VNPOST.CountMaE1 = Convert.ToInt32(dr["COUNT(MAE1)"].ToString());
                            listTransferManagement_SOTUI_Detail_VNPOST.Add(oTransferManagement_SOTUI_Detail_VNPOST);
                        }
                        _ReturnTransferManagement_VNPOST.Code = "00";
                        _ReturnTransferManagement_VNPOST.Message = "Lấy dữ liệu thành công.";
                        _ReturnTransferManagement_VNPOST.ListTransferManagement_SOTUI_VNPOST_Report = listTransferManagement_SOTUI_Detail_VNPOST;
                    }
                    else
                    {
                        _ReturnTransferManagement_VNPOST.Code = "01";
                        _ReturnTransferManagement_VNPOST.Message = "Không có dữ liệu";
                        _ReturnTransferManagement_VNPOST.Total = 0;
                        _ReturnTransferManagement_VNPOST.ListTransferManagement_SOTUI_VNPOST_Report = null;
                    }

                }
            }
            catch (Exception ex)
            {
                _ReturnTransferManagement_VNPOST.Code = "99";
                _ReturnTransferManagement_VNPOST.Message = "Lỗi xử lý dữ liệu";
                _ReturnTransferManagement_VNPOST.Total = 0;
                _ReturnTransferManagement_VNPOST.ListTransferManagement_SOTUI_VNPOST_Report = null;
            }
            return _ReturnTransferManagement_VNPOST;
        }



        #endregion

        //Phần chi tiết của bảng in bản kê E2 lấy theo chuyến thư túi số
        #region TRANSFER_MANAGEMENT_E2_VNPOST_DETAIL          
        public ReturnTransferManagement_VNPOST TRANSFER_MANAGEMENT_E2_VNPOST_DETAIL(int fromposcode, int toposcode, int date, int mailtrip, int postbag, int typecomunication)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnTransferManagement_VNPOST _ReturnTransferManagement_VNPOST = new ReturnTransferManagement_VNPOST();
            
            List<TransferManagement_E2_Detail_VNPOST> listTransferManagement_E2_Detail_VNPOST = null;
            TransferManagement_E2_Detail_VNPOST oTransferManagement_E2_Detail_VNPOST = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    
                    OracleCommand myCommand = new OracleCommand("transfer_management_ems.Detail_Item_VNpost", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_typecomunication", OracleDbType.Int32).Value = typecomunication;
                    myCommand.Parameters.Add("v_FromPosCode", OracleDbType.Int32).Value = fromposcode;
                    myCommand.Parameters.Add("v_ToPosCode", OracleDbType.Int32).Value = toposcode;
                    myCommand.Parameters.Add("v_Date", OracleDbType.Int32).Value = date;
                    myCommand.Parameters.Add("v_Mailtrip", OracleDbType.Int32).Value = mailtrip;
                    myCommand.Parameters.Add("v_Postbag", OracleDbType.Int32).Value = postbag;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listTransferManagement_E2_Detail_VNPOST = new List<TransferManagement_E2_Detail_VNPOST>();
                        while (dr.Read())
                        {
                            oTransferManagement_E2_Detail_VNPOST = new TransferManagement_E2_Detail_VNPOST();
                            oTransferManagement_E2_Detail_VNPOST.MaE1 = dr["MAE1"].ToString();
                            oTransferManagement_E2_Detail_VNPOST.MaBCTra = Convert.ToInt32(dr["MABCTRA"].ToString());
                            oTransferManagement_E2_Detail_VNPOST.KhoiLuong = dr["KHOILUONG"].ToString();
                            oTransferManagement_E2_Detail_VNPOST.CuocCS = dr["CUOCCS"].ToString();
                            oTransferManagement_E2_Detail_VNPOST.Cuoc_DV = dr["CUOC_DICH_VU"].ToString();
                            oTransferManagement_E2_Detail_VNPOST.Tong_Cuoc = dr["TONG_CUOC"].ToString();
                            oTransferManagement_E2_Detail_VNPOST.TrangThai = dr["TRANGTHAI"].ToString();
                            listTransferManagement_E2_Detail_VNPOST.Add(oTransferManagement_E2_Detail_VNPOST);

                        }
                        _ReturnTransferManagement_VNPOST.Code = "00";
                        _ReturnTransferManagement_VNPOST.Message = "Lấy dữ liệu thành công.";
                        _ReturnTransferManagement_VNPOST.ListTransferManagement_E2_VNPOST_Report = listTransferManagement_E2_Detail_VNPOST;
                    }
                    else
                    {
                        _ReturnTransferManagement_VNPOST.Code = "01";
                        _ReturnTransferManagement_VNPOST.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _ReturnTransferManagement_VNPOST.Code = "99";
                _ReturnTransferManagement_VNPOST.Message = "Lỗi xử lý dữ liệu";
                //_returnQuality.Total = 0;
                //_returnQuality.ListQualityDeliveryReport = null;
            }
            return _ReturnTransferManagement_VNPOST;
        }



        #endregion

        //Phần chi tiết của chuyến thư túi số phần in bản kê E2 lấy theo chuyến thư túi số
        #region TRANSFER_MANAGEMENT_CTTS_E2_VNPOST_DETAIL          
        public ReturnTransferManagement_VNPOST TRANSFER_MANAGEMENT_CTTS_E2_VNPOST_DETAIL(int fromposcode, int toposcode, int date, int mailtrip, int postbag, int typecomunication)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnTransferManagement_VNPOST _ReturnTransferManagement_VNPOST = new ReturnTransferManagement_VNPOST();

            List<TransferManagement_CTTS_E2_Detail_VNPOST> listTransferManagement_CTTS_E2_Detail_VNPOST = null;
            TransferManagement_CTTS_E2_Detail_VNPOST oTransferManagement_CTTS_E2_Detail_VNPOST = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {

                    OracleCommand myCommand = new OracleCommand("transfer_management_ems.Detail_Info_vnpost", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_typecomunication", OracleDbType.Int32).Value = typecomunication;
                    myCommand.Parameters.Add("v_FromPosCode", OracleDbType.Int32).Value = fromposcode;
                    myCommand.Parameters.Add("v_ToPosCode", OracleDbType.Int32).Value = toposcode;
                    myCommand.Parameters.Add("v_Date", OracleDbType.Int32).Value = date;
                    myCommand.Parameters.Add("v_Mailtrip", OracleDbType.Int32).Value = mailtrip;
                    myCommand.Parameters.Add("v_Postbag", OracleDbType.Int32).Value = postbag;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listTransferManagement_CTTS_E2_Detail_VNPOST = new List<TransferManagement_CTTS_E2_Detail_VNPOST>();
                        while (dr.Read())
                        {
                            oTransferManagement_CTTS_E2_Detail_VNPOST = new TransferManagement_CTTS_E2_Detail_VNPOST();
                            oTransferManagement_CTTS_E2_Detail_VNPOST.FromPos = dr["FROMPOS"].ToString();
                            oTransferManagement_CTTS_E2_Detail_VNPOST.ToPos = dr["TOPOS"].ToString();
                            oTransferManagement_CTTS_E2_Detail_VNPOST.MailTrip = Convert.ToInt32(dr["MAILTRIP"].ToString());
                            oTransferManagement_CTTS_E2_Detail_VNPOST.PostBag = Convert.ToInt32(dr["POSTBAG"].ToString());
                            oTransferManagement_CTTS_E2_Detail_VNPOST.PickUpDate = dr["PICKUPDATE"].ToString();
                            oTransferManagement_CTTS_E2_Detail_VNPOST.PickUpTime = dr["PICKUPTIME"].ToString();
                            oTransferManagement_CTTS_E2_Detail_VNPOST.TotalWeight = Convert.ToDecimal(dr["TOTALWEIGHT"].ToString());
                            listTransferManagement_CTTS_E2_Detail_VNPOST.Add(oTransferManagement_CTTS_E2_Detail_VNPOST);
                        }
                        _ReturnTransferManagement_VNPOST.Code = "00";
                        _ReturnTransferManagement_VNPOST.Message = "Lấy dữ liệu thành công.";
                        _ReturnTransferManagement_VNPOST.ListTransferManagement_CTTS_E2_VNPOST_Report = listTransferManagement_CTTS_E2_Detail_VNPOST;
                    }
                    else
                    {
                        _ReturnTransferManagement_VNPOST.Code = "01";
                        _ReturnTransferManagement_VNPOST.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _ReturnTransferManagement_VNPOST.Code = "99";
                _ReturnTransferManagement_VNPOST.Message = "Lỗi xử lý dữ liệu";
                //_returnQuality.Total = 0;
                //_returnQuality.ListQualityDeliveryReport = null;
            }
            return _ReturnTransferManagement_VNPOST;
        }



        #endregion

        //Phần chi tiết của bảng in bản kê E2 lấy theo mã cổ túi
        #region TRANSFER_MANAGEMENT_MCT_E2_VNPOST_DETAIL          
        public ReturnTransferManagement_VNPOST TRANSFER_MANAGEMENT_MCT_E2_VNPOST_DETAIL(int fromposcode, int toposcode, int date, int mailtrip, int postbag, int typecomunication)
        {
            //int parafromposcode = Convert.ToInt32(fromposcode.ToString().Substring(0, 6));
            //int paratoposcode = Convert.ToInt32(fromposcode.ToString().Substring(7, 6));
            //int paradate = Convert.ToInt32(fromposcode.ToString().Substring(13, 8));
            //int paramailtrip = Convert.ToInt32(fromposcode.ToString().Substring(21, 3));
            //int parapostbag = Convert.ToInt32(fromposcode.ToString().Substring(24, 3));
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnTransferManagement_VNPOST _ReturnTransferManagement_VNPOST = new ReturnTransferManagement_VNPOST();

            List<TransferManagement_MCT_E2_Detail_VNPOST> listTransferManagement_MCT_E2_Detail_VNPOST = null;
            TransferManagement_MCT_E2_Detail_VNPOST oTransferManagement_MCT_E2_Detail_VNPOST = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {

                    OracleCommand myCommand = new OracleCommand("transfer_management_ems.Detail_Item_VNpost", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_typecomunication", OracleDbType.Int32).Value = typecomunication;
                    myCommand.Parameters.Add("v_FromPosCode", OracleDbType.Int32).Value = fromposcode;
                    myCommand.Parameters.Add("v_ToPosCode", OracleDbType.Int32).Value = toposcode;
                    myCommand.Parameters.Add("v_Date", OracleDbType.Int32).Value = date;
                    myCommand.Parameters.Add("v_Mailtrip", OracleDbType.Int32).Value = mailtrip;
                    myCommand.Parameters.Add("v_Postbag", OracleDbType.Int32).Value = postbag;
                    //myCommand.Parameters.Add("v_FromPosCode", OracleDbType.Int32).Value = parafromposcode;
                    //myCommand.Parameters.Add("v_ToPosCode", OracleDbType.Int32).Value = paratoposcode;
                    //myCommand.Parameters.Add("v_Date", OracleDbType.Int32).Value = paradate;
                    //myCommand.Parameters.Add("v_Mailtrip", OracleDbType.Int32).Value = paramailtrip;
                    //myCommand.Parameters.Add("v_Postbag", OracleDbType.Int32).Value = parapostbag;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listTransferManagement_MCT_E2_Detail_VNPOST = new List<TransferManagement_MCT_E2_Detail_VNPOST>();
                        while (dr.Read())
                        {
                            oTransferManagement_MCT_E2_Detail_VNPOST = new TransferManagement_MCT_E2_Detail_VNPOST();
                            oTransferManagement_MCT_E2_Detail_VNPOST.MaE1 = dr["MAE1"].ToString();
                            oTransferManagement_MCT_E2_Detail_VNPOST.MaBCTra = Convert.ToInt32(dr["MABCTRA"].ToString());
                            oTransferManagement_MCT_E2_Detail_VNPOST.KhoiLuong = dr["KHOILUONG"].ToString();
                            oTransferManagement_MCT_E2_Detail_VNPOST.CuocCS = dr["CUOCCS"].ToString();
                            oTransferManagement_MCT_E2_Detail_VNPOST.Cuoc_DV = dr["CUOC_DICH_VU"].ToString();
                            oTransferManagement_MCT_E2_Detail_VNPOST.Tong_Cuoc = dr["TONG_CUOC"].ToString();
                            oTransferManagement_MCT_E2_Detail_VNPOST.TrangThai = dr["TRANGTHAI"].ToString();
                            listTransferManagement_MCT_E2_Detail_VNPOST.Add(oTransferManagement_MCT_E2_Detail_VNPOST);

                        }
                        _ReturnTransferManagement_VNPOST.Code = "00";
                        _ReturnTransferManagement_VNPOST.Message = "Lấy dữ liệu thành công.";
                        _ReturnTransferManagement_VNPOST.ListTransferManagement_MCT_E2_VNPOST_Report = listTransferManagement_MCT_E2_Detail_VNPOST;
                    }
                    else
                    {
                        _ReturnTransferManagement_VNPOST.Code = "01";
                        _ReturnTransferManagement_VNPOST.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _ReturnTransferManagement_VNPOST.Code = "99";
                _ReturnTransferManagement_VNPOST.Message = "Lỗi xử lý dữ liệu";
                //_returnQuality.Total = 0;
                //_returnQuality.ListQualityDeliveryReport = null;
            }
            return _ReturnTransferManagement_VNPOST;
        }



        #endregion

        //Phần chi tiết của chuyến thư túi số phần in bản kê E2 lấy theo mã cổ túi
        #region TRANSFER_MANAGEMENT_CTTS_MCT_E2_VNPOST_DETAIL          
        public ReturnTransferManagement_VNPOST TRANSFER_MANAGEMENT_CTTS_MCT_E2_VNPOST_DETAIL(int fromposcode, int toposcode, int date, int mailtrip, int postbag, int typecomunication)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnTransferManagement_VNPOST _ReturnTransferManagement_VNPOST = new ReturnTransferManagement_VNPOST();

            List<TransferManagement_CTTS_MCT_E2_Detail_VNPOST> listTransferManagement_CTTS_MCT_E2_Detail_VNPOST = null;
            TransferManagement_CTTS_MCT_E2_Detail_VNPOST oTransferManagement_CTTS_MCT_E2_Detail_VNPOST = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {

                    OracleCommand myCommand = new OracleCommand("transfer_management_ems.Detail_Info_vnpost", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_typecomunication", OracleDbType.Int32).Value = typecomunication;
                    myCommand.Parameters.Add("v_FromPosCode", OracleDbType.Int32).Value = fromposcode;
                    myCommand.Parameters.Add("v_ToPosCode", OracleDbType.Int32).Value = toposcode;
                    myCommand.Parameters.Add("v_Date", OracleDbType.Int32).Value = date;
                    myCommand.Parameters.Add("v_Mailtrip", OracleDbType.Int32).Value = mailtrip;
                    myCommand.Parameters.Add("v_Postbag", OracleDbType.Int32).Value = postbag;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listTransferManagement_CTTS_MCT_E2_Detail_VNPOST = new List<TransferManagement_CTTS_MCT_E2_Detail_VNPOST>();
                        while (dr.Read())
                        {
                            oTransferManagement_CTTS_MCT_E2_Detail_VNPOST = new TransferManagement_CTTS_MCT_E2_Detail_VNPOST();
                            oTransferManagement_CTTS_MCT_E2_Detail_VNPOST.FromPos = dr["FROMPOS"].ToString();
                            oTransferManagement_CTTS_MCT_E2_Detail_VNPOST.ToPos = dr["TOPOS"].ToString();
                            oTransferManagement_CTTS_MCT_E2_Detail_VNPOST.MailTrip = Convert.ToInt32(dr["MAILTRIP"].ToString());
                            oTransferManagement_CTTS_MCT_E2_Detail_VNPOST.PostBag = Convert.ToInt32(dr["POSTBAG"].ToString());
                            oTransferManagement_CTTS_MCT_E2_Detail_VNPOST.PickUpDate = dr["PICKUPDATE"].ToString();
                            oTransferManagement_CTTS_MCT_E2_Detail_VNPOST.PickUpTime = dr["PICKUPTIME"].ToString();
                            oTransferManagement_CTTS_MCT_E2_Detail_VNPOST.TotalWeight = Convert.ToDecimal(dr["TOTALWEIGHT"].ToString());
                            listTransferManagement_CTTS_MCT_E2_Detail_VNPOST.Add(oTransferManagement_CTTS_MCT_E2_Detail_VNPOST);
                        }
                        _ReturnTransferManagement_VNPOST.Code = "00";
                        _ReturnTransferManagement_VNPOST.Message = "Lấy dữ liệu thành công.";
                        _ReturnTransferManagement_VNPOST.ListTransferManagement_CTTS_MCT_E2_VNPOST_Report = listTransferManagement_CTTS_MCT_E2_Detail_VNPOST;
                    }
                    else
                    {
                        _ReturnTransferManagement_VNPOST.Code = "01";
                        _ReturnTransferManagement_VNPOST.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _ReturnTransferManagement_VNPOST.Code = "99";
                _ReturnTransferManagement_VNPOST.Message = "Lỗi xử lý dữ liệu";
                //_returnQuality.Total = 0;
                //_returnQuality.ListQualityDeliveryReport = null;
            }
            return _ReturnTransferManagement_VNPOST;
        }
        #endregion
    }

}

