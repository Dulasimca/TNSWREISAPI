using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TNSWREISAPI.ManageSQL;
using TNSWREISAPI.Model;

namespace TNSWREISAPI.Controllers.Forms
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeAttendanceController : ControllerBase
    {
        SqlConnection sqlConnection = new SqlConnection();
        SqlCommand sqlCommand = new SqlCommand();
        [HttpPost("{id}")]
        public bool Post([FromBody] List<EmployeeAttendanceEntity> entity)
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
                        sqlCommand.CommandText = "InsertEmployeeAttendanceDetails";
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@AttendanceId", item.AttendanceId);
                        sqlCommand.Parameters.AddWithValue("@HostelID", item.HostelID);
                        sqlCommand.Parameters.AddWithValue("@TalukID", item.TalukID);
                        sqlCommand.Parameters.AddWithValue("@Districtcode", item.Districtcode);
                        sqlCommand.Parameters.AddWithValue("@Id", item.Id);
                        sqlCommand.Parameters.AddWithValue("@AttendanceDate", item.AttendanceDate);
                        sqlCommand.Parameters.AddWithValue("@PresenAbsent", item.PresenAbsent);
                        sqlCommand.Parameters.AddWithValue("@Remarks", item.Remarks);
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

        public string Get(int DCode, int TCode, int HostelId, string FromDate, string Todate)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@Id", Convert.ToString(DCode)));
            sqlParameters.Add(new KeyValuePair<string, string>("@TCode", Convert.ToString(TCode)));
            sqlParameters.Add(new KeyValuePair<string, string>("@HCode", Convert.ToString(HostelId)));
            sqlParameters.Add(new KeyValuePair<string, string>("@FromDate", Convert.ToString(FromDate)));
            sqlParameters.Add(new KeyValuePair<string, string>("@Todate", Convert.ToString(Todate)));
            var result = manageSQL.GetDataSetValues("GetEmployeeAttendanceDetails", sqlParameters);
            return JsonConvert.SerializeObject(result);
        }
    }

    public class EmployeeAttendanceEntity
    {
        public Int64 AttendanceId { get; set; }
        public int HostelID { get; set; }
        public int TalukID { get; set; }
        public int Districtcode { get; set; }
        public int Id { get; set; }
        public string AttendanceDate { get; set; }
        public int PresenAbsent { get; set; }
        public string Remarks { get; set; }
        public int Flag { get; set; }
    }
}
    

