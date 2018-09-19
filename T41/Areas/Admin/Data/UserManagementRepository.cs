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
    public class UserManagementRepository
    {
        Convertion common = new Convertion();

        #region GETPOSCODE_GIAO_DICH
        //Lấy mã bưu cục giao dịch dưới DB
        public String GETPOSCODE_GIAO_DICH()
        {
            string LISTPOSTCODE_GD = "<option value=\"0\">Tất Cả</option>";
            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDCOracleConnection;
                    cm.CommandText = Helper.SchemaName + "CRM_USER_MANAGEMENT.GetPoscodeGD_Ems";
                    cm.CommandType = CommandType.StoredProcedure;

                    cm.Parameters.Add("v_liststage", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            LISTPOSTCODE_GD += "<option value='" + dr["MA_BC"].ToString() + "'>" + dr["MA_BC"].ToString() + '-' + dr["TEN_BC"].ToString() + "</option>";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "LISTPOSTCODE_GD" + ex.Message);
            }

            return LISTPOSTCODE_GD;
        }
        #endregion


        // Phần lấy dữ liệu từ bảng business_profile_oa
        #region USER_MANAGEMENT_DETAIL          
        public ReturnUserManagement USER_MANAGEMENT_DETAIL(int poscode )
        {
            int STT = 1;
            DataTable da = new DataTable();
            Convertion common = new Convertion();
            ReturnUserManagement _returnUserManagement = new ReturnUserManagement();
            List<UserManagement_CRM_Detail> listUserManagementDetail = null;
            UserManagement_CRM_Detail oUserManagementDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    //xử lý tham số truyền vào data table
                    //OracleCommand myCommand = new OracleCommand("management_sale.get_list_Customer_Crm", Helper.OraEVComOracleConnection);
                    OracleCommand myCommand = new OracleCommand("CRM_USER_MANAGEMENT.Detail_User", Helper.OraEVComOracleConnection);
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("p_ID_BUU_CUC", OracleDbType.Int32).Value = poscode;
                    //myCommand.Parameters.Add("P_PAGE_SIZE", OracleDbType.Int32).Value = page_size;
                    //myCommand.Parameters.Add("p_ID", OracleDbType.Int32).Value = user_id;
                    //myCommand.Parameters.Add("p_CUSTOMER_CODE", OracleDbType.NVarchar2).Value = user_customer_code;
                    //myCommand.Parameters.Add("P_CONTACT_PHONE_WORK", OracleDbType.Int32).Value = user_contact_phone_work;
                    //myCommand.Parameters.Add("p_CUSTOMER_CODE", OracleDbType.NVarchar2).Value = user_general_email;
                    //myCommand.Parameters.Add("P_TOTAL", OracleDbType.Int32, 0, ParameterDirection.Output);
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);
                    myCommand.ExecuteNonQuery();
                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listUserManagementDetail = new List<UserManagement_CRM_Detail>();
                        while (dr.Read())
                        {
                            oUserManagementDetail = new UserManagement_CRM_Detail();
                            oUserManagementDetail.STT = STT++;
                            oUserManagementDetail.ACCOUNT_ID = dr["ACCOUNT_ID"].ToString();
                            oUserManagementDetail.DEAL_ID = dr["DEAL_ID"].ToString();
                            oUserManagementDetail.CONTACT_ID = dr["CONTACT_ID"].ToString();
                            oUserManagementDetail.SALES_ORDER_OWNER_ID = dr["SALES_ORDER_OWNER_ID"].ToString();
                            oUserManagementDetail.PO_ACCEPTANCE = dr["PO_ACCEPTANCE"].ToString();
                            oUserManagementDetail.CUSTOMER_NO = dr["CUSTOMER_NO"].ToString();
                            oUserManagementDetail.PICKUP_NAME = dr["PICKUP_NAME"].ToString();
                            oUserManagementDetail.PICKUP_FULL_ADDRESS = dr["PICKUP_FULL_ADDRESS"].ToString();
                            listUserManagementDetail.Add(oUserManagementDetail);

                        }
                        _returnUserManagement.Code = "00";
                        _returnUserManagement.Message = "Lấy dữ liệu thành công.";
                        _returnUserManagement.Total = listUserManagementDetail.Count();
                        _returnUserManagement.ListUserManagement_CRM_Report = listUserManagementDetail;
                    }
                    else
                    {
                        _returnUserManagement.Code = "01";
                        _returnUserManagement.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _returnUserManagement.Code = "99";
                _returnUserManagement.Message = "Lỗi xử lý dữ liệu";
            }
            return _returnUserManagement;
        }
        
        #endregion

        
    }



}

