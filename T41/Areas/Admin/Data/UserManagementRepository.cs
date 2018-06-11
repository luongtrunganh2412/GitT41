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

        // Phần lấy dữ liệu từ bảng business_profile
        #region USER_MANAGEMENT_DETAIL          
        public ReturnUserManagement USER_MANAGEMENT_DETAIL()
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

                    OracleCommand myCommand = new OracleCommand("USER_MANAGEMENT.Detail_User", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    //myCommand.Parameters.Add("v_Zone", OracleDbType.Int32).Value = zone;
                    //myCommand.Parameters.Add("v_StartDate", OracleDbType.Int32).Value = startdate;
                    //myCommand.Parameters.Add("v_EndDate", OracleDbType.Int32).Value = enddate;
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
                            oUserManagementDetail.Id = Convert.ToInt32(dr["Id"].ToString());
                            oUserManagementDetail.Address = dr["ADDRESS"].ToString();
                            oUserManagementDetail.BusinessTax = dr["BUSINESS_TAX"].ToString();
                            oUserManagementDetail.ContactName = dr["CONTACT_NAME"].ToString();
                            oUserManagementDetail.ContactAddress = dr["CONTACT_ADDRESS"].ToString();
                            oUserManagementDetail.ContactDistrict = dr["CONTACT_DISTRICT"].ToString();
                            oUserManagementDetail.ContactProvince = dr["CONTACT_PROVINCE"].ToString();
                            oUserManagementDetail.ContactPhoneWork = dr["CONTACT_PHONE_WORK"].ToString();
                            oUserManagementDetail.GeneralShortName = dr["GENERAL_SHORT_NAME"].ToString();
                            oUserManagementDetail.CustomerCode = dr["CUSTOMER_CODE"].ToString();
                            oUserManagementDetail.GeneralEmail = dr["GENERAL_EMAIL"].ToString();
                            oUserManagementDetail.GeneralAccountType = dr["GENERAL_ACCOUNT_TYPE"].ToString();
                            oUserManagementDetail.GeneralFullName = dr["GENERAL_FULL_NAME"].ToString();
                            oUserManagementDetail.Contract = dr["CONTRACT"].ToString();
                            oUserManagementDetail.UnitCode = dr["UNIT_CODE"].ToString();
                            oUserManagementDetail.Street = dr["STREET"].ToString();
                            oUserManagementDetail.SystemRefCode = dr["SYSTEM_REF_CODE"].ToString();
                            oUserManagementDetail.ApiKey = dr["API_KEY"].ToString();

                            
                            //Phần mã hóa api key 
                            //oUserManagementDetail.ApiKey = Common.Security.CreatPassWordHash(oUserManagementDetail.ApiKey + "6688");
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


        // Phần lấy dữ liệu từ bảng business_profile theo id người dùng
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

                    OracleCommand myCommand = new OracleCommand("USER_MANAGEMENT.Detail_Id_User", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("P_ID", OracleDbType.Int32).Value = edit_id;
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
                            oUserManagementDetail.Id = Convert.ToInt32(dr["Id"].ToString());
                            oUserManagementDetail.Address = dr["ADDRESS"].ToString();
                            oUserManagementDetail.BusinessTax = dr["BUSINESS_TAX"].ToString();
                            oUserManagementDetail.ContactName = dr["CONTACT_NAME"].ToString();
                            oUserManagementDetail.ContactAddress = dr["CONTACT_ADDRESS"].ToString();
                            oUserManagementDetail.ContactDistrict = dr["CONTACT_DISTRICT"].ToString();
                            oUserManagementDetail.ContactProvince = dr["CONTACT_PROVINCE"].ToString();
                            oUserManagementDetail.ContactPhoneWork = dr["CONTACT_PHONE_WORK"].ToString();
                            oUserManagementDetail.GeneralShortName = dr["GENERAL_SHORT_NAME"].ToString();
                            oUserManagementDetail.CustomerCode = dr["CUSTOMER_CODE"].ToString();
                            oUserManagementDetail.GeneralEmail = dr["GENERAL_EMAIL"].ToString();
                            oUserManagementDetail.GeneralAccountType = dr["GENERAL_ACCOUNT_TYPE"].ToString();
                            oUserManagementDetail.GeneralFullName = dr["GENERAL_FULL_NAME"].ToString();
                            oUserManagementDetail.Contract = dr["CONTRACT"].ToString();
                            oUserManagementDetail.UnitCode = dr["UNIT_CODE"].ToString();
                            oUserManagementDetail.Street = dr["STREET"].ToString();
                            oUserManagementDetail.SystemRefCode = dr["SYSTEM_REF_CODE"].ToString();
                            oUserManagementDetail.ApiKey = dr["API_KEY"].ToString();


                            //Phần mã hóa api key 
                            //oUserManagementDetail.ApiKey = Common.Security.CreatPassWordHash(oUserManagementDetail.ApiKey + "6688");
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
                    cmd.Connection = Helper.OraDCOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "BUSINESS_PROFILE_PKG.BUSINESS_PROFILE_CREATE";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("P_RETURN", OracleDbType.Int32, ParameterDirection.ReturnValue);
                    cmd.Parameters.Add("P_CUSTOMER_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = business.CUSTOMER_CODE;
                    cmd.Parameters.Add("P_CUSTOMER_TYPE", OracleDbType.Varchar2, ParameterDirection.Input).Value = business.GENERAL_ACCOUNT_TYPE;
                    cmd.Parameters.Add("P_COMPANY_NAME", OracleDbType.NVarchar2, ParameterDirection.Input).Value = business.GENERAL_FULL_NAME;
                    cmd.Parameters.Add("P_SHORTNAME", OracleDbType.Varchar2, ParameterDirection.Input).Value = business.GENERAL_SHORT_NAME;
                    cmd.Parameters.Add("P_CONTACT_NAME", OracleDbType.NVarchar2, ParameterDirection.Input).Value = business.CONTACT_NAME;
                    cmd.Parameters.Add("P_EMAIL", OracleDbType.Varchar2, ParameterDirection.Input).Value = business.GENERAL_EMAIL;
                    cmd.Parameters.Add("P_PHONE", OracleDbType.Varchar2, ParameterDirection.Input).Value = business.CONTACT_PHONE_WORK;
                    cmd.Parameters.Add("P_TAX", OracleDbType.Varchar2, ParameterDirection.Input).Value = business.BUSINESS_TAX;
                    cmd.Parameters.Add("P_CONTRACT", OracleDbType.Varchar2, ParameterDirection.Input).Value = business.CONTRACT;
                    cmd.Parameters.Add("P_ADDRESS", OracleDbType.Varchar2, ParameterDirection.Input).Value = business.CONTACT_ADDRESS;
                    cmd.Parameters.Add("P_PROVINCE_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = business.CONTACT_PROVINCE;
                    cmd.Parameters.Add("P_DISTRICT_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = business.CONTACT_DISTRICT;
                    cmd.Parameters.Add("P_STREET", OracleDbType.NVarchar2, ParameterDirection.Input).Value = business.STREET;
                    cmd.Parameters.Add("P_UNIT_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = business.UNIT_CODE;
                    cmd.Parameters.Add("P_SYSTEM_REF_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = business.SYSTEM_REF_CODE;
                    //cmd.Parameters.Add("P_API_KEY", OracleDbType.Varchar2, ParameterDirection.Input).Value = business.API_KEY;
                    //Phần mã hóa api_key , dữ liệu truyền vào para P_API_KEY = mahoa md5(email nhap vao + "6688")
                    cmd.Parameters.Add("P_API_KEY", OracleDbType.Varchar2, ParameterDirection.Input).Value = Common.Security.CreatPassWordHash(business.GENERAL_EMAIL + "6688");

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

        //Phần gọi đến package BUSINESS_PROFILE_CREATE trong database oracle để thêm dữ liệu vào !
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
                    cmd.Connection = Helper.OraDCOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "USER_MANAGEMENT.EDIT_USER";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("P_RETURN", OracleDbType.Int32, ParameterDirection.ReturnValue);
                    cmd.Parameters.Add("P_ADDRESS", OracleDbType.NVarchar2, ParameterDirection.Input).Value = business.ADDRESS;
                    cmd.Parameters.Add("P_BUSINESS_TAX", OracleDbType.Varchar2, ParameterDirection.Input).Value = business.BUSINESS_TAX;
                    cmd.Parameters.Add("P_CONTACT_NAME", OracleDbType.Varchar2, ParameterDirection.Input).Value = business.CONTACT_NAME;
                    cmd.Parameters.Add("P_CONTACT_ADDRESS", OracleDbType.NVarchar2, ParameterDirection.Input).Value = business.CONTACT_ADDRESS;
                    cmd.Parameters.Add("P_CONTACT_DISTRICT", OracleDbType.Varchar2, ParameterDirection.Input).Value = business.CONTACT_DISTRICT;
                    cmd.Parameters.Add("P_CONTACT_PROVINCE", OracleDbType.Varchar2, ParameterDirection.Input).Value = business.CONTACT_PROVINCE;
                    cmd.Parameters.Add("P_CONTACT_PHONE_WORK", OracleDbType.Varchar2, ParameterDirection.Input).Value = business.CONTACT_PHONE_WORK;
                    cmd.Parameters.Add("P_GENERAL_SHORT_NAME", OracleDbType.Varchar2, ParameterDirection.Input).Value = business.GENERAL_SHORT_NAME;
                    cmd.Parameters.Add("P_CUSTOMER_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = business.CUSTOMER_CODE;
                    cmd.Parameters.Add("P_GENERAL_EMAIL", OracleDbType.Varchar2, ParameterDirection.Input).Value = business.GENERAL_EMAIL;
                    cmd.Parameters.Add("P_GENERAL_ACCOUNT_TYPE", OracleDbType.Varchar2, ParameterDirection.Input).Value = business.GENERAL_ACCOUNT_TYPE;
                    cmd.Parameters.Add("P_GENERAL_FULL_NAME", OracleDbType.Varchar2, ParameterDirection.Input).Value = business.GENERAL_FULL_NAME;
                    cmd.Parameters.Add("P_CONTRACT", OracleDbType.Varchar2, ParameterDirection.Input).Value = business.CONTRACT;
                    cmd.Parameters.Add("P_UNIT_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = business.UNIT_CODE;
                    cmd.Parameters.Add("P_STREET", OracleDbType.NVarchar2, ParameterDirection.Input).Value = business.STREET;
                    cmd.Parameters.Add("P_SYSTEM_REF_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = business.SYSTEM_REF_CODE;
                    cmd.Parameters.Add("P_ID", OracleDbType.Int32, ParameterDirection.Input).Value = business.EDIT_ID;


                    //cmd.Parameters.Add("P_RETURN", OracleDbType.Int32, ParameterDirection.ReturnValue);
                    //cmd.Parameters.Add("P_CUSTOMER_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = business.CUSTOMER_CODE;
                    //cmd.Parameters.Add("P_GENERAL_ACCOUNT_TYPE", OracleDbType.Varchar2, ParameterDirection.Input).Value = business.GENERAL_ACCOUNT_TYPE;
                    //cmd.Parameters.Add("P_GENERAL_FULL_NAME", OracleDbType.NVarchar2, ParameterDirection.Input).Value = business.GENERAL_FULL_NAME;
                    //cmd.Parameters.Add("P_GENERAL_SHORT_NAME", OracleDbType.Varchar2, ParameterDirection.Input).Value = business.GENERAL_SHORT_NAME;
                    //cmd.Parameters.Add("P_CONTACT_NAME", OracleDbType.NVarchar2, ParameterDirection.Input).Value = business.CONTACT_NAME;
                    //cmd.Parameters.Add("P_GENERAL_EMAIL", OracleDbType.Varchar2, ParameterDirection.Input).Value = business.GENERAL_EMAIL;
                    //cmd.Parameters.Add("P_CONTACT_PHONE_WORK", OracleDbType.Varchar2, ParameterDirection.Input).Value = business.CONTACT_PHONE_WORK;
                    //cmd.Parameters.Add("P_BUSINESS_TAX", OracleDbType.Varchar2, ParameterDirection.Input).Value = business.BUSINESS_TAX;
                    //cmd.Parameters.Add("P_CONTRACT", OracleDbType.Varchar2, ParameterDirection.Input).Value = business.CONTRACT;
                    //cmd.Parameters.Add("P_CONTACT_ADDRESS", OracleDbType.Varchar2, ParameterDirection.Input).Value = business.CONTACT_ADDRESS;
                    //cmd.Parameters.Add("P_CONTACT_PROVINCE", OracleDbType.Varchar2, ParameterDirection.Input).Value = business.CONTACT_PROVINCE;
                    //cmd.Parameters.Add("P_CONTACT_DISTRICT", OracleDbType.Varchar2, ParameterDirection.Input).Value = business.CONTACT_DISTRICT;
                    //cmd.Parameters.Add("P_STREET", OracleDbType.NVarchar2, ParameterDirection.Input).Value = business.STREET;
                    //cmd.Parameters.Add("P_UNIT_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = business.UNIT_CODE;
                    //cmd.Parameters.Add("P_SYSTEM_REF_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = business.SYSTEM_REF_CODE;
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
                    cmd.Connection = Helper.OraDCOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "USER_MANAGEMENT.DELETE_USER";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("P_RETURN", OracleDbType.Int32, ParameterDirection.ReturnValue);
                    cmd.Parameters.Add("p_id", OracleDbType.Int32, ParameterDirection.Input).Value = delete_id;
                    
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

