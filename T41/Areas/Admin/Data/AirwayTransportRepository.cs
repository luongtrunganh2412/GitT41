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
    public class AirwayTransportRepository
    {
        //Phần chi tiết của bảng Tổng Hợp Dữ Liệu
        #region TOTAL_DATA          
        public ReturnAirwayTransport TOTAL_DATA(int date)
        {
            ReturnAirwayTransport oAirwayTransportDetail = new ReturnAirwayTransport();
            OracleCommand cmd;
            try
            {
                using (cmd = new OracleCommand())
                {
                    cmd.Connection = Helper.OraDSOracleConnection;
                    cmd.CommandText = Helper.SchemaName + "ems.merger_ds_mailtrip";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("v_Date", OracleDbType.Int32, ParameterDirection.Input).Value = date;
                    cmd.ExecuteNonQuery();
                    oAirwayTransportDetail.Code = "00";
                    oAirwayTransportDetail.Message = "Tổng Hợp Dữ Liệu Thành Công";
                    
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, ex.Message);
                oAirwayTransportDetail = null;
            }
            return oAirwayTransportDetail;
        }

        #endregion


        //Phần chi tiết của bảng LOAD_DATA1
        #region LOAD_DATA1          
        public ReturnAirwayTransport LOAD_DATA1( int date, int way)
        {
            
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnAirwayTransport _returnAirwayTransport = new ReturnAirwayTransport();

            
            List<AirwayTransportDetail> listAirwayTransportDetail = null;
            AirwayTransportDetail oAirwayTransportDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    //Helper.OraDSOracleConnection gọi đến database đối soát
                    OracleCommand myCommand = new OracleCommand("AIRWAY_TRANSPORT_2.LOAD_DATA1", Helper.OraDSOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("P_CHIEU", OracleDbType.Int32).Value = way;
                    myCommand.Parameters.Add("P_NGAY", OracleDbType.Int32).Value = date;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listAirwayTransportDetail = new List<AirwayTransportDetail>();
                        while (dr.Read())
                        {
                            oAirwayTransportDetail = new AirwayTransportDetail();
                            oAirwayTransportDetail.STT = Convert.ToInt32(dr["STT"].ToString());
                            oAirwayTransportDetail.CHANGBAY =  dr["CHANGBAY"].ToString();
                            oAirwayTransportDetail.DONVIVANCHUYEN = dr["DONVIVANCHUYEN"].ToString();
                            oAirwayTransportDetail.DICHVUVANCHUYEN = dr["DICHVUVANCHUYEN"].ToString();
                            oAirwayTransportDetail.KHUNGGIO = dr["KHUNGGIO"].ToString();
                            oAirwayTransportDetail.TONGTAITHEOHOPDONG = dr["TONGTAITHEOHOPDONG"].ToString();
                            //oAirwayTransportDetail.TAICUNG_KH = string.IsNullOrEmpty(dr["TAICUNG_KH"].ToString()) ? 0 : Convert.ToInt32(dr["TAICUNG_KH"].ToString());
                            oAirwayTransportDetail.TAICUNG_KH = dr["TAICUNG_KH"].ToString();
                            oAirwayTransportDetail.TAIMEM_KH = dr["TAIMEM_KH"].ToString();
                            oAirwayTransportDetail.MASP = dr["MASP"].ToString();
                            oAirwayTransportDetail.TAICUNG_TH = dr["TAICUNG_TH"].ToString();
                            oAirwayTransportDetail.TAIMEM_TH = dr["TAIMEM_TH"].ToString();
                            oAirwayTransportDetail.TAICUNG_LK = dr["TAICUNG_LK"].ToString();
                            oAirwayTransportDetail.TAIMEM_LK = dr["TAIMEM_LK"].ToString();
                            oAirwayTransportDetail.GIOGIAO_QD = dr["GIOGIAO_QD"].ToString();
                            oAirwayTransportDetail.GIOBAY_QD = dr["GIOBAY_QD"].ToString();
                            oAirwayTransportDetail.GIOGIAO_TT = dr["GIOGIAO_TT"].ToString();
                            oAirwayTransportDetail.GIOBAY_TT = dr["GIOBAY_TT"].ToString();
                            oAirwayTransportDetail.SOHIEUCHUYENBAY = dr["SOHIEUCHUYENBAY"].ToString();
                            oAirwayTransportDetail.GIOGIAO_CL = dr["GIOGIAO_CL"].ToString();
                            oAirwayTransportDetail.GIOBAY_CL = dr["GIOBAY_CL"].ToString();
                            oAirwayTransportDetail.GIODAP_QD = dr["GIODAP_QD"].ToString();
                            oAirwayTransportDetail.GIONHAN_QD = dr["GIONHAN_QD"].ToString();
                            oAirwayTransportDetail.GIONHAN_TT = dr["GIONHAN_TT"].ToString();
                            oAirwayTransportDetail.GIONHAN_CL = dr["GIONHAN_CL"].ToString();
                            listAirwayTransportDetail.Add(oAirwayTransportDetail);

                            
                        }
                        _returnAirwayTransport.Code = "00";
                        _returnAirwayTransport.Message = "Lấy dữ liệu thành công.";
                        _returnAirwayTransport.ListAirwayTransportReport = listAirwayTransportDetail;
                    }
                    else
                    {
                        _returnAirwayTransport.Code = "01";
                        _returnAirwayTransport.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _returnAirwayTransport.Code = "99";
                _returnAirwayTransport.Message = "Lỗi xử lý dữ liệu";
                
            }
            return _returnAirwayTransport;
        }



        #endregion

        //Phần chi tiết của bảng LOAD_DATA2
        #region LOAD_DATA2          
        public ReturnAirwayTransport LOAD_DATA2(int date, int way)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnAirwayTransport _returnAirwayTransport = new ReturnAirwayTransport();


            List<AirwayTransportDetail> listAirwayTransportDetail = null;
            AirwayTransportDetail oAirwayTransportDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    //Helper.OraDSOracleConnection gọi đến database đối soát
                    OracleCommand myCommand = new OracleCommand("AIRWAY_TRANSPORT_2.LOAD_DATA2", Helper.OraDSOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("P_CHIEU", OracleDbType.Int32).Value = way;
                    myCommand.Parameters.Add("P_NGAY", OracleDbType.Int32).Value = date;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listAirwayTransportDetail = new List<AirwayTransportDetail>();
                        while (dr.Read())
                        {
                            oAirwayTransportDetail = new AirwayTransportDetail();
                            //oAirwayTransportDetail.SO = Convert.ToInt32(dr["1"].ToString());
                            oAirwayTransportDetail.STT = Convert.ToInt32(dr["STT"].ToString());
                            oAirwayTransportDetail.CHANGBAY = dr["CHANGBAY"].ToString();
                            oAirwayTransportDetail.DONVIVANCHUYEN = dr["DONVIVANCHUYEN"].ToString();
                            oAirwayTransportDetail.DICHVUVANCHUYEN = dr["DICHVUVANCHUYEN"].ToString();
                            oAirwayTransportDetail.KHUNGGIO = dr["KHUNGGIO"].ToString();
                            oAirwayTransportDetail.TONGTAITHEOHOPDONG = dr["TONGTAITHEOHOPDONG"].ToString();
                            //oAirwayTransportDetail.ID = Convert.ToInt32(dr["ID"].ToString());
                            //oAirwayTransportDetail.NGAY = Convert.ToInt32(dr["NGAY"].ToString());
                            //oAirwayTransportDetail.MABC = Convert.ToInt32(dr["MABC"].ToString());
                            //oAirwayTransportDetail.CHIEU = Convert.ToInt32(dr["CHIEU"].ToString());
                            //oAirwayTransportDetail.HUONG = Convert.ToInt32(dr["HUONG"].ToString());
                            oAirwayTransportDetail.TAICUNG_KH = dr["TAICUNG_KH"].ToString();
                            oAirwayTransportDetail.TAIMEM_KH = dr["TAIMEM_KH"].ToString();
                            oAirwayTransportDetail.MASP = dr["MASP"].ToString();
                            oAirwayTransportDetail.TAICUNG_TH = dr["TAICUNG_TH"].ToString();
                            oAirwayTransportDetail.TAIMEM_TH = dr["TAIMEM_TH"].ToString();
                            oAirwayTransportDetail.TAICUNG_LK = dr["TAICUNG_LK"].ToString();
                            oAirwayTransportDetail.TAIMEM_LK = dr["TAIMEM_LK"].ToString();
                            oAirwayTransportDetail.GIOGIAO_QD = dr["GIOGIAO_QD"].ToString();
                            oAirwayTransportDetail.GIOBAY_QD = dr["GIOBAY_QD"].ToString();
                            oAirwayTransportDetail.GIOGIAO_TT = dr["GIOGIAO_TT"].ToString();
                            oAirwayTransportDetail.GIOBAY_TT = dr["GIOBAY_TT"].ToString();
                            oAirwayTransportDetail.SOHIEUCHUYENBAY = dr["SOHIEUCHUYENBAY"].ToString();
                            oAirwayTransportDetail.GIOGIAO_CL = dr["GIOGIAO_CL"].ToString();
                            oAirwayTransportDetail.GIOBAY_CL = dr["GIOBAY_CL"].ToString();
                            oAirwayTransportDetail.GIODAP_QD = dr["GIODAP_QD"].ToString();
                            oAirwayTransportDetail.GIONHAN_QD = dr["GIONHAN_QD"].ToString();
                            //oAirwayTransportDetail.GIODAP_TT = dr["GIODAP_TT"].ToString();
                            oAirwayTransportDetail.GIONHAN_TT = dr["GIONHAN_TT"].ToString();
                            oAirwayTransportDetail.GIONHAN_CL = dr["GIONHAN_CL"].ToString();
                            listAirwayTransportDetail.Add(oAirwayTransportDetail);

                        }
                        _returnAirwayTransport.Code = "00";
                        _returnAirwayTransport.Message = "Lấy dữ liệu thành công.";
                        _returnAirwayTransport.ListAirwayTransportReport = listAirwayTransportDetail;
                    }
                    else
                    {
                        _returnAirwayTransport.Code = "01";
                        _returnAirwayTransport.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _returnAirwayTransport.Code = "99";
                _returnAirwayTransport.Message = "Lỗi xử lý dữ liệu";

            }
            return _returnAirwayTransport;
        }



        #endregion

        //Phần chi tiết của bảng LOAD_DATA3
        #region LOAD_DATA3          
        public ReturnAirwayTransport LOAD_DATA3(int date, int way)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnAirwayTransport _returnAirwayTransport = new ReturnAirwayTransport();


            List<AirwayTransportDetail> listAirwayTransportDetail = null;
            AirwayTransportDetail oAirwayTransportDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    //Helper.OraDSOracleConnection gọi đến database đối soát
                    OracleCommand myCommand = new OracleCommand("AIRWAY_TRANSPORT_2.LOAD_DATA3", Helper.OraDSOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("P_CHIEU", OracleDbType.Int32).Value = way;
                    myCommand.Parameters.Add("P_NGAY", OracleDbType.Int32).Value = date;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listAirwayTransportDetail = new List<AirwayTransportDetail>();
                        while (dr.Read())
                        {
                            oAirwayTransportDetail = new AirwayTransportDetail();
                            //oAirwayTransportDetail.SO = Convert.ToInt32(dr["1"].ToString());
                            oAirwayTransportDetail.STT = Convert.ToInt32(dr["STT"].ToString());
                            oAirwayTransportDetail.CHANGBAY = dr["CHANGBAY"].ToString();
                            oAirwayTransportDetail.DONVIVANCHUYEN = dr["DONVIVANCHUYEN"].ToString();
                            oAirwayTransportDetail.DICHVUVANCHUYEN = dr["DICHVUVANCHUYEN"].ToString();
                            oAirwayTransportDetail.KHUNGGIO = dr["KHUNGGIO"].ToString();
                            oAirwayTransportDetail.TONGTAITHEOHOPDONG = dr["TONGTAITHEOHOPDONG"].ToString();
                            //oAirwayTransportDetail.ID = Convert.ToInt32(dr["ID"].ToString());
                            //oAirwayTransportDetail.NGAY = Convert.ToInt32(dr["NGAY"].ToString());
                            //oAirwayTransportDetail.MABC = Convert.ToInt32(dr["MABC"].ToString());
                            //oAirwayTransportDetail.CHIEU = Convert.ToInt32(dr["CHIEU"].ToString());
                            //oAirwayTransportDetail.HUONG = Convert.ToInt32(dr["HUONG"].ToString());
                            oAirwayTransportDetail.TAICUNG_KH = dr["TAICUNG_KH"].ToString();
                            oAirwayTransportDetail.TAIMEM_KH = dr["TAIMEM_KH"].ToString();
                            oAirwayTransportDetail.MASP = dr["MASP"].ToString();
                            oAirwayTransportDetail.TAICUNG_TH = dr["TAICUNG_TH"].ToString();
                            oAirwayTransportDetail.TAIMEM_TH = dr["TAIMEM_TH"].ToString();
                            oAirwayTransportDetail.TAICUNG_LK = dr["TAICUNG_LK"].ToString();
                            oAirwayTransportDetail.TAIMEM_LK = dr["TAIMEM_LK"].ToString();
                            oAirwayTransportDetail.GIOGIAO_QD = dr["GIOGIAO_QD"].ToString();
                            oAirwayTransportDetail.GIOBAY_QD = dr["GIOBAY_QD"].ToString();
                            oAirwayTransportDetail.GIOGIAO_TT = dr["GIOGIAO_TT"].ToString();
                            oAirwayTransportDetail.GIOBAY_TT = dr["GIOBAY_TT"].ToString();
                            oAirwayTransportDetail.SOHIEUCHUYENBAY = dr["SOHIEUCHUYENBAY"].ToString();
                            oAirwayTransportDetail.GIOGIAO_CL = dr["GIOGIAO_CL"].ToString();
                            oAirwayTransportDetail.GIOBAY_CL = dr["GIOBAY_CL"].ToString();
                            oAirwayTransportDetail.GIODAP_QD = dr["GIODAP_QD"].ToString();
                            oAirwayTransportDetail.GIONHAN_QD = dr["GIONHAN_QD"].ToString();
                            //oAirwayTransportDetail.GIODAP_TT = dr["GIODAP_TT"].ToString();
                            oAirwayTransportDetail.GIONHAN_TT = dr["GIONHAN_TT"].ToString();
                            oAirwayTransportDetail.GIONHAN_CL = dr["GIONHAN_CL"].ToString();
                            listAirwayTransportDetail.Add(oAirwayTransportDetail);

                        }
                        _returnAirwayTransport.Code = "00";
                        _returnAirwayTransport.Message = "Lấy dữ liệu thành công.";
                        _returnAirwayTransport.ListAirwayTransportReport = listAirwayTransportDetail;
                    }
                    else
                    {
                        _returnAirwayTransport.Code = "01";
                        _returnAirwayTransport.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _returnAirwayTransport.Code = "99";
                _returnAirwayTransport.Message = "Lỗi xử lý dữ liệu";

            }
            return _returnAirwayTransport;
        }



        #endregion

        //Phần chi tiết của bảng LOAD_DATA4
        #region LOAD_DATA4          
        public ReturnAirwayTransport LOAD_DATA4(int date, int way)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnAirwayTransport _returnAirwayTransport = new ReturnAirwayTransport();


            List<AirwayTransportDetail> listAirwayTransportDetail = null;
            AirwayTransportDetail oAirwayTransportDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    //Helper.OraDSOracleConnection gọi đến database đối soát
                    OracleCommand myCommand = new OracleCommand("AIRWAY_TRANSPORT_2.LOAD_DATA4", Helper.OraDSOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("P_CHIEU", OracleDbType.Int32).Value = way;
                    myCommand.Parameters.Add("P_NGAY", OracleDbType.Int32).Value = date;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listAirwayTransportDetail = new List<AirwayTransportDetail>();
                        while (dr.Read())
                        {
                            oAirwayTransportDetail = new AirwayTransportDetail();
                            //oAirwayTransportDetail.SO = Convert.ToInt32(dr["1"].ToString());
                            oAirwayTransportDetail.STT = Convert.ToInt32(dr["STT"].ToString());
                            oAirwayTransportDetail.CHANGBAY = dr["CHANGBAY"].ToString();
                            oAirwayTransportDetail.DONVIVANCHUYEN = dr["DONVIVANCHUYEN"].ToString();
                            oAirwayTransportDetail.DICHVUVANCHUYEN = dr["DICHVUVANCHUYEN"].ToString();
                            oAirwayTransportDetail.KHUNGGIO = dr["KHUNGGIO"].ToString();
                            oAirwayTransportDetail.TONGTAITHEOHOPDONG = dr["TONGTAITHEOHOPDONG"].ToString();
                            //oAirwayTransportDetail.ID = Convert.ToInt32(dr["ID"].ToString());
                            //oAirwayTransportDetail.NGAY = Convert.ToInt32(dr["NGAY"].ToString());
                            //oAirwayTransportDetail.MABC = Convert.ToInt32(dr["MABC"].ToString());
                            //oAirwayTransportDetail.CHIEU = Convert.ToInt32(dr["CHIEU"].ToString());
                            //oAirwayTransportDetail.HUONG = Convert.ToInt32(dr["HUONG"].ToString());
                            oAirwayTransportDetail.TAICUNG_KH = dr["TAICUNG_KH"].ToString();
                            oAirwayTransportDetail.TAIMEM_KH = dr["TAIMEM_KH"].ToString();
                            oAirwayTransportDetail.MASP = dr["MASP"].ToString();
                            oAirwayTransportDetail.TAICUNG_TH = dr["TAICUNG_TH"].ToString();
                            oAirwayTransportDetail.TAIMEM_TH = dr["TAIMEM_TH"].ToString();
                            oAirwayTransportDetail.TAICUNG_LK = dr["TAICUNG_LK"].ToString();
                            oAirwayTransportDetail.TAIMEM_LK = dr["TAIMEM_LK"].ToString();
                            oAirwayTransportDetail.GIOGIAO_QD = dr["GIOGIAO_QD"].ToString();
                            oAirwayTransportDetail.GIOBAY_QD = dr["GIOBAY_QD"].ToString();
                            oAirwayTransportDetail.GIOGIAO_TT = dr["GIOGIAO_TT"].ToString();
                            oAirwayTransportDetail.GIOBAY_TT = dr["GIOBAY_TT"].ToString();
                            oAirwayTransportDetail.SOHIEUCHUYENBAY = dr["SOHIEUCHUYENBAY"].ToString();
                            oAirwayTransportDetail.GIOGIAO_CL = dr["GIOGIAO_CL"].ToString();
                            oAirwayTransportDetail.GIOBAY_CL = dr["GIOBAY_CL"].ToString();
                            oAirwayTransportDetail.GIODAP_QD = dr["GIODAP_QD"].ToString();
                            oAirwayTransportDetail.GIONHAN_QD = dr["GIONHAN_QD"].ToString();
                            //oAirwayTransportDetail.GIODAP_TT = dr["GIODAP_TT"].ToString();
                            oAirwayTransportDetail.GIONHAN_TT = dr["GIONHAN_TT"].ToString();
                            oAirwayTransportDetail.GIONHAN_CL = dr["GIONHAN_CL"].ToString();
                            listAirwayTransportDetail.Add(oAirwayTransportDetail);

                        }
                        _returnAirwayTransport.Code = "00";
                        _returnAirwayTransport.Message = "Lấy dữ liệu thành công.";
                        _returnAirwayTransport.ListAirwayTransportReport = listAirwayTransportDetail;
                    }
                    else
                    {
                        _returnAirwayTransport.Code = "01";
                        _returnAirwayTransport.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _returnAirwayTransport.Code = "99";
                _returnAirwayTransport.Message = "Lỗi xử lý dữ liệu";

            }
            return _returnAirwayTransport;
        }



        #endregion

        //Phần chi tiết của bảng LOAD_DATA5
        #region LOAD_DATA5          
        public ReturnAirwayTransport LOAD_DATA5(int date, int way)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnAirwayTransport _returnAirwayTransport = new ReturnAirwayTransport();


            List<AirwayTransportDetail> listAirwayTransportDetail = null;
            AirwayTransportDetail oAirwayTransportDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    //Helper.OraDSOracleConnection gọi đến database đối soát
                    OracleCommand myCommand = new OracleCommand("AIRWAY_TRANSPORT_2.LOAD_DATA5", Helper.OraDSOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("P_CHIEU", OracleDbType.Int32).Value = way;
                    myCommand.Parameters.Add("P_NGAY", OracleDbType.Int32).Value = date;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listAirwayTransportDetail = new List<AirwayTransportDetail>();
                        while (dr.Read())
                        {
                            oAirwayTransportDetail = new AirwayTransportDetail();
                            //oAirwayTransportDetail.SO = Convert.ToInt32(dr["1"].ToString());
                            oAirwayTransportDetail.STT = Convert.ToInt32(dr["STT"].ToString());
                            oAirwayTransportDetail.CHANGBAY = dr["CHANGBAY"].ToString();
                            oAirwayTransportDetail.DONVIVANCHUYEN = dr["DONVIVANCHUYEN"].ToString();
                            oAirwayTransportDetail.DICHVUVANCHUYEN = dr["DICHVUVANCHUYEN"].ToString();
                            oAirwayTransportDetail.KHUNGGIO = dr["KHUNGGIO"].ToString();
                            oAirwayTransportDetail.TONGTAITHEOHOPDONG = dr["TONGTAITHEOHOPDONG"].ToString();
                            //oAirwayTransportDetail.ID = Convert.ToInt32(dr["ID"].ToString());
                            //oAirwayTransportDetail.NGAY = Convert.ToInt32(dr["NGAY"].ToString());
                            //oAirwayTransportDetail.MABC = Convert.ToInt32(dr["MABC"].ToString());
                            //oAirwayTransportDetail.CHIEU = Convert.ToInt32(dr["CHIEU"].ToString());
                            //oAirwayTransportDetail.HUONG = Convert.ToInt32(dr["HUONG"].ToString());
                            oAirwayTransportDetail.TAICUNG_KH = dr["TAICUNG_KH"].ToString();
                            oAirwayTransportDetail.TAIMEM_KH = dr["TAIMEM_KH"].ToString();
                            oAirwayTransportDetail.MASP = dr["MASP"].ToString();
                            oAirwayTransportDetail.TAICUNG_TH = dr["TAICUNG_TH"].ToString();
                            oAirwayTransportDetail.TAIMEM_TH = dr["TAIMEM_TH"].ToString();
                            oAirwayTransportDetail.TAICUNG_LK = dr["TAICUNG_LK"].ToString();
                            oAirwayTransportDetail.TAIMEM_LK = dr["TAIMEM_LK"].ToString();
                            oAirwayTransportDetail.GIOGIAO_QD = dr["GIOGIAO_QD"].ToString();
                            oAirwayTransportDetail.GIOBAY_QD = dr["GIOBAY_QD"].ToString();
                            oAirwayTransportDetail.GIOGIAO_TT = dr["GIOGIAO_TT"].ToString();
                            oAirwayTransportDetail.GIOBAY_TT = dr["GIOBAY_TT"].ToString();
                            oAirwayTransportDetail.SOHIEUCHUYENBAY = dr["SOHIEUCHUYENBAY"].ToString();
                            oAirwayTransportDetail.GIOGIAO_CL = dr["GIOGIAO_CL"].ToString();
                            oAirwayTransportDetail.GIOBAY_CL = dr["GIOBAY_CL"].ToString();
                            oAirwayTransportDetail.GIODAP_QD = dr["GIODAP_QD"].ToString();
                            oAirwayTransportDetail.GIONHAN_QD = dr["GIONHAN_QD"].ToString();
                            //oAirwayTransportDetail.GIODAP_TT = dr["GIODAP_TT"].ToString();
                            oAirwayTransportDetail.GIONHAN_TT = dr["GIONHAN_TT"].ToString();
                            oAirwayTransportDetail.GIONHAN_CL = dr["GIONHAN_CL"].ToString();
                            listAirwayTransportDetail.Add(oAirwayTransportDetail);

                        }
                        _returnAirwayTransport.Code = "00";
                        _returnAirwayTransport.Message = "Lấy dữ liệu thành công.";
                        _returnAirwayTransport.ListAirwayTransportReport = listAirwayTransportDetail;
                    }
                    else
                    {
                        _returnAirwayTransport.Code = "01";
                        _returnAirwayTransport.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _returnAirwayTransport.Code = "99";
                _returnAirwayTransport.Message = "Lỗi xử lý dữ liệu";

            }
            return _returnAirwayTransport;
        }



        #endregion

        //Phần chi tiết của bảng LOAD_DATA6
        #region LOAD_DATA6          
        public ReturnAirwayTransport LOAD_DATA6(int date, int way)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnAirwayTransport _returnAirwayTransport = new ReturnAirwayTransport();


            List<AirwayTransportDetail> listAirwayTransportDetail = null;
            AirwayTransportDetail oAirwayTransportDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    //Helper.OraDSOracleConnection gọi đến database đối soát
                    OracleCommand myCommand = new OracleCommand("AIRWAY_TRANSPORT_2.LOAD_DATA6", Helper.OraDSOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("P_CHIEU", OracleDbType.Int32).Value = way;
                    myCommand.Parameters.Add("P_NGAY", OracleDbType.Int32).Value = date;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listAirwayTransportDetail = new List<AirwayTransportDetail>();
                        while (dr.Read())
                        {
                            oAirwayTransportDetail = new AirwayTransportDetail();
                            //oAirwayTransportDetail.SO = Convert.ToInt32(dr["1"].ToString());
                            oAirwayTransportDetail.STT = Convert.ToInt32(dr["STT"].ToString());
                            oAirwayTransportDetail.CHANGBAY = dr["CHANGBAY"].ToString();
                            oAirwayTransportDetail.DONVIVANCHUYEN = dr["DONVIVANCHUYEN"].ToString();
                            oAirwayTransportDetail.DICHVUVANCHUYEN = dr["DICHVUVANCHUYEN"].ToString();
                            oAirwayTransportDetail.KHUNGGIO = dr["KHUNGGIO"].ToString();
                            oAirwayTransportDetail.TONGTAITHEOHOPDONG = dr["TONGTAITHEOHOPDONG"].ToString();
                            //oAirwayTransportDetail.ID = Convert.ToInt32(dr["ID"].ToString());
                            //oAirwayTransportDetail.NGAY = Convert.ToInt32(dr["NGAY"].ToString());
                            //oAirwayTransportDetail.MABC = Convert.ToInt32(dr["MABC"].ToString());
                            //oAirwayTransportDetail.CHIEU = Convert.ToInt32(dr["CHIEU"].ToString());
                            //oAirwayTransportDetail.HUONG = Convert.ToInt32(dr["HUONG"].ToString());
                            oAirwayTransportDetail.TAICUNG_KH = dr["TAICUNG_KH"].ToString();
                            oAirwayTransportDetail.TAIMEM_KH = dr["TAIMEM_KH"].ToString();
                            oAirwayTransportDetail.MASP = dr["MASP"].ToString();
                            oAirwayTransportDetail.TAICUNG_TH = dr["TAICUNG_TH"].ToString();
                            oAirwayTransportDetail.TAIMEM_TH = dr["TAIMEM_TH"].ToString();
                            oAirwayTransportDetail.TAICUNG_LK = dr["TAICUNG_LK"].ToString();
                            oAirwayTransportDetail.TAIMEM_LK = dr["TAIMEM_LK"].ToString();
                            oAirwayTransportDetail.GIOGIAO_QD = dr["GIOGIAO_QD"].ToString();
                            oAirwayTransportDetail.GIOBAY_QD = dr["GIOBAY_QD"].ToString();
                            oAirwayTransportDetail.GIOGIAO_TT = dr["GIOGIAO_TT"].ToString();
                            oAirwayTransportDetail.GIOBAY_TT = dr["GIOBAY_TT"].ToString();
                            oAirwayTransportDetail.SOHIEUCHUYENBAY = dr["SOHIEUCHUYENBAY"].ToString();
                            oAirwayTransportDetail.GIOGIAO_CL = dr["GIOGIAO_CL"].ToString();
                            oAirwayTransportDetail.GIOBAY_CL = dr["GIOBAY_CL"].ToString();
                            oAirwayTransportDetail.GIODAP_QD = dr["GIODAP_QD"].ToString();
                            oAirwayTransportDetail.GIONHAN_QD = dr["GIONHAN_QD"].ToString();
                            //oAirwayTransportDetail.GIODAP_TT = dr["GIODAP_TT"].ToString();
                            oAirwayTransportDetail.GIONHAN_TT = dr["GIONHAN_TT"].ToString();
                            oAirwayTransportDetail.GIONHAN_CL = dr["GIONHAN_CL"].ToString();
                            listAirwayTransportDetail.Add(oAirwayTransportDetail);

                        }
                        _returnAirwayTransport.Code = "00";
                        _returnAirwayTransport.Message = "Lấy dữ liệu thành công.";
                        _returnAirwayTransport.ListAirwayTransportReport = listAirwayTransportDetail;
                    }
                    else
                    {
                        _returnAirwayTransport.Code = "01";
                        _returnAirwayTransport.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _returnAirwayTransport.Code = "99";
                _returnAirwayTransport.Message = "Lỗi xử lý dữ liệu";

            }
            return _returnAirwayTransport;
        }



        #endregion

        //Phần chi tiết của bảng LOAD_DATA7
        #region LOAD_DATA7          
        public ReturnAirwayTransport LOAD_DATA7(int date, int way)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnAirwayTransport _returnAirwayTransport = new ReturnAirwayTransport();


            List<AirwayTransportDetail> listAirwayTransportDetail = null;
            AirwayTransportDetail oAirwayTransportDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    //Helper.OraDSOracleConnection gọi đến database đối soát
                    OracleCommand myCommand = new OracleCommand("AIRWAY_TRANSPORT_2.LOAD_DATA7", Helper.OraDSOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("P_CHIEU", OracleDbType.Int32).Value = way;
                    myCommand.Parameters.Add("P_NGAY", OracleDbType.Int32).Value = date;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listAirwayTransportDetail = new List<AirwayTransportDetail>();
                        while (dr.Read())
                        {
                            oAirwayTransportDetail = new AirwayTransportDetail();
                            //oAirwayTransportDetail.SO = Convert.ToInt32(dr["1"].ToString());
                            oAirwayTransportDetail.STT = Convert.ToInt32(dr["STT"].ToString());
                            oAirwayTransportDetail.CHANGBAY = dr["CHANGBAY"].ToString();
                            oAirwayTransportDetail.DONVIVANCHUYEN = dr["DONVIVANCHUYEN"].ToString();
                            oAirwayTransportDetail.DICHVUVANCHUYEN = dr["DICHVUVANCHUYEN"].ToString();
                            oAirwayTransportDetail.KHUNGGIO = dr["KHUNGGIO"].ToString();
                            oAirwayTransportDetail.TONGTAITHEOHOPDONG = dr["TONGTAITHEOHOPDONG"].ToString();
                            //oAirwayTransportDetail.ID = Convert.ToInt32(dr["ID"].ToString());
                            //oAirwayTransportDetail.NGAY = Convert.ToInt32(dr["NGAY"].ToString());
                            //oAirwayTransportDetail.MABC = Convert.ToInt32(dr["MABC"].ToString());
                            //oAirwayTransportDetail.CHIEU = Convert.ToInt32(dr["CHIEU"].ToString());
                            //oAirwayTransportDetail.HUONG = Convert.ToInt32(dr["HUONG"].ToString());
                            oAirwayTransportDetail.TAICUNG_KH = dr["TAICUNG_KH"].ToString();
                            oAirwayTransportDetail.TAIMEM_KH = dr["TAIMEM_KH"].ToString();
                            oAirwayTransportDetail.MASP = dr["MASP"].ToString();
                            oAirwayTransportDetail.TAICUNG_TH = dr["TAICUNG_TH"].ToString();
                            oAirwayTransportDetail.TAIMEM_TH = dr["TAIMEM_TH"].ToString();
                            oAirwayTransportDetail.TAICUNG_LK = dr["TAICUNG_LK"].ToString();
                            oAirwayTransportDetail.TAIMEM_LK = dr["TAIMEM_LK"].ToString();
                            oAirwayTransportDetail.GIOGIAO_QD = dr["GIOGIAO_QD"].ToString();
                            oAirwayTransportDetail.GIOBAY_QD = dr["GIOBAY_QD"].ToString();
                            oAirwayTransportDetail.GIOGIAO_TT = dr["GIOGIAO_TT"].ToString();
                            oAirwayTransportDetail.GIOBAY_TT = dr["GIOBAY_TT"].ToString();
                            oAirwayTransportDetail.SOHIEUCHUYENBAY = dr["SOHIEUCHUYENBAY"].ToString();
                            oAirwayTransportDetail.GIOGIAO_CL = dr["GIOGIAO_CL"].ToString();
                            oAirwayTransportDetail.GIOBAY_CL = dr["GIOBAY_CL"].ToString();
                            oAirwayTransportDetail.GIODAP_QD = dr["GIODAP_QD"].ToString();
                            oAirwayTransportDetail.GIONHAN_QD = dr["GIONHAN_QD"].ToString();
                            //oAirwayTransportDetail.GIODAP_TT = dr["GIODAP_TT"].ToString();
                            oAirwayTransportDetail.GIONHAN_TT = dr["GIONHAN_TT"].ToString();
                            oAirwayTransportDetail.GIONHAN_CL = dr["GIONHAN_CL"].ToString();
                            listAirwayTransportDetail.Add(oAirwayTransportDetail);

                        }
                        _returnAirwayTransport.Code = "00";
                        _returnAirwayTransport.Message = "Lấy dữ liệu thành công.";
                        _returnAirwayTransport.ListAirwayTransportReport = listAirwayTransportDetail;
                    }
                    else
                    {
                        _returnAirwayTransport.Code = "01";
                        _returnAirwayTransport.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _returnAirwayTransport.Code = "99";
                _returnAirwayTransport.Message = "Lỗi xử lý dữ liệu";

            }
            return _returnAirwayTransport;
        }



        #endregion

        //Phần chi tiết của bảng LOAD_DATA8
        #region LOAD_DATA8          
        public ReturnAirwayTransport LOAD_DATA8(int date, int way)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnAirwayTransport _returnAirwayTransport = new ReturnAirwayTransport();


            List<AirwayTransportDetail> listAirwayTransportDetail = null;
            AirwayTransportDetail oAirwayTransportDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    //Helper.OraDSOracleConnection gọi đến database đối soát
                    OracleCommand myCommand = new OracleCommand("AIRWAY_TRANSPORT_2.LOAD_DATA8", Helper.OraDSOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("P_CHIEU", OracleDbType.Int32).Value = way;
                    myCommand.Parameters.Add("P_NGAY", OracleDbType.Int32).Value = date;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listAirwayTransportDetail = new List<AirwayTransportDetail>();
                        while (dr.Read())
                        {
                            oAirwayTransportDetail = new AirwayTransportDetail();
                            //oAirwayTransportDetail.SO = Convert.ToInt32(dr["1"].ToString());
                            oAirwayTransportDetail.STT = Convert.ToInt32(dr["STT"].ToString());
                            oAirwayTransportDetail.CHANGBAY = dr["CHANGBAY"].ToString();
                            oAirwayTransportDetail.DONVIVANCHUYEN = dr["DONVIVANCHUYEN"].ToString();
                            oAirwayTransportDetail.DICHVUVANCHUYEN = dr["DICHVUVANCHUYEN"].ToString();
                            oAirwayTransportDetail.KHUNGGIO = dr["KHUNGGIO"].ToString();
                            oAirwayTransportDetail.TONGTAITHEOHOPDONG = dr["TONGTAITHEOHOPDONG"].ToString();
                            //oAirwayTransportDetail.ID = Convert.ToInt32(dr["ID"].ToString());
                            //oAirwayTransportDetail.NGAY = Convert.ToInt32(dr["NGAY"].ToString());
                            //oAirwayTransportDetail.MABC = Convert.ToInt32(dr["MABC"].ToString());
                            //oAirwayTransportDetail.CHIEU = Convert.ToInt32(dr["CHIEU"].ToString());
                            //oAirwayTransportDetail.HUONG = Convert.ToInt32(dr["HUONG"].ToString());
                            oAirwayTransportDetail.TAICUNG_KH = dr["TAICUNG_KH"].ToString();
                            oAirwayTransportDetail.TAIMEM_KH = dr["TAIMEM_KH"].ToString();
                            oAirwayTransportDetail.MASP = dr["MASP"].ToString();
                            oAirwayTransportDetail.TAICUNG_TH = dr["TAICUNG_TH"].ToString();
                            oAirwayTransportDetail.TAIMEM_TH = dr["TAIMEM_TH"].ToString();
                            oAirwayTransportDetail.TAICUNG_LK = dr["TAICUNG_LK"].ToString();
                            oAirwayTransportDetail.TAIMEM_LK = dr["TAIMEM_LK"].ToString();
                            oAirwayTransportDetail.GIOGIAO_QD = dr["GIOGIAO_QD"].ToString();
                            oAirwayTransportDetail.GIOBAY_QD = dr["GIOBAY_QD"].ToString();
                            oAirwayTransportDetail.GIOGIAO_TT = dr["GIOGIAO_TT"].ToString();
                            oAirwayTransportDetail.GIOBAY_TT = dr["GIOBAY_TT"].ToString();
                            oAirwayTransportDetail.SOHIEUCHUYENBAY = dr["SOHIEUCHUYENBAY"].ToString();
                            oAirwayTransportDetail.GIOGIAO_CL = dr["GIOGIAO_CL"].ToString();
                            oAirwayTransportDetail.GIOBAY_CL = dr["GIOBAY_CL"].ToString();
                            oAirwayTransportDetail.GIODAP_QD = dr["GIODAP_QD"].ToString();
                            oAirwayTransportDetail.GIONHAN_QD = dr["GIONHAN_QD"].ToString();
                            //oAirwayTransportDetail.GIODAP_TT = dr["GIODAP_TT"].ToString();
                            oAirwayTransportDetail.GIONHAN_TT = dr["GIONHAN_TT"].ToString();
                            oAirwayTransportDetail.GIONHAN_CL = dr["GIONHAN_CL"].ToString();
                            listAirwayTransportDetail.Add(oAirwayTransportDetail);

                        }
                        _returnAirwayTransport.Code = "00";
                        _returnAirwayTransport.Message = "Lấy dữ liệu thành công.";
                        _returnAirwayTransport.ListAirwayTransportReport = listAirwayTransportDetail;
                    }
                    else
                    {
                        _returnAirwayTransport.Code = "01";
                        _returnAirwayTransport.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _returnAirwayTransport.Code = "99";
                _returnAirwayTransport.Message = "Lỗi xử lý dữ liệu";

            }
            return _returnAirwayTransport;
        }



        #endregion

        //Phần chi tiết của bảng LOAD_DATA9
        #region LOAD_DATA9          
        public ReturnAirwayTransport LOAD_DATA9(int date, int way)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnAirwayTransport _returnAirwayTransport = new ReturnAirwayTransport();


            List<AirwayTransportDetail> listAirwayTransportDetail = null;
            AirwayTransportDetail oAirwayTransportDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    //Helper.OraDSOracleConnection gọi đến database đối soát
                    OracleCommand myCommand = new OracleCommand("AIRWAY_TRANSPORT_2.LOAD_DATA9", Helper.OraDSOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("P_CHIEU", OracleDbType.Int32).Value = way;
                    myCommand.Parameters.Add("P_NGAY", OracleDbType.Int32).Value = date;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listAirwayTransportDetail = new List<AirwayTransportDetail>();
                        while (dr.Read())
                        {
                            oAirwayTransportDetail = new AirwayTransportDetail();
                            //oAirwayTransportDetail.SO = Convert.ToInt32(dr["1"].ToString());
                            oAirwayTransportDetail.STT = Convert.ToInt32(dr["STT"].ToString());
                            oAirwayTransportDetail.CHANGBAY = dr["CHANGBAY"].ToString();
                            oAirwayTransportDetail.DONVIVANCHUYEN = dr["DONVIVANCHUYEN"].ToString();
                            oAirwayTransportDetail.DICHVUVANCHUYEN = dr["DICHVUVANCHUYEN"].ToString();
                            oAirwayTransportDetail.KHUNGGIO = dr["KHUNGGIO"].ToString();
                            oAirwayTransportDetail.TONGTAITHEOHOPDONG = dr["TONGTAITHEOHOPDONG"].ToString();
                            //oAirwayTransportDetail.ID = Convert.ToInt32(dr["ID"].ToString());
                            //oAirwayTransportDetail.NGAY = Convert.ToInt32(dr["NGAY"].ToString());
                            //oAirwayTransportDetail.MABC = Convert.ToInt32(dr["MABC"].ToString());
                            //oAirwayTransportDetail.CHIEU = Convert.ToInt32(dr["CHIEU"].ToString());
                            //oAirwayTransportDetail.HUONG = Convert.ToInt32(dr["HUONG"].ToString());
                            oAirwayTransportDetail.TAICUNG_KH = dr["TAICUNG_KH"].ToString();
                            oAirwayTransportDetail.TAIMEM_KH = dr["TAIMEM_KH"].ToString();
                            oAirwayTransportDetail.MASP = dr["MASP"].ToString();
                            oAirwayTransportDetail.TAICUNG_TH = dr["TAICUNG_TH"].ToString();
                            oAirwayTransportDetail.TAIMEM_TH = dr["TAIMEM_TH"].ToString();
                            oAirwayTransportDetail.TAICUNG_LK = dr["TAICUNG_LK"].ToString();
                            oAirwayTransportDetail.TAIMEM_LK = dr["TAIMEM_LK"].ToString();
                            oAirwayTransportDetail.GIOGIAO_QD = dr["GIOGIAO_QD"].ToString();
                            oAirwayTransportDetail.GIOBAY_QD = dr["GIOBAY_QD"].ToString();
                            oAirwayTransportDetail.GIOGIAO_TT = dr["GIOGIAO_TT"].ToString();
                            oAirwayTransportDetail.GIOBAY_TT = dr["GIOBAY_TT"].ToString();
                            oAirwayTransportDetail.SOHIEUCHUYENBAY = dr["SOHIEUCHUYENBAY"].ToString();
                            oAirwayTransportDetail.GIOGIAO_CL = dr["GIOGIAO_CL"].ToString();
                            oAirwayTransportDetail.GIOBAY_CL = dr["GIOBAY_CL"].ToString();
                            oAirwayTransportDetail.GIODAP_QD = dr["GIODAP_QD"].ToString();
                            oAirwayTransportDetail.GIONHAN_QD = dr["GIONHAN_QD"].ToString();
                            //oAirwayTransportDetail.GIODAP_TT = dr["GIODAP_TT"].ToString();
                            oAirwayTransportDetail.GIONHAN_TT = dr["GIONHAN_TT"].ToString();
                            oAirwayTransportDetail.GIONHAN_CL = dr["GIONHAN_CL"].ToString();
                            listAirwayTransportDetail.Add(oAirwayTransportDetail);

                        }
                        _returnAirwayTransport.Code = "00";
                        _returnAirwayTransport.Message = "Lấy dữ liệu thành công.";
                        _returnAirwayTransport.ListAirwayTransportReport = listAirwayTransportDetail;
                    }
                    else
                    {
                        _returnAirwayTransport.Code = "01";
                        _returnAirwayTransport.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _returnAirwayTransport.Code = "99";
                _returnAirwayTransport.Message = "Lỗi xử lý dữ liệu";

            }
            return _returnAirwayTransport;
        }



        #endregion

        //Phần chi tiết của bảng LOAD_DATA10
        #region LOAD_DATA10          
        public ReturnAirwayTransport LOAD_DATA10(int date, int way)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnAirwayTransport _returnAirwayTransport = new ReturnAirwayTransport();


            List<AirwayTransportDetail> listAirwayTransportDetail = null;
            AirwayTransportDetail oAirwayTransportDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    //Helper.OraDSOracleConnection gọi đến database đối soát
                    OracleCommand myCommand = new OracleCommand("AIRWAY_TRANSPORT_2.LOAD_DATA10", Helper.OraDSOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("P_CHIEU", OracleDbType.Int32).Value = way;
                    myCommand.Parameters.Add("P_NGAY", OracleDbType.Int32).Value = date;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listAirwayTransportDetail = new List<AirwayTransportDetail>();
                        while (dr.Read())
                        {
                            oAirwayTransportDetail = new AirwayTransportDetail();
                            //oAirwayTransportDetail.SO = Convert.ToInt32(dr["1"].ToString());
                            oAirwayTransportDetail.STT = Convert.ToInt32(dr["STT"].ToString());
                            oAirwayTransportDetail.CHANGBAY = dr["CHANGBAY"].ToString();
                            oAirwayTransportDetail.DONVIVANCHUYEN = dr["DONVIVANCHUYEN"].ToString();
                            oAirwayTransportDetail.DICHVUVANCHUYEN = dr["DICHVUVANCHUYEN"].ToString();
                            oAirwayTransportDetail.KHUNGGIO = dr["KHUNGGIO"].ToString();
                            oAirwayTransportDetail.TONGTAITHEOHOPDONG = dr["TONGTAITHEOHOPDONG"].ToString();
                            //oAirwayTransportDetail.ID = Convert.ToInt32(dr["ID"].ToString());
                            //oAirwayTransportDetail.NGAY = Convert.ToInt32(dr["NGAY"].ToString());
                            //oAirwayTransportDetail.MABC = Convert.ToInt32(dr["MABC"].ToString());
                            //oAirwayTransportDetail.CHIEU = Convert.ToInt32(dr["CHIEU"].ToString());
                            //oAirwayTransportDetail.HUONG = Convert.ToInt32(dr["HUONG"].ToString());
                            oAirwayTransportDetail.TAICUNG_KH = dr["TAICUNG_KH"].ToString();
                            oAirwayTransportDetail.TAIMEM_KH = dr["TAIMEM_KH"].ToString();
                            oAirwayTransportDetail.MASP = dr["MASP"].ToString();
                            oAirwayTransportDetail.TAICUNG_TH = dr["TAICUNG_TH"].ToString();
                            oAirwayTransportDetail.TAIMEM_TH = dr["TAIMEM_TH"].ToString();
                            oAirwayTransportDetail.TAICUNG_LK = dr["TAICUNG_LK"].ToString();
                            oAirwayTransportDetail.TAIMEM_LK = dr["TAIMEM_LK"].ToString();
                            oAirwayTransportDetail.GIOGIAO_QD = dr["GIOGIAO_QD"].ToString();
                            oAirwayTransportDetail.GIOBAY_QD = dr["GIOBAY_QD"].ToString();
                            oAirwayTransportDetail.GIOGIAO_TT = dr["GIOGIAO_TT"].ToString();
                            oAirwayTransportDetail.GIOBAY_TT = dr["GIOBAY_TT"].ToString();
                            oAirwayTransportDetail.SOHIEUCHUYENBAY = dr["SOHIEUCHUYENBAY"].ToString();
                            oAirwayTransportDetail.GIOGIAO_CL = dr["GIOGIAO_CL"].ToString();
                            oAirwayTransportDetail.GIOBAY_CL = dr["GIOBAY_CL"].ToString();
                            oAirwayTransportDetail.GIODAP_QD = dr["GIODAP_QD"].ToString();
                            oAirwayTransportDetail.GIONHAN_QD = dr["GIONHAN_QD"].ToString();
                            //oAirwayTransportDetail.GIODAP_TT = dr["GIODAP_TT"].ToString();
                            oAirwayTransportDetail.GIONHAN_TT = dr["GIONHAN_TT"].ToString();
                            oAirwayTransportDetail.GIONHAN_CL = dr["GIONHAN_CL"].ToString();
                            listAirwayTransportDetail.Add(oAirwayTransportDetail);

                        }
                        _returnAirwayTransport.Code = "00";
                        _returnAirwayTransport.Message = "Lấy dữ liệu thành công.";
                        _returnAirwayTransport.ListAirwayTransportReport = listAirwayTransportDetail;
                    }
                    else
                    {
                        _returnAirwayTransport.Code = "01";
                        _returnAirwayTransport.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _returnAirwayTransport.Code = "99";
                _returnAirwayTransport.Message = "Lỗi xử lý dữ liệu";

            }
            return _returnAirwayTransport;
        }



        #endregion

        //Phần chi tiết của bảng LOAD_DATA11
        #region LOAD_DATA11          
        public ReturnAirwayTransport LOAD_DATA11(int date, int way)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnAirwayTransport _returnAirwayTransport = new ReturnAirwayTransport();


            List<AirwayTransportDetail> listAirwayTransportDetail = null;
            AirwayTransportDetail oAirwayTransportDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    //Helper.OraDSOracleConnection gọi đến database đối soát
                    OracleCommand myCommand = new OracleCommand("AIRWAY_TRANSPORT_2.LOAD_DATA11", Helper.OraDSOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("P_CHIEU", OracleDbType.Int32).Value = way;
                    myCommand.Parameters.Add("P_NGAY", OracleDbType.Int32).Value = date;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listAirwayTransportDetail = new List<AirwayTransportDetail>();
                        while (dr.Read())
                        {
                            oAirwayTransportDetail = new AirwayTransportDetail();
                            //oAirwayTransportDetail.SO = Convert.ToInt32(dr["1"].ToString());
                            oAirwayTransportDetail.STT = Convert.ToInt32(dr["STT"].ToString());
                            oAirwayTransportDetail.CHANGBAY = dr["CHANGBAY"].ToString();
                            oAirwayTransportDetail.DONVIVANCHUYEN = dr["DONVIVANCHUYEN"].ToString();
                            oAirwayTransportDetail.DICHVUVANCHUYEN = dr["DICHVUVANCHUYEN"].ToString();
                            oAirwayTransportDetail.KHUNGGIO = dr["KHUNGGIO"].ToString();
                            oAirwayTransportDetail.TONGTAITHEOHOPDONG = dr["TONGTAITHEOHOPDONG"].ToString();
                            //oAirwayTransportDetail.ID = Convert.ToInt32(dr["ID"].ToString());
                            //oAirwayTransportDetail.NGAY = Convert.ToInt32(dr["NGAY"].ToString());
                            //oAirwayTransportDetail.MABC = Convert.ToInt32(dr["MABC"].ToString());
                            //oAirwayTransportDetail.CHIEU = Convert.ToInt32(dr["CHIEU"].ToString());
                            //oAirwayTransportDetail.HUONG = Convert.ToInt32(dr["HUONG"].ToString());
                            oAirwayTransportDetail.TAICUNG_KH = dr["TAICUNG_KH"].ToString();
                            oAirwayTransportDetail.TAIMEM_KH = dr["TAIMEM_KH"].ToString();
                            oAirwayTransportDetail.MASP = dr["MASP"].ToString();
                            oAirwayTransportDetail.TAICUNG_TH = dr["TAICUNG_TH"].ToString();
                            oAirwayTransportDetail.TAIMEM_TH = dr["TAIMEM_TH"].ToString();
                            oAirwayTransportDetail.TAICUNG_LK = dr["TAICUNG_LK"].ToString();
                            oAirwayTransportDetail.TAIMEM_LK = dr["TAIMEM_LK"].ToString();
                            oAirwayTransportDetail.GIOGIAO_QD = dr["GIOGIAO_QD"].ToString();
                            oAirwayTransportDetail.GIOBAY_QD = dr["GIOBAY_QD"].ToString();
                            oAirwayTransportDetail.GIOGIAO_TT = dr["GIOGIAO_TT"].ToString();
                            oAirwayTransportDetail.GIOBAY_TT = dr["GIOBAY_TT"].ToString();
                            oAirwayTransportDetail.SOHIEUCHUYENBAY = dr["SOHIEUCHUYENBAY"].ToString();
                            oAirwayTransportDetail.GIOGIAO_CL = dr["GIOGIAO_CL"].ToString();
                            oAirwayTransportDetail.GIOBAY_CL = dr["GIOBAY_CL"].ToString();
                            oAirwayTransportDetail.GIODAP_QD = dr["GIODAP_QD"].ToString();
                            oAirwayTransportDetail.GIONHAN_QD = dr["GIONHAN_QD"].ToString();
                            //oAirwayTransportDetail.GIODAP_TT = dr["GIODAP_TT"].ToString();
                            oAirwayTransportDetail.GIONHAN_TT = dr["GIONHAN_TT"].ToString();
                            oAirwayTransportDetail.GIONHAN_CL = dr["GIONHAN_CL"].ToString();
                            listAirwayTransportDetail.Add(oAirwayTransportDetail);

                        }
                        _returnAirwayTransport.Code = "00";
                        _returnAirwayTransport.Message = "Lấy dữ liệu thành công.";
                        _returnAirwayTransport.ListAirwayTransportReport = listAirwayTransportDetail;
                    }
                    else
                    {
                        _returnAirwayTransport.Code = "01";
                        _returnAirwayTransport.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _returnAirwayTransport.Code = "99";
                _returnAirwayTransport.Message = "Lỗi xử lý dữ liệu";

            }
            return _returnAirwayTransport;
        }



        #endregion

        //Phần chi tiết của bảng LOAD_DATA12
        #region LOAD_DATA12          
        public ReturnAirwayTransport LOAD_DATA12(int date, int way)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnAirwayTransport _returnAirwayTransport = new ReturnAirwayTransport();


            List<AirwayTransportDetail> listAirwayTransportDetail = null;
            AirwayTransportDetail oAirwayTransportDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    //Helper.OraDSOracleConnection gọi đến database đối soát
                    OracleCommand myCommand = new OracleCommand("AIRWAY_TRANSPORT_2.LOAD_DATA12", Helper.OraDSOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("P_CHIEU", OracleDbType.Int32).Value = way;
                    myCommand.Parameters.Add("P_NGAY", OracleDbType.Int32).Value = date;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listAirwayTransportDetail = new List<AirwayTransportDetail>();
                        while (dr.Read())
                        {
                            oAirwayTransportDetail = new AirwayTransportDetail();
                            //oAirwayTransportDetail.SO = Convert.ToInt32(dr["1"].ToString());
                            oAirwayTransportDetail.STT = Convert.ToInt32(dr["STT"].ToString());
                            oAirwayTransportDetail.CHANGBAY = dr["CHANGBAY"].ToString();
                            oAirwayTransportDetail.DONVIVANCHUYEN = dr["DONVIVANCHUYEN"].ToString();
                            oAirwayTransportDetail.DICHVUVANCHUYEN = dr["DICHVUVANCHUYEN"].ToString();
                            oAirwayTransportDetail.KHUNGGIO = dr["KHUNGGIO"].ToString();
                            oAirwayTransportDetail.TONGTAITHEOHOPDONG = dr["TONGTAITHEOHOPDONG"].ToString();
                            //oAirwayTransportDetail.ID = Convert.ToInt32(dr["ID"].ToString());
                            //oAirwayTransportDetail.NGAY = Convert.ToInt32(dr["NGAY"].ToString());
                            //oAirwayTransportDetail.MABC = Convert.ToInt32(dr["MABC"].ToString());
                            //oAirwayTransportDetail.CHIEU = Convert.ToInt32(dr["CHIEU"].ToString());
                            //oAirwayTransportDetail.HUONG = Convert.ToInt32(dr["HUONG"].ToString());
                            oAirwayTransportDetail.TAICUNG_KH = dr["TAICUNG_KH"].ToString();
                            oAirwayTransportDetail.TAIMEM_KH = dr["TAIMEM_KH"].ToString();
                            oAirwayTransportDetail.MASP = dr["MASP"].ToString();
                            oAirwayTransportDetail.TAICUNG_TH = dr["TAICUNG_TH"].ToString();
                            oAirwayTransportDetail.TAIMEM_TH = dr["TAIMEM_TH"].ToString();
                            oAirwayTransportDetail.TAICUNG_LK = dr["TAICUNG_LK"].ToString();
                            oAirwayTransportDetail.TAIMEM_LK = dr["TAIMEM_LK"].ToString();
                            oAirwayTransportDetail.GIOGIAO_QD = dr["GIOGIAO_QD"].ToString();
                            oAirwayTransportDetail.GIOBAY_QD = dr["GIOBAY_QD"].ToString();
                            oAirwayTransportDetail.GIOGIAO_TT = dr["GIOGIAO_TT"].ToString();
                            oAirwayTransportDetail.GIOBAY_TT = dr["GIOBAY_TT"].ToString();
                            oAirwayTransportDetail.SOHIEUCHUYENBAY = dr["SOHIEUCHUYENBAY"].ToString();
                            oAirwayTransportDetail.GIOGIAO_CL = dr["GIOGIAO_CL"].ToString();
                            oAirwayTransportDetail.GIOBAY_CL = dr["GIOBAY_CL"].ToString();
                            oAirwayTransportDetail.GIODAP_QD = dr["GIODAP_QD"].ToString();
                            oAirwayTransportDetail.GIONHAN_QD = dr["GIONHAN_QD"].ToString();
                            //oAirwayTransportDetail.GIODAP_TT = dr["GIODAP_TT"].ToString();
                            oAirwayTransportDetail.GIONHAN_TT = dr["GIONHAN_TT"].ToString();
                            oAirwayTransportDetail.GIONHAN_CL = dr["GIONHAN_CL"].ToString();
                            listAirwayTransportDetail.Add(oAirwayTransportDetail);

                        }
                        _returnAirwayTransport.Code = "00";
                        _returnAirwayTransport.Message = "Lấy dữ liệu thành công.";
                        _returnAirwayTransport.ListAirwayTransportReport = listAirwayTransportDetail;
                    }
                    else
                    {
                        _returnAirwayTransport.Code = "01";
                        _returnAirwayTransport.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _returnAirwayTransport.Code = "99";
                _returnAirwayTransport.Message = "Lỗi xử lý dữ liệu";

            }
            return _returnAirwayTransport;
        }



        #endregion

        //Phần chi tiết của bảng LOAD_DATA13
        #region LOAD_DATA13          
        public ReturnAirwayTransport LOAD_DATA13(int date, int way)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnAirwayTransport _returnAirwayTransport = new ReturnAirwayTransport();


            List<AirwayTransportDetail> listAirwayTransportDetail = null;
            AirwayTransportDetail oAirwayTransportDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    //Helper.OraDSOracleConnection gọi đến database đối soát
                    OracleCommand myCommand = new OracleCommand("AIRWAY_TRANSPORT_2.LOAD_DATA13", Helper.OraDSOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("P_CHIEU", OracleDbType.Int32).Value = way;
                    myCommand.Parameters.Add("P_NGAY", OracleDbType.Int32).Value = date;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listAirwayTransportDetail = new List<AirwayTransportDetail>();
                        while (dr.Read())
                        {
                            oAirwayTransportDetail = new AirwayTransportDetail();
                            //oAirwayTransportDetail.SO = Convert.ToInt32(dr["1"].ToString());
                            oAirwayTransportDetail.STT = Convert.ToInt32(dr["STT"].ToString());
                            oAirwayTransportDetail.CHANGBAY = dr["CHANGBAY"].ToString();
                            oAirwayTransportDetail.DONVIVANCHUYEN = dr["DONVIVANCHUYEN"].ToString();
                            oAirwayTransportDetail.DICHVUVANCHUYEN = dr["DICHVUVANCHUYEN"].ToString();
                            oAirwayTransportDetail.KHUNGGIO = dr["KHUNGGIO"].ToString();
                            oAirwayTransportDetail.TONGTAITHEOHOPDONG = dr["TONGTAITHEOHOPDONG"].ToString();
                            //oAirwayTransportDetail.ID = Convert.ToInt32(dr["ID"].ToString());
                            //oAirwayTransportDetail.NGAY = Convert.ToInt32(dr["NGAY"].ToString());
                            //oAirwayTransportDetail.MABC = Convert.ToInt32(dr["MABC"].ToString());
                            //oAirwayTransportDetail.CHIEU = Convert.ToInt32(dr["CHIEU"].ToString());
                            //oAirwayTransportDetail.HUONG = Convert.ToInt32(dr["HUONG"].ToString());
                            oAirwayTransportDetail.TAICUNG_KH = dr["TAICUNG_KH"].ToString();
                            oAirwayTransportDetail.TAIMEM_KH = dr["TAIMEM_KH"].ToString();
                            oAirwayTransportDetail.MASP = dr["MASP"].ToString();
                            oAirwayTransportDetail.TAICUNG_TH = dr["TAICUNG_TH"].ToString();
                            oAirwayTransportDetail.TAIMEM_TH = dr["TAIMEM_TH"].ToString();
                            oAirwayTransportDetail.TAICUNG_LK = dr["TAICUNG_LK"].ToString();
                            oAirwayTransportDetail.TAIMEM_LK = dr["TAIMEM_LK"].ToString();
                            oAirwayTransportDetail.GIOGIAO_QD = dr["GIOGIAO_QD"].ToString();
                            oAirwayTransportDetail.GIOBAY_QD = dr["GIOBAY_QD"].ToString();
                            oAirwayTransportDetail.GIOGIAO_TT = dr["GIOGIAO_TT"].ToString();
                            oAirwayTransportDetail.GIOBAY_TT = dr["GIOBAY_TT"].ToString();
                            oAirwayTransportDetail.SOHIEUCHUYENBAY = dr["SOHIEUCHUYENBAY"].ToString();
                            oAirwayTransportDetail.GIOGIAO_CL = dr["GIOGIAO_CL"].ToString();
                            oAirwayTransportDetail.GIOBAY_CL = dr["GIOBAY_CL"].ToString();
                            oAirwayTransportDetail.GIODAP_QD = dr["GIODAP_QD"].ToString();
                            oAirwayTransportDetail.GIONHAN_QD = dr["GIONHAN_QD"].ToString();
                            //oAirwayTransportDetail.GIODAP_TT = dr["GIODAP_TT"].ToString();
                            oAirwayTransportDetail.GIONHAN_TT = dr["GIONHAN_TT"].ToString();
                            oAirwayTransportDetail.GIONHAN_CL = dr["GIONHAN_CL"].ToString();
                            listAirwayTransportDetail.Add(oAirwayTransportDetail);

                        }
                        _returnAirwayTransport.Code = "00";
                        _returnAirwayTransport.Message = "Lấy dữ liệu thành công.";
                        _returnAirwayTransport.ListAirwayTransportReport = listAirwayTransportDetail;
                    }
                    else
                    {
                        _returnAirwayTransport.Code = "01";
                        _returnAirwayTransport.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _returnAirwayTransport.Code = "99";
                _returnAirwayTransport.Message = "Lỗi xử lý dữ liệu";

            }
            return _returnAirwayTransport;
        }



        #endregion

        //Phần chi tiết của bảng LOAD_DATA14
        #region LOAD_DATA14          
        public ReturnAirwayTransport LOAD_DATA14(int date, int way)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnAirwayTransport _returnAirwayTransport = new ReturnAirwayTransport();


            List<AirwayTransportDetail> listAirwayTransportDetail = null;
            AirwayTransportDetail oAirwayTransportDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    //Helper.OraDSOracleConnection gọi đến database đối soát
                    OracleCommand myCommand = new OracleCommand("AIRWAY_TRANSPORT_2.LOAD_DATA14", Helper.OraDSOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("P_CHIEU", OracleDbType.Int32).Value = way;
                    myCommand.Parameters.Add("P_NGAY", OracleDbType.Int32).Value = date;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listAirwayTransportDetail = new List<AirwayTransportDetail>();
                        while (dr.Read())
                        {
                            oAirwayTransportDetail = new AirwayTransportDetail();
                            //oAirwayTransportDetail.SO = Convert.ToInt32(dr["1"].ToString());
                            oAirwayTransportDetail.STT = Convert.ToInt32(dr["STT"].ToString());
                            oAirwayTransportDetail.CHANGBAY = dr["CHANGBAY"].ToString();
                            oAirwayTransportDetail.DONVIVANCHUYEN = dr["DONVIVANCHUYEN"].ToString();
                            oAirwayTransportDetail.DICHVUVANCHUYEN = dr["DICHVUVANCHUYEN"].ToString();
                            oAirwayTransportDetail.KHUNGGIO = dr["KHUNGGIO"].ToString();
                            oAirwayTransportDetail.TONGTAITHEOHOPDONG = dr["TONGTAITHEOHOPDONG"].ToString();
                            //oAirwayTransportDetail.ID = Convert.ToInt32(dr["ID"].ToString());
                            //oAirwayTransportDetail.NGAY = Convert.ToInt32(dr["NGAY"].ToString());
                            //oAirwayTransportDetail.MABC = Convert.ToInt32(dr["MABC"].ToString());
                            //oAirwayTransportDetail.CHIEU = Convert.ToInt32(dr["CHIEU"].ToString());
                            //oAirwayTransportDetail.HUONG = Convert.ToInt32(dr["HUONG"].ToString());
                            oAirwayTransportDetail.TAICUNG_KH = dr["TAICUNG_KH"].ToString();
                            oAirwayTransportDetail.TAIMEM_KH = dr["TAIMEM_KH"].ToString();
                            oAirwayTransportDetail.MASP = dr["MASP"].ToString();
                            oAirwayTransportDetail.TAICUNG_TH = dr["TAICUNG_TH"].ToString();
                            oAirwayTransportDetail.TAIMEM_TH = dr["TAIMEM_TH"].ToString();
                            oAirwayTransportDetail.TAICUNG_LK = dr["TAICUNG_LK"].ToString();
                            oAirwayTransportDetail.TAIMEM_LK = dr["TAIMEM_LK"].ToString();
                            oAirwayTransportDetail.GIOGIAO_QD = dr["GIOGIAO_QD"].ToString();
                            oAirwayTransportDetail.GIOBAY_QD = dr["GIOBAY_QD"].ToString();
                            oAirwayTransportDetail.GIOGIAO_TT = dr["GIOGIAO_TT"].ToString();
                            oAirwayTransportDetail.GIOBAY_TT = dr["GIOBAY_TT"].ToString();
                            oAirwayTransportDetail.SOHIEUCHUYENBAY = dr["SOHIEUCHUYENBAY"].ToString();
                            oAirwayTransportDetail.GIOGIAO_CL = dr["GIOGIAO_CL"].ToString();
                            oAirwayTransportDetail.GIOBAY_CL = dr["GIOBAY_CL"].ToString();
                            oAirwayTransportDetail.GIODAP_QD = dr["GIODAP_QD"].ToString();
                            oAirwayTransportDetail.GIONHAN_QD = dr["GIONHAN_QD"].ToString();
                            //oAirwayTransportDetail.GIODAP_TT = dr["GIODAP_TT"].ToString();
                            oAirwayTransportDetail.GIONHAN_TT = dr["GIONHAN_TT"].ToString();
                            oAirwayTransportDetail.GIONHAN_CL = dr["GIONHAN_CL"].ToString();
                            listAirwayTransportDetail.Add(oAirwayTransportDetail);

                        }
                        _returnAirwayTransport.Code = "00";
                        _returnAirwayTransport.Message = "Lấy dữ liệu thành công.";
                        _returnAirwayTransport.ListAirwayTransportReport = listAirwayTransportDetail;
                    }
                    else
                    {
                        _returnAirwayTransport.Code = "01";
                        _returnAirwayTransport.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _returnAirwayTransport.Code = "99";
                _returnAirwayTransport.Message = "Lỗi xử lý dữ liệu";

            }
            return _returnAirwayTransport;
        }



        #endregion

        //Phần chi tiết của bảng LOAD_DATA15
        #region LOAD_DATA15          
        public ReturnAirwayTransport LOAD_DATA15(int date, int way)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnAirwayTransport _returnAirwayTransport = new ReturnAirwayTransport();


            List<AirwayTransportDetail> listAirwayTransportDetail = null;
            AirwayTransportDetail oAirwayTransportDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    //Helper.OraDSOracleConnection gọi đến database đối soát
                    OracleCommand myCommand = new OracleCommand("AIRWAY_TRANSPORT_2.LOAD_DATA15", Helper.OraDSOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("P_CHIEU", OracleDbType.Int32).Value = way;
                    myCommand.Parameters.Add("P_NGAY", OracleDbType.Int32).Value = date;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listAirwayTransportDetail = new List<AirwayTransportDetail>();
                        while (dr.Read())
                        {
                            oAirwayTransportDetail = new AirwayTransportDetail();
                            //oAirwayTransportDetail.SO = Convert.ToInt32(dr["1"].ToString());
                            oAirwayTransportDetail.STT = Convert.ToInt32(dr["STT"].ToString());
                            oAirwayTransportDetail.CHANGBAY = dr["CHANGBAY"].ToString();
                            oAirwayTransportDetail.DONVIVANCHUYEN = dr["DONVIVANCHUYEN"].ToString();
                            oAirwayTransportDetail.DICHVUVANCHUYEN = dr["DICHVUVANCHUYEN"].ToString();
                            oAirwayTransportDetail.KHUNGGIO = dr["KHUNGGIO"].ToString();
                            oAirwayTransportDetail.TONGTAITHEOHOPDONG = dr["TONGTAITHEOHOPDONG"].ToString();
                            //oAirwayTransportDetail.ID = Convert.ToInt32(dr["ID"].ToString());
                            //oAirwayTransportDetail.NGAY = Convert.ToInt32(dr["NGAY"].ToString());
                            //oAirwayTransportDetail.MABC = Convert.ToInt32(dr["MABC"].ToString());
                            //oAirwayTransportDetail.CHIEU = Convert.ToInt32(dr["CHIEU"].ToString());
                            //oAirwayTransportDetail.HUONG = Convert.ToInt32(dr["HUONG"].ToString());
                            oAirwayTransportDetail.TAICUNG_KH = dr["TAICUNG_KH"].ToString();
                            oAirwayTransportDetail.TAIMEM_KH = dr["TAIMEM_KH"].ToString();
                            oAirwayTransportDetail.MASP = dr["MASP"].ToString();
                            oAirwayTransportDetail.TAICUNG_TH = dr["TAICUNG_TH"].ToString();
                            oAirwayTransportDetail.TAIMEM_TH = dr["TAIMEM_TH"].ToString();
                            oAirwayTransportDetail.TAICUNG_LK = dr["TAICUNG_LK"].ToString();
                            oAirwayTransportDetail.TAIMEM_LK = dr["TAIMEM_LK"].ToString();
                            oAirwayTransportDetail.GIOGIAO_QD = dr["GIOGIAO_QD"].ToString();
                            oAirwayTransportDetail.GIOBAY_QD = dr["GIOBAY_QD"].ToString();
                            oAirwayTransportDetail.GIOGIAO_TT = dr["GIOGIAO_TT"].ToString();
                            oAirwayTransportDetail.GIOBAY_TT = dr["GIOBAY_TT"].ToString();
                            oAirwayTransportDetail.SOHIEUCHUYENBAY = dr["SOHIEUCHUYENBAY"].ToString();
                            oAirwayTransportDetail.GIOGIAO_CL = dr["GIOGIAO_CL"].ToString();
                            oAirwayTransportDetail.GIOBAY_CL = dr["GIOBAY_CL"].ToString();
                            oAirwayTransportDetail.GIODAP_QD = dr["GIODAP_QD"].ToString();
                            oAirwayTransportDetail.GIONHAN_QD = dr["GIONHAN_QD"].ToString();
                            //oAirwayTransportDetail.GIODAP_TT = dr["GIODAP_TT"].ToString();
                            oAirwayTransportDetail.GIONHAN_TT = dr["GIONHAN_TT"].ToString();
                            oAirwayTransportDetail.GIONHAN_CL = dr["GIONHAN_CL"].ToString();
                            listAirwayTransportDetail.Add(oAirwayTransportDetail);

                        }
                        _returnAirwayTransport.Code = "00";
                        _returnAirwayTransport.Message = "Lấy dữ liệu thành công.";
                        _returnAirwayTransport.ListAirwayTransportReport = listAirwayTransportDetail;
                    }
                    else
                    {
                        _returnAirwayTransport.Code = "01";
                        _returnAirwayTransport.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _returnAirwayTransport.Code = "99";
                _returnAirwayTransport.Message = "Lỗi xử lý dữ liệu";

            }
            return _returnAirwayTransport;
        }



        #endregion

        //Phần chi tiết của bảng LOAD_DATA16
        #region LOAD_DATA16          
        public ReturnAirwayTransport LOAD_DATA16(int date, int way)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnAirwayTransport _returnAirwayTransport = new ReturnAirwayTransport();


            List<AirwayTransportDetail> listAirwayTransportDetail = null;
            AirwayTransportDetail oAirwayTransportDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    //Helper.OraDSOracleConnection gọi đến database đối soát
                    OracleCommand myCommand = new OracleCommand("AIRWAY_TRANSPORT_2.LOAD_DATA16", Helper.OraDSOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("P_CHIEU", OracleDbType.Int32).Value = way;
                    myCommand.Parameters.Add("P_NGAY", OracleDbType.Int32).Value = date;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listAirwayTransportDetail = new List<AirwayTransportDetail>();
                        while (dr.Read())
                        {
                            oAirwayTransportDetail = new AirwayTransportDetail();
                            //oAirwayTransportDetail.SO = Convert.ToInt32(dr["1"].ToString());
                            oAirwayTransportDetail.STT = Convert.ToInt32(dr["STT"].ToString());
                            oAirwayTransportDetail.CHANGBAY = dr["CHANGBAY"].ToString();
                            oAirwayTransportDetail.DONVIVANCHUYEN = dr["DONVIVANCHUYEN"].ToString();
                            oAirwayTransportDetail.DICHVUVANCHUYEN = dr["DICHVUVANCHUYEN"].ToString();
                            oAirwayTransportDetail.KHUNGGIO = dr["KHUNGGIO"].ToString();
                            oAirwayTransportDetail.TONGTAITHEOHOPDONG = dr["TONGTAITHEOHOPDONG"].ToString();
                            //oAirwayTransportDetail.ID = Convert.ToInt32(dr["ID"].ToString());
                            //oAirwayTransportDetail.NGAY = Convert.ToInt32(dr["NGAY"].ToString());
                            //oAirwayTransportDetail.MABC = Convert.ToInt32(dr["MABC"].ToString());
                            //oAirwayTransportDetail.CHIEU = Convert.ToInt32(dr["CHIEU"].ToString());
                            //oAirwayTransportDetail.HUONG = Convert.ToInt32(dr["HUONG"].ToString());
                            oAirwayTransportDetail.TAICUNG_KH = dr["TAICUNG_KH"].ToString();
                            oAirwayTransportDetail.TAIMEM_KH = dr["TAIMEM_KH"].ToString();
                            oAirwayTransportDetail.MASP = dr["MASP"].ToString();
                            oAirwayTransportDetail.TAICUNG_TH = dr["TAICUNG_TH"].ToString();
                            oAirwayTransportDetail.TAIMEM_TH = dr["TAIMEM_TH"].ToString();
                            oAirwayTransportDetail.TAICUNG_LK = dr["TAICUNG_LK"].ToString();
                            oAirwayTransportDetail.TAIMEM_LK = dr["TAIMEM_LK"].ToString();
                            oAirwayTransportDetail.GIOGIAO_QD = dr["GIOGIAO_QD"].ToString();
                            oAirwayTransportDetail.GIOBAY_QD = dr["GIOBAY_QD"].ToString();
                            oAirwayTransportDetail.GIOGIAO_TT = dr["GIOGIAO_TT"].ToString();
                            oAirwayTransportDetail.GIOBAY_TT = dr["GIOBAY_TT"].ToString();
                            oAirwayTransportDetail.SOHIEUCHUYENBAY = dr["SOHIEUCHUYENBAY"].ToString();
                            oAirwayTransportDetail.GIOGIAO_CL = dr["GIOGIAO_CL"].ToString();
                            oAirwayTransportDetail.GIOBAY_CL = dr["GIOBAY_CL"].ToString();
                            oAirwayTransportDetail.GIODAP_QD = dr["GIODAP_QD"].ToString();
                            oAirwayTransportDetail.GIONHAN_QD = dr["GIONHAN_QD"].ToString();
                            //oAirwayTransportDetail.GIODAP_TT = dr["GIODAP_TT"].ToString();
                            oAirwayTransportDetail.GIONHAN_TT = dr["GIONHAN_TT"].ToString();
                            oAirwayTransportDetail.GIONHAN_CL = dr["GIONHAN_CL"].ToString();
                            listAirwayTransportDetail.Add(oAirwayTransportDetail);

                        }
                        _returnAirwayTransport.Code = "00";
                        _returnAirwayTransport.Message = "Lấy dữ liệu thành công.";
                        _returnAirwayTransport.ListAirwayTransportReport = listAirwayTransportDetail;
                    }
                    else
                    {
                        _returnAirwayTransport.Code = "01";
                        _returnAirwayTransport.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _returnAirwayTransport.Code = "99";
                _returnAirwayTransport.Message = "Lỗi xử lý dữ liệu";

            }
            return _returnAirwayTransport;
        }



        #endregion

        //Phần chi tiết của bảng LOAD_DATA17
        #region LOAD_DATA17          
        public ReturnAirwayTransport LOAD_DATA17(int date, int way)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnAirwayTransport _returnAirwayTransport = new ReturnAirwayTransport();


            List<AirwayTransportDetail> listAirwayTransportDetail = null;
            AirwayTransportDetail oAirwayTransportDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    //Helper.OraDSOracleConnection gọi đến database đối soát
                    OracleCommand myCommand = new OracleCommand("AIRWAY_TRANSPORT_2.LOAD_DATA17", Helper.OraDSOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("P_CHIEU", OracleDbType.Int32).Value = way;
                    myCommand.Parameters.Add("P_NGAY", OracleDbType.Int32).Value = date;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listAirwayTransportDetail = new List<AirwayTransportDetail>();
                        while (dr.Read())
                        {
                            oAirwayTransportDetail = new AirwayTransportDetail();
                            //oAirwayTransportDetail.SO = Convert.ToInt32(dr["1"].ToString());
                            oAirwayTransportDetail.STT = Convert.ToInt32(dr["STT"].ToString());
                            oAirwayTransportDetail.CHANGBAY = dr["CHANGBAY"].ToString();
                            oAirwayTransportDetail.DONVIVANCHUYEN = dr["DONVIVANCHUYEN"].ToString();
                            oAirwayTransportDetail.DICHVUVANCHUYEN = dr["DICHVUVANCHUYEN"].ToString();
                            oAirwayTransportDetail.KHUNGGIO = dr["KHUNGGIO"].ToString();
                            oAirwayTransportDetail.TONGTAITHEOHOPDONG = dr["TONGTAITHEOHOPDONG"].ToString();
                            //oAirwayTransportDetail.ID = Convert.ToInt32(dr["ID"].ToString());
                            //oAirwayTransportDetail.NGAY = Convert.ToInt32(dr["NGAY"].ToString());
                            //oAirwayTransportDetail.MABC = Convert.ToInt32(dr["MABC"].ToString());
                            //oAirwayTransportDetail.CHIEU = Convert.ToInt32(dr["CHIEU"].ToString());
                            //oAirwayTransportDetail.HUONG = Convert.ToInt32(dr["HUONG"].ToString());
                            oAirwayTransportDetail.TAICUNG_KH = dr["TAICUNG_KH"].ToString();
                            oAirwayTransportDetail.TAIMEM_KH = dr["TAIMEM_KH"].ToString();
                            oAirwayTransportDetail.MASP = dr["MASP"].ToString();
                            oAirwayTransportDetail.TAICUNG_TH = dr["TAICUNG_TH"].ToString();
                            oAirwayTransportDetail.TAIMEM_TH = dr["TAIMEM_TH"].ToString();
                            oAirwayTransportDetail.TAICUNG_LK = dr["TAICUNG_LK"].ToString();
                            oAirwayTransportDetail.TAIMEM_LK = dr["TAIMEM_LK"].ToString();
                            oAirwayTransportDetail.GIOGIAO_QD = dr["GIOGIAO_QD"].ToString();
                            oAirwayTransportDetail.GIOBAY_QD = dr["GIOBAY_QD"].ToString();
                            oAirwayTransportDetail.GIOGIAO_TT = dr["GIOGIAO_TT"].ToString();
                            oAirwayTransportDetail.GIOBAY_TT = dr["GIOBAY_TT"].ToString();
                            oAirwayTransportDetail.SOHIEUCHUYENBAY = dr["SOHIEUCHUYENBAY"].ToString();
                            oAirwayTransportDetail.GIOGIAO_CL = dr["GIOGIAO_CL"].ToString();
                            oAirwayTransportDetail.GIOBAY_CL = dr["GIOBAY_CL"].ToString();
                            oAirwayTransportDetail.GIODAP_QD = dr["GIODAP_QD"].ToString();
                            oAirwayTransportDetail.GIONHAN_QD = dr["GIONHAN_QD"].ToString();
                            //oAirwayTransportDetail.GIODAP_TT = dr["GIODAP_TT"].ToString();
                            oAirwayTransportDetail.GIONHAN_TT = dr["GIONHAN_TT"].ToString();
                            oAirwayTransportDetail.GIONHAN_CL = dr["GIONHAN_CL"].ToString();
                            listAirwayTransportDetail.Add(oAirwayTransportDetail);

                        }
                        _returnAirwayTransport.Code = "00";
                        _returnAirwayTransport.Message = "Lấy dữ liệu thành công.";
                        _returnAirwayTransport.ListAirwayTransportReport = listAirwayTransportDetail;
                    }
                    else
                    {
                        _returnAirwayTransport.Code = "01";
                        _returnAirwayTransport.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _returnAirwayTransport.Code = "99";
                _returnAirwayTransport.Message = "Lỗi xử lý dữ liệu";

            }
            return _returnAirwayTransport;
        }



        #endregion

        //Phần chi tiết của bảng LOAD_DATA18
        #region LOAD_DATA18          
        public ReturnAirwayTransport LOAD_DATA18(int date, int way)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnAirwayTransport _returnAirwayTransport = new ReturnAirwayTransport();


            List<AirwayTransportDetail> listAirwayTransportDetail = null;
            AirwayTransportDetail oAirwayTransportDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    //Helper.OraDSOracleConnection gọi đến database đối soát
                    OracleCommand myCommand = new OracleCommand("AIRWAY_TRANSPORT_2.LOAD_DATA18", Helper.OraDSOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("P_CHIEU", OracleDbType.Int32).Value = way;
                    myCommand.Parameters.Add("P_NGAY", OracleDbType.Int32).Value = date;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listAirwayTransportDetail = new List<AirwayTransportDetail>();
                        while (dr.Read())
                        {
                            oAirwayTransportDetail = new AirwayTransportDetail();
                            //oAirwayTransportDetail.SO = Convert.ToInt32(dr["1"].ToString());
                            oAirwayTransportDetail.STT = Convert.ToInt32(dr["STT"].ToString());
                            oAirwayTransportDetail.CHANGBAY = dr["CHANGBAY"].ToString();
                            oAirwayTransportDetail.DONVIVANCHUYEN = dr["DONVIVANCHUYEN"].ToString();
                            oAirwayTransportDetail.DICHVUVANCHUYEN = dr["DICHVUVANCHUYEN"].ToString();
                            oAirwayTransportDetail.KHUNGGIO = dr["KHUNGGIO"].ToString();
                            oAirwayTransportDetail.TONGTAITHEOHOPDONG = dr["TONGTAITHEOHOPDONG"].ToString();
                            //oAirwayTransportDetail.ID = Convert.ToInt32(dr["ID"].ToString());
                            //oAirwayTransportDetail.NGAY = Convert.ToInt32(dr["NGAY"].ToString());
                            //oAirwayTransportDetail.MABC = Convert.ToInt32(dr["MABC"].ToString());
                            //oAirwayTransportDetail.CHIEU = Convert.ToInt32(dr["CHIEU"].ToString());
                            //oAirwayTransportDetail.HUONG = Convert.ToInt32(dr["HUONG"].ToString());
                            oAirwayTransportDetail.TAICUNG_KH = dr["TAICUNG_KH"].ToString();
                            oAirwayTransportDetail.TAIMEM_KH = dr["TAIMEM_KH"].ToString();
                            oAirwayTransportDetail.MASP = dr["MASP"].ToString();
                            oAirwayTransportDetail.TAICUNG_TH = dr["TAICUNG_TH"].ToString();
                            oAirwayTransportDetail.TAIMEM_TH = dr["TAIMEM_TH"].ToString();
                            oAirwayTransportDetail.TAICUNG_LK = dr["TAICUNG_LK"].ToString();
                            oAirwayTransportDetail.TAIMEM_LK = dr["TAIMEM_LK"].ToString();
                            oAirwayTransportDetail.GIOGIAO_QD = dr["GIOGIAO_QD"].ToString();
                            oAirwayTransportDetail.GIOBAY_QD = dr["GIOBAY_QD"].ToString();
                            oAirwayTransportDetail.GIOGIAO_TT = dr["GIOGIAO_TT"].ToString();
                            oAirwayTransportDetail.GIOBAY_TT = dr["GIOBAY_TT"].ToString();
                            oAirwayTransportDetail.SOHIEUCHUYENBAY = dr["SOHIEUCHUYENBAY"].ToString();
                            oAirwayTransportDetail.GIOGIAO_CL = dr["GIOGIAO_CL"].ToString();
                            oAirwayTransportDetail.GIOBAY_CL = dr["GIOBAY_CL"].ToString();
                            oAirwayTransportDetail.GIODAP_QD = dr["GIODAP_QD"].ToString();
                            oAirwayTransportDetail.GIONHAN_QD = dr["GIONHAN_QD"].ToString();
                            //oAirwayTransportDetail.GIODAP_TT = dr["GIODAP_TT"].ToString();
                            oAirwayTransportDetail.GIONHAN_TT = dr["GIONHAN_TT"].ToString();
                            oAirwayTransportDetail.GIONHAN_CL = dr["GIONHAN_CL"].ToString();
                            listAirwayTransportDetail.Add(oAirwayTransportDetail);

                        }
                        _returnAirwayTransport.Code = "00";
                        _returnAirwayTransport.Message = "Lấy dữ liệu thành công.";
                        _returnAirwayTransport.ListAirwayTransportReport = listAirwayTransportDetail;
                    }
                    else
                    {
                        _returnAirwayTransport.Code = "01";
                        _returnAirwayTransport.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _returnAirwayTransport.Code = "99";
                _returnAirwayTransport.Message = "Lỗi xử lý dữ liệu";

            }
            return _returnAirwayTransport;
        }



        #endregion

        //Phần chi tiết của bảng LOAD_DATA19
        #region LOAD_DATA19          
        public ReturnAirwayTransport LOAD_DATA19(int date, int way)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnAirwayTransport _returnAirwayTransport = new ReturnAirwayTransport();


            List<AirwayTransportDetail> listAirwayTransportDetail = null;
            AirwayTransportDetail oAirwayTransportDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    //Helper.OraDSOracleConnection gọi đến database đối soát
                    OracleCommand myCommand = new OracleCommand("AIRWAY_TRANSPORT_2.LOAD_DATA19", Helper.OraDSOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("P_CHIEU", OracleDbType.Int32).Value = way;
                    myCommand.Parameters.Add("P_NGAY", OracleDbType.Int32).Value = date;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listAirwayTransportDetail = new List<AirwayTransportDetail>();
                        while (dr.Read())
                        {
                            oAirwayTransportDetail = new AirwayTransportDetail();
                            //oAirwayTransportDetail.SO = Convert.ToInt32(dr["1"].ToString());
                            oAirwayTransportDetail.STT = Convert.ToInt32(dr["STT"].ToString());
                            oAirwayTransportDetail.CHANGBAY = dr["CHANGBAY"].ToString();
                            oAirwayTransportDetail.DONVIVANCHUYEN = dr["DONVIVANCHUYEN"].ToString();
                            oAirwayTransportDetail.DICHVUVANCHUYEN = dr["DICHVUVANCHUYEN"].ToString();
                            oAirwayTransportDetail.KHUNGGIO = dr["KHUNGGIO"].ToString();
                            oAirwayTransportDetail.TONGTAITHEOHOPDONG = dr["TONGTAITHEOHOPDONG"].ToString();
                            //oAirwayTransportDetail.ID = Convert.ToInt32(dr["ID"].ToString());
                            //oAirwayTransportDetail.NGAY = Convert.ToInt32(dr["NGAY"].ToString());
                            //oAirwayTransportDetail.MABC = Convert.ToInt32(dr["MABC"].ToString());
                            //oAirwayTransportDetail.CHIEU = Convert.ToInt32(dr["CHIEU"].ToString());
                            //oAirwayTransportDetail.HUONG = Convert.ToInt32(dr["HUONG"].ToString());
                            oAirwayTransportDetail.TAICUNG_KH = dr["TAICUNG_KH"].ToString();
                            oAirwayTransportDetail.TAIMEM_KH = dr["TAIMEM_KH"].ToString();
                            oAirwayTransportDetail.MASP = dr["MASP"].ToString();
                            oAirwayTransportDetail.TAICUNG_TH = dr["TAICUNG_TH"].ToString();
                            oAirwayTransportDetail.TAIMEM_TH = dr["TAIMEM_TH"].ToString();
                            oAirwayTransportDetail.TAICUNG_LK = dr["TAICUNG_LK"].ToString();
                            oAirwayTransportDetail.TAIMEM_LK = dr["TAIMEM_LK"].ToString();
                            oAirwayTransportDetail.GIOGIAO_QD = dr["GIOGIAO_QD"].ToString();
                            oAirwayTransportDetail.GIOBAY_QD = dr["GIOBAY_QD"].ToString();
                            oAirwayTransportDetail.GIOGIAO_TT = dr["GIOGIAO_TT"].ToString();
                            oAirwayTransportDetail.GIOBAY_TT = dr["GIOBAY_TT"].ToString();
                            oAirwayTransportDetail.SOHIEUCHUYENBAY = dr["SOHIEUCHUYENBAY"].ToString();
                            oAirwayTransportDetail.GIOGIAO_CL = dr["GIOGIAO_CL"].ToString();
                            oAirwayTransportDetail.GIOBAY_CL = dr["GIOBAY_CL"].ToString();
                            oAirwayTransportDetail.GIODAP_QD = dr["GIODAP_QD"].ToString();
                            oAirwayTransportDetail.GIONHAN_QD = dr["GIONHAN_QD"].ToString();
                            //oAirwayTransportDetail.GIODAP_TT = dr["GIODAP_TT"].ToString();
                            oAirwayTransportDetail.GIONHAN_TT = dr["GIONHAN_TT"].ToString();
                            oAirwayTransportDetail.GIONHAN_CL = dr["GIONHAN_CL"].ToString();
                            listAirwayTransportDetail.Add(oAirwayTransportDetail);

                        }
                        _returnAirwayTransport.Code = "00";
                        _returnAirwayTransport.Message = "Lấy dữ liệu thành công.";
                        _returnAirwayTransport.ListAirwayTransportReport = listAirwayTransportDetail;
                    }
                    else
                    {
                        _returnAirwayTransport.Code = "01";
                        _returnAirwayTransport.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _returnAirwayTransport.Code = "99";
                _returnAirwayTransport.Message = "Lỗi xử lý dữ liệu";

            }
            return _returnAirwayTransport;
        }



        #endregion

        //Phần chi tiết của bảng LOAD_DATA20
        #region LOAD_DATA20          
        public ReturnAirwayTransport LOAD_DATA20(int date, int way)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnAirwayTransport _returnAirwayTransport = new ReturnAirwayTransport();


            List<AirwayTransportDetail> listAirwayTransportDetail = null;
            AirwayTransportDetail oAirwayTransportDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    //Helper.OraDSOracleConnection gọi đến database đối soát
                    OracleCommand myCommand = new OracleCommand("AIRWAY_TRANSPORT_2.LOAD_DATA20", Helper.OraDSOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("P_CHIEU", OracleDbType.Int32).Value = way;
                    myCommand.Parameters.Add("P_NGAY", OracleDbType.Int32).Value = date;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listAirwayTransportDetail = new List<AirwayTransportDetail>();
                        while (dr.Read())
                        {
                            oAirwayTransportDetail = new AirwayTransportDetail();
                            //oAirwayTransportDetail.SO = Convert.ToInt32(dr["1"].ToString());
                            oAirwayTransportDetail.STT = Convert.ToInt32(dr["STT"].ToString());
                            oAirwayTransportDetail.CHANGBAY = dr["CHANGBAY"].ToString();
                            oAirwayTransportDetail.DONVIVANCHUYEN = dr["DONVIVANCHUYEN"].ToString();
                            oAirwayTransportDetail.DICHVUVANCHUYEN = dr["DICHVUVANCHUYEN"].ToString();
                            oAirwayTransportDetail.KHUNGGIO = dr["KHUNGGIO"].ToString();
                            oAirwayTransportDetail.TONGTAITHEOHOPDONG = dr["TONGTAITHEOHOPDONG"].ToString();
                            //oAirwayTransportDetail.ID = Convert.ToInt32(dr["ID"].ToString());
                            //oAirwayTransportDetail.NGAY = Convert.ToInt32(dr["NGAY"].ToString());
                            //oAirwayTransportDetail.MABC = Convert.ToInt32(dr["MABC"].ToString());
                            //oAirwayTransportDetail.CHIEU = Convert.ToInt32(dr["CHIEU"].ToString());
                            //oAirwayTransportDetail.HUONG = Convert.ToInt32(dr["HUONG"].ToString());
                            oAirwayTransportDetail.TAICUNG_KH = dr["TAICUNG_KH"].ToString();
                            oAirwayTransportDetail.TAIMEM_KH = dr["TAIMEM_KH"].ToString();
                            oAirwayTransportDetail.MASP = dr["MASP"].ToString();
                            oAirwayTransportDetail.TAICUNG_TH = dr["TAICUNG_TH"].ToString();
                            oAirwayTransportDetail.TAIMEM_TH = dr["TAIMEM_TH"].ToString();
                            oAirwayTransportDetail.TAICUNG_LK = dr["TAICUNG_LK"].ToString();
                            oAirwayTransportDetail.TAIMEM_LK = dr["TAIMEM_LK"].ToString();
                            oAirwayTransportDetail.GIOGIAO_QD = dr["GIOGIAO_QD"].ToString();
                            oAirwayTransportDetail.GIOBAY_QD = dr["GIOBAY_QD"].ToString();
                            oAirwayTransportDetail.GIOGIAO_TT = dr["GIOGIAO_TT"].ToString();
                            oAirwayTransportDetail.GIOBAY_TT = dr["GIOBAY_TT"].ToString();
                            oAirwayTransportDetail.SOHIEUCHUYENBAY = dr["SOHIEUCHUYENBAY"].ToString();
                            oAirwayTransportDetail.GIOGIAO_CL = dr["GIOGIAO_CL"].ToString();
                            oAirwayTransportDetail.GIOBAY_CL = dr["GIOBAY_CL"].ToString();
                            oAirwayTransportDetail.GIODAP_QD = dr["GIODAP_QD"].ToString();
                            oAirwayTransportDetail.GIONHAN_QD = dr["GIONHAN_QD"].ToString();
                            //oAirwayTransportDetail.GIODAP_TT = dr["GIODAP_TT"].ToString();
                            oAirwayTransportDetail.GIONHAN_TT = dr["GIONHAN_TT"].ToString();
                            oAirwayTransportDetail.GIONHAN_CL = dr["GIONHAN_CL"].ToString();
                            listAirwayTransportDetail.Add(oAirwayTransportDetail);

                        }
                        _returnAirwayTransport.Code = "00";
                        _returnAirwayTransport.Message = "Lấy dữ liệu thành công.";
                        _returnAirwayTransport.ListAirwayTransportReport = listAirwayTransportDetail;
                    }
                    else
                    {
                        _returnAirwayTransport.Code = "01";
                        _returnAirwayTransport.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _returnAirwayTransport.Code = "99";
                _returnAirwayTransport.Message = "Lỗi xử lý dữ liệu";

            }
            return _returnAirwayTransport;
        }



        #endregion

        //Phần chi tiết của bảng LOAD_DATA21
        #region LOAD_DATA21          
        public ReturnAirwayTransport LOAD_DATA21(int date, int way)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnAirwayTransport _returnAirwayTransport = new ReturnAirwayTransport();


            List<AirwayTransportDetail> listAirwayTransportDetail = null;
            AirwayTransportDetail oAirwayTransportDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    //Helper.OraDSOracleConnection gọi đến database đối soát
                    OracleCommand myCommand = new OracleCommand("AIRWAY_TRANSPORT_2.LOAD_DATA21", Helper.OraDSOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("P_CHIEU", OracleDbType.Int32).Value = way;
                    myCommand.Parameters.Add("P_NGAY", OracleDbType.Int32).Value = date;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listAirwayTransportDetail = new List<AirwayTransportDetail>();
                        while (dr.Read())
                        {
                            oAirwayTransportDetail = new AirwayTransportDetail();
                            //oAirwayTransportDetail.SO = Convert.ToInt32(dr["1"].ToString());
                            oAirwayTransportDetail.STT = Convert.ToInt32(dr["STT"].ToString());
                            oAirwayTransportDetail.CHANGBAY = dr["CHANGBAY"].ToString();
                            oAirwayTransportDetail.DONVIVANCHUYEN = dr["DONVIVANCHUYEN"].ToString();
                            oAirwayTransportDetail.DICHVUVANCHUYEN = dr["DICHVUVANCHUYEN"].ToString();
                            oAirwayTransportDetail.KHUNGGIO = dr["KHUNGGIO"].ToString();
                            oAirwayTransportDetail.TONGTAITHEOHOPDONG = dr["TONGTAITHEOHOPDONG"].ToString();
                            //oAirwayTransportDetail.ID = Convert.ToInt32(dr["ID"].ToString());
                            //oAirwayTransportDetail.NGAY = Convert.ToInt32(dr["NGAY"].ToString());
                            //oAirwayTransportDetail.MABC = Convert.ToInt32(dr["MABC"].ToString());
                            //oAirwayTransportDetail.CHIEU = Convert.ToInt32(dr["CHIEU"].ToString());
                            //oAirwayTransportDetail.HUONG = Convert.ToInt32(dr["HUONG"].ToString());
                            oAirwayTransportDetail.TAICUNG_KH = dr["TAICUNG_KH"].ToString();
                            oAirwayTransportDetail.TAIMEM_KH = dr["TAIMEM_KH"].ToString();
                            oAirwayTransportDetail.MASP = dr["MASP"].ToString();
                            oAirwayTransportDetail.TAICUNG_TH = dr["TAICUNG_TH"].ToString();
                            oAirwayTransportDetail.TAIMEM_TH = dr["TAIMEM_TH"].ToString();
                            oAirwayTransportDetail.TAICUNG_LK = dr["TAICUNG_LK"].ToString();
                            oAirwayTransportDetail.TAIMEM_LK = dr["TAIMEM_LK"].ToString();
                            oAirwayTransportDetail.GIOGIAO_QD = dr["GIOGIAO_QD"].ToString();
                            oAirwayTransportDetail.GIOBAY_QD = dr["GIOBAY_QD"].ToString();
                            oAirwayTransportDetail.GIOGIAO_TT = dr["GIOGIAO_TT"].ToString();
                            oAirwayTransportDetail.GIOBAY_TT = dr["GIOBAY_TT"].ToString();
                            oAirwayTransportDetail.SOHIEUCHUYENBAY = dr["SOHIEUCHUYENBAY"].ToString();
                            oAirwayTransportDetail.GIOGIAO_CL = dr["GIOGIAO_CL"].ToString();
                            oAirwayTransportDetail.GIOBAY_CL = dr["GIOBAY_CL"].ToString();
                            oAirwayTransportDetail.GIODAP_QD = dr["GIODAP_QD"].ToString();
                            oAirwayTransportDetail.GIONHAN_QD = dr["GIONHAN_QD"].ToString();
                            //oAirwayTransportDetail.GIODAP_TT = dr["GIODAP_TT"].ToString();
                            oAirwayTransportDetail.GIONHAN_TT = dr["GIONHAN_TT"].ToString();
                            oAirwayTransportDetail.GIONHAN_CL = dr["GIONHAN_CL"].ToString();
                            listAirwayTransportDetail.Add(oAirwayTransportDetail);

                        }
                        _returnAirwayTransport.Code = "00";
                        _returnAirwayTransport.Message = "Lấy dữ liệu thành công.";
                        _returnAirwayTransport.ListAirwayTransportReport = listAirwayTransportDetail;
                    }
                    else
                    {
                        _returnAirwayTransport.Code = "01";
                        _returnAirwayTransport.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _returnAirwayTransport.Code = "99";
                _returnAirwayTransport.Message = "Lỗi xử lý dữ liệu";

            }
            return _returnAirwayTransport;
        }



        #endregion

        //Phần chi tiết của bảng LOAD_DATA22
        #region LOAD_DATA22          
        public ReturnAirwayTransport LOAD_DATA22(int date, int way)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnAirwayTransport _returnAirwayTransport = new ReturnAirwayTransport();


            List<AirwayTransportDetail> listAirwayTransportDetail = null;
            AirwayTransportDetail oAirwayTransportDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    //Helper.OraDSOracleConnection gọi đến database đối soát
                    OracleCommand myCommand = new OracleCommand("AIRWAY_TRANSPORT_2.LOAD_DATA22", Helper.OraDSOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("P_CHIEU", OracleDbType.Int32).Value = way;
                    myCommand.Parameters.Add("P_NGAY", OracleDbType.Int32).Value = date;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listAirwayTransportDetail = new List<AirwayTransportDetail>();
                        while (dr.Read())
                        {
                            oAirwayTransportDetail = new AirwayTransportDetail();
                            //oAirwayTransportDetail.SO = Convert.ToInt32(dr["1"].ToString());
                            oAirwayTransportDetail.STT = Convert.ToInt32(dr["STT"].ToString());
                            oAirwayTransportDetail.CHANGBAY = dr["CHANGBAY"].ToString();
                            oAirwayTransportDetail.DONVIVANCHUYEN = dr["DONVIVANCHUYEN"].ToString();
                            oAirwayTransportDetail.DICHVUVANCHUYEN = dr["DICHVUVANCHUYEN"].ToString();
                            oAirwayTransportDetail.KHUNGGIO = dr["KHUNGGIO"].ToString();
                            oAirwayTransportDetail.TONGTAITHEOHOPDONG = dr["TONGTAITHEOHOPDONG"].ToString();
                            //oAirwayTransportDetail.ID = Convert.ToInt32(dr["ID"].ToString());
                            //oAirwayTransportDetail.NGAY = Convert.ToInt32(dr["NGAY"].ToString());
                            //oAirwayTransportDetail.MABC = Convert.ToInt32(dr["MABC"].ToString());
                            //oAirwayTransportDetail.CHIEU = Convert.ToInt32(dr["CHIEU"].ToString());
                            //oAirwayTransportDetail.HUONG = Convert.ToInt32(dr["HUONG"].ToString());
                            oAirwayTransportDetail.TAICUNG_KH = dr["TAICUNG_KH"].ToString();
                            oAirwayTransportDetail.TAIMEM_KH = dr["TAIMEM_KH"].ToString();
                            oAirwayTransportDetail.MASP = dr["MASP"].ToString();
                            oAirwayTransportDetail.TAICUNG_TH = dr["TAICUNG_TH"].ToString();
                            oAirwayTransportDetail.TAIMEM_TH = dr["TAIMEM_TH"].ToString();
                            oAirwayTransportDetail.TAICUNG_LK = dr["TAICUNG_LK"].ToString();
                            oAirwayTransportDetail.TAIMEM_LK = dr["TAIMEM_LK"].ToString();
                            oAirwayTransportDetail.GIOGIAO_QD = dr["GIOGIAO_QD"].ToString();
                            oAirwayTransportDetail.GIOBAY_QD = dr["GIOBAY_QD"].ToString();
                            oAirwayTransportDetail.GIOGIAO_TT = dr["GIOGIAO_TT"].ToString();
                            oAirwayTransportDetail.GIOBAY_TT = dr["GIOBAY_TT"].ToString();
                            oAirwayTransportDetail.SOHIEUCHUYENBAY = dr["SOHIEUCHUYENBAY"].ToString();
                            oAirwayTransportDetail.GIOGIAO_CL = dr["GIOGIAO_CL"].ToString();
                            oAirwayTransportDetail.GIOBAY_CL = dr["GIOBAY_CL"].ToString();
                            oAirwayTransportDetail.GIODAP_QD = dr["GIODAP_QD"].ToString();
                            oAirwayTransportDetail.GIONHAN_QD = dr["GIONHAN_QD"].ToString();
                            //oAirwayTransportDetail.GIODAP_TT = dr["GIODAP_TT"].ToString();
                            oAirwayTransportDetail.GIONHAN_TT = dr["GIONHAN_TT"].ToString();
                            oAirwayTransportDetail.GIONHAN_CL = dr["GIONHAN_CL"].ToString();
                            listAirwayTransportDetail.Add(oAirwayTransportDetail);

                        }
                        _returnAirwayTransport.Code = "00";
                        _returnAirwayTransport.Message = "Lấy dữ liệu thành công.";
                        _returnAirwayTransport.ListAirwayTransportReport = listAirwayTransportDetail;
                    }
                    else
                    {
                        _returnAirwayTransport.Code = "01";
                        _returnAirwayTransport.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _returnAirwayTransport.Code = "99";
                _returnAirwayTransport.Message = "Lỗi xử lý dữ liệu";

            }
            return _returnAirwayTransport;
        }



        #endregion

        //Phần chi tiết của bảng LOAD_DATA23
        #region LOAD_DATA23          
        public ReturnAirwayTransport LOAD_DATA23(int date, int way)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnAirwayTransport _returnAirwayTransport = new ReturnAirwayTransport();


            List<AirwayTransportDetail> listAirwayTransportDetail = null;
            AirwayTransportDetail oAirwayTransportDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    //Helper.OraDSOracleConnection gọi đến database đối soát
                    OracleCommand myCommand = new OracleCommand("AIRWAY_TRANSPORT_2.LOAD_DATA23", Helper.OraDSOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("P_CHIEU", OracleDbType.Int32).Value = way;
                    myCommand.Parameters.Add("P_NGAY", OracleDbType.Int32).Value = date;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listAirwayTransportDetail = new List<AirwayTransportDetail>();
                        while (dr.Read())
                        {
                            oAirwayTransportDetail = new AirwayTransportDetail();
                            //oAirwayTransportDetail.SO = Convert.ToInt32(dr["1"].ToString());
                            oAirwayTransportDetail.STT = Convert.ToInt32(dr["STT"].ToString());
                            oAirwayTransportDetail.CHANGBAY = dr["CHANGBAY"].ToString();
                            oAirwayTransportDetail.DONVIVANCHUYEN = dr["DONVIVANCHUYEN"].ToString();
                            oAirwayTransportDetail.DICHVUVANCHUYEN = dr["DICHVUVANCHUYEN"].ToString();
                            oAirwayTransportDetail.KHUNGGIO = dr["KHUNGGIO"].ToString();
                            oAirwayTransportDetail.TONGTAITHEOHOPDONG = dr["TONGTAITHEOHOPDONG"].ToString();
                            //oAirwayTransportDetail.ID = Convert.ToInt32(dr["ID"].ToString());
                            //oAirwayTransportDetail.NGAY = Convert.ToInt32(dr["NGAY"].ToString());
                            //oAirwayTransportDetail.MABC = Convert.ToInt32(dr["MABC"].ToString());
                            //oAirwayTransportDetail.CHIEU = Convert.ToInt32(dr["CHIEU"].ToString());
                            //oAirwayTransportDetail.HUONG = Convert.ToInt32(dr["HUONG"].ToString());
                            oAirwayTransportDetail.TAICUNG_KH = dr["TAICUNG_KH"].ToString();
                            oAirwayTransportDetail.TAIMEM_KH = dr["TAIMEM_KH"].ToString();
                            oAirwayTransportDetail.MASP = dr["MASP"].ToString();
                            oAirwayTransportDetail.TAICUNG_TH = dr["TAICUNG_TH"].ToString();
                            oAirwayTransportDetail.TAIMEM_TH = dr["TAIMEM_TH"].ToString();
                            oAirwayTransportDetail.TAICUNG_LK = dr["TAICUNG_LK"].ToString();
                            oAirwayTransportDetail.TAIMEM_LK = dr["TAIMEM_LK"].ToString();
                            oAirwayTransportDetail.GIOGIAO_QD = dr["GIOGIAO_QD"].ToString();
                            oAirwayTransportDetail.GIOBAY_QD = dr["GIOBAY_QD"].ToString();
                            oAirwayTransportDetail.GIOGIAO_TT = dr["GIOGIAO_TT"].ToString();
                            oAirwayTransportDetail.GIOBAY_TT = dr["GIOBAY_TT"].ToString();
                            oAirwayTransportDetail.SOHIEUCHUYENBAY = dr["SOHIEUCHUYENBAY"].ToString();
                            oAirwayTransportDetail.GIOGIAO_CL = dr["GIOGIAO_CL"].ToString();
                            oAirwayTransportDetail.GIOBAY_CL = dr["GIOBAY_CL"].ToString();
                            oAirwayTransportDetail.GIODAP_QD = dr["GIODAP_QD"].ToString();
                            oAirwayTransportDetail.GIONHAN_QD = dr["GIONHAN_QD"].ToString();
                            //oAirwayTransportDetail.GIODAP_TT = dr["GIODAP_TT"].ToString();
                            oAirwayTransportDetail.GIONHAN_TT = dr["GIONHAN_TT"].ToString();
                            oAirwayTransportDetail.GIONHAN_CL = dr["GIONHAN_CL"].ToString();
                            listAirwayTransportDetail.Add(oAirwayTransportDetail);

                        }
                        _returnAirwayTransport.Code = "00";
                        _returnAirwayTransport.Message = "Lấy dữ liệu thành công.";
                        _returnAirwayTransport.ListAirwayTransportReport = listAirwayTransportDetail;
                    }
                    else
                    {
                        _returnAirwayTransport.Code = "01";
                        _returnAirwayTransport.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _returnAirwayTransport.Code = "99";
                _returnAirwayTransport.Message = "Lỗi xử lý dữ liệu";

            }
            return _returnAirwayTransport;
        }



        #endregion

        //Phần chi tiết của bảng LOAD_DATA24
        #region LOAD_DATA24          
        public ReturnAirwayTransport LOAD_DATA24(int date, int way)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnAirwayTransport _returnAirwayTransport = new ReturnAirwayTransport();


            List<AirwayTransportDetail> listAirwayTransportDetail = null;
            AirwayTransportDetail oAirwayTransportDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    //Helper.OraDSOracleConnection gọi đến database đối soát
                    OracleCommand myCommand = new OracleCommand("AIRWAY_TRANSPORT_2.LOAD_DATA24", Helper.OraDSOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("P_CHIEU", OracleDbType.Int32).Value = way;
                    myCommand.Parameters.Add("P_NGAY", OracleDbType.Int32).Value = date;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listAirwayTransportDetail = new List<AirwayTransportDetail>();
                        while (dr.Read())
                        {
                            oAirwayTransportDetail = new AirwayTransportDetail();
                            //oAirwayTransportDetail.SO = Convert.ToInt32(dr["1"].ToString());
                            oAirwayTransportDetail.STT = Convert.ToInt32(dr["STT"].ToString());
                            oAirwayTransportDetail.CHANGBAY = dr["CHANGBAY"].ToString();
                            oAirwayTransportDetail.DONVIVANCHUYEN = dr["DONVIVANCHUYEN"].ToString();
                            oAirwayTransportDetail.DICHVUVANCHUYEN = dr["DICHVUVANCHUYEN"].ToString();
                            oAirwayTransportDetail.KHUNGGIO = dr["KHUNGGIO"].ToString();
                            oAirwayTransportDetail.TONGTAITHEOHOPDONG = dr["TONGTAITHEOHOPDONG"].ToString();
                            //oAirwayTransportDetail.ID = Convert.ToInt32(dr["ID"].ToString());
                            //oAirwayTransportDetail.NGAY = Convert.ToInt32(dr["NGAY"].ToString());
                            //oAirwayTransportDetail.MABC = Convert.ToInt32(dr["MABC"].ToString());
                            //oAirwayTransportDetail.CHIEU = Convert.ToInt32(dr["CHIEU"].ToString());
                            //oAirwayTransportDetail.HUONG = Convert.ToInt32(dr["HUONG"].ToString());
                            oAirwayTransportDetail.TAICUNG_KH = dr["TAICUNG_KH"].ToString();
                            oAirwayTransportDetail.TAIMEM_KH = dr["TAIMEM_KH"].ToString();
                            oAirwayTransportDetail.MASP = dr["MASP"].ToString();
                            oAirwayTransportDetail.TAICUNG_TH = dr["TAICUNG_TH"].ToString();
                            oAirwayTransportDetail.TAIMEM_TH = dr["TAIMEM_TH"].ToString();
                            oAirwayTransportDetail.TAICUNG_LK = dr["TAICUNG_LK"].ToString();
                            oAirwayTransportDetail.TAIMEM_LK = dr["TAIMEM_LK"].ToString();
                            oAirwayTransportDetail.GIOGIAO_QD = dr["GIOGIAO_QD"].ToString();
                            oAirwayTransportDetail.GIOBAY_QD = dr["GIOBAY_QD"].ToString();
                            oAirwayTransportDetail.GIOGIAO_TT = dr["GIOGIAO_TT"].ToString();
                            oAirwayTransportDetail.GIOBAY_TT = dr["GIOBAY_TT"].ToString();
                            oAirwayTransportDetail.SOHIEUCHUYENBAY = dr["SOHIEUCHUYENBAY"].ToString();
                            oAirwayTransportDetail.GIOGIAO_CL = dr["GIOGIAO_CL"].ToString();
                            oAirwayTransportDetail.GIOBAY_CL = dr["GIOBAY_CL"].ToString();
                            oAirwayTransportDetail.GIODAP_QD = dr["GIODAP_QD"].ToString();
                            oAirwayTransportDetail.GIONHAN_QD = dr["GIONHAN_QD"].ToString();
                            //oAirwayTransportDetail.GIODAP_TT = dr["GIODAP_TT"].ToString();
                            oAirwayTransportDetail.GIONHAN_TT = dr["GIONHAN_TT"].ToString();
                            oAirwayTransportDetail.GIONHAN_CL = dr["GIONHAN_CL"].ToString();
                            listAirwayTransportDetail.Add(oAirwayTransportDetail);

                        }
                        _returnAirwayTransport.Code = "00";
                        _returnAirwayTransport.Message = "Lấy dữ liệu thành công.";
                        _returnAirwayTransport.ListAirwayTransportReport = listAirwayTransportDetail;
                    }
                    else
                    {
                        _returnAirwayTransport.Code = "01";
                        _returnAirwayTransport.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _returnAirwayTransport.Code = "99";
                _returnAirwayTransport.Message = "Lỗi xử lý dữ liệu";

            }
            return _returnAirwayTransport;
        }



        #endregion

        //Phần chi tiết của bảng LOAD_DATA25
        #region LOAD_DATA25          
        public ReturnAirwayTransport LOAD_DATA25(int date, int way)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnAirwayTransport _returnAirwayTransport = new ReturnAirwayTransport();


            List<AirwayTransportDetail> listAirwayTransportDetail = null;
            AirwayTransportDetail oAirwayTransportDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    //Helper.OraDSOracleConnection gọi đến database đối soát
                    OracleCommand myCommand = new OracleCommand("AIRWAY_TRANSPORT_2.LOAD_DATA25", Helper.OraDSOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("P_CHIEU", OracleDbType.Int32).Value = way;
                    myCommand.Parameters.Add("P_NGAY", OracleDbType.Int32).Value = date;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listAirwayTransportDetail = new List<AirwayTransportDetail>();
                        while (dr.Read())
                        {
                            oAirwayTransportDetail = new AirwayTransportDetail();
                            //oAirwayTransportDetail.SO = Convert.ToInt32(dr["1"].ToString());
                            oAirwayTransportDetail.STT = Convert.ToInt32(dr["STT"].ToString());
                            oAirwayTransportDetail.CHANGBAY = dr["CHANGBAY"].ToString();
                            oAirwayTransportDetail.DONVIVANCHUYEN = dr["DONVIVANCHUYEN"].ToString();
                            oAirwayTransportDetail.DICHVUVANCHUYEN = dr["DICHVUVANCHUYEN"].ToString();
                            oAirwayTransportDetail.KHUNGGIO = dr["KHUNGGIO"].ToString();
                            oAirwayTransportDetail.TONGTAITHEOHOPDONG = dr["TONGTAITHEOHOPDONG"].ToString();
                            //oAirwayTransportDetail.ID = Convert.ToInt32(dr["ID"].ToString());
                            //oAirwayTransportDetail.NGAY = Convert.ToInt32(dr["NGAY"].ToString());
                            //oAirwayTransportDetail.MABC = Convert.ToInt32(dr["MABC"].ToString());
                            //oAirwayTransportDetail.CHIEU = Convert.ToInt32(dr["CHIEU"].ToString());
                            //oAirwayTransportDetail.HUONG = Convert.ToInt32(dr["HUONG"].ToString());
                            oAirwayTransportDetail.TAICUNG_KH = dr["TAICUNG_KH"].ToString();
                            oAirwayTransportDetail.TAIMEM_KH = dr["TAIMEM_KH"].ToString();
                            oAirwayTransportDetail.MASP = dr["MASP"].ToString();
                            oAirwayTransportDetail.TAICUNG_TH = dr["TAICUNG_TH"].ToString();
                            oAirwayTransportDetail.TAIMEM_TH = dr["TAIMEM_TH"].ToString();
                            oAirwayTransportDetail.TAICUNG_LK = dr["TAICUNG_LK"].ToString();
                            oAirwayTransportDetail.TAIMEM_LK = dr["TAIMEM_LK"].ToString();
                            oAirwayTransportDetail.GIOGIAO_QD = dr["GIOGIAO_QD"].ToString();
                            oAirwayTransportDetail.GIOBAY_QD = dr["GIOBAY_QD"].ToString();
                            oAirwayTransportDetail.GIOGIAO_TT = dr["GIOGIAO_TT"].ToString();
                            oAirwayTransportDetail.GIOBAY_TT = dr["GIOBAY_TT"].ToString();
                            oAirwayTransportDetail.SOHIEUCHUYENBAY = dr["SOHIEUCHUYENBAY"].ToString();
                            oAirwayTransportDetail.GIOGIAO_CL = dr["GIOGIAO_CL"].ToString();
                            oAirwayTransportDetail.GIOBAY_CL = dr["GIOBAY_CL"].ToString();
                            oAirwayTransportDetail.GIODAP_QD = dr["GIODAP_QD"].ToString();
                            oAirwayTransportDetail.GIONHAN_QD = dr["GIONHAN_QD"].ToString();
                            //oAirwayTransportDetail.GIODAP_TT = dr["GIODAP_TT"].ToString();
                            oAirwayTransportDetail.GIONHAN_TT = dr["GIONHAN_TT"].ToString();
                            oAirwayTransportDetail.GIONHAN_CL = dr["GIONHAN_CL"].ToString();
                            listAirwayTransportDetail.Add(oAirwayTransportDetail);

                        }
                        _returnAirwayTransport.Code = "00";
                        _returnAirwayTransport.Message = "Lấy dữ liệu thành công.";
                        _returnAirwayTransport.ListAirwayTransportReport = listAirwayTransportDetail;
                    }
                    else
                    {
                        _returnAirwayTransport.Code = "01";
                        _returnAirwayTransport.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _returnAirwayTransport.Code = "99";
                _returnAirwayTransport.Message = "Lỗi xử lý dữ liệu";

            }
            return _returnAirwayTransport;
        }



        #endregion

        //Phần chi tiết của bảng LOAD_DATA26
        #region LOAD_DATA26          
        public ReturnAirwayTransport LOAD_DATA26(int date, int way)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnAirwayTransport _returnAirwayTransport = new ReturnAirwayTransport();


            List<AirwayTransportDetail> listAirwayTransportDetail = null;
            AirwayTransportDetail oAirwayTransportDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    //Helper.OraDSOracleConnection gọi đến database đối soát
                    OracleCommand myCommand = new OracleCommand("AIRWAY_TRANSPORT_2.LOAD_DATA26", Helper.OraDSOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("P_CHIEU", OracleDbType.Int32).Value = way;
                    myCommand.Parameters.Add("P_NGAY", OracleDbType.Int32).Value = date;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listAirwayTransportDetail = new List<AirwayTransportDetail>();
                        while (dr.Read())
                        {
                            oAirwayTransportDetail = new AirwayTransportDetail();
                            //oAirwayTransportDetail.SO = Convert.ToInt32(dr["1"].ToString());
                            oAirwayTransportDetail.STT = Convert.ToInt32(dr["STT"].ToString());
                            oAirwayTransportDetail.CHANGBAY = dr["CHANGBAY"].ToString();
                            oAirwayTransportDetail.DONVIVANCHUYEN = dr["DONVIVANCHUYEN"].ToString();
                            oAirwayTransportDetail.DICHVUVANCHUYEN = dr["DICHVUVANCHUYEN"].ToString();
                            oAirwayTransportDetail.KHUNGGIO = dr["KHUNGGIO"].ToString();
                            oAirwayTransportDetail.TONGTAITHEOHOPDONG = dr["TONGTAITHEOHOPDONG"].ToString();
                            //oAirwayTransportDetail.ID = Convert.ToInt32(dr["ID"].ToString());
                            //oAirwayTransportDetail.NGAY = Convert.ToInt32(dr["NGAY"].ToString());
                            //oAirwayTransportDetail.MABC = Convert.ToInt32(dr["MABC"].ToString());
                            //oAirwayTransportDetail.CHIEU = Convert.ToInt32(dr["CHIEU"].ToString());
                            //oAirwayTransportDetail.HUONG = Convert.ToInt32(dr["HUONG"].ToString());
                            oAirwayTransportDetail.TAICUNG_KH = dr["TAICUNG_KH"].ToString();
                            oAirwayTransportDetail.TAIMEM_KH = dr["TAIMEM_KH"].ToString();
                            oAirwayTransportDetail.MASP = dr["MASP"].ToString();
                            oAirwayTransportDetail.TAICUNG_TH = dr["TAICUNG_TH"].ToString();
                            oAirwayTransportDetail.TAIMEM_TH = dr["TAIMEM_TH"].ToString();
                            oAirwayTransportDetail.TAICUNG_LK = dr["TAICUNG_LK"].ToString();
                            oAirwayTransportDetail.TAIMEM_LK = dr["TAIMEM_LK"].ToString();
                            oAirwayTransportDetail.GIOGIAO_QD = dr["GIOGIAO_QD"].ToString();
                            oAirwayTransportDetail.GIOBAY_QD = dr["GIOBAY_QD"].ToString();
                            oAirwayTransportDetail.GIOGIAO_TT = dr["GIOGIAO_TT"].ToString();
                            oAirwayTransportDetail.GIOBAY_TT = dr["GIOBAY_TT"].ToString();
                            oAirwayTransportDetail.SOHIEUCHUYENBAY = dr["SOHIEUCHUYENBAY"].ToString();
                            oAirwayTransportDetail.GIOGIAO_CL = dr["GIOGIAO_CL"].ToString();
                            oAirwayTransportDetail.GIOBAY_CL = dr["GIOBAY_CL"].ToString();
                            oAirwayTransportDetail.GIODAP_QD = dr["GIODAP_QD"].ToString();
                            oAirwayTransportDetail.GIONHAN_QD = dr["GIONHAN_QD"].ToString();
                            //oAirwayTransportDetail.GIODAP_TT = dr["GIODAP_TT"].ToString();
                            oAirwayTransportDetail.GIONHAN_TT = dr["GIONHAN_TT"].ToString();
                            oAirwayTransportDetail.GIONHAN_CL = dr["GIONHAN_CL"].ToString();
                            listAirwayTransportDetail.Add(oAirwayTransportDetail);

                        }
                        _returnAirwayTransport.Code = "00";
                        _returnAirwayTransport.Message = "Lấy dữ liệu thành công.";
                        _returnAirwayTransport.ListAirwayTransportReport = listAirwayTransportDetail;
                    }
                    else
                    {
                        _returnAirwayTransport.Code = "01";
                        _returnAirwayTransport.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _returnAirwayTransport.Code = "99";
                _returnAirwayTransport.Message = "Lỗi xử lý dữ liệu";

            }
            return _returnAirwayTransport;
        }



        #endregion

        //Phần chi tiết của bảng LOAD_DATA27
        #region LOAD_DATA27          
        public ReturnAirwayTransport LOAD_DATA27(int date, int way)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnAirwayTransport _returnAirwayTransport = new ReturnAirwayTransport();


            List<AirwayTransportDetail> listAirwayTransportDetail = null;
            AirwayTransportDetail oAirwayTransportDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    //Helper.OraDSOracleConnection gọi đến database đối soát
                    OracleCommand myCommand = new OracleCommand("AIRWAY_TRANSPORT_2.LOAD_DATA27", Helper.OraDSOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("P_CHIEU", OracleDbType.Int32).Value = way;
                    myCommand.Parameters.Add("P_NGAY", OracleDbType.Int32).Value = date;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listAirwayTransportDetail = new List<AirwayTransportDetail>();
                        while (dr.Read())
                        {
                            oAirwayTransportDetail = new AirwayTransportDetail();
                            //oAirwayTransportDetail.SO = Convert.ToInt32(dr["1"].ToString());
                            oAirwayTransportDetail.STT = Convert.ToInt32(dr["STT"].ToString());
                            oAirwayTransportDetail.CHANGBAY = dr["CHANGBAY"].ToString();
                            oAirwayTransportDetail.DONVIVANCHUYEN = dr["DONVIVANCHUYEN"].ToString();
                            oAirwayTransportDetail.DICHVUVANCHUYEN = dr["DICHVUVANCHUYEN"].ToString();
                            oAirwayTransportDetail.KHUNGGIO = dr["KHUNGGIO"].ToString();
                            oAirwayTransportDetail.TONGTAITHEOHOPDONG = dr["TONGTAITHEOHOPDONG"].ToString();
                            //oAirwayTransportDetail.ID = Convert.ToInt32(dr["ID"].ToString());
                            //oAirwayTransportDetail.NGAY = Convert.ToInt32(dr["NGAY"].ToString());
                            //oAirwayTransportDetail.MABC = Convert.ToInt32(dr["MABC"].ToString());
                            //oAirwayTransportDetail.CHIEU = Convert.ToInt32(dr["CHIEU"].ToString());
                            //oAirwayTransportDetail.HUONG = Convert.ToInt32(dr["HUONG"].ToString());
                            oAirwayTransportDetail.TAICUNG_KH = dr["TAICUNG_KH"].ToString();
                            oAirwayTransportDetail.TAIMEM_KH = dr["TAIMEM_KH"].ToString();
                            oAirwayTransportDetail.MASP = dr["MASP"].ToString();
                            oAirwayTransportDetail.TAICUNG_TH = dr["TAICUNG_TH"].ToString();
                            oAirwayTransportDetail.TAIMEM_TH = dr["TAIMEM_TH"].ToString();
                            oAirwayTransportDetail.TAICUNG_LK = dr["TAICUNG_LK"].ToString();
                            oAirwayTransportDetail.TAIMEM_LK = dr["TAIMEM_LK"].ToString();
                            oAirwayTransportDetail.GIOGIAO_QD = dr["GIOGIAO_QD"].ToString();
                            oAirwayTransportDetail.GIOBAY_QD = dr["GIOBAY_QD"].ToString();
                            oAirwayTransportDetail.GIOGIAO_TT = dr["GIOGIAO_TT"].ToString();
                            oAirwayTransportDetail.GIOBAY_TT = dr["GIOBAY_TT"].ToString();
                            oAirwayTransportDetail.SOHIEUCHUYENBAY = dr["SOHIEUCHUYENBAY"].ToString();
                            oAirwayTransportDetail.GIOGIAO_CL = dr["GIOGIAO_CL"].ToString();
                            oAirwayTransportDetail.GIOBAY_CL = dr["GIOBAY_CL"].ToString();
                            oAirwayTransportDetail.GIODAP_QD = dr["GIODAP_QD"].ToString();
                            oAirwayTransportDetail.GIONHAN_QD = dr["GIONHAN_QD"].ToString();
                            //oAirwayTransportDetail.GIODAP_TT = dr["GIODAP_TT"].ToString();
                            oAirwayTransportDetail.GIONHAN_TT = dr["GIONHAN_TT"].ToString();
                            oAirwayTransportDetail.GIONHAN_CL = dr["GIONHAN_CL"].ToString();
                            listAirwayTransportDetail.Add(oAirwayTransportDetail);

                        }
                        _returnAirwayTransport.Code = "00";
                        _returnAirwayTransport.Message = "Lấy dữ liệu thành công.";
                        _returnAirwayTransport.ListAirwayTransportReport = listAirwayTransportDetail;
                    }
                    else
                    {
                        _returnAirwayTransport.Code = "01";
                        _returnAirwayTransport.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _returnAirwayTransport.Code = "99";
                _returnAirwayTransport.Message = "Lỗi xử lý dữ liệu";

            }
            return _returnAirwayTransport;
        }



        #endregion

        //Phần chi tiết của bảng LOAD_DATA28
        #region LOAD_DATA28          
        public ReturnAirwayTransport LOAD_DATA28(int date, int way)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnAirwayTransport _returnAirwayTransport = new ReturnAirwayTransport();


            List<AirwayTransportDetail> listAirwayTransportDetail = null;
            AirwayTransportDetail oAirwayTransportDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    //Helper.OraDSOracleConnection gọi đến database đối soát
                    OracleCommand myCommand = new OracleCommand("AIRWAY_TRANSPORT_2.LOAD_DATA28", Helper.OraDSOracleConnection);
                    //xử lý tham số truyền vào data table
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandTimeout = 20000;
                    OracleDataAdapter mAdapter = new OracleDataAdapter();
                    myCommand.Parameters.Add("P_CHIEU", OracleDbType.Int32).Value = way;
                    myCommand.Parameters.Add("P_NGAY", OracleDbType.Int32).Value = date;
                    myCommand.Parameters.Add(new OracleParameter("v_ListStage", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    mAdapter = new OracleDataAdapter(myCommand);
                    mAdapter.Fill(da);

                    DataTableReader dr = da.CreateDataReader();
                    if (dr.HasRows)
                    {
                        listAirwayTransportDetail = new List<AirwayTransportDetail>();
                        while (dr.Read())
                        {
                            oAirwayTransportDetail = new AirwayTransportDetail();
                            //oAirwayTransportDetail.SO = Convert.ToInt32(dr["1"].ToString());
                            oAirwayTransportDetail.STT = Convert.ToInt32(dr["STT"].ToString());
                            oAirwayTransportDetail.CHANGBAY = dr["CHANGBAY"].ToString();
                            oAirwayTransportDetail.DONVIVANCHUYEN = dr["DONVIVANCHUYEN"].ToString();
                            oAirwayTransportDetail.DICHVUVANCHUYEN = dr["DICHVUVANCHUYEN"].ToString();
                            oAirwayTransportDetail.KHUNGGIO = dr["KHUNGGIO"].ToString();
                            oAirwayTransportDetail.TONGTAITHEOHOPDONG = dr["TONGTAITHEOHOPDONG"].ToString();
                            //oAirwayTransportDetail.ID = Convert.ToInt32(dr["ID"].ToString());
                            //oAirwayTransportDetail.NGAY = Convert.ToInt32(dr["NGAY"].ToString());
                            //oAirwayTransportDetail.MABC = Convert.ToInt32(dr["MABC"].ToString());
                            //oAirwayTransportDetail.CHIEU = Convert.ToInt32(dr["CHIEU"].ToString());
                            //oAirwayTransportDetail.HUONG = Convert.ToInt32(dr["HUONG"].ToString());
                            oAirwayTransportDetail.TAICUNG_KH = dr["TAICUNG_KH"].ToString();
                            oAirwayTransportDetail.TAIMEM_KH = dr["TAIMEM_KH"].ToString();
                            oAirwayTransportDetail.MASP = dr["MASP"].ToString();
                            oAirwayTransportDetail.TAICUNG_TH = dr["TAICUNG_TH"].ToString();
                            oAirwayTransportDetail.TAIMEM_TH = dr["TAIMEM_TH"].ToString();
                            oAirwayTransportDetail.TAICUNG_LK = dr["TAICUNG_LK"].ToString();
                            oAirwayTransportDetail.TAIMEM_LK = dr["TAIMEM_LK"].ToString();
                            oAirwayTransportDetail.GIOGIAO_QD = dr["GIOGIAO_QD"].ToString();
                            oAirwayTransportDetail.GIOBAY_QD = dr["GIOBAY_QD"].ToString();
                            oAirwayTransportDetail.GIOGIAO_TT = dr["GIOGIAO_TT"].ToString();
                            oAirwayTransportDetail.GIOBAY_TT = dr["GIOBAY_TT"].ToString();
                            oAirwayTransportDetail.SOHIEUCHUYENBAY = dr["SOHIEUCHUYENBAY"].ToString();
                            oAirwayTransportDetail.GIOGIAO_CL = dr["GIOGIAO_CL"].ToString();
                            oAirwayTransportDetail.GIOBAY_CL = dr["GIOBAY_CL"].ToString();
                            oAirwayTransportDetail.GIODAP_QD = dr["GIODAP_QD"].ToString();
                            oAirwayTransportDetail.GIONHAN_QD = dr["GIONHAN_QD"].ToString();
                            //oAirwayTransportDetail.GIODAP_TT = dr["GIODAP_TT"].ToString();
                            oAirwayTransportDetail.GIONHAN_TT = dr["GIONHAN_TT"].ToString();
                            oAirwayTransportDetail.GIONHAN_CL = dr["GIONHAN_CL"].ToString();
                            listAirwayTransportDetail.Add(oAirwayTransportDetail);

                        }
                        _returnAirwayTransport.Code = "00";
                        _returnAirwayTransport.Message = "Lấy dữ liệu thành công.";
                        _returnAirwayTransport.ListAirwayTransportReport = listAirwayTransportDetail;
                    }
                    else
                    {
                        _returnAirwayTransport.Code = "01";
                        _returnAirwayTransport.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _returnAirwayTransport.Code = "99";
                _returnAirwayTransport.Message = "Lỗi xử lý dữ liệu";

            }
            return _returnAirwayTransport;
        }



        #endregion
    }

}

