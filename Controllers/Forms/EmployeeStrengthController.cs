using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TNSWREISAPI.ManageSQL;
using TNSWREISAPI.Model;

namespace TNSWREISAPI.Controllers.Forms
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeStrengthController : ControllerBase
    {
        [HttpPost("{id}")]
        public string Post(EmployeeStrengthEntity EmployeeStrengthEntity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@EmpStrengthId", Convert.ToString(EmployeeStrengthEntity.EmpStrengthId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@HostelID", Convert.ToString(EmployeeStrengthEntity.HostelID)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Districtcode", Convert.ToString(EmployeeStrengthEntity.Districtcode)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Talukid", Convert.ToString(EmployeeStrengthEntity.Talukid)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Designation", Convert.ToString(EmployeeStrengthEntity.Designation)));
                sqlParameters.Add(new KeyValuePair<string, string>("@GoNumber", Convert.ToString(EmployeeStrengthEntity.GoNumber)));
                sqlParameters.Add(new KeyValuePair<string, string>("@AllotmentStrength", Convert.ToString(EmployeeStrengthEntity.AllotmentStrength)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Availability", Convert.ToString(EmployeeStrengthEntity.Availability)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Required", Convert.ToString(EmployeeStrengthEntity.Required)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Flag", Convert.ToString(EmployeeStrengthEntity.Flag)));
                var result = manageSQL.InsertData("InsertEmployeeStrength", sqlParameters);
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
            }
            return "false";
        }
        [HttpGet("{id}")]

        public string Get(int DCode, int TCode, int HostelId)
        {
            ManageSQLConnection manageSQL = new ManageSQLConnection();
            DataSet ds = new DataSet();
            List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
            sqlParameters.Add(new KeyValuePair<string, string>("@Id", Convert.ToString(DCode)));
            sqlParameters.Add(new KeyValuePair<string, string>("@TCode", Convert.ToString(TCode)));
            sqlParameters.Add(new KeyValuePair<string, string>("@HCode", Convert.ToString(HostelId)));
            var result = manageSQL.GetDataSetValues("GetEmployeeStrength", sqlParameters);
            return JsonConvert.SerializeObject(result);
        }
        public class EmployeeStrengthEntity
        {
            public int EmpStrengthId { get; set; }
            public int HostelID { get; set; }
            public int Districtcode { get; set; }
            public int Talukid { get; set; }
            public int Designation { get; set; }
            public String GoNumber { get; set; }
            public int AllotmentStrength { get; set; }
            public int Availability { get; set; }
            public int Required { get; set; }
            public bool Flag { get; set; }
        }
        }
    
}
