using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TNSWREISAPI.ManageSQL;

namespace TNSWREISAPI.Controllers.Forms
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentTransferDetailsController : Controller
    {
        SqlConnection sqlConnection = new SqlConnection();
        SqlCommand sqlCommand = new SqlCommand();
        [HttpPost("{id}")]
        public bool Post([FromBody] List<TransferEntity> entity)
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
                        sqlCommand.CommandText = "InsertIntoStudentApprovalDetails";
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@Id", item.Id);
                        sqlCommand.Parameters.AddWithValue("@HostelId", item.HostelId);
                        sqlCommand.Parameters.AddWithValue("@AcademicYear", item.AcademicYear);
                        sqlCommand.Parameters.AddWithValue("@StudentId", item.StudentId);
                        sqlCommand.Parameters.AddWithValue("@EMISNO", item.EMISNO);
                        sqlCommand.Parameters.AddWithValue("@AcademicStatus", item.AcademicStatus);
                        sqlCommand.Parameters.AddWithValue("@Flag", item.Flag);
                        sqlCommand.Parameters.AddWithValue("@Remarks", item.Remarks);
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
        public string Get(int AcademicYear, int HostelId)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@HCode", Convert.ToString(HostelId)));
            sqlParameters.Add(new KeyValuePair<string, string>("@AcademicYear", Convert.ToString(AcademicYear)));
            ds = manageSQL.GetDataSetValues("GetStudentsByAcademicYear", sqlParameters);
            return JsonConvert.SerializeObject(ds.Tables[0]);
        }

        [HttpPut("{id}")]
        public bool Put(TransferEntity entity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@StudentId", Convert.ToString(entity.StudentId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@HostelId", Convert.ToString(entity.HostelId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@AcademicYear", Convert.ToString(entity.AcademicYear)));
                sqlParameters.Add(new KeyValuePair<string, string>("@AcademicStatus", Convert.ToString(entity.AcademicStatus)));
                var result = manageSQL.UpdateValues("UpdateStudentsTransferStatus", sqlParameters);
                return result;
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
                return false;
            }

        }

    }

    public class TransferEntity
    {
        public int Id { get; set; }
        public int HostelId { get; set; }
        public Int64 StudentId { get; set; }
        public int AcademicYear { get; set; }
        public string EMISNO { get; set; }
        public int AcademicStatus { get; set; }
        public int Flag { get; set; }
        public string Remarks { get; set; }
    }
}
