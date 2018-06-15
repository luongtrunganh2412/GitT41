using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using T41.Areas.Admin.Models.DataModel;
using T41.Areas.Admin.Common;
using System.Data;

namespace T41.Areas.Admin.Data
{
    public class DeliveryRepository
    {
        #region GetALLDeliveryPostCode

        public IEnumerable<DeliveryPostCode> GetAllDeliveryPostCode()
        {
            List<DeliveryPostCode> listDeliveryPostCode = null;
            DeliveryPostCode oDeliveryPostCode = null;

            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDCOracleConnection;
                    cm.CommandText = string.Format("SELECT * FROM DELIVERY_POST_CODE ORDER BY DELIVERY_POST_CODE");
                    cm.CommandType = CommandType.Text;
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        listDeliveryPostCode = new List<DeliveryPostCode>();
                        while (dr.Read())
                        {
                            oDeliveryPostCode = new DeliveryPostCode();
                            oDeliveryPostCode.POST_CODE = int.Parse(dr["DELIVERY_POST_CODE"].ToString());
                            oDeliveryPostCode.POST_CODE_NAME = dr["POST_CODE_NAME"].ToString();
                            listDeliveryPostCode.Add(oDeliveryPostCode);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "GetAllDeliveryPostCode" + ex.Message);
                listDeliveryPostCode = null;
            }

            return listDeliveryPostCode;
        }
        #endregion

        #region GetAllPostMan
        public IEnumerable<PostMan> GetAllPostMan()
        {
            List<PostMan> listPostMan = null;
            PostMan oPostMan = null;
            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDCOracleConnection;
                    cm.CommandText = string.Format("SELECT ID_NHAN_VIEN, HO_TEN FROM Nguoi_Dung_Sale ORDER BY ID_NHAN_VIEN asc");
                    cm.CommandType = CommandType.Text;
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        listPostMan = new List<PostMan>();
                        while (dr.Read())
                        {
                            oPostMan = new PostMan();
                            //oPostMan.POSTMAN_ID = int.Parse(dr["ID_NHAN_VIEN"].ToString());
                            oPostMan.POSTMAN_ID = dr["ID_NHAN_VIEN"].ToString();
                            oPostMan.POSTMAN_NAME = dr["HO_TEN"].ToString();
                            listPostMan.Add(oPostMan);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "GetAllDeliveryPostCode" + ex.Message);
                listPostMan = null;
            }

            return listPostMan;
        }
        #endregion

        #region GetDeliveryRouteCodeById
        public IEnumerable<DeliveryPostCode> GetDeliveryRouteCodeById(int id)
        {
            List<DeliveryPostCode> listRoute = null;
            DeliveryPostCode oRoute = null;

            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDCOracleConnection;
                    cm.CommandText = string.Format("SELECT * FROM DELIVERY_ROUTE_CODE WHERE DELIVERY_ROUTE=" + id + " ORDER BY POST_CODE");
                    cm.CommandType = CommandType.Text;
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        listRoute = new List<DeliveryPostCode>();
                        while (dr.Read())
                        {
                            oRoute = new DeliveryPostCode();
                            oRoute.POST_CODE = int.Parse(dr["POST_CODE"].ToString());
                            oRoute.POST_CODE_NAME = dr["POST_CODE_NAME"].ToString();
                            listRoute.Add(oRoute);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "GetRouteById" + ex.Message);
                listRoute = null;
            }

            return listRoute;
        }
        #endregion

        #region DELIVERY_DEPART_DETAIL

        public ReturnDelivery DELIVERY_DEPART_DETAIL(int channel, int post_man, int status, int from_date, int to_date, int delivery_post_code, int delivery_route_code, int page_size, int page_index)
        {
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnDelivery _returnDelivery = new ReturnDelivery();
            switch (channel)
            {
                case 0:
                    _metadata.channel = "Tất cả"; break;
                case 1:
                    _metadata.channel = "EmsEnterprise"; break;
                case 2:
                    _metadata.channel = "SmartPhone"; break;
            }
            if (post_man == 0)
                _metadata.postman = "Tất cả";
            else
                _metadata.postman = "Bưu tá: " + post_man.ToString();

            switch (status)
            {
                case 0:
                    _metadata.status = "Tất cả"; break;
                case 1:
                    _metadata.status = "Thành công"; break;
                case 2:
                    _metadata.status = "Không thành công"; break;
            }
            _metadata.from_to_date = "Từ ngày " + common.Convert_Date(from_date) + " đến ngày " + common.Convert_Date(to_date);
            if (delivery_post_code == 0)
                _metadata.delivery_post_code = "Tất cả";
            else
                _metadata.delivery_post_code = "Bưu cục phát: " + delivery_post_code;

            if (delivery_route_code == 0)
                _metadata.delivery_route_code = "Tất cả";
            else
                _metadata.delivery_route_code = "Tuyến phát: " + delivery_route_code;
            _returnDelivery.MetaData = _metadata;
            List<DeliveryDetail> listDeliveryDetail = null;
            DeliveryDetail oDeliveryDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = Helper.OraDCOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "DELIVERY_PKG.DELIVERY_DEPART_DETAIL";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new OracleParameter("P_DELIVERY_POST_CODE", OracleDbType.Int32)).Value = delivery_post_code;
                    cmd.Parameters.Add(new OracleParameter("P_DELIVERY_ROUTE_CODE", OracleDbType.Int32)).Value = delivery_route_code;
                    cmd.Parameters.Add(new OracleParameter("P_STATUS", OracleDbType.Int32)).Value = status;
                    cmd.Parameters.Add(new OracleParameter("P_FROM_DATE", OracleDbType.Int32)).Value = from_date;
                    cmd.Parameters.Add(new OracleParameter("P_TO_DATE", OracleDbType.Int32)).Value = to_date;
                    cmd.Parameters.Add(new OracleParameter("P_CHANNEL", OracleDbType.Int32)).Value = channel;
                    cmd.Parameters.Add(new OracleParameter("P_POSTMAN", OracleDbType.Int32)).Value = post_man;
                    cmd.Parameters.Add(new OracleParameter("P_PAGE_INDEX", OracleDbType.Int32)).Value = page_index;
                    cmd.Parameters.Add(new OracleParameter("P_PAGE_SIZE", OracleDbType.Int32)).Value = page_size;
                    cmd.Parameters.Add("P_TOTAL", OracleDbType.Int32, 0, ParameterDirection.Output);
                    cmd.Parameters.Add("P_RETURN_CODE", OracleDbType.Int32, 0, ParameterDirection.Output);
                    cmd.Parameters.Add("P_OUT_CURSOR", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    OracleDataReader dr = Helper.ExecuteDataReader(cmd, Helper.OraDCOracleConnection);
                    if (float.Parse(cmd.Parameters["P_RETURN_CODE"].Value.ToString()) > 0)
                    {
                        if (dr.HasRows)
                        {
                            listDeliveryDetail = new List<DeliveryDetail>();
                            while (dr.Read())
                            {
                                oDeliveryDetail = new DeliveryDetail();
                                oDeliveryDetail.CHANNEL = dr["CHANNEL"].ToString();
                                oDeliveryDetail.LADING_CODE = dr["MAE1"].ToString();
                                oDeliveryDetail.DELIVERY_DATE_TIME = dr["DELIVERY_DATE_TIME"].ToString();
                                oDeliveryDetail.SYSTEM_DATE_TIME = dr["SYSTEM_DATE_TIME"].ToString();
                                oDeliveryDetail.DELIVERY_RESULT = dr["DELIVERY_RESULT"].ToString();
                                oDeliveryDetail.REASON = dr["REASON"].ToString();
                                oDeliveryDetail.FEE = Convert.ToInt32(dr["FEE"].ToString());
                                oDeliveryDetail.AMOUNT = Convert.ToInt32(dr["AMOUNT"].ToString());
                                oDeliveryDetail.DELIVERY_POST_CODE = Convert.ToInt32(dr["DELIVERY_POST_CODE"].ToString());
                                oDeliveryDetail.DELIVERY_ROUTE_CODE = Convert.ToInt32(dr["DELIVERY_ROUTE_CODE"].ToString());
                                oDeliveryDetail.COME_DATE = dr["COME_DATE"].ToString();
                                oDeliveryDetail.REMAIN_DATE = dr["REMAIN_DATE"].ToString();
                                oDeliveryDetail.POSTMAN_ID = dr["POST_MAN"].ToString();
                                oDeliveryDetail.SERVICE_TYPE = dr["DV"].ToString();
                                oDeliveryDetail.ID_PROCESS = int.Parse(dr["ID"].ToString());
                                listDeliveryDetail.Add(oDeliveryDetail);
                            }
                            _returnDelivery.Code = "00";
                            _returnDelivery.Message = "Lấy dữ liệu thành công.";
                            _returnDelivery.Total = Convert.ToInt32(cmd.Parameters["P_TOTAL"].Value.ToString());
                            _returnDelivery.ListDeliveryReport = listDeliveryDetail;
                        }
                    }
                    else
                    {
                        _returnDelivery.Code = "01";
                        _returnDelivery.Message = "Không tồn tại bản ghi nào.";
                        _returnDelivery.Total = 0;
                        _returnDelivery.ListDeliveryReport = null;
                    }
                }
            }
            catch
            {
                _returnDelivery.Code = "99";
                _returnDelivery.Message = "Lỗi xử lý dữ liệu";
                _returnDelivery.Total = 0;
                _returnDelivery.ListDeliveryReport = null;
            }
            return _returnDelivery;
        }
        #endregion

        #region DELIVERY_SUMMARY
        public ReturnSummaryDelivery COME_DELIVERY_SUMMARY(int channel, int post_man, int status, int from_date, int to_date, int delivery_post_code, int delivery_route_code)
        {
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnSummaryDelivery _returnSummaryDelivery = new ReturnSummaryDelivery();
            switch (channel)
            {
                case 0:
                    _metadata.channel = "Tất cả"; break;
                case 1:
                    _metadata.channel = "EmsEnterprise"; break;
                case 2:
                    _metadata.channel = "SmartPhone"; break;
            }
            if (post_man == 0)
                _metadata.postman = "Tất cả";
            else
                _metadata.postman = "Bưu tá: " + post_man.ToString();

            switch (status)
            {
                case 0:
                    _metadata.status = "Tất cả"; break;
                case 1:
                    _metadata.status = "Thành công"; break;
                case 2:
                    _metadata.status = "Không thành công"; break;
            }
            _metadata.from_to_date = "Từ ngày " + common.Convert_Date(from_date) + " đến ngày " + common.Convert_Date(to_date);
            if (delivery_post_code == 0)
                _metadata.delivery_post_code = "Tất cả";
            else
                _metadata.delivery_post_code = "Bưu cục phát: " + delivery_post_code;

            if (delivery_route_code == 0)
                _metadata.delivery_route_code = "Tất cả";
            else
                _metadata.delivery_route_code = "Tuyến phát: " + delivery_route_code;
            _returnSummaryDelivery.MetaData = _metadata;
            try
            {
                _returnSummaryDelivery.ComeDeliverySummary = ComeDeliveryTotal(channel, post_man, status, from_date, to_date, delivery_post_code, delivery_route_code);
                _returnSummaryDelivery.RemainDeliverySummary = RemainDeliveryTotal(channel, post_man, status, from_date, to_date, delivery_post_code, delivery_route_code);
                _returnSummaryDelivery.SuccessDeliverySummary = SuccessDeliveryTotal(channel, post_man, status, from_date, to_date, delivery_post_code, delivery_route_code); ;
                _returnSummaryDelivery.UnSuccessDeliverySummary = UnSuccessDeliveryTotal(channel, post_man, status, from_date, to_date, delivery_post_code, delivery_route_code); ;
                _returnSummaryDelivery.ReturnDeliverySummary = ReturnDeliveryTotal(channel, post_man, status, from_date, to_date, delivery_post_code, delivery_route_code); ;
            }
            catch
            {
                _returnSummaryDelivery.Code = "99";
                _returnSummaryDelivery.Message = "Lỗi xử lý dữ liệu";

            }
            return _returnSummaryDelivery;
        }
        #endregion

        #region ComeDeliveryTotal
        protected ComeDeliverySummary ComeDeliveryTotal(int channel, int post_man, int status, int from_date, int to_date, int delivery_post_code, int delivery_route_code)
        {
            ComeDeliverySummary comedelivery = null;
            using (OracleCommand cmd = new OracleCommand())
            {
                try
                {
                    cmd.Connection = Helper.OraDCOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "DELIVERY_PKG.COME_DELIVERY_TOTAL";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new OracleParameter("P_DELIVERY_POST_CODE", OracleDbType.Int32)).Value = delivery_post_code;
                    cmd.Parameters.Add(new OracleParameter("P_DELIVERY_ROUTE_CODE", OracleDbType.Int32)).Value = delivery_route_code;
                    cmd.Parameters.Add(new OracleParameter("P_STATUS", OracleDbType.Int32)).Value = status;
                    cmd.Parameters.Add(new OracleParameter("P_FROM_DATE", OracleDbType.Int32)).Value = from_date;
                    cmd.Parameters.Add(new OracleParameter("P_TO_DATE", OracleDbType.Int32)).Value = to_date;
                    cmd.Parameters.Add(new OracleParameter("P_CHANNEL", OracleDbType.Int32)).Value = channel;
                    cmd.Parameters.Add(new OracleParameter("P_POSTMAN", OracleDbType.Int32)).Value = post_man;
                    cmd.Parameters.Add("P_RETURN_CODE", OracleDbType.Int32, 0, ParameterDirection.Output);
                    cmd.Parameters.Add("P_OUT_CURSOR", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    OracleDataReader dr = Helper.ExecuteDataReader(cmd, Helper.OraDCOracleConnection);
                    if (float.Parse(cmd.Parameters["P_RETURN_CODE"].Value.ToString()) > 0)
                    {
                        if (dr.HasRows)
                        {
                            comedelivery = new ComeDeliverySummary();
                            while (dr.Read())
                            {
                                comedelivery.COUNT = int.Parse(dr["COUNT"].ToString());
                                comedelivery.TOTAL_WEIGHT = int.Parse(dr["TOTAL_WEIGHT"].ToString());
                                comedelivery.TOTAL_AMOUNT = int.Parse(dr["TOTAL_AMOUNT"].ToString());
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    comedelivery = null;
                }
                return comedelivery;
            }
        }
        #endregion

        #region RemainDeliveryTotal
        protected RemainDeliverySummary RemainDeliveryTotal(int channel, int post_man, int status, int from_date, int to_date, int delivery_post_code, int delivery_route_code)
        {
            RemainDeliverySummary remaindelivery = null;
            using (OracleCommand cmd = new OracleCommand())
            {
                try
                {
                    cmd.Connection = Helper.OraDCOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "DELIVERY_PKG.REMAIN_DELIVERY_TOTAL";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new OracleParameter("P_DELIVERY_POST_CODE", OracleDbType.Int32)).Value = delivery_post_code;
                    cmd.Parameters.Add(new OracleParameter("P_DELIVERY_ROUTE_CODE", OracleDbType.Int32)).Value = delivery_route_code;
                    cmd.Parameters.Add(new OracleParameter("P_STATUS", OracleDbType.Int32)).Value = status;
                    cmd.Parameters.Add(new OracleParameter("P_FROM_DATE", OracleDbType.Int32)).Value = from_date;
                    cmd.Parameters.Add(new OracleParameter("P_TO_DATE", OracleDbType.Int32)).Value = to_date;
                    cmd.Parameters.Add(new OracleParameter("P_CHANNEL", OracleDbType.Int32)).Value = channel;
                    cmd.Parameters.Add(new OracleParameter("P_POSTMAN", OracleDbType.Int32)).Value = post_man;
                    cmd.Parameters.Add("P_RETURN_CODE", OracleDbType.Int32, 0, ParameterDirection.Output);
                    cmd.Parameters.Add("P_OUT_CURSOR", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    OracleDataReader dr = Helper.ExecuteDataReader(cmd, Helper.OraDCOracleConnection);
                    if (float.Parse(cmd.Parameters["P_RETURN_CODE"].Value.ToString()) > 0)
                    {
                        if (dr.HasRows)
                        {
                            remaindelivery = new RemainDeliverySummary();
                            while (dr.Read())
                            {
                                remaindelivery.COUNT = int.Parse(dr["COUNT"].ToString());
                                remaindelivery.TOTAL_WEIGHT = int.Parse(dr["TOTAL_WEIGHT"].ToString());
                                remaindelivery.TOTAL_AMOUNT = int.Parse(dr["TOTAL_AMOUNT"].ToString());
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    remaindelivery = null;
                }
                return remaindelivery;
            }
        }
        #endregion

        #region SuccessDeliveryTotal
        protected SuccessDeliverySummary SuccessDeliveryTotal(int channel, int post_man, int status, int from_date, int to_date, int delivery_post_code, int delivery_route_code)
        {
            SuccessDeliverySummary successdelivery = null;
            using (OracleCommand cmd = new OracleCommand())
            {
                try
                {
                    cmd.Connection = Helper.OraDCOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "DELIVERY_PKG.SUCCESS_DELIVERY_TOTAL";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new OracleParameter("P_DELIVERY_POST_CODE", OracleDbType.Int32)).Value = delivery_post_code;
                    cmd.Parameters.Add(new OracleParameter("P_DELIVERY_ROUTE_CODE", OracleDbType.Int32)).Value = delivery_route_code;
                    cmd.Parameters.Add(new OracleParameter("P_STATUS", OracleDbType.Int32)).Value = status;
                    cmd.Parameters.Add(new OracleParameter("P_FROM_DATE", OracleDbType.Int32)).Value = from_date;
                    cmd.Parameters.Add(new OracleParameter("P_TO_DATE", OracleDbType.Int32)).Value = to_date;
                    cmd.Parameters.Add(new OracleParameter("P_CHANNEL", OracleDbType.Int32)).Value = channel;
                    cmd.Parameters.Add(new OracleParameter("P_POSTMAN", OracleDbType.Int32)).Value = post_man;
                    cmd.Parameters.Add("P_RETURN_CODE", OracleDbType.Int32, 0, ParameterDirection.Output);
                    cmd.Parameters.Add("P_OUT_CURSOR", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    OracleDataReader dr = Helper.ExecuteDataReader(cmd, Helper.OraDCOracleConnection);
                    if (float.Parse(cmd.Parameters["P_RETURN_CODE"].Value.ToString()) > 0)
                    {
                        if (dr.HasRows)
                        {
                            successdelivery = new SuccessDeliverySummary();
                            while (dr.Read())
                            {
                                successdelivery.COUNT = int.Parse(dr["COUNT"].ToString());
                                successdelivery.TOTAL_WEIGHT = int.Parse(dr["TOTAL_WEIGHT"].ToString());
                                successdelivery.TOTAL_AMOUNT = int.Parse(dr["TOTAL_AMOUNT"].ToString());
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    successdelivery = null;
                }
                return successdelivery;
            }
        }
        #endregion

        #region UnSuccessDeliveryTotal
        protected UnSuccessDeliverySummary UnSuccessDeliveryTotal(int channel, int post_man, int status, int from_date, int to_date, int delivery_post_code, int delivery_route_code)
        {
            UnSuccessDeliverySummary unsuccessdelivery = null;
            using (OracleCommand cmd = new OracleCommand())
            {
                try
                {
                    cmd.Connection = Helper.OraDCOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "DELIVERY_PKG.UNSUCCESS_DELIVERY_TOTAL";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new OracleParameter("P_DELIVERY_POST_CODE", OracleDbType.Int32)).Value = delivery_post_code;
                    cmd.Parameters.Add(new OracleParameter("P_DELIVERY_ROUTE_CODE", OracleDbType.Int32)).Value = delivery_route_code;
                    cmd.Parameters.Add(new OracleParameter("P_STATUS", OracleDbType.Int32)).Value = status;
                    cmd.Parameters.Add(new OracleParameter("P_FROM_DATE", OracleDbType.Int32)).Value = from_date;
                    cmd.Parameters.Add(new OracleParameter("P_TO_DATE", OracleDbType.Int32)).Value = to_date;
                    cmd.Parameters.Add(new OracleParameter("P_CHANNEL", OracleDbType.Int32)).Value = channel;
                    cmd.Parameters.Add(new OracleParameter("P_POSTMAN", OracleDbType.Int32)).Value = post_man;
                    cmd.Parameters.Add("P_RETURN_CODE", OracleDbType.Int32, 0, ParameterDirection.Output);
                    cmd.Parameters.Add("P_OUT_CURSOR", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    OracleDataReader dr = Helper.ExecuteDataReader(cmd, Helper.OraDCOracleConnection);
                    if (float.Parse(cmd.Parameters["P_RETURN_CODE"].Value.ToString()) > 0)
                    {
                        if (dr.HasRows)
                        {
                            unsuccessdelivery = new UnSuccessDeliverySummary();
                            while (dr.Read())
                            {
                                unsuccessdelivery.COUNT = int.Parse(dr["COUNT"].ToString());
                                unsuccessdelivery.TOTAL_WEIGHT = int.Parse(dr["TOTAL_WEIGHT"].ToString());
                                unsuccessdelivery.TOTAL_AMOUNT = int.Parse(dr["TOTAL_AMOUNT"].ToString());
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    unsuccessdelivery = null;
                }
                return unsuccessdelivery;
            }
        }
        #endregion

        #region ReturnDeliveryTotal
        protected ReturnDeliverySummary ReturnDeliveryTotal(int channel, int post_man, int status, int from_date, int to_date, int delivery_post_code, int delivery_route_code)
        {
            ReturnDeliverySummary returndelivery = null;
            using (OracleCommand cmd = new OracleCommand())
            {
                try
                {
                    cmd.Connection = Helper.OraDCOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "DELIVERY_PKG.RETURN_DELIVERY_TOTAL";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new OracleParameter("P_DELIVERY_POST_CODE", OracleDbType.Int32)).Value = delivery_post_code;
                    cmd.Parameters.Add(new OracleParameter("P_DELIVERY_ROUTE_CODE", OracleDbType.Int32)).Value = delivery_route_code;
                    cmd.Parameters.Add(new OracleParameter("P_STATUS", OracleDbType.Int32)).Value = status;
                    cmd.Parameters.Add(new OracleParameter("P_FROM_DATE", OracleDbType.Int32)).Value = from_date;
                    cmd.Parameters.Add(new OracleParameter("P_TO_DATE", OracleDbType.Int32)).Value = to_date;
                    cmd.Parameters.Add(new OracleParameter("P_CHANNEL", OracleDbType.Int32)).Value = channel;
                    cmd.Parameters.Add(new OracleParameter("P_POSTMAN", OracleDbType.Int32)).Value = post_man;
                    cmd.Parameters.Add("P_RETURN_CODE", OracleDbType.Int32, 0, ParameterDirection.Output);
                    cmd.Parameters.Add("P_OUT_CURSOR", OracleDbType.RefCursor, null, ParameterDirection.Output);
                    OracleDataReader dr = Helper.ExecuteDataReader(cmd, Helper.OraDCOracleConnection);
                    if (float.Parse(cmd.Parameters["P_RETURN_CODE"].Value.ToString()) > 0)
                    {
                        if (dr.HasRows)
                        {
                            returndelivery = new ReturnDeliverySummary();
                            while (dr.Read())
                            {
                                returndelivery.COUNT = int.Parse(dr["COUNT"].ToString());
                                returndelivery.TOTAL_WEIGHT = int.Parse(dr["TOTAL_WEIGHT"].ToString());
                                returndelivery.TOTAL_AMOUNT = int.Parse(dr["TOTAL_AMOUNT"].ToString());
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    returndelivery = null;
                }
                return returndelivery;
            }
        }
        #endregion
    }
}