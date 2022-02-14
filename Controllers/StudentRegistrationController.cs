using Microsoft.AspNetCore.Http;
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
    public class StudentRegistrationController : ControllerBase
    {
        [HttpPost("{id}")]
        public string Post(StudentEntity entity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@Id", Convert.ToString(entity.StudentId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@StudentName", entity.Name));
                sqlParameters.Add(new KeyValuePair<string, string>("@DCode", Convert.ToString(entity.DCode)));
                sqlParameters.Add(new KeyValuePair<string, string>("@TCode", Convert.ToString(entity.TCode)));
                sqlParameters.Add(new KeyValuePair<string, string>("@HostelId", Convert.ToString(entity.HCode)));
                sqlParameters.Add(new KeyValuePair<string, string>("@AadharNumber ", entity.Aadharno));
                sqlParameters.Add(new KeyValuePair<string, string>("@EmailId", entity.EmailId));
                sqlParameters.Add(new KeyValuePair<string, string>("@Flag", Convert.ToString(entity.Flag)));

                var result = manageSQL.InsertData("InsertStudentFeedbackRegistration", sqlParameters);
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
            }
            return "false";
        }

        [HttpGet]
        public string Get()
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            ds = manageSQL.GetDataSetValues("GetStudentFeedbackRegistration");
            return JsonConvert.SerializeObject(ds.Tables[0]);
        }
        public class StudentEntity
        {
            public long StudentId { get; set; }
            public string Name { get; set; }
            public int DCode { get; set; }
            public int TCode { get; set; }
            public int HCode { get; set; }
            public string Aadharno { get; set; }
            public string EmailId { get; set; }
            public bool Flag { get; set; }
        }

    }
}
