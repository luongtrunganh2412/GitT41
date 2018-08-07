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
    public class BusinessManagementRepository
    {
        #region GETUNIT
        //Lấy mã DỊCH VỤ DƯỚI SP BUSINESS_MANAGEMENT.List_Service
        public IEnumerable<GETUNIT> GETUNIT()
        {
            List<GETUNIT> listGETUNITCode = null;
            GETUNIT oGETUNITCode = null;

            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDCOracleConnection;
                    cm.CommandText = Helper.SchemaName + "BUSINESS_MANAGEMENT.List_Unit";
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.Add("v_liststage", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        listGETUNITCode = new List<GETUNIT>();
                        while (dr.Read())
                        {
                            oGETUNITCode = new GETUNIT();
                            oGETUNITCode.UNIT_CODE = dr["UNIT_CODE"].ToString();
                            oGETUNITCode.UNIT_NAME = dr["UNIT_NAME"].ToString();
                            listGETUNITCode.Add(oGETUNITCode);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "GETUNIT" + ex.Message);
                listGETUNITCode = null;
            }

            return listGETUNITCode;
        }
        #endregion

        #region GETSERVICE
        //Lấy mã DỊCH VỤ DƯỚI SP BUSINESS_MANAGEMENT.List_Service
        public IEnumerable<GETSERVICE> GETSERVICE()
        {
            List<GETSERVICE> listGETSERVICECode = null;
            GETSERVICE oGETSERVICECode = null;

            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDCOracleConnection;
                    cm.CommandText = Helper.SchemaName + "BUSINESS_MANAGEMENT.List_Service";
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.Add("v_liststage", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        listGETSERVICECode = new List<GETSERVICE>();
                        while (dr.Read())
                        {
                            oGETSERVICECode = new GETSERVICE();
                            oGETSERVICECode.SERVICE_CODE = dr["SERVICE_CODE"].ToString();
                            oGETSERVICECode.SERVICE_NAME = dr["SERVICE_NAME"].ToString();
                            listGETSERVICECode.Add(oGETSERVICECode);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "GETSERVICE" + ex.Message);
                listGETSERVICECode = null;
            }

            return listGETSERVICECode;
        }
        #endregion

        #region GET_BM_POSCODE
        //Lấy mã DỊCH VỤ DƯỚI SP BUSINESS_MANAGEMENT.List_POSCODE
        public IEnumerable<GET_BM_POSCODE> GET_BM_POSCODE(int id_don_vi)
        {
            List<GET_BM_POSCODE> listGET_BM_POSCODECode = null;
            GET_BM_POSCODE oGET_BM_POSCODECode = null;

            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDCOracleConnection;
                    cm.CommandText = Helper.SchemaName + "BUSINESS_MANAGEMENT.List_POSCODE";
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.Add(new OracleParameter("P_ID_DON_VI", OracleDbType.Int32)).Value = id_don_vi;
                    cm.Parameters.Add("v_liststage", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        listGET_BM_POSCODECode = new List<GET_BM_POSCODE>();
                        while (dr.Read())
                        {
                            oGET_BM_POSCODECode = new GET_BM_POSCODE();
                            oGET_BM_POSCODECode.POS_CODE = dr["POS_CODE"].ToString();
                            oGET_BM_POSCODECode.POS_NAME = dr["POS_NAME"].ToString();
                            listGET_BM_POSCODECode.Add(oGET_BM_POSCODECode);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "GET_BM_POSCODE" + ex.Message);
                listGET_BM_POSCODECode = null;
            }

            return listGET_BM_POSCODECode;
        }
        #endregion

        // Phần lấy dữ liệu từ bảng doikiem_kh_th_ngay và bảng Khach_Hang ĐÃ PHÂN TRANG theo câu lệnh cũ lấy từ bảng doikiem_kh_th_ngay
        #region BUSINESS_MANAGEMENT_Detail          
        public ReturnBusinessManagement BUSINESS_MANAGEMENT_Detail(int page_size, int page_index,int ma_don_vi, int ma_bc_khai_thac )
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnBusinessManagement _ReturnBusinessManagement = new ReturnBusinessManagement();

            List<BUSINESS_MANAGEMENT_Detail> listBusinessManagementDetail = null;
            BUSINESS_MANAGEMENT_Detail oBusinessManagementDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {

                    OracleCommand myCommand = new OracleCommand("BUSINESS_MANAGEMENT.Page_Detail_User", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("P_PAGE_INDEX", OracleDbType.Int32).Value = page_index;
                    myCommand.Parameters.Add("P_PAGE_SIZE", OracleDbType.Int32).Value = page_size;
                    myCommand.Parameters.Add("p_ID_DON_VI", OracleDbType.Int32).Value = ma_don_vi;
                    myCommand.Parameters.Add("p_ID_BUU_CUC", OracleDbType.Int32).Value = ma_bc_khai_thac;
                    myCommand.Parameters.Add("P_TOTAL", OracleDbType.Int32, 0, ParameterDirection.Output);
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);
                    myCommand.ExecuteNonQuery();
                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listBusinessManagementDetail = new List<BUSINESS_MANAGEMENT_Detail>();
                        while (dr.Read())
                        {
                            oBusinessManagementDetail = new BUSINESS_MANAGEMENT_Detail();
                            oBusinessManagementDetail.NGAY_HD = dr["NGAY_HD"].ToString();
                            oBusinessManagementDetail.MA_KH = dr["MA_KH"].ToString();
                            oBusinessManagementDetail.TEN_KHACH_HANG = dr["TEN_KHACH_HANG"].ToString();
                            oBusinessManagementDetail.Ma_NV_Sale = dr["Ma_NV_Sale"].ToString();
                            oBusinessManagementDetail.MA_BC_KHAI_THAC = dr["MA_BC_KHAI_THAC"].ToString();
                            oBusinessManagementDetail.TONG_SO = dr["TONG_SO"].ToString();
                            oBusinessManagementDetail.TONG_KHOI_LUONG_QD = dr["TONG_KHOI_LUONG_QD"].ToString();
                            oBusinessManagementDetail.TONG_CUOC_CHINH = dr["TONG_CUOC_CHINH"].ToString();
                            oBusinessManagementDetail.TONG_CUOC_DV = dr["TONG_CUOC_DV"].ToString();
                            oBusinessManagementDetail.TONG_CUOC_COD = dr["TONG_CUOC_COD"].ToString();
                            oBusinessManagementDetail.VAT = dr["VAT"].ToString();
                            oBusinessManagementDetail.TONG_CUOC_E1 = dr["TONG_CUOC_E1"].ToString();
                            oBusinessManagementDetail.CHIET_KHAU = dr["CHIET_KHAU"].ToString();
                            oBusinessManagementDetail.TRICH_THUONG = dr["TRICH_THUONG"].ToString();
                            oBusinessManagementDetail.TONG_NO = dr["TONG_NO"].ToString();
                            listBusinessManagementDetail.Add(oBusinessManagementDetail);

                        }
                        _ReturnBusinessManagement.Code = "00";
                        _ReturnBusinessManagement.Message = "Lấy dữ liệu thành công.";
                        _ReturnBusinessManagement.Total = Convert.ToInt32(myCommand.Parameters["P_TOTAL"].Value.ToString());
                        _ReturnBusinessManagement.ListBusinessManagement_Report = listBusinessManagementDetail;
                    }
                    else
                    {
                        _ReturnBusinessManagement.Code = "01";
                        _ReturnBusinessManagement.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _ReturnBusinessManagement.Code = "99";
                _ReturnBusinessManagement.Message = "Lỗi xử lý dữ liệu";
                //_returnQuality.Total = 0;
                //_returnQuality.ListQualityDeliveryReport = null;
            }
            return _ReturnBusinessManagement;
        }


        #endregion

        // Phần lấy dữ liệu từ bảng doikiem_kh_th_ngay và bảng Khach_Hang CHƯA PHÂN TRANG theo câu lệnh cũ lấy từ bảng doikiem_kh_th_ngay
        #region BUSINESS_MANAGEMENT_2_Detail          
        public ReturnBusinessManagement BUSINESS_MANAGEMENT_2_Detail(int ma_don_vi, int ma_bc_khai_thac, int ngay_xac_dinh_khach_hang, int tu_ngay, int den_ngay)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnBusinessManagement _ReturnBusinessManagement = new ReturnBusinessManagement();

            List<BUSINESS_MANAGEMENT_Detail> listBusinessManagementDetail = null;
            BUSINESS_MANAGEMENT_Detail oBusinessManagementDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {

                    OracleCommand myCommand = new OracleCommand("BUSINESS_MANAGEMENT.Detail_User", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("p_ID_DON_VI", OracleDbType.Int32).Value = ma_don_vi;
                    myCommand.Parameters.Add("p_ID_BUU_CUC", OracleDbType.Int32).Value = ma_bc_khai_thac;
                    myCommand.Parameters.Add("p_XD_KH", OracleDbType.Int32).Value = ngay_xac_dinh_khach_hang;
                    myCommand.Parameters.Add("p_TU_NGAY", OracleDbType.Int32).Value = tu_ngay;
                    myCommand.Parameters.Add("p_DEN_NGAY", OracleDbType.Int32).Value = den_ngay;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);
                    myCommand.ExecuteNonQuery();
                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listBusinessManagementDetail = new List<BUSINESS_MANAGEMENT_Detail>();
                        while (dr.Read())
                        {
                            oBusinessManagementDetail = new BUSINESS_MANAGEMENT_Detail();
                            oBusinessManagementDetail.NGAY_HD = dr["NGAY_HD"].ToString();
                            oBusinessManagementDetail.MA_KH = dr["MA_KH"].ToString();
                            oBusinessManagementDetail.THOI_GIAN_HD = dr["THOI_GIAN_HD"].ToString();
                            oBusinessManagementDetail.TEN_KHACH_HANG = dr["TEN_KHACH_HANG"].ToString();
                            oBusinessManagementDetail.Ma_NV_Sale = dr["Ma_NV_Sale"].ToString();
                            oBusinessManagementDetail.MA_BC_KHAI_THAC = dr["MA_BC_KHAI_THAC"].ToString();
                            oBusinessManagementDetail.TONG_SO = dr["TONG_SO"].ToString();
                            oBusinessManagementDetail.TONG_KHOI_LUONG_QD = dr["TONG_KHOI_LUONG_QD"].ToString();
                            oBusinessManagementDetail.TONG_TIEN_COD = dr["TONG_TIEN_COD"].ToString();
                            oBusinessManagementDetail.TONG_CUOC_CHINH = dr["TONG_CUOC_CHINH"].ToString();
                            oBusinessManagementDetail.TONG_CUOC_DV = dr["TONG_CUOC_DV"].ToString();
                            oBusinessManagementDetail.TONG_CUOC_COD = dr["TONG_CUOC_COD"].ToString();
                            oBusinessManagementDetail.VAT = dr["VAT"].ToString();
                            oBusinessManagementDetail.TONG_CUOC_E1 = dr["TONG_CUOC_E1"].ToString();
                            oBusinessManagementDetail.CHIET_KHAU = dr["CHIET_KHAU"].ToString();
                            oBusinessManagementDetail.TRICH_THUONG = dr["TRICH_THUONG"].ToString();
                            oBusinessManagementDetail.TONG_NO = dr["TONG_NO"].ToString();
                            listBusinessManagementDetail.Add(oBusinessManagementDetail);

                        }
                        _ReturnBusinessManagement.Code = "00";
                        _ReturnBusinessManagement.Message = "Lấy dữ liệu thành công.";
                        _ReturnBusinessManagement.ListBusinessManagement_Report = listBusinessManagementDetail;
                    }
                    else
                    {
                        _ReturnBusinessManagement.Code = "01";
                        _ReturnBusinessManagement.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _ReturnBusinessManagement.Code = "99";
                _ReturnBusinessManagement.Message = "Lỗi xử lý dữ liệu";
                //_returnQuality.Total = 0;
                //_returnQuality.ListQualityDeliveryReport = null;
            }
            return _ReturnBusinessManagement;
        }


        #endregion

        // Phần lấy dữ liệu từ bảng doikiem_kh_th_ngay và bảng Khach_Hang theo câu lệnh mới lấy từ bảng e1e2_ph
        #region BUSINESS_MANAGEMENT_3_Detail          
        public ReturnBusinessManagement BUSINESS_MANAGEMENT_3_Detail(int ma_don_vi, int ma_bc_khai_thac, int ngay_xac_dinh_khach_hang, int tu_ngay, int den_ngay, string dich_vu,int id_nguoi_dung)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnBusinessManagement _ReturnBusinessManagement = new ReturnBusinessManagement();

            List<BUSINESS_MANAGEMENT_Detail> listBusinessManagementDetail = null;
            BUSINESS_MANAGEMENT_Detail oBusinessManagementDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {

                    OracleCommand myCommand = new OracleCommand("BUSINESS_MANAGEMENT.Detail_User_NEW", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("p_ID_DON_VI", OracleDbType.Int32).Value = ma_don_vi;
                    myCommand.Parameters.Add("p_ID_BUU_CUC", OracleDbType.Int32).Value = ma_bc_khai_thac;
                    myCommand.Parameters.Add("p_XD_KH", OracleDbType.Int32).Value = ngay_xac_dinh_khach_hang;
                    myCommand.Parameters.Add("p_TU_NGAY", OracleDbType.Int32).Value = tu_ngay;
                    myCommand.Parameters.Add("p_DEN_NGAY", OracleDbType.Int32).Value = den_ngay;
                    myCommand.Parameters.Add("p_DICH_VU", OracleDbType.NVarchar2).Value = dich_vu;
                    myCommand.Parameters.Add("p_ID_NGUOI_DUNG", OracleDbType.Int32).Value = id_nguoi_dung;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);
                    myCommand.ExecuteNonQuery();
                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listBusinessManagementDetail = new List<BUSINESS_MANAGEMENT_Detail>();
                        while (dr.Read())
                        {
                            oBusinessManagementDetail = new BUSINESS_MANAGEMENT_Detail();
                            oBusinessManagementDetail.NGAY_HD = dr["NGAY_HD"].ToString();
                            oBusinessManagementDetail.MA_KH = dr["MA_KH"].ToString();
                            oBusinessManagementDetail.THOI_GIAN_HD = dr["THOI_GIAN_HD"].ToString();
                            oBusinessManagementDetail.TEN_KHACH_HANG = dr["TEN_KHACH_HANG"].ToString();
                            oBusinessManagementDetail.Ma_NV_Sale = dr["Ma_NV_Sale"].ToString();
                            oBusinessManagementDetail.MA_BC_KHAI_THAC = dr["MA_BC_KHAI_THAC"].ToString();
                            oBusinessManagementDetail.TONG_SO = dr["TONG_SO"].ToString();
                            oBusinessManagementDetail.TONG_KHOI_LUONG_QD = dr["TONG_KHOI_LUONG_QD"].ToString();
                            oBusinessManagementDetail.TONG_TIEN_COD = dr["TONG_TIEN_COD"].ToString();
                            oBusinessManagementDetail.TONG_CUOC_CHINH = dr["TONG_CUOC_CHINH"].ToString();
                            oBusinessManagementDetail.TONG_CUOC_DV = dr["TONG_CUOC_DV"].ToString();
                            oBusinessManagementDetail.TONG_CUOC_COD = dr["TONG_CUOC_COD"].ToString();
                            oBusinessManagementDetail.VAT = dr["VAT"].ToString();
                            oBusinessManagementDetail.TONG_CUOC_E1 = dr["TONG_CUOC_E1"].ToString();
                            oBusinessManagementDetail.CHIET_KHAU = dr["CHIET_KHAU"].ToString();
                            oBusinessManagementDetail.TRICH_THUONG = dr["TRICH_THUONG"].ToString();
                            oBusinessManagementDetail.TONG_NO = dr["TONG_NO"].ToString();
                            listBusinessManagementDetail.Add(oBusinessManagementDetail);

                        }
                        _ReturnBusinessManagement.Code = "00";
                        _ReturnBusinessManagement.Message = "Lấy dữ liệu thành công.";
                        _ReturnBusinessManagement.ListBusinessManagement_Report = listBusinessManagementDetail;
                    }
                    else
                    {
                        _ReturnBusinessManagement.Code = "01";
                        _ReturnBusinessManagement.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _ReturnBusinessManagement.Code = "99";
                _ReturnBusinessManagement.Message = "Lỗi xử lý dữ liệu";
                //_returnQuality.Total = 0;
                //_returnQuality.ListQualityDeliveryReport = null;
            }
            return _ReturnBusinessManagement;
        }


        #endregion

        // Phần lấy dữ liệu TỔNG CHÂN TRANG từ bảng doikiem_kh_th_ngay và bảng Khach_Hang CHƯA PHÂN TRANG
        #region SUM_BUSINESS_MANAGEMENT_Detail          
        public ReturnBusinessManagement SUM_BUSINESS_MANAGEMENT_Detail(int ma_don_vi, int ma_bc_khai_thac, int ngay_xac_dinh_khach_hang, int tu_ngay, int den_ngay)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnBusinessManagement _ReturnBusinessManagement = new ReturnBusinessManagement();

            List<SUM_BUSINESS_MANAGEMENT_Detail> listSumBusinessManagementDetail = null;
            SUM_BUSINESS_MANAGEMENT_Detail oSumBusinessManagementDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("BUSINESS_MANAGEMENT.List_Sum", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("p_ID_DON_VI", OracleDbType.Int32).Value = ma_don_vi;
                    myCommand.Parameters.Add("p_ID_BUU_CUC", OracleDbType.Int32).Value = ma_bc_khai_thac;
                    myCommand.Parameters.Add("p_XD_KH", OracleDbType.Int32).Value = ngay_xac_dinh_khach_hang;
                    myCommand.Parameters.Add("p_TU_NGAY", OracleDbType.Int32).Value = tu_ngay;
                    myCommand.Parameters.Add("p_DEN_NGAY", OracleDbType.Int32).Value = den_ngay;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);
                    myCommand.ExecuteNonQuery();
                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listSumBusinessManagementDetail = new List<SUM_BUSINESS_MANAGEMENT_Detail>();
                        while (dr.Read())
                        {
                            oSumBusinessManagementDetail = new SUM_BUSINESS_MANAGEMENT_Detail();
                            oSumBusinessManagementDetail.sumThoiGianHD = dr["sumThoiGianHD"].ToString();
                            oSumBusinessManagementDetail.sumTongSo = dr["sumTongSo"].ToString();
                            oSumBusinessManagementDetail.sumTongKLQD = dr["sumTongKLQD"].ToString();
                            oSumBusinessManagementDetail.sumTongTienCOD = dr["sumTongTienCOD"].ToString();
                            oSumBusinessManagementDetail.sumTongCC = dr["sumTongCC"].ToString();
                            oSumBusinessManagementDetail.sumTongCuocDV = dr["sumTongCuocDV"].ToString();
                            oSumBusinessManagementDetail.sumTongCuocCOD = dr["sumTongCuocCOD"].ToString();
                            oSumBusinessManagementDetail.sumVat = dr["sumVat"].ToString();
                            oSumBusinessManagementDetail.sumTongCuocE1 = dr["sumTongCuocE1"].ToString();
                            oSumBusinessManagementDetail.sumChietKhau = dr["sumChietKhau"].ToString();
                            oSumBusinessManagementDetail.sumTongNo = dr["sumTongNo"].ToString();
                            listSumBusinessManagementDetail.Add(oSumBusinessManagementDetail);

                        }
                        _ReturnBusinessManagement.Code = "00";
                        _ReturnBusinessManagement.Message = "Lấy dữ liệu thành công.";
                        _ReturnBusinessManagement.ListSumBusinessManagement_Report = listSumBusinessManagementDetail;
                    }
                    else
                    {
                        _ReturnBusinessManagement.Code = "01";
                        _ReturnBusinessManagement.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _ReturnBusinessManagement.Code = "99";
                _ReturnBusinessManagement.Message = "Lỗi xử lý dữ liệu";
                //_returnQuality.Total = 0;
                //_returnQuality.ListQualityDeliveryReport = null;
            }
            return _ReturnBusinessManagement;
        }


        #endregion

        // Phần lấy dữ liệu TỔNG CHÂN TRANG từ bảng doikiem_kh_th_ngay và bảng Khach_Hang CHƯA PHÂN TRANG
        #region SUM_BUSINESS_MANAGEMENT_Detail          
        public ReturnBusinessManagement SUM_BUSINESS_MANAGEMENT_2_Detail(int ma_don_vi, int ma_bc_khai_thac, int ngay_xac_dinh_khach_hang, int tu_ngay, int den_ngay, string dich_vu, int id_nguoi_dung)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnBusinessManagement _ReturnBusinessManagement = new ReturnBusinessManagement();

            List<SUM_BUSINESS_MANAGEMENT_Detail> listSumBusinessManagementDetail = null;
            SUM_BUSINESS_MANAGEMENT_Detail oSumBusinessManagementDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    OracleCommand myCommand = new OracleCommand("BUSINESS_MANAGEMENT.List_Sum_NEW", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("p_ID_DON_VI", OracleDbType.Int32).Value = ma_don_vi;
                    myCommand.Parameters.Add("p_ID_BUU_CUC", OracleDbType.Int32).Value = ma_bc_khai_thac;
                    myCommand.Parameters.Add("p_XD_KH", OracleDbType.Int32).Value = ngay_xac_dinh_khach_hang;
                    myCommand.Parameters.Add("p_TU_NGAY", OracleDbType.Int32).Value = tu_ngay;
                    myCommand.Parameters.Add("p_DEN_NGAY", OracleDbType.Int32).Value = den_ngay;
                    myCommand.Parameters.Add("p_DICH_VU", OracleDbType.NVarchar2).Value = dich_vu;
                    myCommand.Parameters.Add("p_ID_NGUOI_DUNG", OracleDbType.Int32).Value = id_nguoi_dung;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);
                    myCommand.ExecuteNonQuery();
                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listSumBusinessManagementDetail = new List<SUM_BUSINESS_MANAGEMENT_Detail>();
                        while (dr.Read())
                        {
                            oSumBusinessManagementDetail = new SUM_BUSINESS_MANAGEMENT_Detail();
                            oSumBusinessManagementDetail.sumThoiGianHD = dr["sumThoiGianHD"].ToString();
                            oSumBusinessManagementDetail.sumTongSo = dr["sumTongSo"].ToString();
                            oSumBusinessManagementDetail.sumTongKLQD = dr["sumTongKLQD"].ToString();
                            oSumBusinessManagementDetail.sumTongTienCOD = dr["sumTongTienCOD"].ToString();
                            oSumBusinessManagementDetail.sumTongCC = dr["sumTongCC"].ToString();
                            oSumBusinessManagementDetail.sumTongCuocDV = dr["sumTongCuocDV"].ToString();
                            oSumBusinessManagementDetail.sumTongCuocCOD = dr["sumTongCuocCOD"].ToString();
                            oSumBusinessManagementDetail.sumVat = dr["sumVat"].ToString();
                            oSumBusinessManagementDetail.sumTongCuocE1 = dr["sumTongCuocE1"].ToString();
                            oSumBusinessManagementDetail.sumChietKhau = dr["sumChietKhau"].ToString();
                            oSumBusinessManagementDetail.sumTongNo = dr["sumTongNo"].ToString();
                            listSumBusinessManagementDetail.Add(oSumBusinessManagementDetail);

                        }
                        _ReturnBusinessManagement.Code = "00";
                        _ReturnBusinessManagement.Message = "Lấy dữ liệu thành công.";
                        _ReturnBusinessManagement.ListSumBusinessManagement_Report = listSumBusinessManagementDetail;
                    }
                    else
                    {
                        _ReturnBusinessManagement.Code = "01";
                        _ReturnBusinessManagement.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _ReturnBusinessManagement.Code = "99";
                _ReturnBusinessManagement.Message = "Lỗi xử lý dữ liệu";
                //_returnQuality.Total = 0;
                //_returnQuality.ListQualityDeliveryReport = null;
            }
            return _ReturnBusinessManagement;
        }


        #endregion

    }



}

