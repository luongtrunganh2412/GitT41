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
        public ReturnExpressRoad EXPRESS_ROAD_DETAIL()
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

                    OracleCommand myCommand = new OracleCommand("USER_MANAGEMENT.Detail_Id_User_BP", Helper.OraDCDevOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("P_ID", OracleDbType.Int32).Value = 22;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);
                    listExpressRoad = ConvertListToDataTable.DataTableToList<ExpressRoadDetail>(da);
                    if (listExpressRoad != null && listExpressRoad.Count != 0)
                    {
                        _returnExpressRoad.Code = "00";
                        _returnExpressRoad.Message = "Lấy dữ liệu thành công.";
                        _returnExpressRoad.ListExpressRoadReport = listExpressRoad;
                    }

                    else
                    {
                        _returnExpressRoad.Code = "01";
                        _returnExpressRoad.Message = "Không có dữ liệu";
                        _returnExpressRoad.ListExpressRoadReport = null;
                    }



                    //DataTableReader dr = da.CreateDataReader();

                    //if (dr.HasRows)
                    //{
                    //    listExpressRoad = new List<ExpressRoadDetail>();
                    //    while (dr.Read())
                    //    {
                    //        oExpressRoadDetail = new ExpressRoadDetail();
                    //        oExpressRoadDetail.PROVINCECODE = dr["PROVINCECODE"].ToString();
                    //        oExpressRoadDetail.PROVINCENAME = dr["PROVINCENAME"].ToString();
                    //        listExpressRoad.Add(oExpressRoadDetail);

                    //    }

                    //    _returnExpressRoad.Code = "00";
                    //    _returnExpressRoad.Message = "Lấy dữ liệu thành công.";
                    //    _returnExpressRoad.ListExpressRoadReport = listExpressRoad;
                    //}
                    //else
                    //{
                    //    _returnExpressRoad.Code = "01";
                    //    _returnExpressRoad.Message = "Không có dữ liệu";

                    //}


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

