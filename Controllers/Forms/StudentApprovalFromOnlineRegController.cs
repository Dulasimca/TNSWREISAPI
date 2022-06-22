using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TNSWREISAPI.ManageSQL;
using TNSWREISAPI.Model;

namespace TNSWREISAPI.Controllers.Forms
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentApprovalFromOnlineRegController : Controller
    {
        [HttpPost("{id}")]
        public string Post(StudentApprovalFromOnlineReg entity)
        {
            try
            {
                ManageSQLConnection manageSQL = new ManageSQLConnection();
                List<KeyValuePair<string, string>> sqlParameters = new List<KeyValuePair<string, string>>();
                sqlParameters.Add(new KeyValuePair<string, string>("@Slno", Convert.ToString(entity.Slno)));
                sqlParameters.Add(new KeyValuePair<string, string>("@DistrictId", Convert.ToString(entity.DistrictId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@TalukId", Convert.ToString(entity.TalukId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@HostelId", Convert.ToString(entity.HostelId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@StudentId", Convert.ToString(entity.StudentId)));
                sqlParameters.Add(new KeyValuePair<string, string>("@wardenapproval", Convert.ToString(entity.wardenapproval)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Districtapproval", Convert.ToString(entity.Districtapproval)));
                sqlParameters.Add(new KeyValuePair<string, string>("@AccountingYear", Convert.ToString(entity.AccountingYear)));
                sqlParameters.Add(new KeyValuePair<string, string>("@ReasonForDisapprove", Convert.ToString(entity.ReasonForDisapprove)));
                sqlParameters.Add(new KeyValuePair<string, string>("@Flag", Convert.ToString(entity.Flag)));
                var result = manageSQL.InsertData("InsertStudentApprovalStatus", sqlParameters);
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                AuditLog.WriteError(ex.Message);
            }
            return "false";
        }
    }
    public class StudentApprovalFromOnlineReg
    {
        public int Slno { get; set; }
        public int wardenapproval { get; set; }
        public int DistrictId { get; set; }
        public int TalukId { get; set; }
        public int HostelId { get; set; }
        public int StudentId { get; set; }
        public int AccountingYear { get; set; }
        public int Districtapproval { get; set; }
        public string ReasonForDisapprove { get; set; }
        public bool Flag { get; set; }


    }
}
