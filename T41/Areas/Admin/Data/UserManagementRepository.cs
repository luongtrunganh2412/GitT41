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
        #region GETPROVINCE
        //Lấy mã bưu cục phát dưới DB Procedure USER_MANAGEMENT.Detail_Province
        public IEnumerable<GETPROVINCE> GETPROVINCE()
        {
            List<GETPROVINCE> listGetProvinceCode = null;
            GETPROVINCE oGetProvinceCode = null;

            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDCDevOracleConnection;
                    cm.CommandText = Helper.SchemaName + "USER_MANAGEMENT.Detail_Province";
                    cm.CommandType = CommandType.StoredProcedure;

                    cm.Parameters.Add("v_liststage", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        listGetProvinceCode = new List<GETPROVINCE>();
                        while (dr.Read())
                        {
                            oGetProvinceCode = new GETPROVINCE();
                            //oGetProvinceCode.PROVINCECODE = int.Parse(dr["PROVINCECODE"].ToString());
                            oGetProvinceCode.PROVINCECODE = dr["PROVINCECODE"].ToString();
                            oGetProvinceCode.PROVINCENAME = dr["PROVINCENAME"].ToString();
                            listGetProvinceCode.Add(oGetProvinceCode);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "GETPROVINCE" + ex.Message);
                listGetProvinceCode = null;
            }

            return listGetProvinceCode;
        }
        #endregion


        #region GETDISTRICT
        //Lấy mã bưu cục phát dưới DB Procedure USER_MANAGEMENT.Detail_District
        public IEnumerable<GETDISTRICT> GETDISTRICT(int provincecode)
        {
            List<GETDISTRICT> listGetDistrictCode = null;
            GETDISTRICT oGetDistrictCode = null;

            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDCDevOracleConnection;
                    cm.CommandText = Helper.SchemaName + "USER_MANAGEMENT.Detail_District";
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.Add(new OracleParameter("P_PROVINCECODE", OracleDbType.Int32)).Value = provincecode;
                    cm.Parameters.Add("v_liststage", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        listGetDistrictCode = new List<GETDISTRICT>();
                        while (dr.Read())
                        {
                            oGetDistrictCode = new GETDISTRICT();
                            oGetDistrictCode.DISTRICTCODE = dr["DISTRICTCODE"].ToString();
                            oGetDistrictCode.DISTRICTNAME = dr["DISTRICTNAME"].ToString();
                            listGetDistrictCode.Add(oGetDistrictCode);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "GETDISTRICT" + ex.Message);
                listGetDistrictCode = null;
            }

            return listGetDistrictCode;
        }
        #endregion

        // Phần lấy dữ liệu từ bảng business_profile_temp
        #region USER_MANAGEMENT_DETAIL          
        public ReturnUserManagement USER_MANAGEMENT_DETAIL(int page_size, int page_index,int user_id, string user_customer_code, int user_contact_phone_work )
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnUserManagement _returnUserManagement = new ReturnUserManagement();

            List<UserManagementDetail> listUserManagementDetail = null;
            UserManagementDetail oUserManagementDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {

                    OracleCommand myCommand = new OracleCommand("USER_MANAGEMENT.Page_Detail_User", Helper.OraDCDevOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("P_PAGE_INDEX", OracleDbType.Int32).Value = page_index;
                    myCommand.Parameters.Add("P_PAGE_SIZE", OracleDbType.Int32).Value = page_size;
                    myCommand.Parameters.Add("p_ID", OracleDbType.Int32).Value = user_id;
                    myCommand.Parameters.Add("p_CUSTOMER_CODE", OracleDbType.Int32).Value = user_customer_code;
                    myCommand.Parameters.Add("P_CONTACT_PHONE_WORK", OracleDbType.Int32).Value = user_contact_phone_work;
                    myCommand.Parameters.Add("P_TOTAL", OracleDbType.Int32, 0, ParameterDirection.Output);
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);
                    myCommand.ExecuteNonQuery();
                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listUserManagementDetail = new List<UserManagementDetail>();
                        while (dr.Read())
                        {
                            oUserManagementDetail = new UserManagementDetail();
                            oUserManagementDetail.CUSTOMER_ID = Convert.ToInt32(dr["CUSTOMER_ID"].ToString());
                            oUserManagementDetail.CUSTOMER_CODE = dr["CUSTOMER_CODE"].ToString();
                            oUserManagementDetail.CONTACT_NAME = dr["CONTACT_NAME"].ToString();
                            oUserManagementDetail.CONTACT_PHONE_WORK = dr["CONTACT_PHONE_WORK"].ToString();
                            oUserManagementDetail.GENERAL_EMAIL = dr["GENERAL_EMAIL"].ToString();
                            oUserManagementDetail.CONTACT_ADDRESS = dr["CONTACT_ADDRESS"].ToString();
                            //Phần mã hóa api key 
                            //oUserManagementDetail.ApiKey = Common.Security.CreatPassWordHash(oUserManagementDetail.ApiKey + "6688");
                            listUserManagementDetail.Add(oUserManagementDetail);

                        }
                        _returnUserManagement.Code = "00";
                        _returnUserManagement.Message = "Lấy dữ liệu thành công.";
                        _returnUserManagement.Total = Convert.ToInt32(myCommand.Parameters["P_TOTAL"].Value.ToString());
                        _returnUserManagement.ListUserManagementReport = listUserManagementDetail;
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
                //_returnQuality.Total = 0;
                //_returnQuality.ListQualityDeliveryReport = null;
            }
            return _returnUserManagement;
        }



        #endregion


        // Phần lấy dữ liệu từ bảng business_profile_temp theo id người dùng
        #region ID_USER_MANAGEMENT_DETAIL          
        public ReturnUserManagement ID_USER_MANAGEMENT_DETAIL(int edit_id)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnUserManagement _returnUserManagement = new ReturnUserManagement();

            List<UserManagementDetail> listUserManagementDetail = null;
            UserManagementDetail oUserManagementDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {

                    OracleCommand myCommand = new OracleCommand("USER_MANAGEMENT.Detail_Id_User", Helper.OraDCDevOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("P_CUSTOMER_ID", OracleDbType.Int32).Value = edit_id;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listUserManagementDetail = new List<UserManagementDetail>();
                        while (dr.Read())
                        {
                            oUserManagementDetail = new UserManagementDetail();
                            oUserManagementDetail.CUSTOMER_ID = Convert.ToInt32(dr["CUSTOMER_ID"].ToString());
                            oUserManagementDetail.CUSTOMER_CODE = dr["CUSTOMER_CODE"].ToString();
                            oUserManagementDetail.CONTACT_NAME = dr["CONTACT_NAME"].ToString();
                            oUserManagementDetail.CONTACT_PHONE_WORK = dr["CONTACT_PHONE_WORK"].ToString();
                            oUserManagementDetail.GENERAL_EMAIL = dr["GENERAL_EMAIL"].ToString();
                            oUserManagementDetail.CONTACT_ADDRESS = dr["CONTACT_ADDRESS"].ToString();
                            oUserManagementDetail.BUSINESS_TAX = dr["BUSINESS_TAX"].ToString();
                            oUserManagementDetail.UNIT_CODE = dr["UNIT_CODE"].ToString();
                            oUserManagementDetail.CONTRACT_NUMBER = dr["CONTRACT_NUMBER"].ToString();
                            oUserManagementDetail.TOTAL_CUSTOMER_CODE = dr["TOTAL_CUSTOMER_CODE"].ToString();
                            oUserManagementDetail.PAYMENT_ADDRESS = dr["PAYMENT_ADDRESS"].ToString();
                            oUserManagementDetail.PAYMENT_METHOD = dr["PAYMENT_METHOD"].ToString();
                            oUserManagementDetail.CONTACT_PROVINCE = dr["CONTACT_PROVINCE"].ToString();
                            oUserManagementDetail.CONTACT_DISTRICT = dr["CONTACT_DISTRICT"].ToString();
                            oUserManagementDetail.EMPLOYEE_DEBT_CODE = dr["EMPLOYEE_DEBT_CODE"].ToString();
                            oUserManagementDetail.EMPLOYEE_SALE_CODE = dr["EMPLOYEE_SALE_CODE"].ToString();
                            listUserManagementDetail.Add(oUserManagementDetail);

                        }
                        _returnUserManagement.Code = "00";
                        _returnUserManagement.Message = "Lấy dữ liệu thành công.";
                        _returnUserManagement.ListUserManagementReport = listUserManagementDetail;
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
                //_returnQuality.Total = 0;
                //_returnQuality.ListQualityDeliveryReport = null;
            }
            return _returnUserManagement;
        }



        #endregion





        //Phần gọi đến package BUSINESS_PROFILE_CREATE trong database oracle để thêm dữ liệu vào !
        #region CREATE_USER_MANAGEMENT_DETAIL  
        public ReturnUserManagement CreatBusinessProfile(PARAMETER_BUSINESS business)
        {
            ReturnUserManagement oReturnUserManagement = new ReturnUserManagement();
            int id = 0;
            OracleCommand cmd;
            try
            {
                using (cmd = new OracleCommand())
                {
                    cmd.Connection = Helper.OraDCDevOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "USER_MANAGEMENT.Insert_User";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("P_RETURN", OracleDbType.Int32, ParameterDirection.ReturnValue);
                    cmd.Parameters.Add("P_CUSTOMER_CODE", OracleDbType.NVarchar2, ParameterDirection.Input).Value = business.CUSTOMER_CODE;
                    cmd.Parameters.Add("P_CONTACT_NAME", OracleDbType.NVarchar2, ParameterDirection.Input).Value = business.CONTACT_NAME;
                    cmd.Parameters.Add("P_DATE_CREATE", OracleDbType.Int32, ParameterDirection.Input).Value = business.DATE_CREATE;
                    cmd.Parameters.Add("P_DATE_END", OracleDbType.Int32, ParameterDirection.Input).Value = business.DATE_END;
                    cmd.Parameters.Add("P_CONTACT_PHONE_WORK", OracleDbType.NVarchar2, ParameterDirection.Input).Value = business.CONTACT_PHONE_WORK;
                    cmd.Parameters.Add("P_GENERAL_EMAIL", OracleDbType.NVarchar2, ParameterDirection.Input).Value = business.GENERAL_EMAIL;
                    cmd.Parameters.Add("P_CONTACT_ADDRESS", OracleDbType.NVarchar2, ParameterDirection.Input).Value = business.CONTACT_ADDRESS;
                    cmd.Parameters.Add("P_BUSINESS_TAX", OracleDbType.NVarchar2, ParameterDirection.Input).Value = business.BUSINESS_TAX;
                    cmd.Parameters.Add("P_UNIT_CODE", OracleDbType.Int32, ParameterDirection.Input).Value = business.UNIT_CODE;
                    cmd.Parameters.Add("P_CONTRACT_NUMBER", OracleDbType.Varchar2, ParameterDirection.Input).Value = business.CONTRACT_NUMBER;
                    cmd.Parameters.Add("P_CUSTOMER_ACTIVE", OracleDbType.Int32, ParameterDirection.Input).Value = business.CUSTOMER_ACTIVE;
                    cmd.Parameters.Add("P_TOTAL_CUSTOMER_CODE", OracleDbType.NVarchar2, ParameterDirection.Input).Value = business.TOTAL_CUSTOMER_CODE;
                    cmd.Parameters.Add("P_PAYMENT_ADDRESS", OracleDbType.NVarchar2, ParameterDirection.Input).Value = business.PAYMENT_ADDRESS;
                    cmd.Parameters.Add("P_PAYMENT_METHOD", OracleDbType.Varchar2, ParameterDirection.Input).Value = business.PAYMENT_METHOD;
                    cmd.Parameters.Add("P_CONTACT_PROVINCE", OracleDbType.Varchar2, ParameterDirection.Input).Value = business.CONTACT_PROVINCE;
                    cmd.Parameters.Add("P_CONTACT_DISTRICT", OracleDbType.Varchar2, ParameterDirection.Input).Value = business.CONTACT_DISTRICT;
                    cmd.Parameters.Add("P_EMPLOYEE_DEBT_CODE", OracleDbType.Int32, ParameterDirection.Input).Value = business.EMPLOYEE_DEBT_CODE;
                    cmd.Parameters.Add("P_EMPLOYEE_SALE_CODE", OracleDbType.Int32, ParameterDirection.Input).Value = business.EMPLOYEE_SALE_CODE;
                    //cmd.Parameters.Add("P_API_KEY", OracleDbType.Varchar2, ParameterDirection.Input).Value = Common.Security.CreatPassWordHash(business.GENERAL_EMAIL + "6688");

                    cmd.ExecuteNonQuery();
                    id = Convert.ToInt32(cmd.Parameters["P_RETURN"].Value.ToString());
                    if (id > 0)
                    {
                        oReturnUserManagement.Code = "00";
                        oReturnUserManagement.Message = "Thêm dữ liệu thành công";
                        oReturnUserManagement.Value = id.ToString();

                    }
                    else
                    {
                        if (id == -1)
                        {
                            oReturnUserManagement.Code = "-1";
                            oReturnUserManagement.Message = "Đã tồn tại tài khoản này";
                            oReturnUserManagement.Value = id.ToString();
                        }
                        else
                        {
                            oReturnUserManagement.Code = "-99";
                            oReturnUserManagement.Message = "Lỗi cập nhật dữ liệu";
                            oReturnUserManagement.Value = string.Empty;
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, ex.Message);
                oReturnUserManagement = null;
            }
            return oReturnUserManagement;
        }
        #endregion

        //Phần gọi đến package BUSINESS_PROFILE_CREATE trong database oracle để sửa dữ liệu !
        #region CREATE_USER_MANAGEMENT_DETAIL  
        public ReturnUserManagement EditBusinessProfile(PARAMETER_BUSINESS business)
        {
            ReturnUserManagement oReturnUserManagement = new ReturnUserManagement();
            int id = 0;
            OracleCommand cmd;
            try
            {
                using (cmd = new OracleCommand())
                {
                    cmd.Connection = Helper.OraDCDevOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "USER_MANAGEMENT.EDIT_USER";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("P_RETURN", OracleDbType.Int32, ParameterDirection.ReturnValue);
                    cmd.Parameters.Add("P_CUSTOMER_CODE", OracleDbType.NVarchar2, ParameterDirection.Input).Value = business.CUSTOMER_CODE;
                    cmd.Parameters.Add("P_CONTACT_NAME", OracleDbType.NVarchar2, ParameterDirection.Input).Value = business.CONTACT_NAME;
                    cmd.Parameters.Add("P_DATE_CREATE", OracleDbType.Int32, ParameterDirection.Input).Value = business.DATE_CREATE;
                    cmd.Parameters.Add("P_DATE_END", OracleDbType.Int32, ParameterDirection.Input).Value = business.DATE_END;
                    cmd.Parameters.Add("P_CONTACT_PHONE_WORK", OracleDbType.NVarchar2, ParameterDirection.Input).Value = business.CONTACT_PHONE_WORK;
                    cmd.Parameters.Add("P_GENERAL_EMAIL", OracleDbType.NVarchar2, ParameterDirection.Input).Value = business.GENERAL_EMAIL;
                    cmd.Parameters.Add("P_CONTACT_ADDRESS", OracleDbType.NVarchar2, ParameterDirection.Input).Value = business.CONTACT_ADDRESS;
                    cmd.Parameters.Add("P_BUSINESS_TAX", OracleDbType.NVarchar2, ParameterDirection.Input).Value = business.BUSINESS_TAX;
                    cmd.Parameters.Add("P_UNIT_CODE", OracleDbType.Int32, ParameterDirection.Input).Value = business.UNIT_CODE;
                    cmd.Parameters.Add("P_CONTRACT_NUMBER", OracleDbType.Varchar2, ParameterDirection.Input).Value = business.CONTRACT_NUMBER;
                    cmd.Parameters.Add("P_CUSTOMER_ACTIVE", OracleDbType.Int32, ParameterDirection.Input).Value = business.CUSTOMER_ACTIVE;
                    cmd.Parameters.Add("P_TOTAL_CUSTOMER_CODE", OracleDbType.NVarchar2, ParameterDirection.Input).Value = business.TOTAL_CUSTOMER_CODE;
                    cmd.Parameters.Add("P_PAYMENT_ADDRESS", OracleDbType.NVarchar2, ParameterDirection.Input).Value = business.PAYMENT_ADDRESS;
                    cmd.Parameters.Add("P_PAYMENT_METHOD", OracleDbType.Varchar2, ParameterDirection.Input).Value = business.PAYMENT_METHOD;
                    cmd.Parameters.Add("P_CONTACT_PROVINCE", OracleDbType.Varchar2, ParameterDirection.Input).Value = business.CONTACT_PROVINCE;
                    cmd.Parameters.Add("P_CONTACT_DISTRICT", OracleDbType.Varchar2, ParameterDirection.Input).Value = business.CONTACT_DISTRICT;
                    cmd.Parameters.Add("P_EMPLOYEE_DEBT_CODE", OracleDbType.Int32, ParameterDirection.Input).Value = business.EMPLOYEE_DEBT_CODE;
                    cmd.Parameters.Add("P_EMPLOYEE_SALE_CODE", OracleDbType.Int32, ParameterDirection.Input).Value = business.EMPLOYEE_SALE_CODE;
                    cmd.Parameters.Add("P_CUSTOMER_ID", OracleDbType.Int32, ParameterDirection.Input).Value = business.EDIT_ID;

                    cmd.ExecuteNonQuery();

                    Oracle.ManagedDataAccess.Types.OracleDecimal P_return = (Oracle.ManagedDataAccess.Types.OracleDecimal)cmd.Parameters["P_RETURN"].Value;

                    if (P_return.IsNull)
                    {
                        id = 0;
                    }

                    else {
                        id = P_return.ToInt32();
                    }
                        

                    //id = int.Parse(cmd.Parameters["P_RETURN"].Value.ToString());
                    //id = Convert.ToInt32(cmd.Parameters["P_RETURN"].Value.ToString());
                    if (id > 0)
                    {
                        oReturnUserManagement.Code = "00";
                        oReturnUserManagement.Message = "Sửa dữ liệu thành công";
                        oReturnUserManagement.Value = id.ToString();

                    }
                    else
                    {
                        if (id == -1)
                        {
                            oReturnUserManagement.Code = "-1";
                            oReturnUserManagement.Message = "Đã tồn tại tài khoản này";
                            oReturnUserManagement.Value = id.ToString();
                        }
                        else
                        {
                            oReturnUserManagement.Code = "-99";
                            oReturnUserManagement.Message = "Lỗi cập nhật dữ liệu";
                            oReturnUserManagement.Value = string.Empty;
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, ex.Message);
                oReturnUserManagement = null;
            }
            return oReturnUserManagement;
        }
        #endregion


        //Phần gọi đến DeleteBusinessProfile để xóa dữ liệu theo id trong database oracle  !
        #region DELETE_USER_MANAGEMENT_DETAIL  
        public ReturnUserManagement DeleteBusinessProfile(int delete_id)
        {
            ReturnUserManagement oReturnUserManagement = new ReturnUserManagement();
            int id = 0;
            OracleCommand cmd;
            try
            {
                using (cmd = new OracleCommand())
                {
                    cmd.Connection = Helper.OraDCDevOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "USER_MANAGEMENT.DELETE_USER";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("P_RETURN", OracleDbType.Int32, ParameterDirection.ReturnValue);
                    cmd.Parameters.Add("P_CUSTOMER_ID", OracleDbType.Int32, ParameterDirection.Input).Value = delete_id;
                    
                    cmd.ExecuteNonQuery();
                    id = Convert.ToInt32(cmd.Parameters["P_RETURN"].Value.ToString());
                    if (id > 0)
                    {
                        oReturnUserManagement.Code = "00";
                        oReturnUserManagement.Message = "Xóa dữ liệu thành công";
                        oReturnUserManagement.Value = id.ToString();

                    }
                    else
                    {
                        
                         oReturnUserManagement.Code = "-99";
                         oReturnUserManagement.Message = "Lỗi cập nhật dữ liệu";
                         oReturnUserManagement.Value = string.Empty;
                    }
                }


            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, ex.Message);
                oReturnUserManagement = null;
            }
            return oReturnUserManagement;
        }
        #endregion
    }



}

