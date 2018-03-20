using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using T41.Areas.Admin.Models.DataModel;
using Oracle.ManagedDataAccess.Client;
using T41.Areas.Admin.Common;
using System.Data;
using T41.Areas.Admin.Model.DataModel;

namespace T41.Areas.Admin.Data
{
    public class TimeKeepingRepository
    {
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

        public IEnumerable<TimeKeeping> GetAllDMKip(int don_vi)
        {
            List<TimeKeeping> listTimeKeeping = null;
            TimeKeeping oTimeKeeping = null;

            try
            {
                using (OracleCommand cm = new OracleCommand())
                {
                    cm.Connection = Helper.OraDCOracleConnection;
                    cm.CommandText = string.Format("SELECT * FROM DM_KIP WHERE DONVI = " + don_vi);
                    cm.CommandType = CommandType.Text;
                    using (OracleDataReader dr = cm.ExecuteReader())
                    {
                        listTimeKeeping = new List<TimeKeeping>();
                        while (dr.Read())
                        {
                            oTimeKeeping = new TimeKeeping();
                            oTimeKeeping.MAKIP = int.Parse(dr["MAKIP"].ToString());
                            oTimeKeeping.TENKIP = dr["TENKIP"].ToString();
                            listTimeKeeping.Add(oTimeKeeping);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "GetAllDMKip" + ex.Message);
                listTimeKeeping = null;
            }

            return listTimeKeeping;
        }
    }
}