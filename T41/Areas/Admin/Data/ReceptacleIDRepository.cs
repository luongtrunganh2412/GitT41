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
    public class ReceptacleIDRepository
    {

        // Phần lấy dữ liệu từ bảng e1_bd13_di
        #region RECEPTACLE_Detail          
        public ReturnRECEPTACLE RECEPTACLE_Detail(string fromdate, string todate, string receptacle_id)
        {
            DataTable da = new DataTable();
            Convertion common = new Convertion();
            int id = 1;
            ReturnRECEPTACLE _ReturnRECEPTACLE = new ReturnRECEPTACLE();

            List<RECEPTACLE_Detail> listReceptacleDetail = null;
            RECEPTACLE_Detail oRECEPTACLEDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {

                    OracleCommand myCommand = new OracleCommand("FIND_RECEPTACLEID.Detail_RECEPTACLEID", Helper.OraDCOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("P_RECEPTACLEID", OracleDbType.NVarchar2).Value = receptacle_id;
                    myCommand.Parameters.Add("P_FROMDATE", OracleDbType.Int32).Value = common.DateToInt(fromdate);
                    myCommand.Parameters.Add("P_TODATE", OracleDbType.Int32).Value = common.DateToInt(todate);
                    myCommand.Parameters.Add(new OracleParameter("P_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);
                    myCommand.ExecuteNonQuery();
                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listReceptacleDetail = new List<RECEPTACLE_Detail>();
                        while (dr.Read())
                        {
                            oRECEPTACLEDetail = new RECEPTACLE_Detail();
                            oRECEPTACLEDetail.ID = id++;
                            oRECEPTACLEDetail.NGAY = dr["NGAY"].ToString();
                            oRECEPTACLEDetail.FLIGHTNUMBER = dr["FLIGHTNUMBER"].ToString();
                            oRECEPTACLEDetail.MO_TA = dr["MO_TA"].ToString();
                            oRECEPTACLEDetail.VI_TRI = dr["VI_TRI"].ToString();
                            oRECEPTACLEDetail.CN38 = dr["CN38"].ToString();
                            listReceptacleDetail.Add(oRECEPTACLEDetail);

                        }
                        _ReturnRECEPTACLE.Code = "00";
                        _ReturnRECEPTACLE.Message = "Lấy dữ liệu thành công.";
                        _ReturnRECEPTACLE.ListReceptacleReport = listReceptacleDetail;
                    }
                    else
                    {
                        _ReturnRECEPTACLE.Code = "01";
                        _ReturnRECEPTACLE.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _ReturnRECEPTACLE.Code = "99";
                _ReturnRECEPTACLE.Message = "Lỗi xử lý dữ liệu";
                //_returnQuality.Total = 0;
                //_returnQuality.ListQualityDeliveryReport = null;
            }
            return _ReturnRECEPTACLE;
        }
        #endregion
        
    }



}

