using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TNSWREISAPI.ManageSQL;

namespace TNSWREISAPI.Controllers.Forms
{
    [Route("api/[controller]")]
    [ApiController]
    public class DailyConsumptionController : ControllerBase
    {
        SqlConnection sqlConnection = new SqlConnection();
        SqlCommand sqlCommand = new SqlCommand();
        [HttpPost("{id}")]
        public bool Post([FromBody]List<ConsumptionEntity> entity)
        {
            SqlTransaction objTrans = null;
            using (sqlConnection = new SqlConnection(GlobalVariable.ConnectionString))
            {
                sqlCommand = new SqlCommand();
                try
                {
                    if (sqlConnection.State == 0)
                    {
                        sqlConnection.Open();
                    }
                    objTrans = sqlConnection.BeginTransaction();

                    foreach (var item in entity)
                    {
                        sqlCommand.Parameters.Clear();
                        sqlCommand.Dispose();
                        sqlCommand = new SqlCommand();
                        sqlCommand.Transaction = objTrans;
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.CommandText = "InsertIntoConsumption";
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@Id", item.Id);
                        sqlCommand.Parameters.AddWithValue("@HostelId", item.HostelId);
                        sqlCommand.Parameters.AddWithValue("@TalukCode", item.TalukCode);
                        sqlCommand.Parameters.AddWithValue("@DistrictCode", item.DistrictCode);
                        sqlCommand.Parameters.AddWithValue("@ConsumptionType", item.ConsumptionType);
                        sqlCommand.Parameters.AddWithValue("@ConsumptionDate", item.ConsumptionDate);
                        sqlCommand.Parameters.AddWithValue("@CommodityId", item.CommodityId);
                        sqlCommand.Parameters.AddWithValue("@UnitId", item.UnitId);
                        sqlCommand.Parameters.AddWithValue("@OB", item.OB);
                        sqlCommand.Parameters.AddWithValue("@QTY", item.QTY);
                        sqlCommand.Parameters.AddWithValue("@CB", item.CB);
                        sqlCommand.Parameters.AddWithValue("@Flag", 1);
                        sqlCommand.ExecuteNonQuery();
                    }
                    objTrans.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    AuditLog.WriteError(ex.Message);
                    objTrans.Rollback();
                    return false;
                }
                finally
                {
                    sqlConnection.Close();
                    sqlCommand.Dispose();
                }

            }
        }

        [HttpGet("{id}")]
        public string Get(string FromDate, string ToDate, int HostelId, int Type)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            var procedure = "";
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            if (Type == 1)
            {
                sqlParameters.Add(new KeyValuePair<string, string>("@HCode", Convert.ToString(HostelId)));
                procedure = "GetDeviceIdByHostelId";
            }
            else
            {
                sqlParameters.Add(new KeyValuePair<string, string>("@FromDate", FromDate));
                sqlParameters.Add(new KeyValuePair<string, string>("@ToDate", ToDate));
                sqlParameters.Add(new KeyValuePair<string, string>("@HostelId", Convert.ToString(HostelId)));
                procedure = "GetDailyConsumption";
            }
            ds = manageSQL.GetDataSetValues(procedure, sqlParameters);
            return JsonConvert.SerializeObject(ds.Tables[0]);
        }

        [HttpPut("{id}")]
        public bool Put(ConsumptionEntity entity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@Id", Convert.ToString(entity.Id)));
                var result = manageSQL.UpdateValues("DeleteDailyConsumption", sqlParameters);
                return result;
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
                return false;
            }

        }
}

        public class ConsumptionEntity
        {
            public Int64 Id { get; set; }
            public int HostelId { get; set; }
            public int TalukCode { get; set; }
            public int DistrictCode { get; set; }
            public int ConsumptionType { get; set; }
            public string ConsumptionDate { get; set; }
            public int CommodityId { get; set; }
            public int UnitId { get; set; }
            public float OB { get; set; }
            public float QTY { get; set; }
            public float CB { get; set; }
            public int Flag { get; set; }
        }
    }