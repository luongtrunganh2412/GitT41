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
    public class SaleUserManagementRepository
    {
        
        // Phần lấy dữ liệu từ bảng business_profile_oa
        #region SALE_USER_MANAGEMENT_DETAIL          
        public ReturnSaleUserManagement SALE_USER_MANAGEMENT_DETAIL(int page_size, int page_index, int id_nguoi_dung, int id_don_vi, int dien_thoai, string email )
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnSaleUserManagement _ReturnSaleUserManagement = new ReturnSaleUserManagement();

            List<SALE_USER_MANAGEMENT_Detail> listSaleUserManagementDetail = null;
            SALE_USER_MANAGEMENT_Detail oSaleUserManagementDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {

                    OracleCommand myCommand = new OracleCommand("USER_SALE_MANAGEMENT.Page_Detail_User", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("P_PAGE_INDEX", OracleDbType.Int32).Value = page_index;
                    myCommand.Parameters.Add("P_PAGE_SIZE", OracleDbType.Int32).Value = page_size;
                    myCommand.Parameters.Add("p_ID_NGUOI_DUNG", OracleDbType.Int32).Value = id_nguoi_dung;
                    myCommand.Parameters.Add("p_ID_DON_VI", OracleDbType.Int32).Value = id_don_vi;
                    myCommand.Parameters.Add("P_DIEN_THOAI", OracleDbType.Int32).Value = dien_thoai;
                    myCommand.Parameters.Add("P_EMAIL", OracleDbType.Int32).Value = email;
                    myCommand.Parameters.Add("P_TOTAL", OracleDbType.Int32, 0, ParameterDirection.Output);
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);
                    myCommand.ExecuteNonQuery();
                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listSaleUserManagementDetail = new List<SALE_USER_MANAGEMENT_Detail>();
                        while (dr.Read())
                        {
                            oSaleUserManagementDetail = new SALE_USER_MANAGEMENT_Detail();
                            oSaleUserManagementDetail.ID_NGUOI_DUNG = Convert.ToInt32(dr["ID_NGUOI_DUNG"].ToString());
                            oSaleUserManagementDetail.ID_DON_VI = Convert.ToInt32(dr["ID_DON_VI"].ToString());
                            oSaleUserManagementDetail.HO_TEN = dr["HO_TEN"].ToString();
                            oSaleUserManagementDetail.CHUC_VU = dr["CHUC_VU"].ToString();
                            oSaleUserManagementDetail.DIEN_THOAI = dr["DIEN_THOAI"].ToString();
                            oSaleUserManagementDetail.EMAIL = dr["EMAIL"].ToString();
                            listSaleUserManagementDetail.Add(oSaleUserManagementDetail);

                        }
                        _ReturnSaleUserManagement.Code = "00";
                        _ReturnSaleUserManagement.Message = "Lấy dữ liệu thành công.";
                        _ReturnSaleUserManagement.Total = Convert.ToInt32(myCommand.Parameters["P_TOTAL"].Value.ToString());
                        _ReturnSaleUserManagement.ListSaleUserManagement_Report = listSaleUserManagementDetail;
                    }
                    else
                    {
                        _ReturnSaleUserManagement.Code = "01";
                        _ReturnSaleUserManagement.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _ReturnSaleUserManagement.Code = "99";
                _ReturnSaleUserManagement.Message = "Lỗi xử lý dữ liệu";
                //_returnQuality.Total = 0;
                //_returnQuality.ListQualityDeliveryReport = null;
            }
            return _ReturnSaleUserManagement;
        }



        #endregion

        
    }



}

