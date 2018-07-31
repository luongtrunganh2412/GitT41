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
        
        // Phần lấy dữ liệu từ bảng business_profile_oa
        #region BUSINESS_MANAGEMENT_Detail          
        public ReturnBusinessManagement BUSINESS_MANAGEMENT_Detail(int page_size, int page_index )
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

        
    }



}

