using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TNSWREISAPI.ManageSQL;

namespace TNSWREISAPI.Controllers.Forms
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentTransferDetailsController : Controller
    {
        [HttpPost("{id}")]
        public string Post(TransferEntity entity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@Id", Convert.ToString(entity.Id)));
                sqlParameters.Add(new KeyValuePair<string, string>("@AcademicYear", Convert.ToString(entity.AcademicYear)));
                sqlParameters.Add(new KeyValuePair<string, string>("@HostelId", Convert.ToString(entity.HostelId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@StudentId", Convert.ToString(entity.StudentId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@EMISNO", Convert.ToString(entity.EMISNO)));
                sqlParameters.Add(new KeyValuePair<string, string>("@AcademicStatus", Convert.ToString(entity.AcademicStatus)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Flag", Convert.ToString(entity.Flag)));
                var result = manageSQL.InsertData("InsertIntoStudentApprovalDetails", sqlParameters);
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
            }
            return "false";
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
    }
}
