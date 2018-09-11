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
        public ReturnRECEPTACLE RECEPTACLE_Detail(int page_index, int page_size, string fromdate, string todate, string receptacle_id)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
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
                    myCommand.Parameters.Add("P_PAGE_INDEX", OracleDbType.Int32).Value = page_index;
                    myCommand.Parameters.Add("P_PAGE_SIZE", OracleDbType.Int32).Value = page_size;
                    myCommand.Parameters.Add("P_RECEPTACLEID", OracleDbType.NVarchar2).Value = receptacle_id;
                    myCommand.Parameters.Add("P_FROMDATE", OracleDbType.Int32).Value = common.DateToInt(fromdate);
                    myCommand.Parameters.Add("P_TODATE", OracleDbType.Int32).Value = common.DateToInt(todate);
                    myCommand.Parameters.Add("P_TOTAL", OracleDbType.Int32, 0, ParameterDirection.Output);
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
                            oRECEPTACLEDetail.CONSIGMENTID = dr["CONSIGMENTID"].ToString();
                            oRECEPTACLEDetail.EVTCODE = dr["EVTCODE"].ToString();
                            oRECEPTACLEDetail.EVTDATE = dr["EVTDATE"].ToString();
                            oRECEPTACLEDetail.EVTTIME = dr["EVTTIME"].ToString();
                            oRECEPTACLEDetail.EVTLOCATIONS = dr["EVTLOCATIONS"].ToString();
                            oRECEPTACLEDetail.FLIGHTNUMBER = dr["FLIGHTNUMBER"].ToString();
                            oRECEPTACLEDetail.DEPARTURELOC = dr["DEPARTURELOC"].ToString();
                            oRECEPTACLEDetail.ARRIVALLOC = dr["ARRIVALLOC"].ToString();
                            oRECEPTACLEDetail.DEPARTUREDATE = dr["DEPARTUREDATE"].ToString();
                            oRECEPTACLEDetail.DEPARTURETIME = dr["DEPARTURETIME"].ToString();
                            oRECEPTACLEDetail.ARRIVALDATE = dr["ARRIVALDATE"].ToString();
                            oRECEPTACLEDetail.ARRIVALTIME = dr["ARRIVALTIME"].ToString();
                            oRECEPTACLEDetail.EQUIPMENTID = dr["EQUIPMENTID"].ToString();
                            oRECEPTACLEDetail.CONTAINERTYPE = dr["CONTAINERTYPE"].ToString();
                            oRECEPTACLEDetail.RECEPTACLEID = dr["RECEPTACLEID"].ToString();
                            oRECEPTACLEDetail.MESSAGEID = dr["MESSAGEID"].ToString();
                            oRECEPTACLEDetail.SENDERMAILBOX = dr["SENDERMAILBOX"].ToString();
                            listReceptacleDetail.Add(oRECEPTACLEDetail);

                        }
                        _ReturnRECEPTACLE.Code = "00";
                        _ReturnRECEPTACLE.Message = "Lấy dữ liệu thành công.";
                        _ReturnRECEPTACLE.Total = Convert.ToInt32(myCommand.Parameters["P_TOTAL"].Value.ToString());
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

