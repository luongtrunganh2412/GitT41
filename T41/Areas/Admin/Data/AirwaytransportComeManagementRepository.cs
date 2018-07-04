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
    public class AirwaytransportComeManagementRepository
    {
        
        //Phần gọi đến package INSERT_DATA1 trong database oracle để thêm dữ liệu vào !
        #region InsertAirwaytransportComeManagement  
        public ReturnAirwaytransportComeManagement InsertAirwaytransportComeManagement(int NGAY, int CHIEU, string TAICUNG_TH, string TAIMEM_TH, string GIOGIAO_TT, string GIOBAY_TT, string SOHIEUCHUYENBAY, string GIONHAN_TT, int ID_VNP)
        {
            ReturnAirwaytransportComeManagement oReturnAirwaytransportComeManagement = new ReturnAirwaytransportComeManagement();
            int id = 0;
            OracleCommand cmd;
            try
            {
                using (cmd = new OracleCommand())
                {
                    cmd.Connection = Helper.OraDSOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "INSERT_AIRWAY_TRANSPORT.INSERT_DATA1";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("P_RETURN", OracleDbType.Int32, ParameterDirection.ReturnValue);
                    cmd.Parameters.Add("P_NGAY", OracleDbType.Int32, ParameterDirection.Input).Value = NGAY;
                    cmd.Parameters.Add("P_CHIEU", OracleDbType.Int32, ParameterDirection.Input).Value = CHIEU;
                    cmd.Parameters.Add("P_TAICUNG_TH", OracleDbType.NVarchar2, ParameterDirection.Input).Value = TAICUNG_TH;
                    cmd.Parameters.Add("P_TAIMEM_TH", OracleDbType.NVarchar2, ParameterDirection.Input).Value = TAIMEM_TH;
                    cmd.Parameters.Add("P_GIOGIAO_TT", OracleDbType.NVarchar2, ParameterDirection.Input).Value = GIOGIAO_TT;
                    cmd.Parameters.Add("P_GIOBAY_TT", OracleDbType.NVarchar2, ParameterDirection.Input).Value = GIOBAY_TT;
                    cmd.Parameters.Add("P_SOHIEUCHUYENBAY", OracleDbType.NVarchar2, ParameterDirection.Input).Value = SOHIEUCHUYENBAY;
                    cmd.Parameters.Add("P_GIONHAN_TT", OracleDbType.NVarchar2, ParameterDirection.Input).Value = GIONHAN_TT;
                    cmd.Parameters.Add("P_ID_VNP", OracleDbType.Int32, ParameterDirection.Input).Value = ID_VNP;
                    
                    cmd.ExecuteNonQuery();
                    id = Convert.ToInt32(cmd.Parameters["P_RETURN"].Value.ToString());
                    if (id > 0)
                    {
                        oReturnAirwaytransportComeManagement.Code = "00";
                        oReturnAirwaytransportComeManagement.Message = "Thêm dữ liệu thành công";
                        oReturnAirwaytransportComeManagement.Value = id.ToString();

                    }
                    else
                    {
                        if (id == -1)
                        {
                            oReturnAirwaytransportComeManagement.Code = "-1";
                            oReturnAirwaytransportComeManagement.Message = "Cập Nhật dữ liệu thành công";
                            oReturnAirwaytransportComeManagement.Value = id.ToString();
                        }
                        else
                        {
                            oReturnAirwaytransportComeManagement.Code = "-99";
                            oReturnAirwaytransportComeManagement.Message = "Lỗi cập nhật dữ liệu";
                            oReturnAirwaytransportComeManagement.Value = string.Empty;
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, ex.Message);
                oReturnAirwaytransportComeManagement = null;
            }
            return oReturnAirwaytransportComeManagement;
        }
        #endregion

        
    }



}

