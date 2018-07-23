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
    public class ExpressRoadRepository
    {
        
        //Phần chi tiết của bảng tổng hợp 
        #region EXPRESS_ROAD_DETAIL          
        public ReturnExpressRoad EXPRESS_ROAD_DETAIL(int zone)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnExpressRoad _returnExpressRoad = new ReturnExpressRoad();

            List<ExpressRoadDetail> listExpressRoad = null;
            ExpressRoadDetail oExpressRoadDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {

                    OracleCommand myCommand = new OracleCommand("management_qlct.GetJourneyOnePosCode", Helper.OraDSOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("v_zone", OracleDbType.Int32).Value = zone;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);
                    DataTableReader dr = da.CreateDataReader();

                    

                    if (dr.HasRows)
                    {
                        listExpressRoad = new List<ExpressRoadDetail>();
                        while (dr.Read())
                        {
                            oExpressRoadDetail = new ExpressRoadDetail();
                            oExpressRoadDetail.EVENT = dr["EVENT"].ToString();
                            oExpressRoadDetail.POSTTIMEVIEW = dr["POSTTIMEVIEW"].ToString();
                            oExpressRoadDetail.MAILROUTEARRIVE = dr["MAILROUTEARRIVE"].ToString();
                            oExpressRoadDetail.MAILROUTEARRIVENAME = dr["MAILROUTEARRIVENAME"].ToString();
                            oExpressRoadDetail.ARRIVETIME = dr["ARRIVETIME"].ToString();
                            oExpressRoadDetail.MAILROUTE_TYPE = dr["MAILROUTE_TYPE"].ToString();
                            oExpressRoadDetail.MAILROUTE_CLASSIFY = dr["MAILROUTE_CLASSIFY"].ToString();
                            oExpressRoadDetail.TRANSPORT_TYPE = dr["TRANSPORT_TYPE"].ToString();
                            oExpressRoadDetail.MAILLEAVE = dr["MAILLEAVE"].ToString();
                            oExpressRoadDetail.MAILROUTENAME = dr["MAILROUTENAME"].ToString();
                            oExpressRoadDetail.LEAVE = dr["LEAVE"].ToString();
                            oExpressRoadDetail.SERVICE = dr["SERVICE"].ToString();
                            oExpressRoadDetail.MAILROUTELEAVE = dr["MAILROUTELEAVE"].ToString();
                            oExpressRoadDetail.MAILROUTELEAVENAME = dr["MAILROUTELEAVENAME"].ToString();
                            oExpressRoadDetail.LEAVETIME = dr["LEAVETIME"].ToString();
                            oExpressRoadDetail.MAILROUTE_TYPE1 = dr["MAILROUTE_TYPE1"].ToString();
                            oExpressRoadDetail.MAILROUTE_CLASSIFY1 = dr["MAILROUTE_CLASSIFY1"].ToString();
                            oExpressRoadDetail.TRANSPORT_TYPE1 = dr["TRANSPORT_TYPE1"].ToString();
                            listExpressRoad.Add(oExpressRoadDetail);

                        }
                        _returnExpressRoad.Code = "00";
                        _returnExpressRoad.Message = "Lấy dữ liệu thành công.";
                        _returnExpressRoad.ListExpressRoadReport = listExpressRoad;
                    }
                    else
                    {
                        _returnExpressRoad.Code = "01";
                        _returnExpressRoad.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _returnExpressRoad.Code = "99";
                _returnExpressRoad.Message = "Lỗi xử lý dữ liệu";
                //_returnQuality.Total = 0;
                //_returnQuality.ListQualityDeliveryReport = null;
            }
            return _returnExpressRoad;
        }



        #endregion

        
    }

}

