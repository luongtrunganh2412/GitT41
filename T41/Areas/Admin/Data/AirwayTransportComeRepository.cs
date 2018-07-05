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
    public class AirwayTransportComeRepository
    {
        //Phần chi tiết của bảng Tổng Hợp Dữ Liệu
        #region TOTAL_DATA          
        public ReturnAirwayTransportCome TOTAL_DATA(int date)
        {
            ReturnAirwayTransportCome oAirwayTransportComeDetail = new ReturnAirwayTransportCome();
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
                    oAirwayTransportComeDetail.Code = "00";
                    oAirwayTransportComeDetail.Message = "Tổng Hợp Dữ Liệu Thành Công";
                    
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, ex.Message);
                oAirwayTransportComeDetail = null;
            }
            return oAirwayTransportComeDetail;
        }

        #endregion


        //Phần chi tiết của bảng LOAD_DATA1
        #region LOAD_DATA1          
        public ReturnAirwayTransportCome LOAD_DATA1( int date, int way)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnAirwayTransportCome _ReturnAirwayTransportCome = new ReturnAirwayTransportCome();

            
            List<AirwayTransportComeDetail> listAirwayTransportComeDetail = null;
            AirwayTransportComeDetail oAirwayTransportComeDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    //Helper.OraDSOracleConnection gọi đến database đối soát
                    OracleCommand myCommand = new OracleCommand("AIRWAY_TRANSPORT_COME_2.LOAD_DATA1", Helper.OraDSOracleConnection);
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
                        listAirwayTransportComeDetail = new List<AirwayTransportComeDetail>();
                        while (dr.Read())
                        {
                            oAirwayTransportComeDetail = new AirwayTransportComeDetail();
                            //oAirwayTransportComeDetail.SO = Convert.ToInt32(dr["1"].ToString());
                            oAirwayTransportComeDetail.STT = Convert.ToInt32(dr["STT"].ToString());
                            oAirwayTransportComeDetail.CHANGBAY = dr["CHANGBAY"].ToString();
                            oAirwayTransportComeDetail.DONVIVANCHUYEN = dr["DONVIVANCHUYEN"].ToString();
                            oAirwayTransportComeDetail.DICHVUVANCHUYEN = dr["DICHVUVANCHUYEN"].ToString();
                            oAirwayTransportComeDetail.KHUNGGIO = dr["KHUNGGIO"].ToString();
                            oAirwayTransportComeDetail.TONGTAITHEOHOPDONG = dr["TONGTAITHEOHOPDONG"].ToString();
                            //oAirwayTransportComeDetail.ID = Convert.ToInt32(dr["ID"].ToString());
                            //oAirwayTransportComeDetail.NGAY = Convert.ToInt32(dr["NGAY"].ToString());
                            //oAirwayTransportComeDetail.MABC = Convert.ToInt32(dr["MABC"].ToString());
                            //oAirwayTransportComeDetail.CHIEU = Convert.ToInt32(dr["CHIEU"].ToString());
                            //oAirwayTransportComeDetail.HUONG = Convert.ToInt32(dr["HUONG"].ToString());
                            oAirwayTransportComeDetail.TAICUNG_KH = dr["TAICUNG_KH"].ToString();
                            oAirwayTransportComeDetail.TAIMEM_KH = dr["TAIMEM_KH"].ToString();
                            oAirwayTransportComeDetail.MASP = dr["MASP"].ToString();
                            oAirwayTransportComeDetail.TAICUNG_TH = dr["TAICUNG_TH"].ToString();
                            oAirwayTransportComeDetail.TAIMEM_TH = dr["TAIMEM_TH"].ToString();
                            oAirwayTransportComeDetail.TAICUNG_LK = dr["TAICUNG_LK"].ToString();
                            oAirwayTransportComeDetail.TAIMEM_LK = dr["TAIMEM_LK"].ToString();
                            oAirwayTransportComeDetail.GIOGIAO_QD = dr["GIOGIAO_QD"].ToString();
                            oAirwayTransportComeDetail.GIOBAY_QD = dr["GIOBAY_QD"].ToString();
                            oAirwayTransportComeDetail.GIOGIAO_TT = dr["GIOGIAO_TT"].ToString();
                            oAirwayTransportComeDetail.GIOBAY_TT = dr["GIOBAY_TT"].ToString();
                            oAirwayTransportComeDetail.SOHIEUCHUYENBAY = dr["SOHIEUCHUYENBAY"].ToString();
                            oAirwayTransportComeDetail.GIOGIAO_CL = dr["GIOGIAO_CL"].ToString();
                            oAirwayTransportComeDetail.GIOBAY_CL = dr["GIOBAY_CL"].ToString();
                            oAirwayTransportComeDetail.GIODAP_QD = dr["GIODAP_QD"].ToString();
                            oAirwayTransportComeDetail.GIONHAN_QD = dr["GIONHAN_QD"].ToString();
                            oAirwayTransportComeDetail.GIONHAN_TT = dr["GIONHAN_TT"].ToString();
                            oAirwayTransportComeDetail.GIONHAN_CL = dr["GIONHAN_CL"].ToString();
                            listAirwayTransportComeDetail.Add(oAirwayTransportComeDetail);

                        }
                        _ReturnAirwayTransportCome.Code = "00";
                        _ReturnAirwayTransportCome.Message = "Lấy dữ liệu thành công.";
                        _ReturnAirwayTransportCome.ListAirwayTransportComeReport = listAirwayTransportComeDetail;
                    }
                    else
                    {
                        _ReturnAirwayTransportCome.Code = "01";
                        _ReturnAirwayTransportCome.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _ReturnAirwayTransportCome.Code = "99";
                _ReturnAirwayTransportCome.Message = "Lỗi xử lý dữ liệu";
                
            }
            return _ReturnAirwayTransportCome;
        }



        #endregion

        //Phần chi tiết của bảng LOAD_DATA2
        #region LOAD_DATA2          
        public ReturnAirwayTransportCome LOAD_DATA2(int date, int way)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnAirwayTransportCome _ReturnAirwayTransportCome = new ReturnAirwayTransportCome();


            List<AirwayTransportComeDetail> listAirwayTransportComeDetail = null;
            AirwayTransportComeDetail oAirwayTransportComeDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    //Helper.OraDSOracleConnection gọi đến database đối soát
                    OracleCommand myCommand = new OracleCommand("AIRWAY_TRANSPORT_COME_2.LOAD_DATA2", Helper.OraDSOracleConnection);
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
                        listAirwayTransportComeDetail = new List<AirwayTransportComeDetail>();
                        while (dr.Read())
                        {
                            oAirwayTransportComeDetail = new AirwayTransportComeDetail();
                            //oAirwayTransportComeDetail.SO = Convert.ToInt32(dr["1"].ToString());
                            oAirwayTransportComeDetail.STT = Convert.ToInt32(dr["STT"].ToString());
                            oAirwayTransportComeDetail.CHANGBAY = dr["CHANGBAY"].ToString();
                            oAirwayTransportComeDetail.DONVIVANCHUYEN = dr["DONVIVANCHUYEN"].ToString();
                            oAirwayTransportComeDetail.DICHVUVANCHUYEN = dr["DICHVUVANCHUYEN"].ToString();
                            oAirwayTransportComeDetail.KHUNGGIO = dr["KHUNGGIO"].ToString();
                            oAirwayTransportComeDetail.TONGTAITHEOHOPDONG = dr["TONGTAITHEOHOPDONG"].ToString();
                            //oAirwayTransportComeDetail.ID = Convert.ToInt32(dr["ID"].ToString());
                            //oAirwayTransportComeDetail.NGAY = Convert.ToInt32(dr["NGAY"].ToString());
                            //oAirwayTransportComeDetail.MABC = Convert.ToInt32(dr["MABC"].ToString());
                            //oAirwayTransportComeDetail.CHIEU = Convert.ToInt32(dr["CHIEU"].ToString());
                            //oAirwayTransportComeDetail.HUONG = Convert.ToInt32(dr["HUONG"].ToString());
                            oAirwayTransportComeDetail.TAICUNG_KH = dr["TAICUNG_KH"].ToString();
                            oAirwayTransportComeDetail.TAIMEM_KH = dr["TAIMEM_KH"].ToString();
                            oAirwayTransportComeDetail.MASP = dr["MASP"].ToString();
                            oAirwayTransportComeDetail.TAICUNG_TH = dr["TAICUNG_TH"].ToString();
                            oAirwayTransportComeDetail.TAIMEM_TH = dr["TAIMEM_TH"].ToString();
                            oAirwayTransportComeDetail.TAICUNG_LK = dr["TAICUNG_LK"].ToString();
                            oAirwayTransportComeDetail.TAIMEM_LK = dr["TAIMEM_LK"].ToString();
                            oAirwayTransportComeDetail.GIOGIAO_QD = dr["GIOGIAO_QD"].ToString();
                            oAirwayTransportComeDetail.GIOBAY_QD = dr["GIOBAY_QD"].ToString();
                            oAirwayTransportComeDetail.GIOGIAO_TT = dr["GIOGIAO_TT"].ToString();
                            oAirwayTransportComeDetail.GIOBAY_TT = dr["GIOBAY_TT"].ToString();
                            oAirwayTransportComeDetail.SOHIEUCHUYENBAY = dr["SOHIEUCHUYENBAY"].ToString();
                            oAirwayTransportComeDetail.GIOGIAO_CL = dr["GIOGIAO_CL"].ToString();
                            oAirwayTransportComeDetail.GIOBAY_CL = dr["GIOBAY_CL"].ToString();
                            oAirwayTransportComeDetail.GIODAP_QD = dr["GIODAP_QD"].ToString();
                            oAirwayTransportComeDetail.GIONHAN_QD = dr["GIONHAN_QD"].ToString();
                            //oAirwayTransportComeDetail.GIODAP_TT = dr["GIODAP_TT"].ToString();
                            oAirwayTransportComeDetail.GIONHAN_TT = dr["GIONHAN_TT"].ToString();
                            oAirwayTransportComeDetail.GIONHAN_CL = dr["GIONHAN_CL"].ToString();
                            listAirwayTransportComeDetail.Add(oAirwayTransportComeDetail);

                        }
                        _ReturnAirwayTransportCome.Code = "00";
                        _ReturnAirwayTransportCome.Message = "Lấy dữ liệu thành công.";
                        _ReturnAirwayTransportCome.ListAirwayTransportComeReport = listAirwayTransportComeDetail;
                    }
                    else
                    {
                        _ReturnAirwayTransportCome.Code = "01";
                        _ReturnAirwayTransportCome.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _ReturnAirwayTransportCome.Code = "99";
                _ReturnAirwayTransportCome.Message = "Lỗi xử lý dữ liệu";

            }
            return _ReturnAirwayTransportCome;
        }



        #endregion

        //Phần chi tiết của bảng LOAD_DATA3
        #region LOAD_DATA3          
        public ReturnAirwayTransportCome LOAD_DATA3(int date, int way)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnAirwayTransportCome _ReturnAirwayTransportCome = new ReturnAirwayTransportCome();


            List<AirwayTransportComeDetail> listAirwayTransportComeDetail = null;
            AirwayTransportComeDetail oAirwayTransportComeDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    //Helper.OraDSOracleConnection gọi đến database đối soát
                    OracleCommand myCommand = new OracleCommand("AIRWAY_TRANSPORT_COME_2.LOAD_DATA3", Helper.OraDSOracleConnection);
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
                        listAirwayTransportComeDetail = new List<AirwayTransportComeDetail>();
                        while (dr.Read())
                        {
                            oAirwayTransportComeDetail = new AirwayTransportComeDetail();
                            //oAirwayTransportComeDetail.SO = Convert.ToInt32(dr["1"].ToString());
                            oAirwayTransportComeDetail.STT = Convert.ToInt32(dr["STT"].ToString());
                            oAirwayTransportComeDetail.CHANGBAY = dr["CHANGBAY"].ToString();
                            oAirwayTransportComeDetail.DONVIVANCHUYEN = dr["DONVIVANCHUYEN"].ToString();
                            oAirwayTransportComeDetail.DICHVUVANCHUYEN = dr["DICHVUVANCHUYEN"].ToString();
                            oAirwayTransportComeDetail.KHUNGGIO = dr["KHUNGGIO"].ToString();
                            oAirwayTransportComeDetail.TONGTAITHEOHOPDONG = dr["TONGTAITHEOHOPDONG"].ToString();
                            //oAirwayTransportComeDetail.ID = Convert.ToInt32(dr["ID"].ToString());
                            //oAirwayTransportComeDetail.NGAY = Convert.ToInt32(dr["NGAY"].ToString());
                            //oAirwayTransportComeDetail.MABC = Convert.ToInt32(dr["MABC"].ToString());
                            //oAirwayTransportComeDetail.CHIEU = Convert.ToInt32(dr["CHIEU"].ToString());
                            //oAirwayTransportComeDetail.HUONG = Convert.ToInt32(dr["HUONG"].ToString());
                            oAirwayTransportComeDetail.TAICUNG_KH = dr["TAICUNG_KH"].ToString();
                            oAirwayTransportComeDetail.TAIMEM_KH = dr["TAIMEM_KH"].ToString();
                            oAirwayTransportComeDetail.MASP = dr["MASP"].ToString();
                            oAirwayTransportComeDetail.TAICUNG_TH = dr["TAICUNG_TH"].ToString();
                            oAirwayTransportComeDetail.TAIMEM_TH = dr["TAIMEM_TH"].ToString();
                            oAirwayTransportComeDetail.TAICUNG_LK = dr["TAICUNG_LK"].ToString();
                            oAirwayTransportComeDetail.TAIMEM_LK = dr["TAIMEM_LK"].ToString();
                            oAirwayTransportComeDetail.GIOGIAO_QD = dr["GIOGIAO_QD"].ToString();
                            oAirwayTransportComeDetail.GIOBAY_QD = dr["GIOBAY_QD"].ToString();
                            oAirwayTransportComeDetail.GIOGIAO_TT = dr["GIOGIAO_TT"].ToString();
                            oAirwayTransportComeDetail.GIOBAY_TT = dr["GIOBAY_TT"].ToString();
                            oAirwayTransportComeDetail.SOHIEUCHUYENBAY = dr["SOHIEUCHUYENBAY"].ToString();
                            oAirwayTransportComeDetail.GIOGIAO_CL = dr["GIOGIAO_CL"].ToString();
                            oAirwayTransportComeDetail.GIOBAY_CL = dr["GIOBAY_CL"].ToString();
                            oAirwayTransportComeDetail.GIODAP_QD = dr["GIODAP_QD"].ToString();
                            oAirwayTransportComeDetail.GIONHAN_QD = dr["GIONHAN_QD"].ToString();
                            //oAirwayTransportComeDetail.GIODAP_TT = dr["GIODAP_TT"].ToString();
                            oAirwayTransportComeDetail.GIONHAN_TT = dr["GIONHAN_TT"].ToString();
                            oAirwayTransportComeDetail.GIONHAN_CL = dr["GIONHAN_CL"].ToString();
                            listAirwayTransportComeDetail.Add(oAirwayTransportComeDetail);

                        }
                        _ReturnAirwayTransportCome.Code = "00";
                        _ReturnAirwayTransportCome.Message = "Lấy dữ liệu thành công.";
                        _ReturnAirwayTransportCome.ListAirwayTransportComeReport = listAirwayTransportComeDetail;
                    }
                    else
                    {
                        _ReturnAirwayTransportCome.Code = "01";
                        _ReturnAirwayTransportCome.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _ReturnAirwayTransportCome.Code = "99";
                _ReturnAirwayTransportCome.Message = "Lỗi xử lý dữ liệu";

            }
            return _ReturnAirwayTransportCome;
        }



        #endregion

        //Phần chi tiết của bảng LOAD_DATA4
        #region LOAD_DATA4          
        public ReturnAirwayTransportCome LOAD_DATA4(int date, int way)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnAirwayTransportCome _ReturnAirwayTransportCome = new ReturnAirwayTransportCome();


            List<AirwayTransportComeDetail> listAirwayTransportComeDetail = null;
            AirwayTransportComeDetail oAirwayTransportComeDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    //Helper.OraDSOracleConnection gọi đến database đối soát
                    OracleCommand myCommand = new OracleCommand("AIRWAY_TRANSPORT_COME_2.LOAD_DATA4", Helper.OraDSOracleConnection);
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
                        listAirwayTransportComeDetail = new List<AirwayTransportComeDetail>();
                        while (dr.Read())
                        {
                            oAirwayTransportComeDetail = new AirwayTransportComeDetail();
                            //oAirwayTransportComeDetail.SO = Convert.ToInt32(dr["1"].ToString());
                            oAirwayTransportComeDetail.STT = Convert.ToInt32(dr["STT"].ToString());
                            oAirwayTransportComeDetail.CHANGBAY = dr["CHANGBAY"].ToString();
                            oAirwayTransportComeDetail.DONVIVANCHUYEN = dr["DONVIVANCHUYEN"].ToString();
                            oAirwayTransportComeDetail.DICHVUVANCHUYEN = dr["DICHVUVANCHUYEN"].ToString();
                            oAirwayTransportComeDetail.KHUNGGIO = dr["KHUNGGIO"].ToString();
                            oAirwayTransportComeDetail.TONGTAITHEOHOPDONG = dr["TONGTAITHEOHOPDONG"].ToString();
                            //oAirwayTransportComeDetail.ID = Convert.ToInt32(dr["ID"].ToString());
                            //oAirwayTransportComeDetail.NGAY = Convert.ToInt32(dr["NGAY"].ToString());
                            //oAirwayTransportComeDetail.MABC = Convert.ToInt32(dr["MABC"].ToString());
                            //oAirwayTransportComeDetail.CHIEU = Convert.ToInt32(dr["CHIEU"].ToString());
                            //oAirwayTransportComeDetail.HUONG = Convert.ToInt32(dr["HUONG"].ToString());
                            oAirwayTransportComeDetail.TAICUNG_KH = dr["TAICUNG_KH"].ToString();
                            oAirwayTransportComeDetail.TAIMEM_KH = dr["TAIMEM_KH"].ToString();
                            oAirwayTransportComeDetail.MASP = dr["MASP"].ToString();
                            oAirwayTransportComeDetail.TAICUNG_TH = dr["TAICUNG_TH"].ToString();
                            oAirwayTransportComeDetail.TAIMEM_TH = dr["TAIMEM_TH"].ToString();
                            oAirwayTransportComeDetail.TAICUNG_LK = dr["TAICUNG_LK"].ToString();
                            oAirwayTransportComeDetail.TAIMEM_LK = dr["TAIMEM_LK"].ToString();
                            oAirwayTransportComeDetail.GIOGIAO_QD = dr["GIOGIAO_QD"].ToString();
                            oAirwayTransportComeDetail.GIOBAY_QD = dr["GIOBAY_QD"].ToString();
                            oAirwayTransportComeDetail.GIOGIAO_TT = dr["GIOGIAO_TT"].ToString();
                            oAirwayTransportComeDetail.GIOBAY_TT = dr["GIOBAY_TT"].ToString();
                            oAirwayTransportComeDetail.SOHIEUCHUYENBAY = dr["SOHIEUCHUYENBAY"].ToString();
                            oAirwayTransportComeDetail.GIOGIAO_CL = dr["GIOGIAO_CL"].ToString();
                            oAirwayTransportComeDetail.GIOBAY_CL = dr["GIOBAY_CL"].ToString();
                            oAirwayTransportComeDetail.GIODAP_QD = dr["GIODAP_QD"].ToString();
                            oAirwayTransportComeDetail.GIONHAN_QD = dr["GIONHAN_QD"].ToString();
                            //oAirwayTransportComeDetail.GIODAP_TT = dr["GIODAP_TT"].ToString();
                            oAirwayTransportComeDetail.GIONHAN_TT = dr["GIONHAN_TT"].ToString();
                            oAirwayTransportComeDetail.GIONHAN_CL = dr["GIONHAN_CL"].ToString();
                            listAirwayTransportComeDetail.Add(oAirwayTransportComeDetail);

                        }
                        _ReturnAirwayTransportCome.Code = "00";
                        _ReturnAirwayTransportCome.Message = "Lấy dữ liệu thành công.";
                        _ReturnAirwayTransportCome.ListAirwayTransportComeReport = listAirwayTransportComeDetail;
                    }
                    else
                    {
                        _ReturnAirwayTransportCome.Code = "01";
                        _ReturnAirwayTransportCome.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _ReturnAirwayTransportCome.Code = "99";
                _ReturnAirwayTransportCome.Message = "Lỗi xử lý dữ liệu";

            }
            return _ReturnAirwayTransportCome;
        }



        #endregion

        //Phần chi tiết của bảng LOAD_DATA5
        #region LOAD_DATA5          
        public ReturnAirwayTransportCome LOAD_DATA5(int date, int way)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnAirwayTransportCome _ReturnAirwayTransportCome = new ReturnAirwayTransportCome();


            List<AirwayTransportComeDetail> listAirwayTransportComeDetail = null;
            AirwayTransportComeDetail oAirwayTransportComeDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    //Helper.OraDSOracleConnection gọi đến database đối soát
                    OracleCommand myCommand = new OracleCommand("AIRWAY_TRANSPORT_COME_2.LOAD_DATA5", Helper.OraDSOracleConnection);
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
                        listAirwayTransportComeDetail = new List<AirwayTransportComeDetail>();
                        while (dr.Read())
                        {
                            oAirwayTransportComeDetail = new AirwayTransportComeDetail();
                            //oAirwayTransportComeDetail.SO = Convert.ToInt32(dr["1"].ToString());
                            oAirwayTransportComeDetail.STT = Convert.ToInt32(dr["STT"].ToString());
                            oAirwayTransportComeDetail.CHANGBAY = dr["CHANGBAY"].ToString();
                            oAirwayTransportComeDetail.DONVIVANCHUYEN = dr["DONVIVANCHUYEN"].ToString();
                            oAirwayTransportComeDetail.DICHVUVANCHUYEN = dr["DICHVUVANCHUYEN"].ToString();
                            oAirwayTransportComeDetail.KHUNGGIO = dr["KHUNGGIO"].ToString();
                            oAirwayTransportComeDetail.TONGTAITHEOHOPDONG = dr["TONGTAITHEOHOPDONG"].ToString();
                            //oAirwayTransportComeDetail.ID = Convert.ToInt32(dr["ID"].ToString());
                            //oAirwayTransportComeDetail.NGAY = Convert.ToInt32(dr["NGAY"].ToString());
                            //oAirwayTransportComeDetail.MABC = Convert.ToInt32(dr["MABC"].ToString());
                            //oAirwayTransportComeDetail.CHIEU = Convert.ToInt32(dr["CHIEU"].ToString());
                            //oAirwayTransportComeDetail.HUONG = Convert.ToInt32(dr["HUONG"].ToString());
                            oAirwayTransportComeDetail.TAICUNG_KH = dr["TAICUNG_KH"].ToString();
                            oAirwayTransportComeDetail.TAIMEM_KH = dr["TAIMEM_KH"].ToString();
                            oAirwayTransportComeDetail.MASP = dr["MASP"].ToString();
                            oAirwayTransportComeDetail.TAICUNG_TH = dr["TAICUNG_TH"].ToString();
                            oAirwayTransportComeDetail.TAIMEM_TH = dr["TAIMEM_TH"].ToString();
                            oAirwayTransportComeDetail.TAICUNG_LK = dr["TAICUNG_LK"].ToString();
                            oAirwayTransportComeDetail.TAIMEM_LK = dr["TAIMEM_LK"].ToString();
                            oAirwayTransportComeDetail.GIOGIAO_QD = dr["GIOGIAO_QD"].ToString();
                            oAirwayTransportComeDetail.GIOBAY_QD = dr["GIOBAY_QD"].ToString();
                            oAirwayTransportComeDetail.GIOGIAO_TT = dr["GIOGIAO_TT"].ToString();
                            oAirwayTransportComeDetail.GIOBAY_TT = dr["GIOBAY_TT"].ToString();
                            oAirwayTransportComeDetail.SOHIEUCHUYENBAY = dr["SOHIEUCHUYENBAY"].ToString();
                            oAirwayTransportComeDetail.GIOGIAO_CL = dr["GIOGIAO_CL"].ToString();
                            oAirwayTransportComeDetail.GIOBAY_CL = dr["GIOBAY_CL"].ToString();
                            oAirwayTransportComeDetail.GIODAP_QD = dr["GIODAP_QD"].ToString();
                            oAirwayTransportComeDetail.GIONHAN_QD = dr["GIONHAN_QD"].ToString();
                            //oAirwayTransportComeDetail.GIODAP_TT = dr["GIODAP_TT"].ToString();
                            oAirwayTransportComeDetail.GIONHAN_TT = dr["GIONHAN_TT"].ToString();
                            oAirwayTransportComeDetail.GIONHAN_CL = dr["GIONHAN_CL"].ToString();
                            listAirwayTransportComeDetail.Add(oAirwayTransportComeDetail);

                        }
                        _ReturnAirwayTransportCome.Code = "00";
                        _ReturnAirwayTransportCome.Message = "Lấy dữ liệu thành công.";
                        _ReturnAirwayTransportCome.ListAirwayTransportComeReport = listAirwayTransportComeDetail;
                    }
                    else
                    {
                        _ReturnAirwayTransportCome.Code = "01";
                        _ReturnAirwayTransportCome.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _ReturnAirwayTransportCome.Code = "99";
                _ReturnAirwayTransportCome.Message = "Lỗi xử lý dữ liệu";

            }
            return _ReturnAirwayTransportCome;
        }



        #endregion

        //Phần chi tiết của bảng LOAD_DATA6
        #region LOAD_DATA6          
        public ReturnAirwayTransportCome LOAD_DATA6(int date, int way)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnAirwayTransportCome _ReturnAirwayTransportCome = new ReturnAirwayTransportCome();


            List<AirwayTransportComeDetail> listAirwayTransportComeDetail = null;
            AirwayTransportComeDetail oAirwayTransportComeDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    //Helper.OraDSOracleConnection gọi đến database đối soát
                    OracleCommand myCommand = new OracleCommand("AIRWAY_TRANSPORT_COME_2.LOAD_DATA6", Helper.OraDSOracleConnection);
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
                        listAirwayTransportComeDetail = new List<AirwayTransportComeDetail>();
                        while (dr.Read())
                        {
                            oAirwayTransportComeDetail = new AirwayTransportComeDetail();
                            //oAirwayTransportComeDetail.SO = Convert.ToInt32(dr["1"].ToString());
                            oAirwayTransportComeDetail.STT = Convert.ToInt32(dr["STT"].ToString());
                            oAirwayTransportComeDetail.CHANGBAY = dr["CHANGBAY"].ToString();
                            oAirwayTransportComeDetail.DONVIVANCHUYEN = dr["DONVIVANCHUYEN"].ToString();
                            oAirwayTransportComeDetail.DICHVUVANCHUYEN = dr["DICHVUVANCHUYEN"].ToString();
                            oAirwayTransportComeDetail.KHUNGGIO = dr["KHUNGGIO"].ToString();
                            oAirwayTransportComeDetail.TONGTAITHEOHOPDONG = dr["TONGTAITHEOHOPDONG"].ToString();
                            //oAirwayTransportComeDetail.ID = Convert.ToInt32(dr["ID"].ToString());
                            //oAirwayTransportComeDetail.NGAY = Convert.ToInt32(dr["NGAY"].ToString());
                            //oAirwayTransportComeDetail.MABC = Convert.ToInt32(dr["MABC"].ToString());
                            //oAirwayTransportComeDetail.CHIEU = Convert.ToInt32(dr["CHIEU"].ToString());
                            //oAirwayTransportComeDetail.HUONG = Convert.ToInt32(dr["HUONG"].ToString());
                            oAirwayTransportComeDetail.TAICUNG_KH = dr["TAICUNG_KH"].ToString();
                            oAirwayTransportComeDetail.TAIMEM_KH = dr["TAIMEM_KH"].ToString();
                            oAirwayTransportComeDetail.MASP = dr["MASP"].ToString();
                            oAirwayTransportComeDetail.TAICUNG_TH = dr["TAICUNG_TH"].ToString();
                            oAirwayTransportComeDetail.TAIMEM_TH = dr["TAIMEM_TH"].ToString();
                            oAirwayTransportComeDetail.TAICUNG_LK = dr["TAICUNG_LK"].ToString();
                            oAirwayTransportComeDetail.TAIMEM_LK = dr["TAIMEM_LK"].ToString();
                            oAirwayTransportComeDetail.GIOGIAO_QD = dr["GIOGIAO_QD"].ToString();
                            oAirwayTransportComeDetail.GIOBAY_QD = dr["GIOBAY_QD"].ToString();
                            oAirwayTransportComeDetail.GIOGIAO_TT = dr["GIOGIAO_TT"].ToString();
                            oAirwayTransportComeDetail.GIOBAY_TT = dr["GIOBAY_TT"].ToString();
                            oAirwayTransportComeDetail.SOHIEUCHUYENBAY = dr["SOHIEUCHUYENBAY"].ToString();
                            oAirwayTransportComeDetail.GIOGIAO_CL = dr["GIOGIAO_CL"].ToString();
                            oAirwayTransportComeDetail.GIOBAY_CL = dr["GIOBAY_CL"].ToString();
                            oAirwayTransportComeDetail.GIODAP_QD = dr["GIODAP_QD"].ToString();
                            oAirwayTransportComeDetail.GIONHAN_QD = dr["GIONHAN_QD"].ToString();
                            //oAirwayTransportComeDetail.GIODAP_TT = dr["GIODAP_TT"].ToString();
                            oAirwayTransportComeDetail.GIONHAN_TT = dr["GIONHAN_TT"].ToString();
                            oAirwayTransportComeDetail.GIONHAN_CL = dr["GIONHAN_CL"].ToString();
                            listAirwayTransportComeDetail.Add(oAirwayTransportComeDetail);

                        }
                        _ReturnAirwayTransportCome.Code = "00";
                        _ReturnAirwayTransportCome.Message = "Lấy dữ liệu thành công.";
                        _ReturnAirwayTransportCome.ListAirwayTransportComeReport = listAirwayTransportComeDetail;
                    }
                    else
                    {
                        _ReturnAirwayTransportCome.Code = "01";
                        _ReturnAirwayTransportCome.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _ReturnAirwayTransportCome.Code = "99";
                _ReturnAirwayTransportCome.Message = "Lỗi xử lý dữ liệu";

            }
            return _ReturnAirwayTransportCome;
        }



        #endregion

        //Phần chi tiết của bảng LOAD_DATA7
        #region LOAD_DATA7          
        public ReturnAirwayTransportCome LOAD_DATA7(int date, int way)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnAirwayTransportCome _ReturnAirwayTransportCome = new ReturnAirwayTransportCome();


            List<AirwayTransportComeDetail> listAirwayTransportComeDetail = null;
            AirwayTransportComeDetail oAirwayTransportComeDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    //Helper.OraDSOracleConnection gọi đến database đối soát
                    OracleCommand myCommand = new OracleCommand("AIRWAY_TRANSPORT_COME_2.LOAD_DATA7", Helper.OraDSOracleConnection);
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
                        listAirwayTransportComeDetail = new List<AirwayTransportComeDetail>();
                        while (dr.Read())
                        {
                            oAirwayTransportComeDetail = new AirwayTransportComeDetail();
                            //oAirwayTransportComeDetail.SO = Convert.ToInt32(dr["1"].ToString());
                            oAirwayTransportComeDetail.STT = Convert.ToInt32(dr["STT"].ToString());
                            oAirwayTransportComeDetail.CHANGBAY = dr["CHANGBAY"].ToString();
                            oAirwayTransportComeDetail.DONVIVANCHUYEN = dr["DONVIVANCHUYEN"].ToString();
                            oAirwayTransportComeDetail.DICHVUVANCHUYEN = dr["DICHVUVANCHUYEN"].ToString();
                            oAirwayTransportComeDetail.KHUNGGIO = dr["KHUNGGIO"].ToString();
                            oAirwayTransportComeDetail.TONGTAITHEOHOPDONG = dr["TONGTAITHEOHOPDONG"].ToString();
                            //oAirwayTransportComeDetail.ID = Convert.ToInt32(dr["ID"].ToString());
                            //oAirwayTransportComeDetail.NGAY = Convert.ToInt32(dr["NGAY"].ToString());
                            //oAirwayTransportComeDetail.MABC = Convert.ToInt32(dr["MABC"].ToString());
                            //oAirwayTransportComeDetail.CHIEU = Convert.ToInt32(dr["CHIEU"].ToString());
                            //oAirwayTransportComeDetail.HUONG = Convert.ToInt32(dr["HUONG"].ToString());
                            oAirwayTransportComeDetail.TAICUNG_KH = dr["TAICUNG_KH"].ToString();
                            oAirwayTransportComeDetail.TAIMEM_KH = dr["TAIMEM_KH"].ToString();
                            oAirwayTransportComeDetail.MASP = dr["MASP"].ToString();
                            oAirwayTransportComeDetail.TAICUNG_TH = dr["TAICUNG_TH"].ToString();
                            oAirwayTransportComeDetail.TAIMEM_TH = dr["TAIMEM_TH"].ToString();
                            oAirwayTransportComeDetail.TAICUNG_LK = dr["TAICUNG_LK"].ToString();
                            oAirwayTransportComeDetail.TAIMEM_LK = dr["TAIMEM_LK"].ToString();
                            oAirwayTransportComeDetail.GIOGIAO_QD = dr["GIOGIAO_QD"].ToString();
                            oAirwayTransportComeDetail.GIOBAY_QD = dr["GIOBAY_QD"].ToString();
                            oAirwayTransportComeDetail.GIOGIAO_TT = dr["GIOGIAO_TT"].ToString();
                            oAirwayTransportComeDetail.GIOBAY_TT = dr["GIOBAY_TT"].ToString();
                            oAirwayTransportComeDetail.SOHIEUCHUYENBAY = dr["SOHIEUCHUYENBAY"].ToString();
                            oAirwayTransportComeDetail.GIOGIAO_CL = dr["GIOGIAO_CL"].ToString();
                            oAirwayTransportComeDetail.GIOBAY_CL = dr["GIOBAY_CL"].ToString();
                            oAirwayTransportComeDetail.GIODAP_QD = dr["GIODAP_QD"].ToString();
                            oAirwayTransportComeDetail.GIONHAN_QD = dr["GIONHAN_QD"].ToString();
                            //oAirwayTransportComeDetail.GIODAP_TT = dr["GIODAP_TT"].ToString();
                            oAirwayTransportComeDetail.GIONHAN_TT = dr["GIONHAN_TT"].ToString();
                            oAirwayTransportComeDetail.GIONHAN_CL = dr["GIONHAN_CL"].ToString();
                            listAirwayTransportComeDetail.Add(oAirwayTransportComeDetail);

                        }
                        _ReturnAirwayTransportCome.Code = "00";
                        _ReturnAirwayTransportCome.Message = "Lấy dữ liệu thành công.";
                        _ReturnAirwayTransportCome.ListAirwayTransportComeReport = listAirwayTransportComeDetail;
                    }
                    else
                    {
                        _ReturnAirwayTransportCome.Code = "01";
                        _ReturnAirwayTransportCome.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _ReturnAirwayTransportCome.Code = "99";
                _ReturnAirwayTransportCome.Message = "Lỗi xử lý dữ liệu";

            }
            return _ReturnAirwayTransportCome;
        }



        #endregion

        //Phần chi tiết của bảng LOAD_DATA8
        #region LOAD_DATA8          
        public ReturnAirwayTransportCome LOAD_DATA8(int date, int way)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnAirwayTransportCome _ReturnAirwayTransportCome = new ReturnAirwayTransportCome();


            List<AirwayTransportComeDetail> listAirwayTransportComeDetail = null;
            AirwayTransportComeDetail oAirwayTransportComeDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    //Helper.OraDSOracleConnection gọi đến database đối soát
                    OracleCommand myCommand = new OracleCommand("AIRWAY_TRANSPORT_COME_2.LOAD_DATA8", Helper.OraDSOracleConnection);
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
                        listAirwayTransportComeDetail = new List<AirwayTransportComeDetail>();
                        while (dr.Read())
                        {
                            oAirwayTransportComeDetail = new AirwayTransportComeDetail();
                            //oAirwayTransportComeDetail.SO = Convert.ToInt32(dr["1"].ToString());
                            oAirwayTransportComeDetail.STT = Convert.ToInt32(dr["STT"].ToString());
                            oAirwayTransportComeDetail.CHANGBAY = dr["CHANGBAY"].ToString();
                            oAirwayTransportComeDetail.DONVIVANCHUYEN = dr["DONVIVANCHUYEN"].ToString();
                            oAirwayTransportComeDetail.DICHVUVANCHUYEN = dr["DICHVUVANCHUYEN"].ToString();
                            oAirwayTransportComeDetail.KHUNGGIO = dr["KHUNGGIO"].ToString();
                            oAirwayTransportComeDetail.TONGTAITHEOHOPDONG = dr["TONGTAITHEOHOPDONG"].ToString();
                            //oAirwayTransportComeDetail.ID = Convert.ToInt32(dr["ID"].ToString());
                            //oAirwayTransportComeDetail.NGAY = Convert.ToInt32(dr["NGAY"].ToString());
                            //oAirwayTransportComeDetail.MABC = Convert.ToInt32(dr["MABC"].ToString());
                            //oAirwayTransportComeDetail.CHIEU = Convert.ToInt32(dr["CHIEU"].ToString());
                            //oAirwayTransportComeDetail.HUONG = Convert.ToInt32(dr["HUONG"].ToString());
                            oAirwayTransportComeDetail.TAICUNG_KH = dr["TAICUNG_KH"].ToString();
                            oAirwayTransportComeDetail.TAIMEM_KH = dr["TAIMEM_KH"].ToString();
                            oAirwayTransportComeDetail.MASP = dr["MASP"].ToString();
                            oAirwayTransportComeDetail.TAICUNG_TH = dr["TAICUNG_TH"].ToString();
                            oAirwayTransportComeDetail.TAIMEM_TH = dr["TAIMEM_TH"].ToString();
                            oAirwayTransportComeDetail.TAICUNG_LK = dr["TAICUNG_LK"].ToString();
                            oAirwayTransportComeDetail.TAIMEM_LK = dr["TAIMEM_LK"].ToString();
                            oAirwayTransportComeDetail.GIOGIAO_QD = dr["GIOGIAO_QD"].ToString();
                            oAirwayTransportComeDetail.GIOBAY_QD = dr["GIOBAY_QD"].ToString();
                            oAirwayTransportComeDetail.GIOGIAO_TT = dr["GIOGIAO_TT"].ToString();
                            oAirwayTransportComeDetail.GIOBAY_TT = dr["GIOBAY_TT"].ToString();
                            oAirwayTransportComeDetail.SOHIEUCHUYENBAY = dr["SOHIEUCHUYENBAY"].ToString();
                            oAirwayTransportComeDetail.GIOGIAO_CL = dr["GIOGIAO_CL"].ToString();
                            oAirwayTransportComeDetail.GIOBAY_CL = dr["GIOBAY_CL"].ToString();
                            oAirwayTransportComeDetail.GIODAP_QD = dr["GIODAP_QD"].ToString();
                            oAirwayTransportComeDetail.GIONHAN_QD = dr["GIONHAN_QD"].ToString();
                            //oAirwayTransportComeDetail.GIODAP_TT = dr["GIODAP_TT"].ToString();
                            oAirwayTransportComeDetail.GIONHAN_TT = dr["GIONHAN_TT"].ToString();
                            oAirwayTransportComeDetail.GIONHAN_CL = dr["GIONHAN_CL"].ToString();
                            listAirwayTransportComeDetail.Add(oAirwayTransportComeDetail);

                        }
                        _ReturnAirwayTransportCome.Code = "00";
                        _ReturnAirwayTransportCome.Message = "Lấy dữ liệu thành công.";
                        _ReturnAirwayTransportCome.ListAirwayTransportComeReport = listAirwayTransportComeDetail;
                    }
                    else
                    {
                        _ReturnAirwayTransportCome.Code = "01";
                        _ReturnAirwayTransportCome.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _ReturnAirwayTransportCome.Code = "99";
                _ReturnAirwayTransportCome.Message = "Lỗi xử lý dữ liệu";

            }
            return _ReturnAirwayTransportCome;
        }



        #endregion

        //Phần chi tiết của bảng LOAD_DATA9
        #region LOAD_DATA9          
        public ReturnAirwayTransportCome LOAD_DATA9(int date, int way)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnAirwayTransportCome _ReturnAirwayTransportCome = new ReturnAirwayTransportCome();


            List<AirwayTransportComeDetail> listAirwayTransportComeDetail = null;
            AirwayTransportComeDetail oAirwayTransportComeDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    //Helper.OraDSOracleConnection gọi đến database đối soát
                    OracleCommand myCommand = new OracleCommand("AIRWAY_TRANSPORT_COME_2.LOAD_DATA9", Helper.OraDSOracleConnection);
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
                        listAirwayTransportComeDetail = new List<AirwayTransportComeDetail>();
                        while (dr.Read())
                        {
                            oAirwayTransportComeDetail = new AirwayTransportComeDetail();
                            //oAirwayTransportComeDetail.SO = Convert.ToInt32(dr["1"].ToString());
                            oAirwayTransportComeDetail.STT = Convert.ToInt32(dr["STT"].ToString());
                            oAirwayTransportComeDetail.CHANGBAY = dr["CHANGBAY"].ToString();
                            oAirwayTransportComeDetail.DONVIVANCHUYEN = dr["DONVIVANCHUYEN"].ToString();
                            oAirwayTransportComeDetail.DICHVUVANCHUYEN = dr["DICHVUVANCHUYEN"].ToString();
                            oAirwayTransportComeDetail.KHUNGGIO = dr["KHUNGGIO"].ToString();
                            oAirwayTransportComeDetail.TONGTAITHEOHOPDONG = dr["TONGTAITHEOHOPDONG"].ToString();
                            //oAirwayTransportComeDetail.ID = Convert.ToInt32(dr["ID"].ToString());
                            //oAirwayTransportComeDetail.NGAY = Convert.ToInt32(dr["NGAY"].ToString());
                            //oAirwayTransportComeDetail.MABC = Convert.ToInt32(dr["MABC"].ToString());
                            //oAirwayTransportComeDetail.CHIEU = Convert.ToInt32(dr["CHIEU"].ToString());
                            //oAirwayTransportComeDetail.HUONG = Convert.ToInt32(dr["HUONG"].ToString());
                            oAirwayTransportComeDetail.TAICUNG_KH = dr["TAICUNG_KH"].ToString();
                            oAirwayTransportComeDetail.TAIMEM_KH = dr["TAIMEM_KH"].ToString();
                            oAirwayTransportComeDetail.MASP = dr["MASP"].ToString();
                            oAirwayTransportComeDetail.TAICUNG_TH = dr["TAICUNG_TH"].ToString();
                            oAirwayTransportComeDetail.TAIMEM_TH = dr["TAIMEM_TH"].ToString();
                            oAirwayTransportComeDetail.TAICUNG_LK = dr["TAICUNG_LK"].ToString();
                            oAirwayTransportComeDetail.TAIMEM_LK = dr["TAIMEM_LK"].ToString();
                            oAirwayTransportComeDetail.GIOGIAO_QD = dr["GIOGIAO_QD"].ToString();
                            oAirwayTransportComeDetail.GIOBAY_QD = dr["GIOBAY_QD"].ToString();
                            oAirwayTransportComeDetail.GIOGIAO_TT = dr["GIOGIAO_TT"].ToString();
                            oAirwayTransportComeDetail.GIOBAY_TT = dr["GIOBAY_TT"].ToString();
                            oAirwayTransportComeDetail.SOHIEUCHUYENBAY = dr["SOHIEUCHUYENBAY"].ToString();
                            oAirwayTransportComeDetail.GIOGIAO_CL = dr["GIOGIAO_CL"].ToString();
                            oAirwayTransportComeDetail.GIOBAY_CL = dr["GIOBAY_CL"].ToString();
                            oAirwayTransportComeDetail.GIODAP_QD = dr["GIODAP_QD"].ToString();
                            oAirwayTransportComeDetail.GIONHAN_QD = dr["GIONHAN_QD"].ToString();
                            //oAirwayTransportComeDetail.GIODAP_TT = dr["GIODAP_TT"].ToString();
                            oAirwayTransportComeDetail.GIONHAN_TT = dr["GIONHAN_TT"].ToString();
                            oAirwayTransportComeDetail.GIONHAN_CL = dr["GIONHAN_CL"].ToString();
                            listAirwayTransportComeDetail.Add(oAirwayTransportComeDetail);

                        }
                        _ReturnAirwayTransportCome.Code = "00";
                        _ReturnAirwayTransportCome.Message = "Lấy dữ liệu thành công.";
                        _ReturnAirwayTransportCome.ListAirwayTransportComeReport = listAirwayTransportComeDetail;
                    }
                    else
                    {
                        _ReturnAirwayTransportCome.Code = "01";
                        _ReturnAirwayTransportCome.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _ReturnAirwayTransportCome.Code = "99";
                _ReturnAirwayTransportCome.Message = "Lỗi xử lý dữ liệu";

            }
            return _ReturnAirwayTransportCome;
        }



        #endregion

        //Phần chi tiết của bảng LOAD_DATA10
        #region LOAD_DATA10          
        public ReturnAirwayTransportCome LOAD_DATA10(int date, int way)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnAirwayTransportCome _ReturnAirwayTransportCome = new ReturnAirwayTransportCome();


            List<AirwayTransportComeDetail> listAirwayTransportComeDetail = null;
            AirwayTransportComeDetail oAirwayTransportComeDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    //Helper.OraDSOracleConnection gọi đến database đối soát
                    OracleCommand myCommand = new OracleCommand("AIRWAY_TRANSPORT_COME_2.LOAD_DATA10", Helper.OraDSOracleConnection);
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
                        listAirwayTransportComeDetail = new List<AirwayTransportComeDetail>();
                        while (dr.Read())
                        {
                            oAirwayTransportComeDetail = new AirwayTransportComeDetail();
                            //oAirwayTransportComeDetail.SO = Convert.ToInt32(dr["1"].ToString());
                            oAirwayTransportComeDetail.STT = Convert.ToInt32(dr["STT"].ToString());
                            oAirwayTransportComeDetail.CHANGBAY = dr["CHANGBAY"].ToString();
                            oAirwayTransportComeDetail.DONVIVANCHUYEN = dr["DONVIVANCHUYEN"].ToString();
                            oAirwayTransportComeDetail.DICHVUVANCHUYEN = dr["DICHVUVANCHUYEN"].ToString();
                            oAirwayTransportComeDetail.KHUNGGIO = dr["KHUNGGIO"].ToString();
                            oAirwayTransportComeDetail.TONGTAITHEOHOPDONG = dr["TONGTAITHEOHOPDONG"].ToString();
                            //oAirwayTransportComeDetail.ID = Convert.ToInt32(dr["ID"].ToString());
                            //oAirwayTransportComeDetail.NGAY = Convert.ToInt32(dr["NGAY"].ToString());
                            //oAirwayTransportComeDetail.MABC = Convert.ToInt32(dr["MABC"].ToString());
                            //oAirwayTransportComeDetail.CHIEU = Convert.ToInt32(dr["CHIEU"].ToString());
                            //oAirwayTransportComeDetail.HUONG = Convert.ToInt32(dr["HUONG"].ToString());
                            oAirwayTransportComeDetail.TAICUNG_KH = dr["TAICUNG_KH"].ToString();
                            oAirwayTransportComeDetail.TAIMEM_KH = dr["TAIMEM_KH"].ToString();
                            oAirwayTransportComeDetail.MASP = dr["MASP"].ToString();
                            oAirwayTransportComeDetail.TAICUNG_TH = dr["TAICUNG_TH"].ToString();
                            oAirwayTransportComeDetail.TAIMEM_TH = dr["TAIMEM_TH"].ToString();
                            oAirwayTransportComeDetail.TAICUNG_LK = dr["TAICUNG_LK"].ToString();
                            oAirwayTransportComeDetail.TAIMEM_LK = dr["TAIMEM_LK"].ToString();
                            oAirwayTransportComeDetail.GIOGIAO_QD = dr["GIOGIAO_QD"].ToString();
                            oAirwayTransportComeDetail.GIOBAY_QD = dr["GIOBAY_QD"].ToString();
                            oAirwayTransportComeDetail.GIOGIAO_TT = dr["GIOGIAO_TT"].ToString();
                            oAirwayTransportComeDetail.GIOBAY_TT = dr["GIOBAY_TT"].ToString();
                            oAirwayTransportComeDetail.SOHIEUCHUYENBAY = dr["SOHIEUCHUYENBAY"].ToString();
                            oAirwayTransportComeDetail.GIOGIAO_CL = dr["GIOGIAO_CL"].ToString();
                            oAirwayTransportComeDetail.GIOBAY_CL = dr["GIOBAY_CL"].ToString();
                            oAirwayTransportComeDetail.GIODAP_QD = dr["GIODAP_QD"].ToString();
                            oAirwayTransportComeDetail.GIONHAN_QD = dr["GIONHAN_QD"].ToString();
                            //oAirwayTransportComeDetail.GIODAP_TT = dr["GIODAP_TT"].ToString();
                            oAirwayTransportComeDetail.GIONHAN_TT = dr["GIONHAN_TT"].ToString();
                            oAirwayTransportComeDetail.GIONHAN_CL = dr["GIONHAN_CL"].ToString();
                            listAirwayTransportComeDetail.Add(oAirwayTransportComeDetail);

                        }
                        _ReturnAirwayTransportCome.Code = "00";
                        _ReturnAirwayTransportCome.Message = "Lấy dữ liệu thành công.";
                        _ReturnAirwayTransportCome.ListAirwayTransportComeReport = listAirwayTransportComeDetail;
                    }
                    else
                    {
                        _ReturnAirwayTransportCome.Code = "01";
                        _ReturnAirwayTransportCome.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _ReturnAirwayTransportCome.Code = "99";
                _ReturnAirwayTransportCome.Message = "Lỗi xử lý dữ liệu";

            }
            return _ReturnAirwayTransportCome;
        }



        #endregion

        //Phần chi tiết của bảng LOAD_DATA11
        #region LOAD_DATA11          
        public ReturnAirwayTransportCome LOAD_DATA11(int date, int way)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnAirwayTransportCome _ReturnAirwayTransportCome = new ReturnAirwayTransportCome();


            List<AirwayTransportComeDetail> listAirwayTransportComeDetail = null;
            AirwayTransportComeDetail oAirwayTransportComeDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    //Helper.OraDSOracleConnection gọi đến database đối soát
                    OracleCommand myCommand = new OracleCommand("AIRWAY_TRANSPORT_COME_2.LOAD_DATA11", Helper.OraDSOracleConnection);
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
                        listAirwayTransportComeDetail = new List<AirwayTransportComeDetail>();
                        while (dr.Read())
                        {
                            oAirwayTransportComeDetail = new AirwayTransportComeDetail();
                            //oAirwayTransportComeDetail.SO = Convert.ToInt32(dr["1"].ToString());
                            oAirwayTransportComeDetail.STT = Convert.ToInt32(dr["STT"].ToString());
                            oAirwayTransportComeDetail.CHANGBAY = dr["CHANGBAY"].ToString();
                            oAirwayTransportComeDetail.DONVIVANCHUYEN = dr["DONVIVANCHUYEN"].ToString();
                            oAirwayTransportComeDetail.DICHVUVANCHUYEN = dr["DICHVUVANCHUYEN"].ToString();
                            oAirwayTransportComeDetail.KHUNGGIO = dr["KHUNGGIO"].ToString();
                            oAirwayTransportComeDetail.TONGTAITHEOHOPDONG = dr["TONGTAITHEOHOPDONG"].ToString();
                            //oAirwayTransportComeDetail.ID = Convert.ToInt32(dr["ID"].ToString());
                            //oAirwayTransportComeDetail.NGAY = Convert.ToInt32(dr["NGAY"].ToString());
                            //oAirwayTransportComeDetail.MABC = Convert.ToInt32(dr["MABC"].ToString());
                            //oAirwayTransportComeDetail.CHIEU = Convert.ToInt32(dr["CHIEU"].ToString());
                            //oAirwayTransportComeDetail.HUONG = Convert.ToInt32(dr["HUONG"].ToString());
                            oAirwayTransportComeDetail.TAICUNG_KH = dr["TAICUNG_KH"].ToString();
                            oAirwayTransportComeDetail.TAIMEM_KH = dr["TAIMEM_KH"].ToString();
                            oAirwayTransportComeDetail.MASP = dr["MASP"].ToString();
                            oAirwayTransportComeDetail.TAICUNG_TH = dr["TAICUNG_TH"].ToString();
                            oAirwayTransportComeDetail.TAIMEM_TH = dr["TAIMEM_TH"].ToString();
                            oAirwayTransportComeDetail.TAICUNG_LK = dr["TAICUNG_LK"].ToString();
                            oAirwayTransportComeDetail.TAIMEM_LK = dr["TAIMEM_LK"].ToString();
                            oAirwayTransportComeDetail.GIOGIAO_QD = dr["GIOGIAO_QD"].ToString();
                            oAirwayTransportComeDetail.GIOBAY_QD = dr["GIOBAY_QD"].ToString();
                            oAirwayTransportComeDetail.GIOGIAO_TT = dr["GIOGIAO_TT"].ToString();
                            oAirwayTransportComeDetail.GIOBAY_TT = dr["GIOBAY_TT"].ToString();
                            oAirwayTransportComeDetail.SOHIEUCHUYENBAY = dr["SOHIEUCHUYENBAY"].ToString();
                            oAirwayTransportComeDetail.GIOGIAO_CL = dr["GIOGIAO_CL"].ToString();
                            oAirwayTransportComeDetail.GIOBAY_CL = dr["GIOBAY_CL"].ToString();
                            oAirwayTransportComeDetail.GIODAP_QD = dr["GIODAP_QD"].ToString();
                            oAirwayTransportComeDetail.GIONHAN_QD = dr["GIONHAN_QD"].ToString();
                            //oAirwayTransportComeDetail.GIODAP_TT = dr["GIODAP_TT"].ToString();
                            oAirwayTransportComeDetail.GIONHAN_TT = dr["GIONHAN_TT"].ToString();
                            oAirwayTransportComeDetail.GIONHAN_CL = dr["GIONHAN_CL"].ToString();
                            listAirwayTransportComeDetail.Add(oAirwayTransportComeDetail);

                        }
                        _ReturnAirwayTransportCome.Code = "00";
                        _ReturnAirwayTransportCome.Message = "Lấy dữ liệu thành công.";
                        _ReturnAirwayTransportCome.ListAirwayTransportComeReport = listAirwayTransportComeDetail;
                    }
                    else
                    {
                        _ReturnAirwayTransportCome.Code = "01";
                        _ReturnAirwayTransportCome.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _ReturnAirwayTransportCome.Code = "99";
                _ReturnAirwayTransportCome.Message = "Lỗi xử lý dữ liệu";

            }
            return _ReturnAirwayTransportCome;
        }



        #endregion

        //Phần chi tiết của bảng LOAD_DATA12
        #region LOAD_DATA12          
        public ReturnAirwayTransportCome LOAD_DATA12(int date, int way)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnAirwayTransportCome _ReturnAirwayTransportCome = new ReturnAirwayTransportCome();


            List<AirwayTransportComeDetail> listAirwayTransportComeDetail = null;
            AirwayTransportComeDetail oAirwayTransportComeDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    //Helper.OraDSOracleConnection gọi đến database đối soát
                    OracleCommand myCommand = new OracleCommand("AIRWAY_TRANSPORT_COME_2.LOAD_DATA12", Helper.OraDSOracleConnection);
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
                        listAirwayTransportComeDetail = new List<AirwayTransportComeDetail>();
                        while (dr.Read())
                        {
                            oAirwayTransportComeDetail = new AirwayTransportComeDetail();
                            //oAirwayTransportComeDetail.SO = Convert.ToInt32(dr["1"].ToString());
                            oAirwayTransportComeDetail.STT = Convert.ToInt32(dr["STT"].ToString());
                            oAirwayTransportComeDetail.CHANGBAY = dr["CHANGBAY"].ToString();
                            oAirwayTransportComeDetail.DONVIVANCHUYEN = dr["DONVIVANCHUYEN"].ToString();
                            oAirwayTransportComeDetail.DICHVUVANCHUYEN = dr["DICHVUVANCHUYEN"].ToString();
                            oAirwayTransportComeDetail.KHUNGGIO = dr["KHUNGGIO"].ToString();
                            oAirwayTransportComeDetail.TONGTAITHEOHOPDONG = dr["TONGTAITHEOHOPDONG"].ToString();
                            //oAirwayTransportComeDetail.ID = Convert.ToInt32(dr["ID"].ToString());
                            //oAirwayTransportComeDetail.NGAY = Convert.ToInt32(dr["NGAY"].ToString());
                            //oAirwayTransportComeDetail.MABC = Convert.ToInt32(dr["MABC"].ToString());
                            //oAirwayTransportComeDetail.CHIEU = Convert.ToInt32(dr["CHIEU"].ToString());
                            //oAirwayTransportComeDetail.HUONG = Convert.ToInt32(dr["HUONG"].ToString());
                            oAirwayTransportComeDetail.TAICUNG_KH = dr["TAICUNG_KH"].ToString();
                            oAirwayTransportComeDetail.TAIMEM_KH = dr["TAIMEM_KH"].ToString();
                            oAirwayTransportComeDetail.MASP = dr["MASP"].ToString();
                            oAirwayTransportComeDetail.TAICUNG_TH = dr["TAICUNG_TH"].ToString();
                            oAirwayTransportComeDetail.TAIMEM_TH = dr["TAIMEM_TH"].ToString();
                            oAirwayTransportComeDetail.TAICUNG_LK = dr["TAICUNG_LK"].ToString();
                            oAirwayTransportComeDetail.TAIMEM_LK = dr["TAIMEM_LK"].ToString();
                            oAirwayTransportComeDetail.GIOGIAO_QD = dr["GIOGIAO_QD"].ToString();
                            oAirwayTransportComeDetail.GIOBAY_QD = dr["GIOBAY_QD"].ToString();
                            oAirwayTransportComeDetail.GIOGIAO_TT = dr["GIOGIAO_TT"].ToString();
                            oAirwayTransportComeDetail.GIOBAY_TT = dr["GIOBAY_TT"].ToString();
                            oAirwayTransportComeDetail.SOHIEUCHUYENBAY = dr["SOHIEUCHUYENBAY"].ToString();
                            oAirwayTransportComeDetail.GIOGIAO_CL = dr["GIOGIAO_CL"].ToString();
                            oAirwayTransportComeDetail.GIOBAY_CL = dr["GIOBAY_CL"].ToString();
                            oAirwayTransportComeDetail.GIODAP_QD = dr["GIODAP_QD"].ToString();
                            oAirwayTransportComeDetail.GIONHAN_QD = dr["GIONHAN_QD"].ToString();
                            //oAirwayTransportComeDetail.GIODAP_TT = dr["GIODAP_TT"].ToString();
                            oAirwayTransportComeDetail.GIONHAN_TT = dr["GIONHAN_TT"].ToString();
                            oAirwayTransportComeDetail.GIONHAN_CL = dr["GIONHAN_CL"].ToString();
                            listAirwayTransportComeDetail.Add(oAirwayTransportComeDetail);

                        }
                        _ReturnAirwayTransportCome.Code = "00";
                        _ReturnAirwayTransportCome.Message = "Lấy dữ liệu thành công.";
                        _ReturnAirwayTransportCome.ListAirwayTransportComeReport = listAirwayTransportComeDetail;
                    }
                    else
                    {
                        _ReturnAirwayTransportCome.Code = "01";
                        _ReturnAirwayTransportCome.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _ReturnAirwayTransportCome.Code = "99";
                _ReturnAirwayTransportCome.Message = "Lỗi xử lý dữ liệu";

            }
            return _ReturnAirwayTransportCome;
        }



        #endregion

        //Phần chi tiết của bảng LOAD_DATA13
        #region LOAD_DATA13          
        public ReturnAirwayTransportCome LOAD_DATA13(int date, int way)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnAirwayTransportCome _ReturnAirwayTransportCome = new ReturnAirwayTransportCome();


            List<AirwayTransportComeDetail> listAirwayTransportComeDetail = null;
            AirwayTransportComeDetail oAirwayTransportComeDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    //Helper.OraDSOracleConnection gọi đến database đối soát
                    OracleCommand myCommand = new OracleCommand("AIRWAY_TRANSPORT_COME_2.LOAD_DATA13", Helper.OraDSOracleConnection);
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
                        listAirwayTransportComeDetail = new List<AirwayTransportComeDetail>();
                        while (dr.Read())
                        {
                            oAirwayTransportComeDetail = new AirwayTransportComeDetail();
                            //oAirwayTransportComeDetail.SO = Convert.ToInt32(dr["1"].ToString());
                            oAirwayTransportComeDetail.STT = Convert.ToInt32(dr["STT"].ToString());
                            oAirwayTransportComeDetail.CHANGBAY = dr["CHANGBAY"].ToString();
                            oAirwayTransportComeDetail.DONVIVANCHUYEN = dr["DONVIVANCHUYEN"].ToString();
                            oAirwayTransportComeDetail.DICHVUVANCHUYEN = dr["DICHVUVANCHUYEN"].ToString();
                            oAirwayTransportComeDetail.KHUNGGIO = dr["KHUNGGIO"].ToString();
                            oAirwayTransportComeDetail.TONGTAITHEOHOPDONG = dr["TONGTAITHEOHOPDONG"].ToString();
                            //oAirwayTransportComeDetail.ID = Convert.ToInt32(dr["ID"].ToString());
                            //oAirwayTransportComeDetail.NGAY = Convert.ToInt32(dr["NGAY"].ToString());
                            //oAirwayTransportComeDetail.MABC = Convert.ToInt32(dr["MABC"].ToString());
                            //oAirwayTransportComeDetail.CHIEU = Convert.ToInt32(dr["CHIEU"].ToString());
                            //oAirwayTransportComeDetail.HUONG = Convert.ToInt32(dr["HUONG"].ToString());
                            oAirwayTransportComeDetail.TAICUNG_KH = dr["TAICUNG_KH"].ToString();
                            oAirwayTransportComeDetail.TAIMEM_KH = dr["TAIMEM_KH"].ToString();
                            oAirwayTransportComeDetail.MASP = dr["MASP"].ToString();
                            oAirwayTransportComeDetail.TAICUNG_TH = dr["TAICUNG_TH"].ToString();
                            oAirwayTransportComeDetail.TAIMEM_TH = dr["TAIMEM_TH"].ToString();
                            oAirwayTransportComeDetail.TAICUNG_LK = dr["TAICUNG_LK"].ToString();
                            oAirwayTransportComeDetail.TAIMEM_LK = dr["TAIMEM_LK"].ToString();
                            oAirwayTransportComeDetail.GIOGIAO_QD = dr["GIOGIAO_QD"].ToString();
                            oAirwayTransportComeDetail.GIOBAY_QD = dr["GIOBAY_QD"].ToString();
                            oAirwayTransportComeDetail.GIOGIAO_TT = dr["GIOGIAO_TT"].ToString();
                            oAirwayTransportComeDetail.GIOBAY_TT = dr["GIOBAY_TT"].ToString();
                            oAirwayTransportComeDetail.SOHIEUCHUYENBAY = dr["SOHIEUCHUYENBAY"].ToString();
                            oAirwayTransportComeDetail.GIOGIAO_CL = dr["GIOGIAO_CL"].ToString();
                            oAirwayTransportComeDetail.GIOBAY_CL = dr["GIOBAY_CL"].ToString();
                            oAirwayTransportComeDetail.GIODAP_QD = dr["GIODAP_QD"].ToString();
                            oAirwayTransportComeDetail.GIONHAN_QD = dr["GIONHAN_QD"].ToString();
                            //oAirwayTransportComeDetail.GIODAP_TT = dr["GIODAP_TT"].ToString();
                            oAirwayTransportComeDetail.GIONHAN_TT = dr["GIONHAN_TT"].ToString();
                            oAirwayTransportComeDetail.GIONHAN_CL = dr["GIONHAN_CL"].ToString();
                            listAirwayTransportComeDetail.Add(oAirwayTransportComeDetail);

                        }
                        _ReturnAirwayTransportCome.Code = "00";
                        _ReturnAirwayTransportCome.Message = "Lấy dữ liệu thành công.";
                        _ReturnAirwayTransportCome.ListAirwayTransportComeReport = listAirwayTransportComeDetail;
                    }
                    else
                    {
                        _ReturnAirwayTransportCome.Code = "01";
                        _ReturnAirwayTransportCome.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _ReturnAirwayTransportCome.Code = "99";
                _ReturnAirwayTransportCome.Message = "Lỗi xử lý dữ liệu";

            }
            return _ReturnAirwayTransportCome;
        }



        #endregion

        //Phần chi tiết của bảng LOAD_DATA14
        #region LOAD_DATA14          
        public ReturnAirwayTransportCome LOAD_DATA14(int date, int way)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnAirwayTransportCome _ReturnAirwayTransportCome = new ReturnAirwayTransportCome();


            List<AirwayTransportComeDetail> listAirwayTransportComeDetail = null;
            AirwayTransportComeDetail oAirwayTransportComeDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    //Helper.OraDSOracleConnection gọi đến database đối soát
                    OracleCommand myCommand = new OracleCommand("AIRWAY_TRANSPORT_COME_2.LOAD_DATA14", Helper.OraDSOracleConnection);
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
                        listAirwayTransportComeDetail = new List<AirwayTransportComeDetail>();
                        while (dr.Read())
                        {
                            oAirwayTransportComeDetail = new AirwayTransportComeDetail();
                            //oAirwayTransportComeDetail.SO = Convert.ToInt32(dr["1"].ToString());
                            oAirwayTransportComeDetail.STT = Convert.ToInt32(dr["STT"].ToString());
                            oAirwayTransportComeDetail.CHANGBAY = dr["CHANGBAY"].ToString();
                            oAirwayTransportComeDetail.DONVIVANCHUYEN = dr["DONVIVANCHUYEN"].ToString();
                            oAirwayTransportComeDetail.DICHVUVANCHUYEN = dr["DICHVUVANCHUYEN"].ToString();
                            oAirwayTransportComeDetail.KHUNGGIO = dr["KHUNGGIO"].ToString();
                            oAirwayTransportComeDetail.TONGTAITHEOHOPDONG = dr["TONGTAITHEOHOPDONG"].ToString();
                            //oAirwayTransportComeDetail.ID = Convert.ToInt32(dr["ID"].ToString());
                            //oAirwayTransportComeDetail.NGAY = Convert.ToInt32(dr["NGAY"].ToString());
                            //oAirwayTransportComeDetail.MABC = Convert.ToInt32(dr["MABC"].ToString());
                            //oAirwayTransportComeDetail.CHIEU = Convert.ToInt32(dr["CHIEU"].ToString());
                            //oAirwayTransportComeDetail.HUONG = Convert.ToInt32(dr["HUONG"].ToString());
                            oAirwayTransportComeDetail.TAICUNG_KH = dr["TAICUNG_KH"].ToString();
                            oAirwayTransportComeDetail.TAIMEM_KH = dr["TAIMEM_KH"].ToString();
                            oAirwayTransportComeDetail.MASP = dr["MASP"].ToString();
                            oAirwayTransportComeDetail.TAICUNG_TH = dr["TAICUNG_TH"].ToString();
                            oAirwayTransportComeDetail.TAIMEM_TH = dr["TAIMEM_TH"].ToString();
                            oAirwayTransportComeDetail.TAICUNG_LK = dr["TAICUNG_LK"].ToString();
                            oAirwayTransportComeDetail.TAIMEM_LK = dr["TAIMEM_LK"].ToString();
                            oAirwayTransportComeDetail.GIOGIAO_QD = dr["GIOGIAO_QD"].ToString();
                            oAirwayTransportComeDetail.GIOBAY_QD = dr["GIOBAY_QD"].ToString();
                            oAirwayTransportComeDetail.GIOGIAO_TT = dr["GIOGIAO_TT"].ToString();
                            oAirwayTransportComeDetail.GIOBAY_TT = dr["GIOBAY_TT"].ToString();
                            oAirwayTransportComeDetail.SOHIEUCHUYENBAY = dr["SOHIEUCHUYENBAY"].ToString();
                            oAirwayTransportComeDetail.GIOGIAO_CL = dr["GIOGIAO_CL"].ToString();
                            oAirwayTransportComeDetail.GIOBAY_CL = dr["GIOBAY_CL"].ToString();
                            oAirwayTransportComeDetail.GIODAP_QD = dr["GIODAP_QD"].ToString();
                            oAirwayTransportComeDetail.GIONHAN_QD = dr["GIONHAN_QD"].ToString();
                            //oAirwayTransportComeDetail.GIODAP_TT = dr["GIODAP_TT"].ToString();
                            oAirwayTransportComeDetail.GIONHAN_TT = dr["GIONHAN_TT"].ToString();
                            oAirwayTransportComeDetail.GIONHAN_CL = dr["GIONHAN_CL"].ToString();
                            listAirwayTransportComeDetail.Add(oAirwayTransportComeDetail);

                        }
                        _ReturnAirwayTransportCome.Code = "00";
                        _ReturnAirwayTransportCome.Message = "Lấy dữ liệu thành công.";
                        _ReturnAirwayTransportCome.ListAirwayTransportComeReport = listAirwayTransportComeDetail;
                    }
                    else
                    {
                        _ReturnAirwayTransportCome.Code = "01";
                        _ReturnAirwayTransportCome.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _ReturnAirwayTransportCome.Code = "99";
                _ReturnAirwayTransportCome.Message = "Lỗi xử lý dữ liệu";

            }
            return _ReturnAirwayTransportCome;
        }



        #endregion

        //Phần chi tiết của bảng LOAD_DATA15
        #region LOAD_DATA15          
        public ReturnAirwayTransportCome LOAD_DATA15(int date, int way)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnAirwayTransportCome _ReturnAirwayTransportCome = new ReturnAirwayTransportCome();


            List<AirwayTransportComeDetail> listAirwayTransportComeDetail = null;
            AirwayTransportComeDetail oAirwayTransportComeDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    //Helper.OraDSOracleConnection gọi đến database đối soát
                    OracleCommand myCommand = new OracleCommand("AIRWAY_TRANSPORT_COME_2.LOAD_DATA15", Helper.OraDSOracleConnection);
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
                        listAirwayTransportComeDetail = new List<AirwayTransportComeDetail>();
                        while (dr.Read())
                        {
                            oAirwayTransportComeDetail = new AirwayTransportComeDetail();
                            //oAirwayTransportComeDetail.SO = Convert.ToInt32(dr["1"].ToString());
                            oAirwayTransportComeDetail.STT = Convert.ToInt32(dr["STT"].ToString());
                            oAirwayTransportComeDetail.CHANGBAY = dr["CHANGBAY"].ToString();
                            oAirwayTransportComeDetail.DONVIVANCHUYEN = dr["DONVIVANCHUYEN"].ToString();
                            oAirwayTransportComeDetail.DICHVUVANCHUYEN = dr["DICHVUVANCHUYEN"].ToString();
                            oAirwayTransportComeDetail.KHUNGGIO = dr["KHUNGGIO"].ToString();
                            oAirwayTransportComeDetail.TONGTAITHEOHOPDONG = dr["TONGTAITHEOHOPDONG"].ToString();
                            //oAirwayTransportComeDetail.ID = Convert.ToInt32(dr["ID"].ToString());
                            //oAirwayTransportComeDetail.NGAY = Convert.ToInt32(dr["NGAY"].ToString());
                            //oAirwayTransportComeDetail.MABC = Convert.ToInt32(dr["MABC"].ToString());
                            //oAirwayTransportComeDetail.CHIEU = Convert.ToInt32(dr["CHIEU"].ToString());
                            //oAirwayTransportComeDetail.HUONG = Convert.ToInt32(dr["HUONG"].ToString());
                            oAirwayTransportComeDetail.TAICUNG_KH = dr["TAICUNG_KH"].ToString();
                            oAirwayTransportComeDetail.TAIMEM_KH = dr["TAIMEM_KH"].ToString();
                            oAirwayTransportComeDetail.MASP = dr["MASP"].ToString();
                            oAirwayTransportComeDetail.TAICUNG_TH = dr["TAICUNG_TH"].ToString();
                            oAirwayTransportComeDetail.TAIMEM_TH = dr["TAIMEM_TH"].ToString();
                            oAirwayTransportComeDetail.TAICUNG_LK = dr["TAICUNG_LK"].ToString();
                            oAirwayTransportComeDetail.TAIMEM_LK = dr["TAIMEM_LK"].ToString();
                            oAirwayTransportComeDetail.GIOGIAO_QD = dr["GIOGIAO_QD"].ToString();
                            oAirwayTransportComeDetail.GIOBAY_QD = dr["GIOBAY_QD"].ToString();
                            oAirwayTransportComeDetail.GIOGIAO_TT = dr["GIOGIAO_TT"].ToString();
                            oAirwayTransportComeDetail.GIOBAY_TT = dr["GIOBAY_TT"].ToString();
                            oAirwayTransportComeDetail.SOHIEUCHUYENBAY = dr["SOHIEUCHUYENBAY"].ToString();
                            oAirwayTransportComeDetail.GIOGIAO_CL = dr["GIOGIAO_CL"].ToString();
                            oAirwayTransportComeDetail.GIOBAY_CL = dr["GIOBAY_CL"].ToString();
                            oAirwayTransportComeDetail.GIODAP_QD = dr["GIODAP_QD"].ToString();
                            oAirwayTransportComeDetail.GIONHAN_QD = dr["GIONHAN_QD"].ToString();
                            //oAirwayTransportComeDetail.GIODAP_TT = dr["GIODAP_TT"].ToString();
                            oAirwayTransportComeDetail.GIONHAN_TT = dr["GIONHAN_TT"].ToString();
                            oAirwayTransportComeDetail.GIONHAN_CL = dr["GIONHAN_CL"].ToString();
                            listAirwayTransportComeDetail.Add(oAirwayTransportComeDetail);

                        }
                        _ReturnAirwayTransportCome.Code = "00";
                        _ReturnAirwayTransportCome.Message = "Lấy dữ liệu thành công.";
                        _ReturnAirwayTransportCome.ListAirwayTransportComeReport = listAirwayTransportComeDetail;
                    }
                    else
                    {
                        _ReturnAirwayTransportCome.Code = "01";
                        _ReturnAirwayTransportCome.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _ReturnAirwayTransportCome.Code = "99";
                _ReturnAirwayTransportCome.Message = "Lỗi xử lý dữ liệu";

            }
            return _ReturnAirwayTransportCome;
        }



        #endregion

        //Phần chi tiết của bảng LOAD_DATA16
        #region LOAD_DATA16          
        public ReturnAirwayTransportCome LOAD_DATA16(int date, int way)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnAirwayTransportCome _ReturnAirwayTransportCome = new ReturnAirwayTransportCome();


            List<AirwayTransportComeDetail> listAirwayTransportComeDetail = null;
            AirwayTransportComeDetail oAirwayTransportComeDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    //Helper.OraDSOracleConnection gọi đến database đối soát
                    OracleCommand myCommand = new OracleCommand("AIRWAY_TRANSPORT_COME_2.LOAD_DATA16", Helper.OraDSOracleConnection);
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
                        listAirwayTransportComeDetail = new List<AirwayTransportComeDetail>();
                        while (dr.Read())
                        {
                            oAirwayTransportComeDetail = new AirwayTransportComeDetail();
                            //oAirwayTransportComeDetail.SO = Convert.ToInt32(dr["1"].ToString());
                            oAirwayTransportComeDetail.STT = Convert.ToInt32(dr["STT"].ToString());
                            oAirwayTransportComeDetail.CHANGBAY = dr["CHANGBAY"].ToString();
                            oAirwayTransportComeDetail.DONVIVANCHUYEN = dr["DONVIVANCHUYEN"].ToString();
                            oAirwayTransportComeDetail.DICHVUVANCHUYEN = dr["DICHVUVANCHUYEN"].ToString();
                            oAirwayTransportComeDetail.KHUNGGIO = dr["KHUNGGIO"].ToString();
                            oAirwayTransportComeDetail.TONGTAITHEOHOPDONG = dr["TONGTAITHEOHOPDONG"].ToString();
                            //oAirwayTransportComeDetail.ID = Convert.ToInt32(dr["ID"].ToString());
                            //oAirwayTransportComeDetail.NGAY = Convert.ToInt32(dr["NGAY"].ToString());
                            //oAirwayTransportComeDetail.MABC = Convert.ToInt32(dr["MABC"].ToString());
                            //oAirwayTransportComeDetail.CHIEU = Convert.ToInt32(dr["CHIEU"].ToString());
                            //oAirwayTransportComeDetail.HUONG = Convert.ToInt32(dr["HUONG"].ToString());
                            oAirwayTransportComeDetail.TAICUNG_KH = dr["TAICUNG_KH"].ToString();
                            oAirwayTransportComeDetail.TAIMEM_KH = dr["TAIMEM_KH"].ToString();
                            oAirwayTransportComeDetail.MASP = dr["MASP"].ToString();
                            oAirwayTransportComeDetail.TAICUNG_TH = dr["TAICUNG_TH"].ToString();
                            oAirwayTransportComeDetail.TAIMEM_TH = dr["TAIMEM_TH"].ToString();
                            oAirwayTransportComeDetail.TAICUNG_LK = dr["TAICUNG_LK"].ToString();
                            oAirwayTransportComeDetail.TAIMEM_LK = dr["TAIMEM_LK"].ToString();
                            oAirwayTransportComeDetail.GIOGIAO_QD = dr["GIOGIAO_QD"].ToString();
                            oAirwayTransportComeDetail.GIOBAY_QD = dr["GIOBAY_QD"].ToString();
                            oAirwayTransportComeDetail.GIOGIAO_TT = dr["GIOGIAO_TT"].ToString();
                            oAirwayTransportComeDetail.GIOBAY_TT = dr["GIOBAY_TT"].ToString();
                            oAirwayTransportComeDetail.SOHIEUCHUYENBAY = dr["SOHIEUCHUYENBAY"].ToString();
                            oAirwayTransportComeDetail.GIOGIAO_CL = dr["GIOGIAO_CL"].ToString();
                            oAirwayTransportComeDetail.GIOBAY_CL = dr["GIOBAY_CL"].ToString();
                            oAirwayTransportComeDetail.GIODAP_QD = dr["GIODAP_QD"].ToString();
                            oAirwayTransportComeDetail.GIONHAN_QD = dr["GIONHAN_QD"].ToString();
                            //oAirwayTransportComeDetail.GIODAP_TT = dr["GIODAP_TT"].ToString();
                            oAirwayTransportComeDetail.GIONHAN_TT = dr["GIONHAN_TT"].ToString();
                            oAirwayTransportComeDetail.GIONHAN_CL = dr["GIONHAN_CL"].ToString();
                            listAirwayTransportComeDetail.Add(oAirwayTransportComeDetail);

                        }
                        _ReturnAirwayTransportCome.Code = "00";
                        _ReturnAirwayTransportCome.Message = "Lấy dữ liệu thành công.";
                        _ReturnAirwayTransportCome.ListAirwayTransportComeReport = listAirwayTransportComeDetail;
                    }
                    else
                    {
                        _ReturnAirwayTransportCome.Code = "01";
                        _ReturnAirwayTransportCome.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _ReturnAirwayTransportCome.Code = "99";
                _ReturnAirwayTransportCome.Message = "Lỗi xử lý dữ liệu";

            }
            return _ReturnAirwayTransportCome;
        }



        #endregion

        //Phần chi tiết của bảng LOAD_DATA17
        #region LOAD_DATA17          
        public ReturnAirwayTransportCome LOAD_DATA17(int date, int way)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnAirwayTransportCome _ReturnAirwayTransportCome = new ReturnAirwayTransportCome();


            List<AirwayTransportComeDetail> listAirwayTransportComeDetail = null;
            AirwayTransportComeDetail oAirwayTransportComeDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    //Helper.OraDSOracleConnection gọi đến database đối soát
                    OracleCommand myCommand = new OracleCommand("AIRWAY_TRANSPORT_COME_2.LOAD_DATA17", Helper.OraDSOracleConnection);
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
                        listAirwayTransportComeDetail = new List<AirwayTransportComeDetail>();
                        while (dr.Read())
                        {
                            oAirwayTransportComeDetail = new AirwayTransportComeDetail();
                            //oAirwayTransportComeDetail.SO = Convert.ToInt32(dr["1"].ToString());
                            oAirwayTransportComeDetail.STT = Convert.ToInt32(dr["STT"].ToString());
                            oAirwayTransportComeDetail.CHANGBAY = dr["CHANGBAY"].ToString();
                            oAirwayTransportComeDetail.DONVIVANCHUYEN = dr["DONVIVANCHUYEN"].ToString();
                            oAirwayTransportComeDetail.DICHVUVANCHUYEN = dr["DICHVUVANCHUYEN"].ToString();
                            oAirwayTransportComeDetail.KHUNGGIO = dr["KHUNGGIO"].ToString();
                            oAirwayTransportComeDetail.TONGTAITHEOHOPDONG = dr["TONGTAITHEOHOPDONG"].ToString();
                            //oAirwayTransportComeDetail.ID = Convert.ToInt32(dr["ID"].ToString());
                            //oAirwayTransportComeDetail.NGAY = Convert.ToInt32(dr["NGAY"].ToString());
                            //oAirwayTransportComeDetail.MABC = Convert.ToInt32(dr["MABC"].ToString());
                            //oAirwayTransportComeDetail.CHIEU = Convert.ToInt32(dr["CHIEU"].ToString());
                            //oAirwayTransportComeDetail.HUONG = Convert.ToInt32(dr["HUONG"].ToString());
                            oAirwayTransportComeDetail.TAICUNG_KH = dr["TAICUNG_KH"].ToString();
                            oAirwayTransportComeDetail.TAIMEM_KH = dr["TAIMEM_KH"].ToString();
                            oAirwayTransportComeDetail.MASP = dr["MASP"].ToString();
                            oAirwayTransportComeDetail.TAICUNG_TH = dr["TAICUNG_TH"].ToString();
                            oAirwayTransportComeDetail.TAIMEM_TH = dr["TAIMEM_TH"].ToString();
                            oAirwayTransportComeDetail.TAICUNG_LK = dr["TAICUNG_LK"].ToString();
                            oAirwayTransportComeDetail.TAIMEM_LK = dr["TAIMEM_LK"].ToString();
                            oAirwayTransportComeDetail.GIOGIAO_QD = dr["GIOGIAO_QD"].ToString();
                            oAirwayTransportComeDetail.GIOBAY_QD = dr["GIOBAY_QD"].ToString();
                            oAirwayTransportComeDetail.GIOGIAO_TT = dr["GIOGIAO_TT"].ToString();
                            oAirwayTransportComeDetail.GIOBAY_TT = dr["GIOBAY_TT"].ToString();
                            oAirwayTransportComeDetail.SOHIEUCHUYENBAY = dr["SOHIEUCHUYENBAY"].ToString();
                            oAirwayTransportComeDetail.GIOGIAO_CL = dr["GIOGIAO_CL"].ToString();
                            oAirwayTransportComeDetail.GIOBAY_CL = dr["GIOBAY_CL"].ToString();
                            oAirwayTransportComeDetail.GIODAP_QD = dr["GIODAP_QD"].ToString();
                            oAirwayTransportComeDetail.GIONHAN_QD = dr["GIONHAN_QD"].ToString();
                            //oAirwayTransportComeDetail.GIODAP_TT = dr["GIODAP_TT"].ToString();
                            oAirwayTransportComeDetail.GIONHAN_TT = dr["GIONHAN_TT"].ToString();
                            oAirwayTransportComeDetail.GIONHAN_CL = dr["GIONHAN_CL"].ToString();
                            listAirwayTransportComeDetail.Add(oAirwayTransportComeDetail);

                        }
                        _ReturnAirwayTransportCome.Code = "00";
                        _ReturnAirwayTransportCome.Message = "Lấy dữ liệu thành công.";
                        _ReturnAirwayTransportCome.ListAirwayTransportComeReport = listAirwayTransportComeDetail;
                    }
                    else
                    {
                        _ReturnAirwayTransportCome.Code = "01";
                        _ReturnAirwayTransportCome.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _ReturnAirwayTransportCome.Code = "99";
                _ReturnAirwayTransportCome.Message = "Lỗi xử lý dữ liệu";

            }
            return _ReturnAirwayTransportCome;
        }



        #endregion

        //Phần chi tiết của bảng LOAD_DATA18
        #region LOAD_DATA18          
        public ReturnAirwayTransportCome LOAD_DATA18(int date, int way)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnAirwayTransportCome _ReturnAirwayTransportCome = new ReturnAirwayTransportCome();


            List<AirwayTransportComeDetail> listAirwayTransportComeDetail = null;
            AirwayTransportComeDetail oAirwayTransportComeDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    //Helper.OraDSOracleConnection gọi đến database đối soát
                    OracleCommand myCommand = new OracleCommand("AIRWAY_TRANSPORT_COME_2.LOAD_DATA18", Helper.OraDSOracleConnection);
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
                        listAirwayTransportComeDetail = new List<AirwayTransportComeDetail>();
                        while (dr.Read())
                        {
                            oAirwayTransportComeDetail = new AirwayTransportComeDetail();
                            //oAirwayTransportComeDetail.SO = Convert.ToInt32(dr["1"].ToString());
                            oAirwayTransportComeDetail.STT = Convert.ToInt32(dr["STT"].ToString());
                            oAirwayTransportComeDetail.CHANGBAY = dr["CHANGBAY"].ToString();
                            oAirwayTransportComeDetail.DONVIVANCHUYEN = dr["DONVIVANCHUYEN"].ToString();
                            oAirwayTransportComeDetail.DICHVUVANCHUYEN = dr["DICHVUVANCHUYEN"].ToString();
                            oAirwayTransportComeDetail.KHUNGGIO = dr["KHUNGGIO"].ToString();
                            oAirwayTransportComeDetail.TONGTAITHEOHOPDONG = dr["TONGTAITHEOHOPDONG"].ToString();
                            //oAirwayTransportComeDetail.ID = Convert.ToInt32(dr["ID"].ToString());
                            //oAirwayTransportComeDetail.NGAY = Convert.ToInt32(dr["NGAY"].ToString());
                            //oAirwayTransportComeDetail.MABC = Convert.ToInt32(dr["MABC"].ToString());
                            //oAirwayTransportComeDetail.CHIEU = Convert.ToInt32(dr["CHIEU"].ToString());
                            //oAirwayTransportComeDetail.HUONG = Convert.ToInt32(dr["HUONG"].ToString());
                            oAirwayTransportComeDetail.TAICUNG_KH = dr["TAICUNG_KH"].ToString();
                            oAirwayTransportComeDetail.TAIMEM_KH = dr["TAIMEM_KH"].ToString();
                            oAirwayTransportComeDetail.MASP = dr["MASP"].ToString();
                            oAirwayTransportComeDetail.TAICUNG_TH = dr["TAICUNG_TH"].ToString();
                            oAirwayTransportComeDetail.TAIMEM_TH = dr["TAIMEM_TH"].ToString();
                            oAirwayTransportComeDetail.TAICUNG_LK = dr["TAICUNG_LK"].ToString();
                            oAirwayTransportComeDetail.TAIMEM_LK = dr["TAIMEM_LK"].ToString();
                            oAirwayTransportComeDetail.GIOGIAO_QD = dr["GIOGIAO_QD"].ToString();
                            oAirwayTransportComeDetail.GIOBAY_QD = dr["GIOBAY_QD"].ToString();
                            oAirwayTransportComeDetail.GIOGIAO_TT = dr["GIOGIAO_TT"].ToString();
                            oAirwayTransportComeDetail.GIOBAY_TT = dr["GIOBAY_TT"].ToString();
                            oAirwayTransportComeDetail.SOHIEUCHUYENBAY = dr["SOHIEUCHUYENBAY"].ToString();
                            oAirwayTransportComeDetail.GIOGIAO_CL = dr["GIOGIAO_CL"].ToString();
                            oAirwayTransportComeDetail.GIOBAY_CL = dr["GIOBAY_CL"].ToString();
                            oAirwayTransportComeDetail.GIODAP_QD = dr["GIODAP_QD"].ToString();
                            oAirwayTransportComeDetail.GIONHAN_QD = dr["GIONHAN_QD"].ToString();
                            //oAirwayTransportComeDetail.GIODAP_TT = dr["GIODAP_TT"].ToString();
                            oAirwayTransportComeDetail.GIONHAN_TT = dr["GIONHAN_TT"].ToString();
                            oAirwayTransportComeDetail.GIONHAN_CL = dr["GIONHAN_CL"].ToString();
                            listAirwayTransportComeDetail.Add(oAirwayTransportComeDetail);

                        }
                        _ReturnAirwayTransportCome.Code = "00";
                        _ReturnAirwayTransportCome.Message = "Lấy dữ liệu thành công.";
                        _ReturnAirwayTransportCome.ListAirwayTransportComeReport = listAirwayTransportComeDetail;
                    }
                    else
                    {
                        _ReturnAirwayTransportCome.Code = "01";
                        _ReturnAirwayTransportCome.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _ReturnAirwayTransportCome.Code = "99";
                _ReturnAirwayTransportCome.Message = "Lỗi xử lý dữ liệu";

            }
            return _ReturnAirwayTransportCome;
        }



        #endregion

        //Phần chi tiết của bảng LOAD_DATA19
        #region LOAD_DATA19          
        public ReturnAirwayTransportCome LOAD_DATA19(int date, int way)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnAirwayTransportCome _ReturnAirwayTransportCome = new ReturnAirwayTransportCome();


            List<AirwayTransportComeDetail> listAirwayTransportComeDetail = null;
            AirwayTransportComeDetail oAirwayTransportComeDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    //Helper.OraDSOracleConnection gọi đến database đối soát
                    OracleCommand myCommand = new OracleCommand("AIRWAY_TRANSPORT_COME_2.LOAD_DATA19", Helper.OraDSOracleConnection);
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
                        listAirwayTransportComeDetail = new List<AirwayTransportComeDetail>();
                        while (dr.Read())
                        {
                            oAirwayTransportComeDetail = new AirwayTransportComeDetail();
                            //oAirwayTransportComeDetail.SO = Convert.ToInt32(dr["1"].ToString());
                            oAirwayTransportComeDetail.STT = Convert.ToInt32(dr["STT"].ToString());
                            oAirwayTransportComeDetail.CHANGBAY = dr["CHANGBAY"].ToString();
                            oAirwayTransportComeDetail.DONVIVANCHUYEN = dr["DONVIVANCHUYEN"].ToString();
                            oAirwayTransportComeDetail.DICHVUVANCHUYEN = dr["DICHVUVANCHUYEN"].ToString();
                            oAirwayTransportComeDetail.KHUNGGIO = dr["KHUNGGIO"].ToString();
                            oAirwayTransportComeDetail.TONGTAITHEOHOPDONG = dr["TONGTAITHEOHOPDONG"].ToString();
                            //oAirwayTransportComeDetail.ID = Convert.ToInt32(dr["ID"].ToString());
                            //oAirwayTransportComeDetail.NGAY = Convert.ToInt32(dr["NGAY"].ToString());
                            //oAirwayTransportComeDetail.MABC = Convert.ToInt32(dr["MABC"].ToString());
                            //oAirwayTransportComeDetail.CHIEU = Convert.ToInt32(dr["CHIEU"].ToString());
                            //oAirwayTransportComeDetail.HUONG = Convert.ToInt32(dr["HUONG"].ToString());
                            oAirwayTransportComeDetail.TAICUNG_KH = dr["TAICUNG_KH"].ToString();
                            oAirwayTransportComeDetail.TAIMEM_KH = dr["TAIMEM_KH"].ToString();
                            oAirwayTransportComeDetail.MASP = dr["MASP"].ToString();
                            oAirwayTransportComeDetail.TAICUNG_TH = dr["TAICUNG_TH"].ToString();
                            oAirwayTransportComeDetail.TAIMEM_TH = dr["TAIMEM_TH"].ToString();
                            oAirwayTransportComeDetail.TAICUNG_LK = dr["TAICUNG_LK"].ToString();
                            oAirwayTransportComeDetail.TAIMEM_LK = dr["TAIMEM_LK"].ToString();
                            oAirwayTransportComeDetail.GIOGIAO_QD = dr["GIOGIAO_QD"].ToString();
                            oAirwayTransportComeDetail.GIOBAY_QD = dr["GIOBAY_QD"].ToString();
                            oAirwayTransportComeDetail.GIOGIAO_TT = dr["GIOGIAO_TT"].ToString();
                            oAirwayTransportComeDetail.GIOBAY_TT = dr["GIOBAY_TT"].ToString();
                            oAirwayTransportComeDetail.SOHIEUCHUYENBAY = dr["SOHIEUCHUYENBAY"].ToString();
                            oAirwayTransportComeDetail.GIOGIAO_CL = dr["GIOGIAO_CL"].ToString();
                            oAirwayTransportComeDetail.GIOBAY_CL = dr["GIOBAY_CL"].ToString();
                            oAirwayTransportComeDetail.GIODAP_QD = dr["GIODAP_QD"].ToString();
                            oAirwayTransportComeDetail.GIONHAN_QD = dr["GIONHAN_QD"].ToString();
                            //oAirwayTransportComeDetail.GIODAP_TT = dr["GIODAP_TT"].ToString();
                            oAirwayTransportComeDetail.GIONHAN_TT = dr["GIONHAN_TT"].ToString();
                            oAirwayTransportComeDetail.GIONHAN_CL = dr["GIONHAN_CL"].ToString();
                            listAirwayTransportComeDetail.Add(oAirwayTransportComeDetail);

                        }
                        _ReturnAirwayTransportCome.Code = "00";
                        _ReturnAirwayTransportCome.Message = "Lấy dữ liệu thành công.";
                        _ReturnAirwayTransportCome.ListAirwayTransportComeReport = listAirwayTransportComeDetail;
                    }
                    else
                    {
                        _ReturnAirwayTransportCome.Code = "01";
                        _ReturnAirwayTransportCome.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _ReturnAirwayTransportCome.Code = "99";
                _ReturnAirwayTransportCome.Message = "Lỗi xử lý dữ liệu";

            }
            return _ReturnAirwayTransportCome;
        }



        #endregion

        //Phần chi tiết của bảng LOAD_DATA20
        #region LOAD_DATA20          
        public ReturnAirwayTransportCome LOAD_DATA20(int date, int way)
        {
            DataTable da = new DataTable();
            MetaData _metadata = new MetaData();
            Convertion common = new Convertion();
            ReturnAirwayTransportCome _ReturnAirwayTransportCome = new ReturnAirwayTransportCome();


            List<AirwayTransportComeDetail> listAirwayTransportComeDetail = null;
            AirwayTransportComeDetail oAirwayTransportComeDetail = null;
            try
            {
                // Gọi vào DB để lấy dữ liệu.
                using (OracleCommand cmd = new OracleCommand())
                {
                    //Helper.OraDSOracleConnection gọi đến database đối soát
                    OracleCommand myCommand = new OracleCommand("AIRWAY_TRANSPORT_COME_2.LOAD_DATA20", Helper.OraDSOracleConnection);
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
                        listAirwayTransportComeDetail = new List<AirwayTransportComeDetail>();
                        while (dr.Read())
                        {
                            oAirwayTransportComeDetail = new AirwayTransportComeDetail();
                            //oAirwayTransportComeDetail.SO = Convert.ToInt32(dr["1"].ToString());
                            oAirwayTransportComeDetail.STT = Convert.ToInt32(dr["STT"].ToString());
                            oAirwayTransportComeDetail.CHANGBAY = dr["CHANGBAY"].ToString();
                            oAirwayTransportComeDetail.DONVIVANCHUYEN = dr["DONVIVANCHUYEN"].ToString();
                            oAirwayTransportComeDetail.DICHVUVANCHUYEN = dr["DICHVUVANCHUYEN"].ToString();
                            oAirwayTransportComeDetail.KHUNGGIO = dr["KHUNGGIO"].ToString();
                            oAirwayTransportComeDetail.TONGTAITHEOHOPDONG = dr["TONGTAITHEOHOPDONG"].ToString();
                            //oAirwayTransportComeDetail.ID = Convert.ToInt32(dr["ID"].ToString());
                            //oAirwayTransportComeDetail.NGAY = Convert.ToInt32(dr["NGAY"].ToString());
                            //oAirwayTransportComeDetail.MABC = Convert.ToInt32(dr["MABC"].ToString());
                            //oAirwayTransportComeDetail.CHIEU = Convert.ToInt32(dr["CHIEU"].ToString());
                            //oAirwayTransportComeDetail.HUONG = Convert.ToInt32(dr["HUONG"].ToString());
                            oAirwayTransportComeDetail.TAICUNG_KH = dr["TAICUNG_KH"].ToString();
                            oAirwayTransportComeDetail.TAIMEM_KH = dr["TAIMEM_KH"].ToString();
                            oAirwayTransportComeDetail.MASP = dr["MASP"].ToString();
                            oAirwayTransportComeDetail.TAICUNG_TH = dr["TAICUNG_TH"].ToString();
                            oAirwayTransportComeDetail.TAIMEM_TH = dr["TAIMEM_TH"].ToString();
                            oAirwayTransportComeDetail.TAICUNG_LK = dr["TAICUNG_LK"].ToString();
                            oAirwayTransportComeDetail.TAIMEM_LK = dr["TAIMEM_LK"].ToString();
                            oAirwayTransportComeDetail.GIOGIAO_QD = dr["GIOGIAO_QD"].ToString();
                            oAirwayTransportComeDetail.GIOBAY_QD = dr["GIOBAY_QD"].ToString();
                            oAirwayTransportComeDetail.GIOGIAO_TT = dr["GIOGIAO_TT"].ToString();
                            oAirwayTransportComeDetail.GIOBAY_TT = dr["GIOBAY_TT"].ToString();
                            oAirwayTransportComeDetail.SOHIEUCHUYENBAY = dr["SOHIEUCHUYENBAY"].ToString();
                            oAirwayTransportComeDetail.GIOGIAO_CL = dr["GIOGIAO_CL"].ToString();
                            oAirwayTransportComeDetail.GIOBAY_CL = dr["GIOBAY_CL"].ToString();
                            oAirwayTransportComeDetail.GIODAP_QD = dr["GIODAP_QD"].ToString();
                            oAirwayTransportComeDetail.GIONHAN_QD = dr["GIONHAN_QD"].ToString();
                            //oAirwayTransportComeDetail.GIODAP_TT = dr["GIODAP_TT"].ToString();
                            oAirwayTransportComeDetail.GIONHAN_TT = dr["GIONHAN_TT"].ToString();
                            oAirwayTransportComeDetail.GIONHAN_CL = dr["GIONHAN_CL"].ToString();
                            listAirwayTransportComeDetail.Add(oAirwayTransportComeDetail);

                        }
                        _ReturnAirwayTransportCome.Code = "00";
                        _ReturnAirwayTransportCome.Message = "Lấy dữ liệu thành công.";
                        _ReturnAirwayTransportCome.ListAirwayTransportComeReport = listAirwayTransportComeDetail;
                    }
                    else
                    {
                        _ReturnAirwayTransportCome.Code = "01";
                        _ReturnAirwayTransportCome.Message = "Không có dữ liệu";

                    }


                }
            }
            catch (Exception ex)
            {
                _ReturnAirwayTransportCome.Code = "99";
                _ReturnAirwayTransportCome.Message = "Lỗi xử lý dữ liệu";

            }
            return _ReturnAirwayTransportCome;
        }



        #endregion

        
    }

}

